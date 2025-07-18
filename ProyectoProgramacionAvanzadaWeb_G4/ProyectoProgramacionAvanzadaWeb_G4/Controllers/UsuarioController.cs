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
    }
}
