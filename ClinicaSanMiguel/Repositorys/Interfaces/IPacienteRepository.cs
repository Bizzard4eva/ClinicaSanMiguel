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
    }
}
