using ApiProyecto.Models;

namespace ApiProyecto.Services
{
    public class Utilitarios : IUtilitarios
    {
        public RespuestaPredeterminada RespuestaCorrecta(object? contenido)
        {
            return new RespuestaPredeterminada
            {
                Codigo = 1,
                Mensaje = "Operación exitosa",
                Contenido = contenido
            };
        }

        public RespuestaPredeterminada RespuestaIncorrecta(string mensaje)
        {
            return new RespuestaPredeterminada
            {
                Codigo = 0,
                Mensaje = mensaje,
                Contenido = null
            };
        }
    }
}
