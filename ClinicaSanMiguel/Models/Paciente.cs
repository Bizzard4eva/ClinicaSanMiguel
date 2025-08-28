using System.ComponentModel.DataAnnotations;

namespace ClinicaSanMiguel.Models
{
    public class Paciente
    {
        public int idPaciente { get; set; }
        public string nombres { get; set; } = null!;
        public string apellidoPaterno { get; set; } = null!;
        public string apellidoMaterno { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public string Password { get; set; } = null!;

        // FK
        public int idTipoDocumento { get; set; }
        public TipoDocumento ? TipoDocumento { get; set; }
        public int ? idGenero {  get; set; }
        public Genero ? genero { get; set; }
        public int ? idTipoSangre { get; set; }
        public TipoSangre ? tipoSangre { get; set; }

        // Opcionales
        [DisplayFormat(DataFormatString = "{0:dd//MM/yyyy}")]
        public DateTime ? FechaNacimiento { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Altura { get; set; }
        public string? Celular { get; set; }
        public string? Correo { get; set; }
    }
}
