namespace ClinicaSanMiguel.DTOs
{
    public class RegisterRequestDto
    {
        public int IdTipoDocumento { get; set; }
        public string Documento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Celular { get; set; } = string.Empty;
        public int IdGenero { get; set; } 
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
