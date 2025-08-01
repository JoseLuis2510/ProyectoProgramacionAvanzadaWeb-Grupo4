using System.Data;
using ApiProyecto.Models;
using ApiProyecto.Services;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ApiProyecto.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        public UsuarioController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _utilitarios = utilitarios;
        }

        [HttpGet]
        [Route("ConsultarUsuario")]
        public IActionResult ConsultarUsuario(long IdUsuario)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.QueryFirstOrDefault<Autenticacion>("ConsultarUsuario",
                    new
                    {
                        IdUsuario
                    });

                if (resultado != null)
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue validada"));
            }
        }

        [HttpPut]
        [Route("ActualizarUsuario")]
        public IActionResult ActualizarUsuario(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("ActualizarUsuario",
                    new
                    {
                        autenticacion.Identificacion,
                        autenticacion.Nombre,
                        autenticacion.Correo,
                        autenticacion.IdUsuario
                    });

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue actualizada"));
            }
        }


        [HttpPut]
        [Route("CambiarContrasenna")]
        public IActionResult CambiarContrasenna(Autenticacion autenticacion)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("ActualizarContrasenna",
                    new
                    {
                        autenticacion.IdUsuario,
                        autenticacion.Contrasenna
                    });

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue actualizada"));
            }
        }

        [HttpGet]
        [Route("ObtenerTotalUsuarios")]
        [Authorize]
        public ActionResult ObtenerTotalUsuarios()
        {
            var idUsuarioClaim = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");
            if (idUsuarioClaim == null)
                return Unauthorized(_utilitarios.RespuestaIncorrecta("Token no válido o sin IdUsuario"));

            if (!long.TryParse(idUsuarioClaim.Value, out long IdUsuario))
                return Unauthorized(_utilitarios.RespuestaIncorrecta("Token no válido o sin IdUsuario"));

            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var total = context.ExecuteScalar<int>(
                    "ObtenerTotalUsuariosGrafico",  
                    commandType: CommandType.StoredProcedure
                );

                return Ok(_utilitarios.RespuestaCorrecta(total));
            }
        }


    }
}