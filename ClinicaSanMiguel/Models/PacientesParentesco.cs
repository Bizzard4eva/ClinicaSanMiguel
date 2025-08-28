namespace ClinicaSanMiguel.Models
{
    public class PacientesParentesco
    {
        public int idPaciente {  get; set; }
        public int idFamiliar { get; set; }
        
        // FK + Relacion
        public int idTipoParentesco { get; set; }
        public Paciente Titular { get; set; } = null!;
        public Paciente Familiar { get; set; } = null!;
        public TipoParentesco TipoParentesco { get; set; } = null!;
    }
}
