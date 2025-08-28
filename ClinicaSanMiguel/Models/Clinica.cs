namespace ClinicaSanMiguel.Models
{
    public class Clinica
    {
        public int idClinica { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string ? direccion {  get; set; }
        public string ? celular { get; set; }

        //Relacion
        public ICollection<Medico> Medicos { get; set; } = new List<Medico>();
        public ICollection<CitaMedica> CitasMedicas { get; set; } = new List<CitaMedica>();

    }
}
