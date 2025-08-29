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
        -- Contraseña incorrecta
        SELECT -1 AS Resultado, 'Contraseña incorrecta' AS Mensaje;
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

    -- Validar duplicado por correo (si lo quieres único)
    IF @correo IS NOT NULL AND EXISTS (
        SELECT 1 
        FROM Pacientes
        WHERE correo = @correo
    )
    BEGIN
        SELECT -2 AS Resultado, 'El correo ya está en uso' AS Mensaje;
        RETURN;
    END

    -- Insertar nuevo paciente
    INSERT INTO Pacientes (
        nombres, apellidoPaterno, apellidoMaterno,
        fechaNacimiento, celular, correo,
        documento, password, idTipoDocumento, idGenero
    )
    VALUES (
        @nombres, @apellidoPaterno, @apellidoMaterno,
        @fechaNacimiento, @celular, @correo,
        @documento, @password, @idTipoDocumento, @idGenero
    );

    -- Retornar el id del paciente recién creado
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

    -- Actualizar los datos clínicos
    UPDATE Pacientes
    SET 
        idGenero = @idGenero,
        peso = @peso,
        altura = @altura,
        idTipoSangre = @idTipoSangre
    WHERE idPaciente = @idPaciente;

    -- Retornar confirmación
    SELECT 1 AS Resultado, 'Perfil clínico actualizado correctamente' AS Mensaje;
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

    -- 1. Validar existencia del titular
    IF NOT EXISTS (
        SELECT 1 FROM Pacientes WHERE idPaciente = @idPacienteTitular
    )
    BEGIN
        SELECT -1 AS Resultado, 'El paciente titular no existe' AS Mensaje;
        RETURN;
    END

    -- 2. Validar que el documento no esté duplicado
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

    -- 3. Insertar al nuevo paciente (familiar)
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

    -- 4. Insertar la relación en PacientesParentesco
    INSERT INTO PacientesParentesco (
        idPaciente, idFamiliar, idTipoParentesco
    )
    VALUES (
        @idPacienteTitular, @idFamiliar, @idTipoParentesco
    );

    -- 5. Retornar confirmación
    SELECT @idFamiliar AS Resultado, 'Familiar agregado correctamente' AS Mensaje;
END
GO
