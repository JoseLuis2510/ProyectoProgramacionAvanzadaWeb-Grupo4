using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProyectoProgramacionAvanzadaWeb_G4.Models;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoProgramacionAvanzadaWeb_G4.Controllers
{
    public class HorarioController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _http;
        public HorarioController(IConfiguration configuration, IHttpClientFactory http)
        {
            _configuration = configuration;
            _http = http;
        }
        [HttpGet]
        public ActionResult CrearHorario()
        {
            using (var context = new SqlConnection(""))
                return View();
        }

        [HttpPost]
        public ActionResult CrearHorario(Horario horario)
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                var resultado = http.PostAsJsonAsync("api/Horario/CrearHorario", horario).Result;

                if (resultado.IsSuccessStatusCode)
                    return RedirectToAction("CrearHorario", "Horario");
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje;
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult VerHorario()
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);

                var response = http.GetAsync("api/Horario/VerHorario").Result;

                if (response.IsSuccessStatusCode)
                {
                    var horarios = response.Content.ReadFromJsonAsync<List<Horario>>().Result;
                    return View(horarios);
                }
                else
                {
                   
                    ViewBag.Mensaje = "No se encontraron horarios.";
                    return View(new List<Horario>()); 
                }
            }
        }
    }
}
