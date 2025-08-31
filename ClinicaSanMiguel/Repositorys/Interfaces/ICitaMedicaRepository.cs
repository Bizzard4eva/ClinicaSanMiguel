using ClinicaSanMiguel.Models;

namespace ClinicaSanMiguel.Repositorys.Interfaces
{
    public interface ICitaMedicaRepository
    {
        Task<List<Especialidad>> GetEspecialidadesAsync();
        Task<List<SeguroSalud>> GetSegurosAsync();
        Task<List<Medico>> GetMedicosByEspecialidadAsync(int especialidadId);
        Task<Especialidad?> GetEspecialidadByIdAsync(int especialidadId);
        Task<Medico?> GetMedicoByIdAsync(int medicoId);
        Task<SeguroSalud?> GetSeguroByIdAsync(int seguroId);
    }
}
