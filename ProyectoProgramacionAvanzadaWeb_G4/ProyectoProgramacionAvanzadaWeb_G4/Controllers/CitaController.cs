using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
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

                    ViewBag.IdHorario = horarios.Select(h => new SelectListItem
                    {
                        Value = h.IdHorario.ToString(),
                        Text = h.HoraFecha.ToString("g")
                    }).ToList();
                }
                else
                {
                    
                    ViewBag.IdHorario = new List<SelectListItem>();
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

                var token = HttpContext.Session.GetString("JWT");
                if (!string.IsNullOrEmpty(token))
                {
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                else
                {
                    ViewBag.Mensaje = "No hay token JWT en sesión";
                    return View();
                }

                var resultado = http.PostAsJsonAsync("api/Cita/CrearCita", cita).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetString("TieneCita", "true");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje;
                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult MisCitas()
        {
            using var http = _http.CreateClient();
            http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);

            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("No se encontró token JWT en sesión.");
            }

    
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var resultado = http.GetAsync("api/Cita/MisCitas").Result;

            if (resultado.IsSuccessStatusCode)
            {
                var contenido = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada<List<Cita>>>().Result;
                return View(contenido.Contenido);
            }
            else
            {
                var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                ViewBag.Mensaje = respuesta?.Mensaje;
                return View(new List<Cita>());
            }

            
        }

        [HttpGet]
        public IActionResult ObtenerCitas()
        {
            using var http = _http.CreateClient();
            http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);

            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("No se encontró token JWT en sesión.");
            }

            
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);



            var resultado = http.GetAsync("api/Cita/ObtenerTodas").Result; 

            if (resultado.IsSuccessStatusCode)
            {
                var contenido = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada<List<Cita>>>().Result;
                return View(contenido.Contenido);
            }
            else
            {
                var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                ViewBag.Mensaje = respuesta?.Mensaje;
                return View(new List<Cita>());
            }
        }


        [HttpGet]
        public IActionResult CitaAgendada()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Eliminar(long consecutivo)
        {
            Console.WriteLine($"Consecutivo recibido en Web: {consecutivo}");

            using var http = _http.CreateClient();
            http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);


            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("No se encontró token JWT en sesión.");
            }

           
            http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            
            var content = new FormUrlEncodedContent(new[]
            {new KeyValuePair<string, string>("consecutivo", consecutivo.ToString())});

            
            var resultado = http.PostAsync("api/Cita/Eliminar", content).Result;

            if (resultado.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("TieneCita", "false");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                
                var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                ViewBag.Mensaje = respuesta?.Mensaje;
                return RedirectToAction("MisCitas", "Cita");
            }
        }

        [HttpGet]
        public IActionResult ObtenerCitasTotales()
        {
            using var http = _http.CreateClient();
            http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);

            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("No se encontró token JWT en sesión.");
            }


            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);



            var resultado = http.GetAsync("api/Cita/ObtenerCitasPacientes").Result; 

            if (resultado.IsSuccessStatusCode)
            {
                var contenido = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada<List<Cita>>>().Result;
                return View(contenido.Contenido);
            }
            else
            {
                var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                ViewBag.Mensaje = respuesta?.Mensaje;
                return View(new List<Cita>());
            }
        }

        [HttpPost]
        public IActionResult AtenderCita(long consecutivo)
        {
            Console.WriteLine($"Consecutivo recibido en Web: {consecutivo}");

            using var http = _http.CreateClient();
            http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);


            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("No se encontró token JWT en sesión.");
            }


            http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            var content = new FormUrlEncodedContent(new[]
            {new KeyValuePair<string, string>("consecutivo", consecutivo.ToString())});


            var resultado = http.PostAsync("api/Cita/AtenderCita", content).Result;

            if (resultado.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("TieneCita", "false");
                return RedirectToAction("Index", "Home");
            }
            else
            {

                var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                ViewBag.Mensaje = respuesta?.Mensaje;
                return RedirectToAction("MisCitas", "Cita");
            }
        }
        [HttpGet]
        public IActionResult GraficoCitas()
        {
            using var http = _http.CreateClient();
            http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);

            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("No se encontró token JWT en sesión.");
            }

            http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var resultado = http.GetAsync("api/Cita/ObtenerTotalCitas").Result;

            if (resultado.IsSuccessStatusCode)
            {
                var contenido = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada<int>>().Result;
                ViewBag.TotalCitas = contenido.Contenido;
                return View();
            }
            else
            {
                var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                ViewBag.Mensaje = respuesta?.Mensaje;
                ViewBag.TotalCitas = 0;
                return View();
            }
        }


        [HttpGet]
        public IActionResult MisCitasCalendario()
        {
            return View("CalendarioWebUsuario");  
        }

        [HttpGet]
        public IActionResult CalendarioWebUsuario()
        {
            using var http = _http.CreateClient();
            http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);

            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var resultado = http.GetAsync("api/Cita/MisCitasCalendario").Result;

            if (resultado.IsSuccessStatusCode)
            {
                var contenido = resultado.Content.ReadFromJsonAsync<List<object>>().Result;
                return Json(contenido);
            }
            else
            {
                return Json(new List<object>());
            }
        }



        [HttpGet]
        public IActionResult TodasCitasCalendario()
        {
            return View("CalendarioWebAdministrador");  
        }

        [HttpGet]
        public IActionResult CalendarioWebAdministrador()
        {
            using var http = _http.CreateClient();
            http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);

            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var resultado = http.GetAsync("api/Cita/TodasCitasCalendario").Result;

            if (resultado.IsSuccessStatusCode)
            {
                var contenido = resultado.Content.ReadFromJsonAsync<List<object>>().Result;
                return Json(contenido);
            }
            else
            {
                return Json(new List<object>());
            }
        }





    }


}



