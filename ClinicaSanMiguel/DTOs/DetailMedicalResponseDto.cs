namespace ClinicaSanMiguel.DTOs
{
    public class DetailMedicalResponseDto
    {
        public string Paciente { get; set; } = string.Empty;
        public string Especialidad {  get; set; } = string.Empty;
        public string Medico {  get; set; } = string.Empty;
        public string Clinica {  get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string Seguro {  get; set; } = string.Empty;
        public decimal Precio { get; set; }
    }
}
