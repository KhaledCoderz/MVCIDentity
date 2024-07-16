using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCIDentity.Helper.User;
using MVCIDentity.Models;
using System.Diagnostics;

namespace MVCIDentity.Controllers.Base
{
    public class BaseController : Controller
    {
        public Identity CurrentUser { get; set; }
        public BaseController()
        {
            CurrentUser = ContextWrapper.GetCurrentUser();
        }

        
    }
}
