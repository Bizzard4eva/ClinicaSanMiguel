namespace ClinicaSanMiguel.Models
{
    public class CitaMedica
    {
        public int idCitaMedica { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; } = "Pendiente";
        public decimal precio { get; set; }

        // FK + Navegacion
        public int idClinica { get; set; }
        public Clinica Clinica { get; set; } = null!;

        public int idPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public int idMedico {  get; set; }
        public Medico Medico { get; set; } = null!;

        public int idSeguroSalud { get; set; }
        public SeguroSalud SeguroSalud { get; set; } = null!;
    }
}
