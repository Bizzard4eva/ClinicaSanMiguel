namespace ClinicaSanMiguel.DTOs
{
    public class PatientAppointmentDto
    {
        public int IdCitaMedica { get; set; }
        public string Paciente { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public string Medico { get; set; } = string.Empty;
        public string Clinica { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string Seguro { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}