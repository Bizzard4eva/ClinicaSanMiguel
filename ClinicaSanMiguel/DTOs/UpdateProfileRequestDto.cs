namespace ClinicaSanMiguel.DTOs
{
    public class UpdateProfileRequestDto
    {
        public int IdPaciente { get; set; }
        public int IdGenero { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
        public int IdTipoSangre { get; set; }
    }
}
