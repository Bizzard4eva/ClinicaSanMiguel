using ClinicaSanMiguel.DTOs;
using ClinicaSanMiguel.Models;
using ClinicaSanMiguel.Repositorys.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Data.Sql;

namespace ClinicaSanMiguel.Repositorys.Implementations
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly string _conexion;

        public PacienteRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("stringConexion")!;
        }

        public async Task<GeneralResponseDto> AddFamiliarAsync(AddFamiliarRequestDto request)
        {
            var response = new GeneralResponseDto();

            await using (SqlConnection con = new SqlConnection(_conexion))
            await using (SqlCommand cmd = new SqlCommand("AgregarFamiliarSP", con))
            { 
                cmd.CommandType = CommandType.StoredProcedure; 
                cmd.Parameters.AddWithValue("@idPacienteTitular", request.idPacienteTitular); 
                cmd.Parameters.AddWithValue("@idTipoParentesco", request.idTipoParentesco);
                cmd.Parameters.AddWithValue("@idTipoDocumento", request.idTipoDocumento);
                cmd.Parameters.AddWithValue("@documento", request.documento); 
                cmd.Parameters.AddWithValue("@apellidoPaterno", request.apellidoPaterno);
                cmd.Parameters.AddWithValue("@apellidoMaterno", request.apellidoMaterno);
                cmd.Parameters.AddWithValue("@nombres", request.nombres); 
                cmd.Parameters.AddWithValue("@fechaNacimiento", request.fechaNacimiento);
                cmd.Parameters.AddWithValue("@celular", request.celular);
                cmd.Parameters.AddWithValue("@correo", request.correo); 
                cmd.Parameters.AddWithValue("@idGenero", request.idGenero);

                await con.OpenAsync();
                
                using (var reader = await cmd.ExecuteReaderAsync()) 
                { 
                    if (await reader.ReadAsync()) 
                    { 
                        response.Resultado = reader.GetInt32(reader.GetOrdinal("Resultado"));
                        response.Mensaje = reader.GetString(reader.GetOrdinal("Mensaje")); 
                    } 
                } 
            } 
            return response;
        }

        public async Task<GeneralResponseDto> LoginAsync(LoginRequestDto request)
        {
            var response = new GeneralResponseDto();

            await using (SqlConnection conexion = new SqlConnection(_conexion))

            await using (SqlCommand command = new SqlCommand("InicioSesionSP", conexion))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idTipoDocumento", request.IdTipoDocumento);
                command.Parameters.AddWithValue("@documento", request.Documento);
                command.Parameters.AddWithValue("@password", request.Password);

                await conexion.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        response.Resultado = reader.GetInt32(reader.GetOrdinal("Resultado"));
                        response.Mensaje = reader.GetString(reader.GetOrdinal("Mensaje"));
                    }
                }
            }


            return response;
        }

        public async Task<GeneralResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var response = new GeneralResponseDto();

            await using (SqlConnection conexion = new SqlConnection(_conexion))

            await using (SqlCommand command = new SqlCommand("RegistroPacienteSP", conexion))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idTipoDocumento", request.IdTipoDocumento);
                command.Parameters.AddWithValue("@documento", request.Documento);
                command.Parameters.AddWithValue("@nombres", request.Nombres);
                command.Parameters.AddWithValue("@apellidoPaterno", request.ApellidoPaterno);
                command.Parameters.AddWithValue("@apellidoMaterno", request.ApellidoMaterno);
                command.Parameters.AddWithValue("@fechaNacimiento", request.FechaNacimiento);
                command.Parameters.AddWithValue("@celular", request.Celular);
                command.Parameters.AddWithValue("@idGenero", request.IdGenero);
                command.Parameters.AddWithValue("@correo", request.Correo);
                command.Parameters.AddWithValue("@password", request.Password);

                await conexion.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        response.Resultado = reader.GetInt32(reader.GetOrdinal("Resultado"));
                        response.Mensaje = reader.GetString(reader.GetOrdinal("Mensaje"));
                    }
                }
            }

            return response;
        }

        public async Task<GeneralResponseDto> UpdateProfileAsync(UpdateProfileRequestDto request)
        {
            var response = new GeneralResponseDto();

            await using (SqlConnection conexion = new SqlConnection(_conexion))

            await using (SqlCommand command = new SqlCommand("ActualizarPerfilSP", conexion))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idPaciente", request.IdPaciente);
                command.Parameters.AddWithValue("@idGenero", request.IdGenero);
                command.Parameters.AddWithValue("@peso", request.Peso);
                command.Parameters.AddWithValue("@altura", request.Altura);
                command.Parameters.AddWithValue("@idTipoSangre", request.IdTipoSangre);

                await conexion.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        response.Resultado = reader.GetInt32(reader.GetOrdinal("Resultado"));
                        response.Mensaje = reader.GetString(reader.GetOrdinal("Mensaje"));
                    }
                }
            }
            return response;
        }

        public async Task<Paciente?> GetPacienteByIdAsync(int pacienteId)
        {
            Paciente? paciente = null;

            await using (SqlConnection conexion = new SqlConnection(_conexion))
            await using (SqlCommand command = new SqlCommand(@"
                SELECT p.idPaciente, p.nombres, p.apellidoPaterno, p.apellidoMaterno, 
                       p.fechaNacimiento, p.peso, p.altura, p.celular, p.correo, 
                       p.documento, p.titular, p.idGenero, p.idTipoSangre, p.idTipoDocumento,
                       g.genero, ts.tipoSangre, td.documento as tipoDocumento
                FROM Pacientes p
                LEFT JOIN Generos g ON p.idGenero = g.idGenero
                LEFT JOIN TipoSangre ts ON p.idTipoSangre = ts.idTipoSangre
                LEFT JOIN TipoDocumento td ON p.idTipoDocumento = td.idTipoDocumento
                WHERE p.idPaciente = @pacienteId", conexion))
            {
                command.Parameters.AddWithValue("@pacienteId", pacienteId);
                await conexion.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        paciente = new Paciente
                        {
                            idPaciente = reader.GetInt32("idPaciente"),
                            nombres = reader.GetString("nombres"),
                            apellidoPaterno = reader.GetString("apellidoPaterno"),
                            apellidoMaterno = reader.GetString("apellidoMaterno"),
                            fechaNacimiento = reader.IsDBNull("fechaNacimiento") ? null : reader.GetDateTime("fechaNacimiento"),
                            peso = reader.IsDBNull("peso") ? null : reader.GetDecimal("peso"),
                            altura = reader.IsDBNull("altura") ? null : reader.GetDecimal("altura"),
                            celular = reader.IsDBNull("celular") ? null : reader.GetString("celular"),
                            correo = reader.IsDBNull("correo") ? null : reader.GetString("correo"),
                            documento = reader.GetString("documento"),
                            titular = reader.GetBoolean("titular"),
                            idGenero = reader.GetInt32("idGenero"),
                            idTipoSangre = reader.IsDBNull("idTipoSangre") ? null : reader.GetInt32("idTipoSangre"),
                            idTipoDocumento = reader.GetInt32("idTipoDocumento")
                        };

                        // Set navigation properties if available
                        if (!reader.IsDBNull("genero"))
                        {
                            paciente.Genero = new Genero
                            {
                                idGenero = reader.GetInt32("idGenero"),
                                genero = reader.GetString("genero")
                            };
                        }

                        if (!reader.IsDBNull("tipoSangre"))
                        {
                            paciente.TipoSangre = new TipoSangre
                            {
                                idTipoSangre = reader.GetInt32("idTipoSangre"),
                                tipoSangre = reader.GetString("tipoSangre")
                            };
                        }

                        if (!reader.IsDBNull("tipoDocumento"))
                        {
                            paciente.TipoDocumento = new TipoDocumento
                            {
                                idTipoDocumento = reader.GetInt32("idTipoDocumento"),
                                documento = reader.GetString("tipoDocumento")
                            };
                        }
                    }
                }
            }

            return paciente;
        }

        public async Task<List<Paciente>> GetFamiliaresByTitularAsync(int titularId)
        {
            var familiares = new List<Paciente>();
            await using (SqlConnection conexion = new SqlConnection(_conexion))
            await using (SqlCommand command = new SqlCommand(@"
                SELECT f.idPaciente, f.nombres, f.apellidoPaterno, f.apellidoMaterno, 
                       f.documento, f.titular, tp.parentesco
                FROM Pacientes f
                INNER JOIN PacientesParentesco pp ON f.idPaciente = pp.idFamiliar
                INNER JOIN TipoParentesco tp ON pp.idTipoParentesco = tp.idTipoParentesco
                WHERE pp.idPaciente = @titularId", conexion))
            {
                command.Parameters.AddWithValue("@titularId", titularId);
                await conexion.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        familiares.Add(new Paciente
                        {
                            idPaciente = reader.GetInt32("idPaciente"),
                            nombres = reader.GetString("nombres"),
                            apellidoPaterno = reader.GetString("apellidoPaterno"),
                            apellidoMaterno = reader.GetString("apellidoMaterno"),
                            documento = reader.GetString("documento"),
                            titular = reader.GetBoolean("titular")
                        });
                    }
                }
            }
            return familiares;
        }
    }
}
