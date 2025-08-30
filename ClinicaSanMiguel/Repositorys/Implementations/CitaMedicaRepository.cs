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

        public Task<List<Paciente>> PatientsForMedicalReserveAsync(int idPaciente)
        {
            throw new NotImplementedException(); // TODO idPaciente, nombre, apllido paterno, apellido materno
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

    }
}
