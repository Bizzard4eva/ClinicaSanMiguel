Use DBSanMiguel
GO

-- Registro de Paciente
CREATE OR ALTER PROCEDURE RegistroPacienteSP
    @nombres VARCHAR(50),
    @apellidoPaterno VARCHAR(25),
    @apellidoMaterno VARCHAR(25),
	@celular VARCHAR(9),
	@correo VARCHAR(50),
	@fechaNacimiento DATE = NULL,
    @idTipoDocumento INT,
    @documento VARCHAR(20),
    @password VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Pacientes WHERE idTipoDocumento = @idTipoDocumento 
			                           AND documento = @documento)
    BEGIN
        RAISERROR('El paciente con este tipo y número de documento ya existe.', 16, 1);
        RETURN;
    END

    INSERT INTO Pacientes (nombres, apellidoPaterno, apellidoMaterno, celular, correo, fechaNacimiento,
							idTipoDocumento, documento, password, titular)
    VALUES (@nombres, @apellidoPaterno, @apellidoMaterno, @celular, @correo, @fechaNacimiento,
								@idTipoDocumento, @documento, @password, 1);
    SELECT SCOPE_IDENTITY() AS idPaciente;
END
GO

-- Registrar Pariente
CREATE OR ALTER PROCEDURE RegistroParienteSP
    @idPaciente        INT,            -- paciente titular que está logeado
    @idTipoParentesco  INT,            -- relación (madre, padre, hijo, etc.)
    @idTipoDocumento   INT,
    @documento         VARCHAR(20),
    @apellidoPaterno   VARCHAR(25),
    @apellidoMaterno   VARCHAR(25),
    @nombres           VARCHAR(50),
    @fechaNacimiento   DATE = NULL,
    @celular           VARCHAR(9) = NULL,
    @correo            VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @idFamiliar INT;

    -- 1. Validar que no exista ya un paciente con ese documento
    IF EXISTS (
        SELECT 1
        FROM Pacientes
        WHERE idTipoDocumento = @idTipoDocumento
          AND documento = @documento
    )
    BEGIN
        RAISERROR('El familiar con este tipo y número de documento ya existe.', 16, 1);
        RETURN;
    END

    -- 2. Insertar al familiar como paciente (pero titular = 0)
    INSERT INTO Pacientes (
        nombres,
        apellidoPaterno,
        apellidoMaterno,
        fechaNacimiento,
        celular,
        correo,
        idTipoDocumento,
        documento,
        titular
    )
    VALUES (
        @nombres,
        @apellidoPaterno,
        @apellidoMaterno,
        @fechaNacimiento,
        @celular,
        @correo,
        @idTipoDocumento,
        @documento,
        0
    );

    SET @idFamiliar = SCOPE_IDENTITY();

    -- 3. Registrar el parentesco entre el paciente titular y el familiar
    INSERT INTO PacientesParentesco (
        idPaciente,
        idFamiliar,
        idTipoParentesco
    )
    VALUES (
        @idPaciente,
        @idFamiliar,
        @idTipoParentesco
    );

    -- 4. Retornar el ID del nuevo familiar
    SELECT @idFamiliar AS idFamiliar;
END
GO

-- Reservar Cita Medica 

CREATE OR ALTER PROCEDURE ReservaCitaMedicaSP
    @idClinica   INT,
    @idPaciente  INT,
    @idMedico    INT,
    @fecha       DATETIME,
    @precio      DECIMAL(10,2),
    @estado      VARCHAR(25) = 'Pendiente'   -- por defecto 'Pendiente'
AS
BEGIN
    SET NOCOUNT ON;

    -- Insertar la cita médica
    INSERT INTO CitaMedica (
        idClinica,
        idPaciente,
        idMedico,
        fecha,
        estado,
        precio
    )
    VALUES (
        @idClinica,
        @idPaciente,
        @idMedico,
        @fecha,
        @estado,
        @precio
    );

    -- Retornar el ID de la cita recién creada
    SELECT SCOPE_IDENTITY() AS idCitaMedica;
END
GO

-- ActualizarPerfilSP
CREATE OR ALTER PROCEDURE ActualizarPerfilSP
    @idPaciente    INT,
    @idGenero      INT = NULL,
    @peso          DECIMAL(10,2) = NULL,
    @altura        DECIMAL(10,2) = NULL,
    @idTipoSangre  INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que el paciente exista
    IF NOT EXISTS (SELECT 1 FROM Pacientes WHERE idPaciente = @idPaciente)
    BEGIN
        RAISERROR('El paciente especificado no existe.', 16, 1);
        RETURN;
    END

    -- Actualizar los datos médicos
    UPDATE Pacientes
    SET idGenero       = ISNULL(@idGenero, idGenero),
        peso         = ISNULL(@peso, peso),
        altura       = ISNULL(@altura, altura),
        idTipoSangre = ISNULL(@idTipoSangre, idTipoSangre)
    WHERE idPaciente = @idPaciente;

    -- Retornar los datos ya actualizados
    SELECT 
        idPaciente,
        nombres,
        apellidoPaterno,
        apellidoMaterno,
        idGenero,
        peso,
        altura,
        idTipoSangre
    FROM Pacientes
    WHERE idPaciente = @idPaciente;
END
GO

