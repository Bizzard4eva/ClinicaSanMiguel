using ClinicaSanMiguel.DTOs;
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

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var response = new LoginResponseDto();

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
                        if(await reader.ReadAsync())
                        {
                            response.Resultado = reader.GetInt32(reader.GetOrdinal("Resultado"));
                            response.Mensaje = reader.GetString(reader.GetOrdinal("Mensaje"));
                        }
                    }
                }
            

            return response;
        }

        public async Task<LoginResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var response = new LoginResponseDto();

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
                        if(await reader.ReadAsync())
                        {
                            response.Resultado = reader.GetInt32(reader.GetOrdinal("Resultado"));
                            response.Mensaje = reader.GetString(reader.GetOrdinal("Mensaje"));
                        }
                    }
                }

            return response;
        }
    }
}
