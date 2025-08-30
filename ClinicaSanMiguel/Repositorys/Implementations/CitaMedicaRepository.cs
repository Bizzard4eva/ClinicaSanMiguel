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

        public Task<List<Paciente>> PatientsForMedicalReserveAsync(int idPaciente)
        {
            throw new NotImplementedException(); // TODO
        }

        public Task<List<Especialidad>> SpecialtiesAsync()
        {
            throw new NotImplementedException(); // TODO
        }

        public Task<List<Clinica>> ClinicsAsync()
        {
            throw new NotImplementedException(); // TODO
        }

        public Task<List<Medico>> DoctorsAsync(int idClinica, int idEspecialidad)
        {
            throw new NotImplementedException(); // TODO
        }

        public Task<List<SeguroSalud>> HealthInsuranceAsync()
        {
            throw new NotImplementedException(); // TODO
        }
    }
}
