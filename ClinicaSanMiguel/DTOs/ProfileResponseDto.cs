namespace ClinicaSanMiguel.DTOs
{
    public class ProfileResponseDto
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int Edad { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Altura { get; set; }
        public string GrupoSanguineo { get; set; } = string.Empty;
    }
}
