namespace ClinicaSanMiguel.Models
{
    public class SeguroSalud
    {
        public int idSeguroSalud { get; set; }
        public string ? nombreSeguro {  get; set; }
        public int ? cobertura { get; set; }

        // Relacion
        public ICollection<CitaMedica> CitaMedicas { get; set; }
    }
}
