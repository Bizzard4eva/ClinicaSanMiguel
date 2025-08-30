using ClinicaSanMiguel.DTOs;
using ClinicaSanMiguel.Models;

namespace ClinicaSanMiguel.Repositorys.Interfaces
{
    public interface ICitaMedicaRepository
    {
        Task<GeneralResponseDto> MedicalReserveAsync(MedicalReserveRequestDto request);
        //Task<DetailMedicalResponseDto> DetailMedicalAsync(DetailMedicalResponseDto request);

        // Listados
        Task<List<Paciente>> PatientsForMedicalReserveAsync(int idPaciente);
        Task<List<Clinica>> ClinicsAsync();
        Task<List<Especialidad>> SpecialtiesAsync();
        Task<List<Medico>> DoctorsAsync(int idClinica, int idEspecialidad);
        Task<List<SeguroSalud>> HealthInsuranceAsync();
    }
}
