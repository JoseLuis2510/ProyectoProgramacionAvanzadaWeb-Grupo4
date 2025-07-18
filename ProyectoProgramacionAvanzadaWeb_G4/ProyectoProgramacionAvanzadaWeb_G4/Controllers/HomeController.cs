using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoProgramacionAvanzadaWeb_G4.Models;
using ProyectoProgramacionAvanzadaWeb_G4.Services;

namespace ProyectoProgramacionAvanzadaWeb_G4.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Sesiones]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
