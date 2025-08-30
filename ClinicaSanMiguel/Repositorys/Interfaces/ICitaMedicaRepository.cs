using ClinicaSanMiguel.DTOs;
using ClinicaSanMiguel.Models;

namespace ClinicaSanMiguel.Repositorys.Interfaces
{
    public interface ICitaMedicaRepository
    {
        Task<GeneralResponseDto> MedicalReserveAsync(MedicalReserveRequestDto request);
        Task<DetailMedicalResponseDto> DetailMedicalAsync(int idCitaMedica);

        // Listados
        Task<List<PatientRelativesDto>> PatientsForMedicalReserveAsync(int idPaciente);
        Task<List<GeneralListResponseDto>> ClinicsAsync();
        Task<List<GeneralListResponseDto>> SpecialtiesAsync();
        Task<List<GeneralListResponseDto>> DoctorsAsync(int idClinica, int idEspecialidad);
        Task<List<GeneralListResponseDto>> HealthInsuranceAsync();
    }
}
