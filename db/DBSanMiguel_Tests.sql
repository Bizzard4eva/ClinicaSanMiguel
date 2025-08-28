use DBSanMiguel
go

------------------------------------------------- TEST DE SP's

-- Insertanto datos en SP Regristro Paciente 

EXEC RegistroPacienteSP
@nombres = 'Natanael',
@apellidoPaterno = 'Lopez',
@apellidoMaterno = 'Diaz',
@celular = '99999999',
@correo = 'samuel.lopez@mail.com',
@fechaNacimiento = '2000-12-02',
@idTipoDocumento = 1,    -- Ej: DNI
@documento = '78945612',
@password = '3333';
GO

select *
from Pacientes
go

-- Insertanto datos en SP Regristro Parientes

EXEC dbo.RegistroParienteSP
    @idPaciente = 5,              
    @idTipoParentesco = 1,
    @idTipoDocumento = 1,
    @documento = '67896785',
    @apellidoPaterno = 'Lopez',
    @apellidoMaterno = 'Diaz',
    @nombres = 'Eduardo',
    @fechaNacimiento = '1976-10-10',
    @celular = '99999999',
    @correo = 'eduardo.lopez@mail.com';
GO

-- Visualizar los parentescos

SELECT 
    pp.idPaciente,
    p.nombres,
    p.apellidoPaterno,
    tp.idTipoParentesco,
    tp.parentesco,
    pp.idFamiliar,
    (SELECT pa.nombres 
     FROM Pacientes pa 
     WHERE pa.idPaciente = pp.idFamiliar) AS nombreFamiliar,
     (SELECT pa.apellidoPaterno 
     FROM Pacientes pa 
     WHERE pa.idPaciente = pp.idFamiliar) AS ApellidoPaterno
FROM PacientesParentesco pp
INNER JOIN Pacientes p 
    ON p.idPaciente = pp.idPaciente
INNER JOIN TipoParentesco tp 
    ON tp.idTipoParentesco = pp.idTipoParentesco;
GO

-- Insertanto datos en SP Regristro Cita Medica

EXEC ReservaCitaMedicaSP
    @idClinica   = 7,
    @idPaciente  = 5,
    @idMedico    = 35,
    @fecha       = '2025-09-07 11:30:00',
    @precio      = 80.00;
go

Select *
from Pacientes;
go

-- consulta de Cita medica
SELECT 
    CM.idCitaMedica,
    C.nombre AS Clinica,
    CONCAT(P.nombres, ' ', P.apellidoPaterno, ' ', P.apellidoMaterno) AS Paciente,
    CONCAT(M.nombres, ' ', M.apellidos) AS Medico,
    E.especialidad AS Especialidad,
    CM.fecha,
    CM.estado,
    CM.precio
FROM CitaMedica CM
INNER JOIN Clinicas C       ON CM.idClinica  = C.idClinica
INNER JOIN Pacientes P      ON CM.idPaciente = P.idPaciente
INNER JOIN Medicos M        ON CM.idMedico   = M.idMedico
INNER JOIN Especialidades E ON M.idEspecialidad = E.idEspecialidad;
go


-- Test Actualizar Pciente | ActualizarPerfilSP
Select * 
from Pacientes
go


EXEC ActualizarPerfilSP
    @idPaciente = 6,
    @idGenero = 1,
    @peso = 80,
    @altura = 1.80,
    @idTipoSangre = 1;
go
