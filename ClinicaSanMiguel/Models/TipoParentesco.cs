namespace ClinicaSanMiguel.Models
{
    public class TipoParentesco
    {
        public int idTipoParentesco { get; set; }
        public string parentesco { get; set; } = string.Empty;

        // Relacion
        public ICollection<PacientesParentesco> PacientesParentescos { get; set; } = new List<PacientesParentesco>();
    }
}
