namespace ApiProyecto.Models
{
    public class Cita
    {
        public long Consecutivo { get; set; }

        public string? Nombre { get; set; }
        public long IdUsuario { get; set; }
        public string? Descripcion { get; set; }
        public long IdHorario { get; set; }

        public DateTime HoraFecha { get; set; }
    }
}
