using ApiProyecto.Models;

namespace ApiProyecto.Services
{
    public interface IUtilitarios
    {

        RespuestaPredeterminada RespuestaCorrecta(object? contenido);

        RespuestaPredeterminada RespuestaIncorrecta(string mensaje);
    }
}
