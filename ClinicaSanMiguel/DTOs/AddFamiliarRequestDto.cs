namespace ClinicaSanMiguel.DTOs
{
    public class AddFamiliarRequestDto
    {
        public int idPaciente { get; set; }
        public int idTipoParentesco { get; set; }
        public int idTipoDocumento { get; set; }
        public string documento { get; set; } = string.Empty;
        public string apellidoPaterno { get; set; } = string.Empty;
        public string apellidoMaterno { get; set; } = string.Empty;
        public string nombres { get; set; } = string.Empty;
        public DateTime fechaNacimiento { get; set; }
        public string celular { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
    }
}
