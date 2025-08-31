using ClinicaSanMiguel.DTOs;
using ClinicaSanMiguel.Models;

namespace ClinicaSanMiguel.Repositorys.Interfaces
{
    public interface IPacienteRepository
    {
        Task<GeneralResponseDto> LoginAsync(LoginRequestDto request);
        Task<GeneralResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<GeneralResponseDto> UpdateProfileAsync(UpdateProfileRequestDto request);
        Task<GeneralResponseDto> AddFamiliarAsync(AddFamiliarRequestDto request);
        Task<ProfileResponseDto> LoadingProfileAsync(int idPaciente);

        // Listados
        Task<List<TipoSangre>> ListBloodTypeAsync();
        Task<List<TipoParentesco>> ListRelationshipTypeAsync();
        Task<List<TipoDocumento>> ListDocumentTypeAsync();
        Task<List<Genero>> ListGenresAsync();


        // Citas médicas
        //Task<List<PatientAppointmentDto>> GetPatientAppointmentsAsync(int idPaciente);
    }
}
