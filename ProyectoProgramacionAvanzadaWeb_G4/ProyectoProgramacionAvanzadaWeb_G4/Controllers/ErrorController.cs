using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoProgramacionAvanzadaWeb_G4.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult CapturarError()
        {
            HttpContext.Features.Get<IExceptionHandlerFeature>();

            return View("Error");
        }
    }
}
