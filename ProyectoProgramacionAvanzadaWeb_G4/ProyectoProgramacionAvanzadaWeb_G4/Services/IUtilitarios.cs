using System.Security.Claims;

namespace ProyectoProgramacionAvanzadaWeb_G4.Services
{
    public interface IUtilitarios
    {

            string Encrypt(string texto);
            long ObtenerIdUsuario(IEnumerable<Claim> token);



    }
}
