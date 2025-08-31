using Azure.Core;
using ClinicaSanMiguel.DTOs;
using ClinicaSanMiguel.Models;
using ClinicaSanMiguel.Repositorys.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicaSanMiguel.Repositorys.Implementations
{
    public class CitaMedicaRepository : ICitaMedicaRepository
    {
        private readonly string _conexion;

        public CitaMedicaRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("stringConexion")!;
        }


        public async Task<GeneralResponseDto> MedicalReserveAsync(MedicalReserveRequestDto request)
        {
            var response = new GeneralResponseDto();

            await using (SqlConnection conexion = new SqlConnection(_conexion))

            await using (SqlCommand command = new SqlCommand("ReservarCitaMedicaSP", conexion))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idPaciente", request.IdPaciente);
                command.Parameters.AddWithValue("@idClinica", request.IdClinica);
                command.Parameters.AddWithValue("@idMedico", request.IdMedico);
                command.Parameters.AddWithValue("@fecha", request.Fecha);
                command.Parameters.AddWithValue("@idSeguroSalud", request.IdSeguroSeguro);

                await conexion.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        response.Resultado = reader.GetInt32(reader.GetOrdinal("Resultado"));
                        // Resultado retorna --> idCitaMedica
                        response.Mensaje = reader.GetString(reader.GetOrdinal("Mensaje"));
                    }
                }
            }
            return response;
        }

        public async Task<DetailMedicalResponseDto> DetailMedicalAsync(int idCitaMedica)
        {
            var response = new DetailMedicalResponseDto();

            await using (SqlConnection conexion = new SqlConnection(_conexion))

                await using (SqlCommand command = new SqlCommand("DetallesCitaMedicaSP", conexion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idCitaMedica", idCitaMedica);

                    await conexion.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                        response.Paciente = reader.GetString(reader.GetOrdinal("Paciente"));
                        response.Especialidad = reader.GetString(reader.GetOrdinal("Especialidad"));
                        response.Medico = reader.GetString(reader.GetOrdinal("Medico"));
                        response.Clinica = reader.GetString(reader.GetOrdinal("Clinica"));
                        response.FechaHora = reader.GetDateTime(reader.GetOrdinal("FechaHora"));
                        response.Seguro = reader.GetString(reader.GetOrdinal("Seguro"));
                        response.Precio = reader.GetDecimal(reader.GetOrdinal("Precio"));
                        }
                    }
                }
            return response;
        }

        public async Task<List<PatientRelativesDto>> PatientsForMedicalReserveAsync(int idPaciente)
        {
            var response = new List<PatientRelativesDto>();

            await using (SqlConnection conexion = new SqlConnection(_conexion))
            await using (SqlCommand command = new SqlCommand("PacienteConFamiliaresSP", conexion))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idPaciente", idPaciente);

                await conexion.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var paciente = new PatientRelativesDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("idPaciente")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Parentesco = reader.GetString(reader.GetOrdinal("Parentesco"))
                        };
                        response.Add(paciente);
                    }
                }
            }
            return response;
        }

        public async Task<List<GeneralListResponseDto>> SpecialtiesAsync()
        {
            var response = new List<GeneralListResponseDto>();

            await using (SqlConnection conexion = new SqlConnection(_conexion))
            await using (SqlCommand command = new SqlCommand("SELECT idEspecialidad, especialidad FROM Especialidades", conexion))
            {
                await conexion.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var especialdad = new GeneralListResponseDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("idEspecialidad")),
                            Nombre = reader.GetString(reader.GetOrdinal("especialidad"))
                        };
                        response.Add(especialdad);
                    }
                }
            }
             return response;
        }

        public async Task<List<GeneralListResponseDto>> ClinicsAsync()
        {
            var response = new List<GeneralListResponseDto>();

            await using (SqlConnection conexion = new SqlConnection(_conexion))
            await using (SqlCommand command = new SqlCommand("SELECT idClinica, nombre FROM Clinicas", conexion))
            {
                await conexion.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var clinica = new GeneralListResponseDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("idClinica")),
                            Nombre = reader.GetString(reader.GetOrdinal("nombre"))
                        };
                        response.Add(clinica);
                    }
                }
            }
            return response;
        }

        public async Task<List<GeneralListResponseDto>> DoctorsAsync(int idClinica, int idEspecialidad)
        {
            var response = new List<GeneralListResponseDto>();

            await using (SqlConnection conexion = new SqlConnection(_conexion))

            await using (SqlCommand command = new SqlCommand("MedicosPorEspecialidadClinicaSP", conexion))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idClinica", idClinica);
                command.Parameters.AddWithValue("@idEspecialidad", idEspecialidad);

                await conexion.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var medico = new GeneralListResponseDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("idMedico")),
                            Nombre = reader.GetString(reader.GetOrdinal("nombre"))
                        };
                        response.Add(medico);
                    }
                }
            }
            return response;
        }

        public async Task<List<GeneralListResponseDto>> HealthInsuranceAsync()
        {
            var response = new List<GeneralListResponseDto>();

            await using (SqlConnection conexion = new SqlConnection(_conexion))
            await using (SqlCommand command = new SqlCommand("SELECT idSeguroSalud, nombreSeguro FROM SeguroSalud", conexion))
            {
                await conexion.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var seguro = new GeneralListResponseDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("idSeguroSalud")),
                            Nombre = reader.GetString(reader.GetOrdinal("nombreSeguro"))
                        };
                        response.Add(seguro);
                    }
                }
            }
            return response;
        }

        public CitaMedicaRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("stringConexion")!;
        }

        public async Task<List<Especialidad>> GetEspecialidadesAsync()
        {
            var especialidades = new List<Especialidad>();
            await using (SqlConnection connection = new SqlConnection(_conexion))
            {
                var command = new SqlCommand("SELECT idEspecialidad, especialidad, precio FROM Especialidades ORDER BY especialidad", connection);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        especialidades.Add(new Especialidad
                        {
                            idEspecialidad = reader.GetInt32("idEspecialidad"),
                            especialidad = reader.GetString("especialidad"),
                            precio = reader.GetInt32("precio")
                        });
                    }
                }
            }
            return especialidades;
        }

        public async Task<List<SeguroSalud>> GetSegurosAsync()
        {
            var seguros = new List<SeguroSalud>();
            await using (SqlConnection connection = new SqlConnection(_conexion))
            {
                var command = new SqlCommand("SELECT idSeguroSalud, nombreSeguro, cobertura FROM SeguroSalud ORDER BY nombreSeguro", connection);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        seguros.Add(new SeguroSalud
                        {
                            idSeguroSalud = reader.GetInt32("idSeguroSalud"),
                            nombreSeguro = reader.GetString("nombreSeguro"),
                            cobertura = reader.GetInt32("cobertura")
                        });
                    }
                }
            }
            return seguros;
        }

        public async Task<List<Medico>> GetMedicosByEspecialidadAsync(int especialidadId)
        {
            var medicos = new List<Medico>();
            await using (SqlConnection connection = new SqlConnection(_conexion))
            {
                var command = new SqlCommand(@"
                    SELECT m.idMedico, m.nombres, m.apellidos, m.celular, m.correo, 
                           m.idEspecialidad, e.especialidad 
                    FROM Medicos m 
                    INNER JOIN Especialidades e ON m.idEspecialidad = e.idEspecialidad 
                    WHERE m.idEspecialidad = @especialidadId
                    ORDER BY m.apellidos, m.nombres", connection);
                command.Parameters.AddWithValue("@especialidadId", especialidadId);
                
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        medicos.Add(new Medico
                        {
                            idMedico = reader.GetInt32("idMedico"),
                            nombres = reader.GetString("nombres"),
                            apellidos = reader.GetString("apellidos"),
                            celular = reader.IsDBNull("celular") ? null : reader.GetString("celular"),
                            correo = reader.IsDBNull("correo") ? null : reader.GetString("correo"),
                            idEspecialidad = reader.GetInt32("idEspecialidad"),
                            Especialidad = new Especialidad
                            {
                                idEspecialidad = reader.GetInt32("idEspecialidad"),
                                especialidad = reader.GetString("especialidad")
                            }
                        });
                    }
                }
            }
            return medicos;
        }

        public async Task<Especialidad?> GetEspecialidadByIdAsync(int especialidadId)
        {
            await using (SqlConnection connection = new SqlConnection(_conexion))
            {
                var command = new SqlCommand("SELECT idEspecialidad, especialidad, precio FROM Especialidades WHERE idEspecialidad = @especialidadId", connection);
                command.Parameters.AddWithValue("@especialidadId", especialidadId);
                
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Especialidad
                        {
                            idEspecialidad = reader.GetInt32("idEspecialidad"),
                            especialidad = reader.GetString("especialidad"),
                            precio = reader.GetInt32("precio")
                        };
                    }
                }
            }
            return null;
        }

        public async Task<Medico?> GetMedicoByIdAsync(int medicoId)
        {
            await using (SqlConnection connection = new SqlConnection(_conexion))
            {
                var command = new SqlCommand(@"
                    SELECT m.idMedico, m.nombres, m.apellidos, m.celular, m.correo, 
                           m.idEspecialidad, e.especialidad 
                    FROM Medicos m 
                    INNER JOIN Especialidades e ON m.idEspecialidad = e.idEspecialidad 
                    WHERE m.idMedico = @medicoId", connection);
                command.Parameters.AddWithValue("@medicoId", medicoId);
                
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Medico
                        {
                            idMedico = reader.GetInt32("idMedico"),
                            nombres = reader.GetString("nombres"),
                            apellidos = reader.GetString("apellidos"),
                            celular = reader.IsDBNull("celular") ? null : reader.GetString("celular"),
                            correo = reader.IsDBNull("correo") ? null : reader.GetString("correo"),
                            idEspecialidad = reader.GetInt32("idEspecialidad"),
                            Especialidad = new Especialidad
                            {
                                idEspecialidad = reader.GetInt32("idEspecialidad"),
                                especialidad = reader.GetString("especialidad")
                            }
                        };
                    }
                }
            }
            return null;
        }

        public async Task<SeguroSalud?> GetSeguroByIdAsync(int seguroId)
        {
            await using (SqlConnection connection = new SqlConnection(_conexion))
            {
                var command = new SqlCommand("SELECT idSeguroSalud, nombreSeguro, cobertura FROM SeguroSalud WHERE idSeguroSalud = @seguroId", connection);
                command.Parameters.AddWithValue("@seguroId", seguroId);
                
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new SeguroSalud
                        {
                            idSeguroSalud = reader.GetInt32("idSeguroSalud"),
                            nombreSeguro = reader.GetString("nombreSeguro"),
                            cobertura = reader.GetInt32("cobertura")
                        };
                    }
                }
            }
            return null;
        }
    }
}
