namespace ClinicaSanMiguel.DTOs
{
    public class MedicalReserveRequestDto
    {
        public int IdPaciente { get; set; }
        public int IdClinica  { get; set; }
        public int IdMedico { get; set; }
        public int IdSeguroSeguro { get; set; }
        public DateTime Fecha { get; set; }
    }
}
