using System.Text;
using ApiProyecto.Models;
using ApiProyecto.Services;
using Dapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IHostEnvironment _environment;
        private readonly IUtilitarios _utilitarios;

        public LoginController(IConfiguration configuration, IHostEnvironment environment, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _environment = environment;
            _utilitarios = utilitarios;
        }

        [HttpPost]
        [Route("Index")]
        [AllowAnonymous]
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


                if (resultado != null)
                {
                    resultado.Token = _utilitarios.GenerarToken(resultado.IdUsuario);
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                }
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue validada"));
            }
        }

        [HttpPost]
        [Route("Registro")]
        [AllowAnonymous]
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

        [HttpPost]
        [Route("RecuperacionContrasena")]
        [AllowAnonymous]
        public IActionResult RecuperacionContrasena(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.QueryFirstOrDefault<Autenticacion>("ValidarCorreo",
                    new { autenticacion.Correo });

                if (resultado != null)
                {
                    var ContrasennaNotificar = _utilitarios.GenerarContrasenna(10);
                    var Contrasenna = _utilitarios.Encrypt(ContrasennaNotificar);

                    var resultadoActualizacion = context.Execute("ActualizarContrasenna",
                        new
                        {
                            resultado.IdUsuario,
                            Contrasenna
                        });

                    if (resultadoActualizacion > 0)
                    {
                        var ruta = Path.Combine(_environment.ContentRootPath, "VisualizacionCorreo.html");
                        var html = System.IO.File.ReadAllText(ruta, UTF8Encoding.UTF8);

                        html = html.Replace("@@Usuario", resultado.Nombre);
                        html = html.Replace("@@Contrasenna", ContrasennaNotificar);
                        _utilitarios.EnviarCorreo(resultado.Correo!, "Recuperación de Acceso", html);
                        return Ok(_utilitarios.RespuestaCorrecta(null));
                    }
                }

                return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue validada"));
            }
        }
    }
}
