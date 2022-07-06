using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Helper;
using RedSocial.Core.Application.ViewModels.User;


namespace WebApp.RedSocial.Middlewares
{
    public class ValidSession
    {
        private readonly IHttpContextAccessor _context;

        public ValidSession(IHttpContextAccessor context)
        {
            _context = context;
        }
        public bool HaveUser()
        {
            UserVM user = _context.HttpContext.Session.Get<UserVM>("user");

            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}
