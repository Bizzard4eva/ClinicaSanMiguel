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
<<<<<<< HEAD
=======
        Task<ProfileResponseDto> LoadingProfileAsync(int idPaciente);
>>>>>>> ebb87ad9ededa54f61699bfa18c097b9863cf2a9
    }
}
