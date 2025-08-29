namespace ClinicaSanMiguel.DTOs
{
    public class LoginRequestDto
    {
        public int IdTipoDocumento {  get; set; }
        public string Documento { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
