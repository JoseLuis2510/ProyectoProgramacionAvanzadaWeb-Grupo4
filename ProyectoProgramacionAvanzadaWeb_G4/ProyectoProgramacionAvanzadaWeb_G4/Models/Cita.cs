using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb_G4.Models
{
    public class Cita
    {

        public long IdUsuario { get; set; }
        public string? Descripcion { get; set; }
        public long IdHorario { get; set; }

    }
}
