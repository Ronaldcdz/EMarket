using EMarket.Core.Application.ViewModels.Users;
using EMarket.Core.Application.Helpers;
namespace EMarket.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ValidateUserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }   

        public bool HasUser()
        {
            UserViewModel uservm = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            if(uservm == null)
            {
                return false;
            }

            return true;
        }
    }
}
