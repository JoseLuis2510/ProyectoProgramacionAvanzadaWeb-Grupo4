using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoProgramacionAvanzadaWeb_G4.Models;
using ProyectoProgramacionAvanzadaWeb_G4.Services;

namespace ProyectoProgramacionAvanzadaWeb_G4.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Sesiones]
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        private readonly IHttpClientFactory _http;
        public UsuarioController(IConfiguration configuration, IUtilitarios utilitarios, IHttpClientFactory http)
        {
            _configuration = configuration;
            _utilitarios = utilitarios;
            _http = http;
        }

        [HttpGet]
        public IActionResult VerPerfil()
        {
            using (var http = _http.CreateClient())
            {
                var IdUsuario = HttpContext.Session.GetString("IdUsuario");
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);

                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.GetAsync("api/Usuario/ConsultarUsuario?IdUsuario=" + IdUsuario).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada<Autenticacion>>().Result;
                    return View(datos?.Contenido!);
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje;
                    return View();
                }
            }
        }

        [HttpPost]
        public IActionResult ActualizarPerfil(Autenticacion autenticacion)
        {
            using (var http = _http.CreateClient())
            {
                var IdUsuario = HttpContext.Session.GetString("IdUsuario");
                autenticacion.IdUsuario = long.Parse(IdUsuario!);

                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.PutAsJsonAsync("api/Usuario/ActualizarUsuario", autenticacion).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetString("Nombre", autenticacion.Nombre!);
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
        public IActionResult CambiarContrasenna()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CambiarContrasenna(Autenticacion autenticacion)
        {
            if (autenticacion.Contrasenna != autenticacion.ConfirmarContrasenna)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden.";
                return View();
            }

            autenticacion.Contrasenna = _utilitarios.Encrypt(autenticacion.Contrasenna!);

            using (var http = _http.CreateClient())
            {
                var IdUsuario = HttpContext.Session.GetString("IdUsuario");
                autenticacion.IdUsuario = long.Parse(IdUsuario!);

                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.PutAsJsonAsync("api/Usuario/CambiarContrasenna", autenticacion).Result;

                if (resultado.IsSuccessStatusCode)
                {
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
        public IActionResult GraficoUsuarios()
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

            var resultado = http.GetAsync("api/Usuario/ObtenerTotalUsuarios").Result;

            if (resultado.IsSuccessStatusCode)
            {
                var contenido = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada<int>>().Result;
                ViewBag.TotalUsuarios = contenido.Contenido;
                return View();
            }
            else
            {
                var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                ViewBag.Mensaje = respuesta?.Mensaje;
                ViewBag.TotalUsuarios = 0;
                return View();
            }
        }

    }
}
