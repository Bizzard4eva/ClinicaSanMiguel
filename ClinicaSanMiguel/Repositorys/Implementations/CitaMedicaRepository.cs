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
