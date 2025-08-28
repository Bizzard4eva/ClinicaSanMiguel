namespace ClinicaSanMiguel.Models
{
    public class TipoSangre
    {
        public int idTipoSangre { get; set; }
        public string tipoSangre {  set; get; } = string.Empty;

        // Relacion
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
    }
}
