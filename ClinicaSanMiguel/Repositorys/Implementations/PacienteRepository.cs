using ClinicaSanMiguel.DTOs;
using ClinicaSanMiguel.Repositorys.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            await using (SqlCommand command = new SqlCommand("ActualizarPerfilClinicoSP", conexion))
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

        async Task<ProfileResponseDto> IPacienteRepository.LoadingProfileAsync(int idPaciente)
        {
            var response = new ProfileResponseDto();
            await using (SqlConnection conexion = new SqlConnection(_conexion))
            await using (SqlCommand command = new SqlCommand("CargarPerfilSP", conexion))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idPaciente", idPaciente);
                await conexion.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        response.IdPaciente = reader.GetInt32(reader.GetOrdinal("idPaciente"));
                        response.Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                        response.Genero = reader.GetString(reader.GetOrdinal("Genero"));
                        response.Edad = reader.GetInt32(reader.GetOrdinal("Edad"));
                        response.Peso = reader.IsDBNull(reader.GetOrdinal("Peso")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("Peso"));
                        response.Altura = reader.IsDBNull(reader.GetOrdinal("Altura")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("Altura"));
                        response.GrupoSanguineo = reader.IsDBNull(reader.GetOrdinal("GrupoSanguineo")) ? string.Empty : reader.GetString(reader.GetOrdinal("GrupoSanguineo"));
                    }
                }
            }
            return response;
        }
    }
}
