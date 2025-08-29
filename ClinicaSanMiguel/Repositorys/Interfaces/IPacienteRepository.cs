using ClinicaSanMiguel.DTOs;
using ClinicaSanMiguel.Models;

namespace ClinicaSanMiguel.Repositorys.Interfaces
{
    public interface IPacienteRepository
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);

    }
}
