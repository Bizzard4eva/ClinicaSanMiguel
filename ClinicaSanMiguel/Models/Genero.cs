namespace ClinicaSanMiguel.Models
{
    public class Genero
    {
        public int idGenero { get; set; }
        public string genero { get; set; } = string.Empty;

        // Relacion
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
    }
}
