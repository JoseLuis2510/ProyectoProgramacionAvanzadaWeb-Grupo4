using System.Data;
using ApiProyecto.Models;
using ApiProyecto.Services;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        public CitaController(IConfiguration configuration, IUtilitarios utilitarios )
        {
            _configuration = configuration;
            _utilitarios = utilitarios;
          

        }

        [HttpPost]
        [Route("CrearCita")]
        [Authorize]
        public ActionResult CrearCita(Cita cita)
        {
            var idUsuarioClaim = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");
            if (idUsuarioClaim == null)
                return Unauthorized(_utilitarios.RespuestaIncorrecta("Token no válido o sin IdUsuario"));

            long IdUsuario = long.Parse(idUsuarioClaim.Value);

            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("RegistrarCita",
                    new
                    {
                        IdUsuario,
                        cita.Descripcion,
                        cita.IdHorario
                    }
                );

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Cita no registrada"));
            }
        }

        [HttpGet]
        [Route("MisCitas")]
        [Authorize]
        public ActionResult MisCitas()
        {
            var idUsuarioClaim = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");
            if (idUsuarioClaim == null)
                return Unauthorized(_utilitarios.RespuestaIncorrecta("Token no válido o sin IdUsuario"));

            long IdUsuario = long.Parse(idUsuarioClaim.Value);

            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var citas = context.Query<Cita>(
                    "ObtenerCitasPorUsuario",
                    new { IdUsuario },
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return Ok(_utilitarios.RespuestaCorrecta(citas));
            }
        }

        [HttpGet]
        [Route("ObtenerTodas")]
        [Authorize]
        public ActionResult ObtenerTodas()
        {
            var idUsuarioClaim = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");
            if (idUsuarioClaim == null)

                return Unauthorized(_utilitarios.RespuestaIncorrecta("Token no válido o sin IdUsuario"));

            if (!long.TryParse(idUsuarioClaim.Value, out long IdUsuario))
                return Unauthorized(_utilitarios.RespuestaIncorrecta("Token no válido o sin IdUsuario"));

            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var citas = context.Query<Cita>(
                    "ObtenerTodasLasCitas",  
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return Ok(_utilitarios.RespuestaCorrecta(citas));
            }
        }





        [HttpPost]
        [Route("Eliminar")]
        [Authorize] 
        public IActionResult Eliminar([FromForm] long consecutivo)
        {

                var idUsuarioClaim = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");
                if (idUsuarioClaim == null)
                    return Unauthorized(_utilitarios.RespuestaIncorrecta("Token no válido o sin IdUsuario"));

            long IdUsuario = long.Parse(idUsuarioClaim.Value);

                using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
                {
                    var resultado = context.Execute("EliminarCita",
                        new
                        {
                            consecutivo,
                            IdUsuario
                        },
                        commandType: CommandType.StoredProcedure);

                    if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                        return NotFound(_utilitarios.RespuestaIncorrecta("No se encontró la cita con ese consecutivo para este usuario"));

                }

        }


        [HttpGet]
        [Route("ObtenerCitasPacientes")]
        [Authorize]
        public ActionResult ObtenerCitasPacientes()
        {
            var idUsuarioClaim = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");
            if (idUsuarioClaim == null)

                return Unauthorized(_utilitarios.RespuestaIncorrecta("Token no válido o sin IdUsuario"));

            if (!long.TryParse(idUsuarioClaim.Value, out long IdUsuario))
                return Unauthorized(_utilitarios.RespuestaIncorrecta("Token no válido o sin IdUsuario"));

            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var citas = context.Query<Cita>(
                    "ObtenerTodasLasCitas",
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return Ok(_utilitarios.RespuestaCorrecta(citas));
            }
        }
        [HttpPost]
        [Route("AtenderCita")]
        [Authorize]
        public IActionResult AtenderCita([FromForm] long consecutivo)
        {

            var idUsuarioClaim = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");
            if (idUsuarioClaim == null)
                return Unauthorized(_utilitarios.RespuestaIncorrecta("Token no válido o sin IdUsuario"));

            long IdUsuario = long.Parse(idUsuarioClaim.Value);

            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Execute("AtenderCita",
                    new
                    {
                        consecutivo
                    },
                    commandType: CommandType.StoredProcedure);

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return NotFound(_utilitarios.RespuestaIncorrecta("No se encontró la cita con ese consecutivo para este usuario"));

            }

        }


    }

    




    }

