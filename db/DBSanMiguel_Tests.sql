use DBSanMiguel
go

------------------------------------------------- TEST DE SP's

-- Insertanto datos en SP Regristro Paciente 

EXEC RegistroPacienteSP
    @idTipoDocumento = 1,
    @documento = '72671009',
    @nombres = 'Abel',
    @apellidoPaterno = 'Sulca',
    @apellidoMaterno = 'Espinoza',
    @fechaNacimiento = '1999-03-15',
    @celular = '999111222',
    @idGenero = 1, -- 2 = Femenino (ejemplo)
    @correo = 'abel.sulca@example.com',
    @password = '12345';


select *
from Pacientes
go

-- Insertanto datos en SP Regristro Parientes

EXEC AgregarFamiliarSP
    @idPacienteTitular = 2,
    @idTipoParentesco = 2,      -- Ej: "Hijo", "Esposa", etc.
    @idTipoDocumento = 1,       -- DNI, Pasaporte, etc.
    @documento = '99999999',
    @apellidoPaterno = 'Ramirez',
    @apellidoMaterno = 'Lopez',
    @nombres = 'Andrea',
    @fechaNacimiento = '2010-05-15',
    @celular = '987654321',
    @correo = 'andrea.ramirez@example.com',
    @idGenero = 2;              -- 1 = Masculino, 2 = Femenino

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



-- Testear el listado paciente con familiar
EXEC PacienteConFamiliaresSP @idPaciente = 2;
GO

-- Testear loading profile por Id
EXEC CargarPerfilSP @idPaciente = 10;
GO