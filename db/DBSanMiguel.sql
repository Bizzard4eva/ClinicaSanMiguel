Use master
GO

DROP DATABASE DBSanMiguel;
go

CREATE DATABASE DBSanMiguel;
GO

USE DBSanMiguel;
GO

Create Table TipoDocumento(
	idTipoDocumento int primary key identity(1,1),
	documento varchar(25) unique not null,
)
GO

Create Table Clinicas(
	idClinica int primary key identity(1,1),
	nombre varchar(50) unique not null,
	direccion varchar(50) null,
	celular varchar(9) null
)
GO

Create Table TipoSangre(
	idTipoSangre int primary key identity(1,1),
	tipoSangre varchar(3) unique not null
)
GO

Create Table SeguroSalud(
	idSeguroSalud int primary key identity(1,1),
	nombreSeguro varchar(25) unique not null,
	cobertura int not null
)
GO

Create Table Especialidades(
	idEspecialidad int primary key identity(1,1),
	especialidad varchar(50) unique not null,
	precio int not null 
)
GO

Create Table Medicos(
	idMedico int primary key identity(1,1),
	nombres varchar(50) not null,
	apellidos varchar(50) not null,
	celular varchar(9) null,
	correo varchar(50) null,
	idEspecialidad int foreign key references Especialidades(idEspecialidad)
)
GO


Create Table Generos(
    idGenero int primary key identity(1,1),
    genero varchar(15) unique not null 
)
GO

Create Table Pacientes(
	idPaciente int primary key identity(1,1),
	nombres varchar(50) not null,
	apellidoPaterno varchar(25) not null,
	apellidoMaterno varchar(25) not null,
	fechaNacimiento date null,
	peso decimal(10,2) null,
	altura decimal(10,2) null,
	celular varchar(9) null,
	correo varchar(50) null,
	documento varchar(20) not null,
	password varchar(50) null,
	titular bit default 0,
	idTipoDocumento int foreign key references TipoDocumento(idTipoDocumento),
	idGenero int foreign key references Generos(idGenero),
	idTipoSangre int foreign key references TipoSangre(idTipoSangre),
	unique(idTipoDocumento, documento)
)
GO

Create Table TipoParentesco(
	idTipoParentesco int primary key identity(1,1), 
	parentesco varchar(25) unique not null
)
GO

Create Table PacientesParentesco(
	idPaciente int not null,
	idFamiliar int not null,
	primary key(idPaciente, idFamiliar),
	idTipoParentesco int foreign key references TipoParentesco(idTipoParentesco)
)
GO

Create Table CitaMedica(
    idCitaMedica int primary key identity(1,1),
    idClinica int foreign key references Clinicas(idClinica),
    idPaciente int foreign key references Pacientes(idPaciente),
    idMedico int foreign key references Medicos(idMedico),
    fecha datetime not null,
    estado varchar(25) default 'Pendiente',
    precio decimal(10,2) not null
)
GO
ALTER TABLE CitaMedica
ADD idSeguroSalud int null foreign key references SeguroSalud(idSeguroSalud);
go

Create Table ClinicaMedico (
    idClinica int not null foreign key references Clinicas(idClinica),
    idMedico int not null foreign key references Medicos(idMedico),
    primary key (idClinica, idMedico)
)
GO


