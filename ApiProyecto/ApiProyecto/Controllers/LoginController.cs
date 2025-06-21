using ApiProyecto.Models;
using ApiProyecto.Services;
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
        private readonly IUtilitarios _utilitarios;
        public LoginController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _utilitarios = utilitarios;
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
                        autenticacion.Correo,
                        autenticacion.Contrasenna
                    }
                    );
               

                if (resultado !=null )
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Informacion Inválida"));
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
                        autenticacion.Identificacion,
                        autenticacion.Contrasenna,
                        Estado
                    }
                    );
                if(resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Usuario no registrado"));
            }
        }
    }
}
