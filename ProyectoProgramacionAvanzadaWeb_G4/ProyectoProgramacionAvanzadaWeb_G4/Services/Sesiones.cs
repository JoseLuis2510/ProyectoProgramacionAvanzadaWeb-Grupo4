using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoProgramacionAvanzadaWeb_G4.Services
{
    public class Sesiones : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("JWT") == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    
    }
}
