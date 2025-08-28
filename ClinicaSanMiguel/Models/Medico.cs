using System.Reflection.Metadata;

namespace ClinicaSanMiguel.Models
{
    public class Medico
    {
        public int idMedico { get; set; }
        public string nombres { get; set; } = string.Empty;
        public string apellidos { get; set; } = string.Empty;
        public string ? celular { get; set; }
        public string ? correo { get; set; }

        // FK + Navegacion
        public int idEspecialidad { get; set; }
        public Especialidad Especialidad { get; set; } = null!;

        // Relacion
        public ICollection<Clinica> Clinicas { get; set; } = new List<Clinica>();
        public ICollection<CitaMedica> CitasMedicas { get; set; } = new List<CitaMedica>();
    }
}
