using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProyectoProgramacionAvanzadaWeb_G4.Models;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoProgramacionAvanzadaWeb_G4.Controllers
{
    public class CitaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _http;

        public CitaController(IConfiguration configuration, IHttpClientFactory http)
        {
            _configuration = configuration;
            _http = http;
        }


        [HttpGet]
        public IActionResult CrearCita()
        {
            using (var httpClient = _http.CreateClient())
            {
                httpClient.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);

                var response = httpClient.GetAsync("api/Horario/VerHorario").Result;

                if (response.IsSuccessStatusCode)
                {
                    var horarios = response.Content.ReadFromJsonAsync<List<Horario>>().Result;

                    ViewBag.Horario = horarios.Select(h => new SelectListItem
                    {
                        Value = h.IdHorario.ToString(),
                        Text = h.HoraFecha.ToString("g")
                    }).ToList();
                }
                else
                {
                    // Aquí manejas errores como 404 o 500
                    ViewBag.Horario = new List<SelectListItem>();
                    ViewBag.Mensaje = "No se encontraron horarios disponibles.";
                }

                return View();
            }
        }

        [HttpPost]
        public IActionResult CrearCita(Cita cita)
        {


            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                var resultado = http.PostAsJsonAsync("api/Cita/CrearCita", cita).Result;

                if (resultado.IsSuccessStatusCode)
                    return RedirectToAction("CrearCita", "Cita");
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje;
                    return View();
                }
            }
        }


    }
}
