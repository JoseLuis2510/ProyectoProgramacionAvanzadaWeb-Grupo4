using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProyectoProgramacionAvanzadaWeb_G4.Models;
using static System.Net.WebRequestMethods;

namespace ProyectoProgramacionAvanzadaWeb_G4.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _http;
        public LoginController(IConfiguration configuration, IHttpClientFactory http)
        {
            _configuration = configuration;
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
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                var resultado = http.PostAsJsonAsync("api/Login/Index", autenticacion).Result;

                if (resultado.IsSuccessStatusCode)
                    return RedirectToAction("Index", "Home");

                ViewBag.Mensaje = "No se pudo autenticar";
                return View();
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
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiUrl").Value!);
                var resultado = http.PostAsJsonAsync("api/Login/Registro", autenticacion).Result;

                if (resultado.IsSuccessStatusCode)
                    return RedirectToAction("Index", "Login");

                ViewBag.Mensaje = "No se pudo registrar";
                return View();
            }
        }

        public ActionResult RecuperacionContrasena()
        {
            return View();
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
