using System.ComponentModel.DataAnnotations;

namespace ClinicaSanMiguel.Models
{
    public class Paciente
    {
        public int idPaciente { get; set; }
        public string nombres { get; set; } = string.Empty;
        public string apellidoPaterno { get; set; } = string.Empty;
        public string apellidoMaterno { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ? fechaNacimiento { get; set; }
        public decimal ? peso { get; set; }
        public decimal ? altura { get; set; }
        public string ? celular { get; set; }
        public string ? correo { get; set; }
        public string documento { get; set; } = string.Empty;
        public string ? password { get; set; }
        public bool titular { get; set; }

        // FK + Navegacion
        public int idGenero { get; set; }
        public Genero Genero { get; set; } = null!;
        public int ? idTipoSangre { get; set; }
        public TipoSangre ? TipoSangre { get; set; }
        public int idTipoDocumento { get; set; }
        public TipoDocumento TipoDocumento { get; set; } = null!;

        // Relacion
        public ICollection<CitaMedica> citaMedicas { get; set; } = new List<CitaMedica>();
    }
}
