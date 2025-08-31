Use DBSanMiguel
GO
-- 1. SP para el inicio de Sesion.
CREATE OR ALTER PROCEDURE InicioSesionSP
    @idTipoDocumento INT,
    @documento VARCHAR(20),
    @password VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @idPaciente INT;

    SELECT @idPaciente = idPaciente
    FROM Pacientes
    WHERE idTipoDocumento = @idTipoDocumento
      AND documento = @documento;

    IF @idPaciente IS NULL
    BEGIN
        -- Usuario no encontrado
        SELECT 0 AS Resultado, 'Usuario no encontrado' AS Mensaje;
        RETURN;
    END

	IF NOT EXISTS (
        SELECT 1
        FROM Pacientes
        WHERE idPaciente = @idPaciente
		AND titular = 1
    )
    BEGIN
        -- Usuario no es titular
        SELECT -2 AS Resultado, 'El usuario no tiene permisos para iniciar sesi�n' AS Mensaje;
        RETURN;
    END

    IF EXISTS (
        SELECT 1
        FROM Pacientes
        WHERE idPaciente = @idPaciente
        AND password = @password
    )
    BEGIN
        -- Login exitoso, retornamos el idPaciente
        SELECT @idPaciente AS Resultado, 'Login exitoso' AS Mensaje;
    END
    ELSE
    BEGIN
        -- Contrase�a incorrecta
        SELECT -1 AS Resultado, 'Contrase�a incorrecta' AS Mensaje;
    END
END
GO


-- 2. SP para el registro de un nuevo Paciente.
CREATE OR ALTER PROCEDURE RegistroPacienteSP
    @idTipoDocumento INT,
    @documento VARCHAR(20),
    @nombres VARCHAR(50),
    @apellidoPaterno VARCHAR(25),
    @apellidoMaterno VARCHAR(25),
    @fechaNacimiento DATE,
    @celular VARCHAR(9),
    @idGenero INT,
    @correo VARCHAR(50),
    @password VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar duplicado por tipoDocumento + documento
    IF EXISTS (
        SELECT 1 
        FROM Pacientes
        WHERE idTipoDocumento = @idTipoDocumento
          AND documento = @documento
    )
    BEGIN
        SELECT -1 AS Resultado, 'Ya existe un paciente con ese documento' AS Mensaje;
        RETURN;
    END

    -- Validar duplicado por correo (si lo quieres �nico)
    IF @correo IS NOT NULL AND EXISTS (
        SELECT 1 
        FROM Pacientes
        WHERE correo = @correo
    )
    BEGIN
        SELECT -2 AS Resultado, 'El correo ya est� en uso' AS Mensaje;
        RETURN;
    END

    -- Insertar nuevo paciente
    INSERT INTO Pacientes (
        nombres, apellidoPaterno, apellidoMaterno,
        fechaNacimiento, celular, correo,
        documento, password, idTipoDocumento, idGenero, titular
    )
    VALUES (
        @nombres, @apellidoPaterno, @apellidoMaterno,
        @fechaNacimiento, @celular, @correo,
        @documento, @password, @idTipoDocumento, @idGenero, 1
    );

    -- Retornar el id del paciente reci�n creado
    DECLARE @idPaciente INT = SCOPE_IDENTITY();

    SELECT @idPaciente AS Resultado, 'Registro exitoso' AS Mensaje;
END
GO

-- 3. SP para la actualizacion de datos secundarios del Titular
CREATE OR ALTER PROCEDURE ActualizarPerfilClinicoSP
    @idPaciente INT,
    @idGenero INT,
    @peso DECIMAL(10,2),
    @altura DECIMAL(10,2),
    @idTipoSangre INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar existencia del paciente
    IF NOT EXISTS (
        SELECT 1 
        FROM Pacientes 
        WHERE idPaciente = @idPaciente
    )
    BEGIN
        SELECT -1 AS Resultado, 'El paciente no existe' AS Mensaje;
        RETURN;
    END

    -- Actualizar los datos cl�nicos
    UPDATE Pacientes
    SET 
        idGenero = @idGenero,
        peso = @peso,
        altura = @altura,
        idTipoSangre = @idTipoSangre
    WHERE idPaciente = @idPaciente;

    -- Retornar confirmaci�n
    SELECT 1 AS Resultado, 'Perfil cl�nico actualizado correctamente' AS Mensaje;
END
GO

-- 4. SP para registrar a un familiar 
CREATE OR ALTER PROCEDURE AgregarFamiliarSP
    @idPacienteTitular INT,
    @idTipoParentesco INT,
    @idTipoDocumento INT,
    @documento VARCHAR(20),
    @apellidoPaterno VARCHAR(25),
    @apellidoMaterno VARCHAR(25),
    @nombres VARCHAR(50),
    @fechaNacimiento DATE,
    @celular VARCHAR(9),
    @correo VARCHAR(50),
    @idGenero INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @idFamiliar INT;

    -- Validar existencia del titular
    IF NOT EXISTS (
        SELECT 1 FROM Pacientes WHERE idPaciente = @idPacienteTitular
    )
    BEGIN
        SELECT -1 AS Resultado, 'El paciente titular no existe' AS Mensaje;
        RETURN;
    END

    -- Validar que el documento no est� duplicado
    IF EXISTS (
        SELECT 1 
        FROM Pacientes
        WHERE idTipoDocumento = @idTipoDocumento
          AND documento = @documento
    )
    BEGIN
        SELECT -2 AS Resultado, 'Ya existe un paciente con ese documento' AS Mensaje;
        RETURN;
    END

    -- Insertar al nuevo paciente (familiar)
    INSERT INTO Pacientes (
        nombres, apellidoPaterno, apellidoMaterno,
        fechaNacimiento, celular, correo,
        documento, idTipoDocumento, idGenero, titular
    )
    VALUES (
        @nombres, @apellidoPaterno, @apellidoMaterno,
        @fechaNacimiento, @celular, @correo,
        @documento, @idTipoDocumento, @idGenero, 0
    );

    SET @idFamiliar = SCOPE_IDENTITY();

    --  Insertar la relaci�n en PacientesParentesco
    INSERT INTO PacientesParentesco (
        idPaciente, idFamiliar, idTipoParentesco
    )
    VALUES (
        @idPacienteTitular, @idFamiliar, @idTipoParentesco
    );

    -- Retornar confirmaci�n
    SELECT @idFamiliar AS Resultado, 'Familiar agregado correctamente' AS Mensaje;
END
GO

-- 5. SP para registrar la Cita Medica
CREATE OR ALTER PROCEDURE ReservarCitaMedicaSP
    @idPaciente INT,
    @idClinica INT,
    @idMedico INT,
    @fecha DATETIME,
    @idSeguroSalud INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @precioEspecialidad DECIMAL(10,2);
    DECLARE @cobertura INT;
    DECLARE @precioFinal DECIMAL(10,2);
    DECLARE @idCitaMedica INT;

    -- Obtener precio de la especialidad del m�dico
    SELECT @precioEspecialidad = E.precio
    FROM 
	Medicos M
    INNER JOIN 
	Especialidades E 
	ON M.idEspecialidad = E.idEspecialidad
    WHERE M.idMedico = @idMedico;

    -- Obtener cobertura del seguro
    SELECT @cobertura = cobertura
    FROM SeguroSalud
    WHERE idSeguroSalud = @idSeguroSalud;

    -- Calcular precio final
    SET @precioFinal = @precioEspecialidad * (100 - @cobertura) / 100.0;

    --  Insertar la cita m�dica
    INSERT INTO CitaMedica (
        idClinica, idPaciente, idMedico, fecha, idSeguroSalud, precio
    )
    VALUES (
        @idClinica, @idPaciente, @idMedico, @fecha, @idSeguroSalud, @precioFinal
    );

    SET @idCitaMedica = SCOPE_IDENTITY();

    -- Retornar �xito
    SELECT @idCitaMedica AS Resultado, 'Cita m�dica reservada correctamente' AS Mensaje;
END
GO

-- 6. SP para visualizar los detalles de la CitaMedica

CREATE OR ALTER PROCEDURE DetallesCitaMedicaSP
    @idCitaMedica INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        -- Paciente
        P.nombres + ' ' + P.apellidoPaterno + ' ' + P.apellidoMaterno AS Paciente,

        -- Especialidad
        E.especialidad AS Especialidad,

        -- M�dico
        M.nombres + ' ' + M.apellidos AS Medico,

        -- Cl�nica
        C.nombre AS Clinica,

        -- Fecha y hora de la cita
        CM.fecha AS FechaHora,

        -- Seguro de salud
        S.nombreSeguro AS Seguro,

        -- Precio final calculado y guardado
        CM.precio AS Precio
    FROM CitaMedica CM
    INNER JOIN Pacientes P ON CM.idPaciente = P.idPaciente
    INNER JOIN Medicos M ON CM.idMedico = M.idMedico
    INNER JOIN Especialidades E ON M.idEspecialidad = E.idEspecialidad
    INNER JOIN Clinicas C ON CM.idClinica = C.idClinica
    INNER JOIN SeguroSalud S ON CM.idSeguroSalud = S.idSeguroSalud
    WHERE CM.idCitaMedica = @idCitaMedica;
END
GO

------------------------------
------------------------------

-- 7. SP para listar Medicos filtrado con Especialidad y Clinica
CREATE OR ALTER PROCEDURE MedicosPorEspecialidadClinicaSP
    @idClinica INT,
	@idEspecialidad INT
AS
BEGIN
    SELECT 
        M.idMedico,
        M.nombres + ' ' + M.apellidos AS nombre
    FROM Medicos AS M
    INNER JOIN Especialidades AS E ON M.idEspecialidad = E.idEspecialidad
    INNER JOIN ClinicaMedico AS CM ON M.idMedico = CM.idMedico
    INNER JOIN Clinicas AS C ON CM.idClinica = C.idClinica
    WHERE M.idEspecialidad = @idEspecialidad
      AND CM.idClinica = @idClinica;
END
GO

-- 8. SP Para listar Pacientes y Familiar
CREATE OR ALTER PROCEDURE PacienteConFamiliaresSP
    @idPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Titular
    SELECT 
        P.idPaciente,
        P.nombres + ' ' + P.apellidoPaterno + ' ' + P.apellidoMaterno AS Nombre,
        'Titular' AS Parentesco
    FROM Pacientes P
    WHERE P.idPaciente = @idPaciente

    UNION ALL

    -- Familiares asociados
    SELECT 
        F.idPaciente,
        F.nombres + ' ' + F.apellidoPaterno + ' ' + F.apellidoMaterno AS Nombre,
        TP.parentesco
    FROM PacientesParentesco PP
    INNER JOIN Pacientes F ON F.idPaciente = PP.idFamiliar
    INNER JOIN TipoParentesco TP ON TP.idTipoParentesco = PP.idTipoParentesco
    WHERE PP.idPaciente = @idPaciente;
END
GO


-- 9. Cargar perfil por Id Paciente
CREATE OR ALTER PROCEDURE CargarPerfilSP
    @idPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        P.idPaciente,
        (P.nombres + ' ' + P.apellidoPaterno + ' ' + P.apellidoMaterno) AS Nombre,
        G.genero AS Genero,
        DATEDIFF(YEAR, P.fechaNacimiento, GETDATE()) 
            - CASE 
                WHEN (MONTH(P.fechaNacimiento) > MONTH(GETDATE())) 
                     OR (MONTH(P.fechaNacimiento) = MONTH(GETDATE()) AND DAY(P.fechaNacimiento) > DAY(GETDATE())) 
                THEN 1 ELSE 0 
              END AS Edad,
        P.peso AS Peso,
        P.altura AS Altura,
        TS.tipoSangre AS GrupoSanguineo
    FROM Pacientes P
    INNER JOIN Generos G ON P.idGenero = G.idGenero
    LEFT JOIN TipoSangre TS ON P.idTipoSangre = TS.idTipoSangre
    WHERE P.idPaciente = @idPaciente;
END
GO

-- 10. SP para obtener citas activas/programadas de un paciente
CREATE OR ALTER PROCEDURE CitasPacienteActivasSP
    @idPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CM.idCitaMedica AS IdCitaMedica,
        P.nombres + ' ' + P.apellidoPaterno + ' ' + P.apellidoMaterno AS Paciente,
        E.especialidad AS Especialidad,
        M.nombres + ' ' + M.apellidos AS Medico,
        C.nombre AS Clinica,
        CM.fecha AS FechaHora,
        S.nombreSeguro AS Seguro,
        CM.precio AS Precio,
        CASE 
            WHEN CM.fecha > GETDATE() THEN 'Programada'
            WHEN CM.fecha <= GETDATE() AND CM.fecha >= DATEADD(HOUR, -2, GETDATE()) THEN 'Confirmada'
            ELSE 'Vencida'
        END AS Estado
    FROM CitaMedica CM
    INNER JOIN Pacientes P ON CM.idPaciente = P.idPaciente
    INNER JOIN Medicos M ON CM.idMedico = M.idMedico
    INNER JOIN Especialidades E ON M.idEspecialidad = E.idEspecialidad
    INNER JOIN Clinicas C ON CM.idClinica = C.idClinica
    INNER JOIN SeguroSalud S ON CM.idSeguroSalud = S.idSeguroSalud
    WHERE (P.idPaciente = @idPaciente 
       OR P.idPaciente IN (
           -- Incluir familiares del paciente titular
           SELECT PP.idFamiliar 
           FROM PacientesParentesco PP 
           WHERE PP.idPaciente = @idPaciente
       )
       OR CM.idPaciente IN (
           -- Incluir citas donde el paciente es familiar de otro titular
           SELECT PP.idPaciente 
           FROM PacientesParentesco PP 
           WHERE PP.idFamiliar = @idPaciente
       ))
    AND CM.fecha >= GETDATE() -- Solo citas futuras o actuales
    ORDER BY CM.fecha ASC;
END
GO

-- 11. SP para obtener historial de citas de un paciente
CREATE OR ALTER PROCEDURE HistorialCitasPacienteSP
    @idPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CM.idCitaMedica AS IdCitaMedica,
        P.nombres + ' ' + P.apellidoPaterno + ' ' + P.apellidoMaterno AS Paciente,
        E.especialidad AS Especialidad,
        M.nombres + ' ' + M.apellidos AS Medico,
        C.nombre AS Clinica,
        CM.fecha AS FechaHora,
        S.nombreSeguro AS Seguro,
        CM.precio AS Precio,
        CASE 
            WHEN CM.fecha < DATEADD(HOUR, -2, GETDATE()) THEN 'Completada'
            WHEN CM.fecha < GETDATE() THEN 'No Asistió'
            ELSE 'Cancelada'
        END AS Estado
    FROM CitaMedica CM
    INNER JOIN Pacientes P ON CM.idPaciente = P.idPaciente
    INNER JOIN Medicos M ON CM.idMedico = M.idMedico
    INNER JOIN Especialidades E ON M.idEspecialidad = E.idEspecialidad
    INNER JOIN Clinicas C ON CM.idClinica = C.idClinica
    INNER JOIN SeguroSalud S ON CM.idSeguroSalud = S.idSeguroSalud
    WHERE (P.idPaciente = @idPaciente 
       OR P.idPaciente IN (
           -- Incluir familiares del paciente titular
           SELECT PP.idFamiliar 
           FROM PacientesParentesco PP 
           WHERE PP.idPaciente = @idPaciente
       )
       OR CM.idPaciente IN (
           -- Incluir citas donde el paciente es familiar de otro titular
           SELECT PP.idPaciente 
           FROM PacientesParentesco PP 
           WHERE PP.idFamiliar = @idPaciente
       ))
    AND CM.fecha < GETDATE() -- Solo citas pasadas (cualquier fecha anterior a ahora)
    ORDER BY CM.fecha DESC;
END
GO