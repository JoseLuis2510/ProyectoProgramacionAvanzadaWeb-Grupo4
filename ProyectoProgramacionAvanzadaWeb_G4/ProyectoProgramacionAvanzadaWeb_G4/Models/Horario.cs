namespace ProyectoProgramacionAvanzadaWeb_G4.Models
{
    public class Horario
    {
        public long IdHorario { get; set; }
        public DateTime HoraFecha { get; set; }
        public string? Observacion { get; set; }
        public bool? Estado { get; set; }
    }
}
