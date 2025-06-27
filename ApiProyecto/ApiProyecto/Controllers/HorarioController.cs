using System.Data;
using ApiProyecto.Models;
using ApiProyecto.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        public HorarioController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _utilitarios = utilitarios;
        }

        [HttpPost]
        [Route("CrearHorario")]
        public ActionResult CrearHorario(Horario horario)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var Estado = true;


                var resultado = context.Execute("RegistrarHorario",
                    new
                    {
                        horario.HoraFecha,
                        horario.Observacion,
                        Estado
                    }
                    );
                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(null));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Horario no registrado"));
            }
        }

        [HttpGet]
        [Route("VerHorario")]
        public ActionResult VerHorario()
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var resultado = context.Query<Horario>("VerHorario",
                    commandType: CommandType.StoredProcedure).ToList();

                if (resultado != null && resultado.Count > 0)
                    return Ok(resultado);  
                else
                    return NotFound("No hay horarios");
            }
        }

    }
}
