using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProyectoProgramacionAvanzadaWeb_G4.Models;
using ProyectoProgramacionAvanzadaWeb_G4.Services;
using System.Text;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoProgramacionAvanzadaWeb_G4.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        private readonly IHttpClientFactory _http;
        public LoginController(IConfiguration configuration, IUtilitarios utilitarios, IHttpClientFactory http)
        {
            _configuration = configuration;
            _utilitarios = utilitarios;
            _http = http;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Autenticacion autenticacion)
      
        {
            autenticacion.Contrasenna = _utilitarios.Encrypt(autenticacion.Contrasenna!);
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                var resultado = http.PostAsJsonAsync("api/Login/Index", autenticacion).Result;
                
                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada<Autenticacion>>().Result;
                    HttpContext.Session.SetString("Nombre", datos?.Contenido?.Nombre!);
                    HttpContext.Session.SetString("IdUsuario", datos?.Contenido?.IdUsuario.ToString());
                    HttpContext.Session.SetString("NombreRol", datos?.Contenido?.Descripcion);
                    HttpContext.Session.SetString("IdRol", datos?.Contenido?.IdRol.ToString());
                    HttpContext.Session.SetString("JWT", datos?.Contenido?.Token!);
                    HttpContext.Session.SetString("TieneCita", datos?.Contenido?.TieneCita.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje;
                    return View();
                };
            }
        }

        [HttpGet]
        public ActionResult Registro()
        {
            using (var context = new SqlConnection(""))
                return View();
        }

        [HttpPost]
        public ActionResult Registro(Autenticacion autenticacion)
        {
            autenticacion.Contrasenna = _utilitarios.Encrypt(autenticacion.Contrasenna!);

            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                var resultado = http.PostAsJsonAsync("api/Login/Registro", autenticacion).Result;

                if (resultado.IsSuccessStatusCode)
                    return RedirectToAction("Index", "Login");
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje;
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult RecuperacionContrasena()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperacionContrasena(Autenticacion autenticacion)
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                var resultado = http.PostAsJsonAsync("api/Login/RecuperacionContrasena", autenticacion).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<RespuestaPredeterminada>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje;
                    return View();
                }
            }
        }
        [Sesiones]
        [HttpGet]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
