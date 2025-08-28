using System.Reflection.Metadata;

namespace ClinicaSanMiguel.Models
{
    public class Medico
    {
        public int ? idMedico { get; set; }
        public string ? nombres { get; set; }
        public string ? apellidos { get; set; }
        public string ? celular { get; set; }
        public string ? correo { get; set; }
        public int ? idEspecialidad { get; set; }
        public Especialidad ? Especialidad { get; set; }
    }
}
