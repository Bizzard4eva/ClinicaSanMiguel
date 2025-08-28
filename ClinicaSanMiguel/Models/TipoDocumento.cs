using System.ComponentModel.DataAnnotations;

namespace ClinicaSanMiguel.Models
{
    public class TipoDocumento
    {
        public int idTipoDocumento { get; set; }
        public string documento { get; set; } = string.Empty;

        // Relacion
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
    }
}
