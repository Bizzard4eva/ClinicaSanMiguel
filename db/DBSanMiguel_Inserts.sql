Use DBSanMiguel
GO


-- Tipo de Documentos
INSERT INTO TipoDocumento (documento)
VALUES 
('DNI'),
('Carnet de Extranjería'),
('Pasaporte');
GO

-- Insertar datos en Clinicas

INSERT INTO Clinicas(nombre, direccion, celular)
VALUES 
('Clinica San Miguel', 'Av. Principal 123 - San Miguel', '999111222'),
('Clinica Surco', 'Av. Principal 456 - Surco', '999111223'),
('Clinica Miraflores', 'Av. Principal 789 - Miraflores', '999111224'),
('Clinica San Juan de Miraflores', 'Av. Central 101 - SJM', '999111225'),
('Clinica Callao', 'Av. Principal 202 - Callao', '999111226'),
('Clinica Breña', 'Av. Principal 303 - Breña', '999111227'),
('Clinica Cercado de Lima', 'Av. Principal 404 - Lima', '999111228'),
('Clinica La Molina', 'Av. Principal 505 - La Molina', '999111229'),
('Clinica San Isidro', 'Av. Principal 606 - San Isidro', '999111230'),
('Clinica San Borja', 'Av. Principal 707 - San Borja', '999111231'),
('Clinica Barranco', 'Av. Principal 808 - Barranco', '999111232'),
('Clinica Surquillo', 'Av. Principal 909 - Surquillo', '999111233');
GO

-- Insertar datos en Tipo de Sangre

INSERT INTO TipoSangre (tipoSangre)
VALUES 
('O+'),
('O-'),
('A+'),
('A-'),
('B+'),
('B-'),
('AB+'),
('AB-');
GO

-- Insertar datos en Seguro de Salud

INSERT INTO SeguroSalud (nombreSeguro, cobertura)
VALUES 
-- EPS
('Pacífico EPS', 90),
('Rímac EPS', 85),
('Mapfre EPS', 80),
('Sanitas EPS', 80),
('La Positiva EPS', 75),
-- Seguros de salud particulares
('Pacífico Seguros', 80),
('Rímac Seguros', 75),
('Mapfre Seguros', 70),
('La Positiva Seguros', 70),
-- Sin seguro
('Particular', 0);
GO

-- Insertar datos en Especialidades

INSERT INTO Especialidades(especialidad, precio) VALUES
('Medicina General', 80),
('Pediatría', 100),
('Ginecología y Obstetricia', 120),
('Cardiología', 150),
('Dermatología', 120),
('Oftalmología', 100),
('Otorrinolaringología', 110),
('Traumatología y Ortopedia', 130),
('Neurología', 160),
('Endocrinología', 140),
('Neumología', 140),
('Gastroenterología', 150),
('Urología', 130),
('Reumatología', 140),
('Psiquiatría', 160),
('Psicología', 90),
('Odontología General', 80),
('Odontopediatría', 100),
('Oncología', 200),
('Medicina Interna', 120),
('Nefrología', 150),
('Cirugía General', 180),
('Cirugía Plástica', 250),
('Geriatría', 110);
GO

-- Insertar datos en Medicos

-- Medicina General (1)
INSERT INTO Medicos (nombres, apellidos, celular, correo, idEspecialidad) VALUES 
('María', 'Gómez', '987100001', 'maria.gomez@clinica.com', 1),
('Carlos', 'Martínez', '987100002', 'carlos.martinez@clinica.com', 1),
('Ana', 'Fernández', '987100003', 'ana.fernandez@clinica.com', 1),
-- Pediatría (2)
('Luis', 'Ramírez', '987100004', 'luis.ramirez@clinica.com', 2),
('Claudia', 'Torres', '987100005', 'claudia.torres@clinica.com', 2),
('Jorge', 'Castro', '987100006', 'jorge.castro@clinica.com', 2),
-- Ginecología y Obstetricia (3)
('Patricia', 'Vargas', '987100007', 'patricia.vargas@clinica.com', 3),
('Gabriela', 'Mendoza', '987100008', 'gabriela.mendoza@clinica.com', 3),
('Rosa', 'Campos', '987100009', 'rosa.campos@clinica.com', 3),
-- Cardiología (4)
('Fernando', 'López', '987100010', 'fernando.lopez@clinica.com', 4),
('Carmen', 'Morales', '987100011', 'carmen.morales@clinica.com', 4),
('Pedro', 'Silva', '987100012', 'pedro.silva@clinica.com', 4),
-- Dermatología (5)
('Lucía', 'Reyes', '987100013', 'lucia.reyes@clinica.com', 5),
('Andrés', 'Soto', '987100014', 'andres.soto@clinica.com', 5),
('Beatriz', 'Chávez', '987100015', 'beatriz.chavez@clinica.com', 5),
-- Oftalmología (6)
('Hugo', 'Flores', '987100016', 'hugo.flores@clinica.com', 6),
('Marta', 'Paredes', '987100017', 'marta.paredes@clinica.com', 6),
('Diego', 'Herrera', '987100018', 'diego.herrera@clinica.com', 6),
-- Otorrinolaringología (7)
('Alberto', 'Navarro', '987100019', 'alberto.navarro@clinica.com', 7),
('Silvia', 'Cruz', '987100020', 'silvia.cruz@clinica.com', 7),
('Ricardo', 'Espinoza', '987100021', 'ricardo.espinoza@clinica.com', 7),
-- Traumatología y Ortopedia (8)
('Eduardo', 'Vega', '987100022', 'eduardo.vega@clinica.com', 8),
('Daniela', 'Ortega', '987100023', 'daniela.ortega@clinica.com', 8),
('Jaime', 'Salazar', '987100024', 'jaime.salazar@clinica.com', 8),
-- Neurología (9)
('Sofía', 'Delgado', '987100025', 'sofia.delgado@clinica.com', 9),
('Manuel', 'Acosta', '987100026', 'manuel.acosta@clinica.com', 9),
('Valeria', 'Peña', '987100027', 'valeria.pena@clinica.com', 9),
-- Endocrinología (10)
('Javier', 'Rojas', '987100028', 'javier.rojas@clinica.com', 10),
('Paola', 'Aguilar', '987100029', 'paola.aguilar@clinica.com', 10),
('Esteban', 'Mejía', '987100030', 'esteban.mejia@clinica.com', 10),
-- Neumología (11)
('Camila', 'Ruiz', '987100031', 'camila.ruiz@clinica.com', 11),
('Roberto', 'Paz', '987100032', 'roberto.paz@clinica.com', 11),
('Isabel', 'Guerrero', '987100033', 'isabel.guerrero@clinica.com', 11),
-- Gastroenterología (12)
('Alfonso', 'Benítez', '987100034', 'alfonso.benitez@clinica.com', 12),
('Mónica', 'Prieto', '987100035', 'monica.prieto@clinica.com', 12),
('Guillermo', 'Solís', '987100036', 'guillermo.solis@clinica.com', 12),
-- Urología (13)
('Martín', 'Ibarra', '987100037', 'martin.ibarra@clinica.com', 13),
('Lorena', 'Suárez', '987100038', 'lorena.suarez@clinica.com', 13),
('Héctor', 'Palacios', '987100039', 'hector.palacios@clinica.com', 13),
-- Reumatología (14)
('Patricio', 'Bravo', '987100040', 'patricio.bravo@clinica.com', 14),
('Karla', 'Serrano', '987100041', 'karla.serrano@clinica.com', 14),
('Renato', 'Ramos', '987100042', 'renato.ramos@clinica.com', 14),
-- Psiquiatría (15)
('Victoria', 'Campos', '987100043', 'victoria.campos@clinica.com', 15),
('Óscar', 'Huerta', '987100044', 'oscar.huerta@clinica.com', 15),
('Alejandra', 'Meza', '987100045', 'alejandra.meza@clinica.com', 15),
-- Psicología (16)
('Nicolás', 'Fuentes', '987100046', 'nicolas.fuentes@clinica.com', 16),
('Elena', 'Cornejo', '987100047', 'elena.cornejo@clinica.com', 16),
('Marcos', 'Villanueva', '987100048', 'marcos.villanueva@clinica.com', 16),
-- Odontología General (17)
('Ángela', 'Pizarro', '987100049', 'angela.pizarro@clinica.com', 17),
('Raúl', 'Peralta', '987100050', 'raul.peralta@clinica.com', 17),
('Teresa', 'Núñez', '987100051', 'teresa.nunez@clinica.com', 17),
-- Odontopediatría (18)
('Carolina', 'Rosales', '987100052', 'carolina.rosales@clinica.com', 18),
('Gonzalo', 'Del Solar', '987100053', 'gonzalo.delsolar@clinica.com', 18),
('Pamela', 'Quispe', '987100054', 'pamela.quispe@clinica.com', 18),
-- Oncología (19)
('Rodrigo', 'Valverde', '987100055', 'rodrigo.valverde@clinica.com', 19),
('Julia', 'Loayza', '987100056', 'julia.loayza@clinica.com', 19),
('Cristian', 'Ormeño', '987100057', 'cristian.ormeno@clinica.com', 19),
-- Medicina Interna (20)
('Francisco', 'Quiroz', '987100058', 'francisco.quiroz@clinica.com', 20),
('Natalia', 'Rengifo', '987100059', 'natalia.rengifo@clinica.com', 20),
('Adrián', 'Portilla', '987100060', 'adrian.portilla@clinica.com', 20),
-- Nefrología (21)
('Fabiola', 'Bermúdez', '987100061', 'fabiola.bermudez@clinica.com', 21),
('José', 'Reategui', '987100062', 'jose.reategui@clinica.com', 21),
('Liliana', 'Durán', '987100063', 'liliana.duran@clinica.com', 21),
-- Cirugía General (22)
('Mauricio', 'Tello', '987100064', 'mauricio.tello@clinica.com', 22),
('Andrea', 'Ibáñez', '987100065', 'andrea.ibanez@clinica.com', 22),
('Felipe', 'Montoya', '987100066', 'felipe.montoya@clinica.com', 22),
-- Cirugía Plástica (23)
('Sandra', 'Villaseñor', '987100067', 'sandra.villasenor@clinica.com', 23),
('Álvaro', 'Linares', '987100068', 'alvaro.linares@clinica.com', 23),
('Daniel', 'Serrano', '987100069', 'daniel.serrano@clinica.com', 23),
-- Geriatría (24)
('Gloria', 'Campos', '987100070', 'gloria.campos@clinica.com', 24),
('Hernán', 'Espino', '987100071', 'hernan.espino@clinica.com', 24),
('Patricia', 'Valdivia', '987100072', 'patricia.valdivia@clinica.com', 24)
GO

-- Insertar datos en Géneros
INSERT INTO Generos (genero) VALUES 
('Masculino'),
('Femenino')
GO

-- Insertar datos en Pacientes 
INSERT INTO Pacientes 
(nombres, apellidoPaterno, apellidoMaterno, fechaNacimiento, idGenero, peso, altura, idTipoSangre, celular, correo, idTipoDocumento, documento, password, titular)
VALUES
('Juan', 'Pérez', 'García', '1990-05-14', 1, 72.5, 1.75, 1, '987111111', 'juan.perez@mail.com', 1, '12345678', 'pass123', 1),
('María', 'López', 'Fernández', '1992-08-22', 2, 60.2, 1.65, 3, '987111112', 'maria.lopez@mail.com', 1, '87654321', 'pass123', 1),
('Gabriela', 'Suárez', 'Delgado', '1995-03-25', 2, 62.3, 1.68, 6, '987111116', 'gabriela.suarez@mail.com', 1, '32165498', 'pass123', 1),
('Diego', 'Ortega', 'Reyes', '1988-12-05', 1, 82.1, 1.82, 1, '987111119', 'diego.ortega@mail.com', 1, '11223344', 'pass123', 1);
GO

-- Insertar datos en Parentescos
INSERT INTO TipoParentesco (parentesco)
VALUES 
('Padre'),
('Madre'),
('Hijo (a)'),
('Hermano (a)'),
('Cónyuge'),
('Otro');
GO
