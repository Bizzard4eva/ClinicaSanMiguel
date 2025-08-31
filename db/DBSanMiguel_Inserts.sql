Use DBSanMiguel
GO


-- Tipo de Documentos
INSERT INTO TipoDocumento (documento)
VALUES 
('DNI'),
('Carnet de Extranjer�a'),
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
('Clinica Bre�a', 'Av. Principal 303 - Bre�a', '999111227'),
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
('Pac�fico EPS', 90),
('R�mac EPS', 85),
('Mapfre EPS', 80),
('Sanitas EPS', 80),
('La Positiva EPS', 75),
-- Seguros de salud particulares
('Pac�fico Seguros', 80),
('R�mac Seguros', 75),
('Mapfre Seguros', 70),
('La Positiva Seguros', 70),
-- Sin seguro
('Particular', 0);
GO

-- Insertar datos en Especialidades

INSERT INTO Especialidades(especialidad, precio) VALUES
('Medicina General', 80),
('Pediatr�a', 100),
('Ginecolog�a y Obstetricia', 120),
('Cardiolog�a', 150),
('Dermatolog�a', 120),
('Oftalmolog�a', 100),
('Otorrinolaringolog�a', 110),
('Traumatolog�a y Ortopedia', 130),
('Neurolog�a', 160),
('Endocrinolog�a', 140),
('Neumolog�a', 140),
('Gastroenterolog�a', 150),
('Urolog�a', 130),
('Reumatolog�a', 140),
('Psiquiatr�a', 160),
('Psicolog�a', 90),
('Odontolog�a General', 80),
('Odontopediatr�a', 100),
('Oncolog�a', 200),
('Medicina Interna', 120),
('Nefrolog�a', 150),
('Cirug�a General', 180),
('Cirug�a Pl�stica', 250),
('Geriatr�a', 110);
GO

-- Insertar datos en Medicos

INSERT INTO Medicos (nombres, apellidos, celular, correo, idEspecialidad) VALUES 
-- Medicina General (1)
('Mar�a', 'G�mez', '987100001', 'maria.gomez@clinica.com', 1),
('Carlos', 'Mart�nez', '987100002', 'carlos.martinez@clinica.com', 1),
('Ana', 'Fern�ndez', '987100003', 'ana.fernandez@clinica.com', 1),
-- Pediatr�a (2)
('Luis', 'Ram�rez', '987100004', 'luis.ramirez@clinica.com', 2),
('Claudia', 'Torres', '987100005', 'claudia.torres@clinica.com', 2),
('Jorge', 'Castro', '987100006', 'jorge.castro@clinica.com', 2),
-- Ginecolog�a y Obstetricia (3)
('Patricia', 'Vargas', '987100007', 'patricia.vargas@clinica.com', 3),
('Gabriela', 'Mendoza', '987100008', 'gabriela.mendoza@clinica.com', 3),
('Rosa', 'Campos', '987100009', 'rosa.campos@clinica.com', 3),
-- Cardiolog�a (4)
('Fernando', 'L�pez', '987100010', 'fernando.lopez@clinica.com', 4),
('Carmen', 'Morales', '987100011', 'carmen.morales@clinica.com', 4),
('Pedro', 'Silva', '987100012', 'pedro.silva@clinica.com', 4),
-- Dermatolog�a (5)
('Luc�a', 'Reyes', '987100013', 'lucia.reyes@clinica.com', 5),
('Andr�s', 'Soto', '987100014', 'andres.soto@clinica.com', 5),
('Beatriz', 'Ch�vez', '987100015', 'beatriz.chavez@clinica.com', 5),
-- Oftalmolog�a (6)
('Hugo', 'Flores', '987100016', 'hugo.flores@clinica.com', 6),
('Marta', 'Paredes', '987100017', 'marta.paredes@clinica.com', 6),
('Diego', 'Herrera', '987100018', 'diego.herrera@clinica.com', 6),
-- Otorrinolaringolog�a (7)
('Alberto', 'Navarro', '987100019', 'alberto.navarro@clinica.com', 7),
('Silvia', 'Cruz', '987100020', 'silvia.cruz@clinica.com', 7),
('Ricardo', 'Espinoza', '987100021', 'ricardo.espinoza@clinica.com', 7),
-- Traumatolog�a y Ortopedia (8)
('Eduardo', 'Vega', '987100022', 'eduardo.vega@clinica.com', 8),
('Daniela', 'Ortega', '987100023', 'daniela.ortega@clinica.com', 8),
('Jaime', 'Salazar', '987100024', 'jaime.salazar@clinica.com', 8),
-- Neurolog�a (9)
('Sof�a', 'Delgado', '987100025', 'sofia.delgado@clinica.com', 9),
('Manuel', 'Acosta', '987100026', 'manuel.acosta@clinica.com', 9),
('Valeria', 'Pe�a', '987100027', 'valeria.pena@clinica.com', 9),
-- Endocrinolog�a (10)
('Javier', 'Rojas', '987100028', 'javier.rojas@clinica.com', 10),
('Paola', 'Aguilar', '987100029', 'paola.aguilar@clinica.com', 10),
('Esteban', 'Mej�a', '987100030', 'esteban.mejia@clinica.com', 10),
-- Neumolog�a (11)
('Camila', 'Ruiz', '987100031', 'camila.ruiz@clinica.com', 11),
('Roberto', 'Paz', '987100032', 'roberto.paz@clinica.com', 11),
('Isabel', 'Guerrero', '987100033', 'isabel.guerrero@clinica.com', 11),
-- Gastroenterolog�a (12)
('Alfonso', 'Ben�tez', '987100034', 'alfonso.benitez@clinica.com', 12),
('M�nica', 'Prieto', '987100035', 'monica.prieto@clinica.com', 12),
('Guillermo', 'Sol�s', '987100036', 'guillermo.solis@clinica.com', 12),
-- Urolog�a (13)
('Mart�n', 'Ibarra', '987100037', 'martin.ibarra@clinica.com', 13),
('Lorena', 'Su�rez', '987100038', 'lorena.suarez@clinica.com', 13),
('H�ctor', 'Palacios', '987100039', 'hector.palacios@clinica.com', 13),
-- Reumatolog�a (14)
('Patricio', 'Bravo', '987100040', 'patricio.bravo@clinica.com', 14),
('Karla', 'Serrano', '987100041', 'karla.serrano@clinica.com', 14),
('Renato', 'Ramos', '987100042', 'renato.ramos@clinica.com', 14),
-- Psiquiatr�a (15)
('Victoria', 'Campos', '987100043', 'victoria.campos@clinica.com', 15),
('�scar', 'Huerta', '987100044', 'oscar.huerta@clinica.com', 15),
('Alejandra', 'Meza', '987100045', 'alejandra.meza@clinica.com', 15),
-- Psicolog�a (16)
('Nicol�s', 'Fuentes', '987100046', 'nicolas.fuentes@clinica.com', 16),
('Elena', 'Cornejo', '987100047', 'elena.cornejo@clinica.com', 16),
('Marcos', 'Villanueva', '987100048', 'marcos.villanueva@clinica.com', 16),
-- Odontolog�a General (17)
('�ngela', 'Pizarro', '987100049', 'angela.pizarro@clinica.com', 17),
('Ra�l', 'Peralta', '987100050', 'raul.peralta@clinica.com', 17),
('Teresa', 'N��ez', '987100051', 'teresa.nunez@clinica.com', 17),
-- Odontopediatr�a (18)
('Carolina', 'Rosales', '987100052', 'carolina.rosales@clinica.com', 18),
('Gonzalo', 'Del Solar', '987100053', 'gonzalo.delsolar@clinica.com', 18),
('Pamela', 'Quispe', '987100054', 'pamela.quispe@clinica.com', 18),
-- Oncolog�a (19)
('Rodrigo', 'Valverde', '987100055', 'rodrigo.valverde@clinica.com', 19),
('Julia', 'Loayza', '987100056', 'julia.loayza@clinica.com', 19),
('Cristian', 'Orme�o', '987100057', 'cristian.ormeno@clinica.com', 19),
-- Medicina Interna (20)
('Francisco', 'Quiroz', '987100058', 'francisco.quiroz@clinica.com', 20),
('Natalia', 'Rengifo', '987100059', 'natalia.rengifo@clinica.com', 20),
('Adri�n', 'Portilla', '987100060', 'adrian.portilla@clinica.com', 20),
-- Nefrolog�a (21)
('Fabiola', 'Berm�dez', '987100061', 'fabiola.bermudez@clinica.com', 21),
('Jos�', 'Reategui', '987100062', 'jose.reategui@clinica.com', 21),
('Liliana', 'Dur�n', '987100063', 'liliana.duran@clinica.com', 21),
-- Cirug�a General (22)
('Mauricio', 'Tello', '987100064', 'mauricio.tello@clinica.com', 22),
('Andrea', 'Ib��ez', '987100065', 'andrea.ibanez@clinica.com', 22),
('Felipe', 'Montoya', '987100066', 'felipe.montoya@clinica.com', 22),
-- Cirug�a Pl�stica (23)
('Sandra', 'Villase�or', '987100067', 'sandra.villasenor@clinica.com', 23),
('�lvaro', 'Linares', '987100068', 'alvaro.linares@clinica.com', 23),
('Daniel', 'Serrano', '987100069', 'daniel.serrano@clinica.com', 23),
-- Geriatr�a (24)
('Gloria', 'Campos', '987100070', 'gloria.campos@clinica.com', 24),
('Hern�n', 'Espino', '987100071', 'hernan.espino@clinica.com', 24),
('Patricia', 'Valdivia', '987100072', 'patricia.valdivia@clinica.com', 24)
GO

-- Insertar datos en G�neros
INSERT INTO Generos (genero) VALUES 
('Masculino'),
('Femenino')
GO

-- Insertar datos en Pacientes 
INSERT INTO Pacientes 
(nombres, apellidoPaterno, apellidoMaterno, fechaNacimiento, idGenero, peso, altura, idTipoSangre, celular, correo, idTipoDocumento, documento, password, titular)
VALUES
('Juan', 'P�rez', 'Garc�a', '1990-05-14', 1, 72.5, 1.75, 1, '987111111', 'juan.perez@mail.com', 1, '12345678', 'pass123', 1),
('Mar�a', 'L�pez', 'Fern�ndez', '1992-08-22', 2, 60.2, 1.65, 3, '987111112', 'maria.lopez@mail.com', 1, '87654321', 'pass123', 1),
('Gabriela', 'Su�rez', 'Delgado', '1995-03-25', 2, 62.3, 1.68, 6, '987111116', 'gabriela.suarez@mail.com', 1, '32165498', 'pass123', 1),
('Diego', 'Ortega', 'Reyes', '1988-12-05', 1, 82.1, 1.82, 1, '987111119', 'diego.ortega@mail.com', 1, '11223344', 'pass123', 1);
GO

-- Insertar datos en Parentescos
INSERT INTO TipoParentesco (parentesco)
VALUES 
('Padre'),
('Madre'),
('Hijo (a)'),
('Hermano (a)'),
('C�nyuge'),
('Otro');
GO



-- Asignar m�dicos (1 al 72) a Cl�nica Miraflores (idClinica = 3)
INSERT INTO ClinicaMedico (idClinica, idMedico) VALUES
(3, 1),
(3, 2),
(3, 3),
(3, 4),
(3, 5),
(3, 6),
(3, 7),
(3, 8),
(3, 9),
(3, 10),
(3, 11),
(3, 12),
(3, 13),
(3, 14),
(3, 15),
(3, 16),
(3, 17),
(3, 18),
(3, 19),
(3, 20),
(3, 21),
(3, 22),
(3, 23),
(3, 24),
(3, 25),
(3, 26),
(3, 27),
(3, 28),
(3, 29),
(3, 30),
(3, 31),
(3, 32),
(3, 33),
(3, 34),
(3, 35),
(3, 36),
(3, 37),
(3, 38),
(3, 39),
(3, 40),
(3, 41),
(3, 42),
(3, 43),
(3, 44),
(3, 45),
(3, 46),
(3, 47),
(3, 48),
(3, 49),
(3, 50),
(3, 51),
(3, 52),
(3, 53),
(3, 54),
(3, 55),
(3, 56),
(3, 57),
(3, 58),
(3, 59),
(3, 60),
(3, 61),
(3, 62),
(3, 63),
(3, 64),
(3, 65),
(3, 66),
(3, 67),
(3, 68),
(3, 69),
(3, 70),
(3, 71),
(3, 72);
GO

-- ================================
-- M�dicos adicionales por especialidad 
-- ================================
INSERT INTO Medicos (nombres, apellidos, celular, correo, idEspecialidad) VALUES
-- Medicina General (1)
('Julio', 'Mendoza', '987100073', 'julio.mendoza@clinica.com', 1),
('Diana', 'Vega', '987100074', 'diana.vega@clinica.com', 1),
('Mateo', 'Paredes', '987100075', 'mateo.paredes@clinica.com', 1),

-- Pediatr�a (2)
('Paula', 'R�os', '987100076', 'paula.rios@clinica.com', 2),
('Tom�s', 'Fuentes', '987100077', 'tomas.fuentes@clinica.com', 2),
('Claudio', 'Sandoval', '987100078', 'claudio.sandoval@clinica.com', 2),

-- Ginecolog�a y Obstetricia (3)
('Angela', 'Dur�n', '987100079', 'angela.duran@clinica.com', 3),
('Estela', 'Bravo', '987100080', 'estela.bravo@clinica.com', 3),
('Marisol', 'Reyna', '987100081', 'marisol.reyna@clinica.com', 3),

-- Cardiolog�a (4)
('Rafael', 'Chac�n', '987100082', 'rafael.chacon@clinica.com', 4),
('M�nica', 'Benavides', '987100083', 'monica.benavides@clinica.com', 4),
('Gustavo', 'Quispe', '987100084', 'gustavo.quispe@clinica.com', 4),

-- Dermatolog�a (5)
('F�tima', 'L�pez', '987100085', 'fatima.lopez@clinica.com', 5),
('Ignacio', 'Carrillo', '987100086', 'ignacio.carrillo@clinica.com', 5),
('Andrea', 'Tapia', '987100087', 'andrea.tapia@clinica.com', 5),

-- Oftalmolog�a (6)
('Esteban', 'Ram�n', '987100088', 'esteban.ramon@clinica.com', 6),
('Cecilia', 'Salas', '987100089', 'cecilia.salas@clinica.com', 6),
('Pablo', 'Alarc�n', '987100090', 'pablo.alarcon@clinica.com', 6),

-- Otorrinolaringolog�a (7)
('Jaime', 'Valdez', '987100091', 'jaime.valdez@clinica.com', 7),
('Rosa', 'Carranza', '987100092', 'rosa.carranza@clinica.com', 7),
('Emilio', 'Ortega', '987100093', 'emilio.ortega@clinica.com', 7),

-- Traumatolog�a y Ortopedia (8)
('Nora', 'Campos', '987100094', 'nora.campos@clinica.com', 8),
('Felipe', 'Estrada', '987100095', 'felipe.estrada@clinica.com', 8),
('Rodrigo', 'Torres', '987100096', 'rodrigo.torres@clinica.com', 8),

-- Neurolog�a (9)
('Clara', 'Jim�nez', '987100097', 'clara.jimenez@clinica.com', 9),
('Iv�n', 'Gallardo', '987100098', 'ivan.gallardo@clinica.com', 9),
('Daniela', 'Su�rez', '987100099', 'daniela.suarez@clinica.com', 9),

-- Endocrinolog�a (10)
('Andr�s', 'Palacios', '987100100', 'andres.palacios@clinica.com', 10),
('Elisa', 'Romero', '987100101', 'elisa.romero@clinica.com', 10),
('Mauricio', 'C�ceres', '987100102', 'mauricio.caceres@clinica.com', 10),

-- Neumolog�a (11)
('Ver�nica', 'Silva', '987100103', 'veronica.silva@clinica.com', 11),
('Hern�n', 'G�mez', '987100104', 'hernan.gomez@clinica.com', 11),
('Paola', 'Qui�ones', '987100105', 'paola.quinones@clinica.com', 11),

-- Gastroenterolog�a (12)
('Ramiro', 'Delgado', '987100106', 'ramiro.delgado@clinica.com', 12),
('Melissa', 'Torres', '987100107', 'melissa.torres@clinica.com', 12),
('Leonardo', 'Paz', '987100108', 'leonardo.paz@clinica.com', 12),

-- Urolog�a (13)
('Gabriel', 'Reyes', '987100109', 'gabriel.reyes@clinica.com', 13),
('Carmen', 'Escobar', '987100110', 'carmen.escobar@clinica.com', 13),
('Juli�n', 'Meza', '987100111', 'julian.meza@clinica.com', 13),

-- Reumatolog�a (14)
('Paola', 'Herrera', '987100112', 'paola.herrera@clinica.com', 14),
('Arturo', 'Santos', '987100113', 'arturo.santos@clinica.com', 14),
('Natalia', 'Ponce', '987100114', 'natalia.ponce@clinica.com', 14),

-- Psiquiatr�a (15)
('Jorge', 'Berm�dez', '987100115', 'jorge.bermudez@clinica.com', 15),
('Silvana', 'Chavez', '987100116', 'silvana.chavez@clinica.com', 15),
('Enrique', 'Soria', 'enrique.soria@clinica.com', 15),

-- Psicolog�a (16)
('Laura', 'Matos', '987100118', 'laura.matos@clinica.com', 16),
('Cristian', 'Huertas', '987100119', 'cristian.huertas@clinica.com', 16),
('Patricia', 'Alfaro', '987100120', 'patricia.alfaro@clinica.com', 16),

-- Odontolog�a General (17)
('Roberto', 'Gamarra', '987100121', 'roberto.gamarra@clinica.com', 17),
('Marta', 'Calder�n', '987100122', 'marta.calderon@clinica.com', 17),
('Vicente', 'Rivas', '987100123', 'vicente.rivas@clinica.com', 17),

-- Odontopediatr�a (18)
('Esther', 'Maldonado', '987100124', 'esther.maldonado@clinica.com', 18),
('Julian', 'Carrillo', '987100125', 'julian.carrillo@clinica.com', 18),
('Carla', 'Villar', '987100126', 'carla.villar@clinica.com', 18),

-- Oncolog�a (19)
('Rodrigo', 'Del Solar', '987100127', 'rodrigo.delsolar@clinica.com', 19),
('Pamela', 'Valverde', '987100128', 'pamela.valverde@clinica.com', 19),
('Hugo', 'S�nchez', '987100129', 'hugo.sanchez@clinica.com', 19),

-- Medicina Interna (20)
('Alejandro', 'Castillo', '987100130', 'alejandro.castillo@clinica.com', 20),
('Teresa', 'Cornejo', '987100131', 'teresa.cornejo@clinica.com', 20),
('Felipe', 'Moreno', '987100132', 'felipe.moreno@clinica.com', 20),

-- Nefrolog�a (21)
('Luciano', 'C�rdenas', '987100133', 'luciano.cardenas@clinica.com', 21),
('Mar�a', 'Bellido', '987100134', 'maria.bellido@clinica.com', 21),
('Javier', 'G�lvez', '987100135', 'javier.galvez@clinica.com', 21),

-- Cirug�a General (22)
('Patricio', 'Lozano', '987100136', 'patricio.lozano@clinica.com', 22),
('Andrea', 'Palma', '987100137', 'andrea.palma@clinica.com', 22),
('Victor', 'Rom�n', '987100138', 'victor.roman@clinica.com', 22),

-- Cirug�a Pl�stica (23)
('Gabriela', 'Farf�n', '987100139', 'gabriela.farfan@clinica.com', 23),
('Esteban', 'Matos', '987100140', 'esteban.matos@clinica.com', 23),
('Ximena', 'Castro', '987100141', 'ximena.castro@clinica.com', 23),

-- Geriatr�a (24)
('Francisco', 'Arroyo', '987100142', 'francisco.arroyo@clinica.com', 24),
('Daniela', 'Bustamante', '987100143', 'daniela.bustamante@clinica.com', 24),
('C�sar', 'Montes', '987100144', 'cesar.montes@clinica.com', 24);
GO


-- Asignar m�dicos (73 al 144) a Cl�nica San Borja (idClinica = 10)
INSERT INTO ClinicaMedico (idClinica, idMedico) VALUES
(10, 73),
(10, 74),
(10, 75),
(10, 76),
(10, 77),
(10, 78),
(10, 79),
(10, 80),
(10, 81),
(10, 82),
(10, 83),
(10, 84),
(10, 85),
(10, 86),
(10, 87),
(10, 88),
(10, 89),
(10, 90),
(10, 91),
(10, 92),
(10, 93),
(10, 94),
(10, 95),
(10, 96),
(10, 97),
(10, 98),
(10, 99),
(10, 100),
(10, 101),
(10, 102),
(10, 103),
(10, 104),
(10, 105),
(10, 106),
(10, 107),
(10, 108),
(10, 109),
(10, 110),
(10, 111),
(10, 112),
(10, 113),
(10, 114),
(10, 115),
(10, 116),
(10, 117),
(10, 118),
(10, 119),
(10, 120),
(10, 121),
(10, 122),
(10, 123),
(10, 124),
(10, 125),
(10, 126),
(10, 127),
(10, 128),
(10, 129),
(10, 130),
(10, 131),
(10, 132),
(10, 133),
(10, 134),
(10, 135),
(10, 136),
(10, 137),
(10, 138),
(10, 139),
(10, 140),
(10, 141),
(10, 142),
(10, 143),
(10, 144);
GO


select * from Medicos
go

select * from Clinicas
go

select * from ClinicaMedico
go