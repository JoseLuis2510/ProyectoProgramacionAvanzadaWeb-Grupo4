using ApiProyecto.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Index")]
        public ActionResult Index(Autenticacion autenticacion)

        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.QueryFirstOrDefault<Autenticacion>("IniciarSesion",
                    new
                    {
                        autenticacion.NombreUsuario,
                        autenticacion.Contrasenna
                    }
                    );
               

                if (resultado != null)
                    return Ok();
                else
                    return NotFound();
            }
        }

        [HttpPost]
        [Route("Registro")]
        public ActionResult Registro(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var Estado = true;
                

                var resultado = context.Execute("RegistrarUsuario",
                    new
                    {
                        autenticacion.Nombre,
                        autenticacion.Correo,
                        autenticacion.NombreUsuario,
                        autenticacion.Contrasenna,
                        Estado
                    }
                    );
                if (resultado > 0)
                    return Ok();
                else
                    return BadRequest();
            }
        }
    }
}
