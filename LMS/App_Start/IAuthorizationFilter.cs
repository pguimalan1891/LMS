using System.Web.Mvc;

namespace LMS
{
    public interface IAuthorizationFilter
    {
        void OnActionExecuting(ActionExecutingContext filterContext);
        void OnAuthorization(AuthorizationContext filterContext);
    }
}