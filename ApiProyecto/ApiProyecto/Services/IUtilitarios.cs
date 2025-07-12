using ApiProyecto.Models;

namespace ApiProyecto.Services
{
    public interface IUtilitarios
    {

        RespuestaPredeterminada RespuestaCorrecta(object? contenido);

        RespuestaPredeterminada RespuestaIncorrecta(string mensaje);

        string GenerarContrasenna(int longitud);

        void EnviarCorreo(string destinatario, string asunto, string cuerpo);

        string Encrypt(string texto);

        string GenerarToken(long IdUsuario);
    }
}
