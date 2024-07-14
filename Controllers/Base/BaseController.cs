using Microsoft.AspNetCore.Mvc;

namespace MVCIDentity.Controllers.Base
{
    public class BaseController : Controller
    {
        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public BaseController() {
            var user = new HttpContextAccessor();
            FirstName = user.HttpContext.User.FindFirst("FirstName").Value;
            LastName = user.HttpContext.User.FindFirst("LastName").Value;
            Id = user.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }
    }
}
