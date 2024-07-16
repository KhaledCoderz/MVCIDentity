using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCIDentity.Core.Extensions;
using MVCIDentity.Models;
using System.Diagnostics;

namespace MVCIDentity.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error", Name = "Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {

            1.ToString();
            var ExceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var Error = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = ExceptionHandler.IsNotNullOrEmpty() ? ExceptionHandler?.Error.Message : ""
            };
            return View(Error);
        }
    }
}
