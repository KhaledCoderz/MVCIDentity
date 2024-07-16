using MVCIDentity.Models;

namespace MVCIDentity.Helper.User
{
    public static  class ContextWrapper
    {
        private static Identity CurrentUser;

        public static Identity GetCurrentUser()
        {
            CurrentUser = new Identity();

            var user = new HttpContextAccessor();
            if (user.HttpContext.User.Claims.Any())
            {
                CurrentUser.FirstName = user.HttpContext.User.FindFirst("FirstName").Value;
                CurrentUser.LastName = user.HttpContext.User.FindFirst("LastName").Value;
                CurrentUser.Id = user.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            }

            return CurrentUser; 
        }
        public static bool IsSignedIn()
        {
            try
            {
                var user = new HttpContextAccessor();
                return user.HttpContext.User.Claims.Any();
            }
            catch (Exception)
            {
                return false;   
            }
        
        }
    }
}
