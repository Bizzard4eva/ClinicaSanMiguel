namespace ClinicaSanMiguel.Models
{
    public class Especialidad
    {
        public int idEspecialidad { get; set; }
        public string especialidad {  set; get; } = string.Empty;
        public int precio { get; set; }

        //Relacion
        public ICollection<Medico> Medicos { get; set; } = new List<Medico>();
    }
}
