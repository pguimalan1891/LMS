using System.Web;
using System.Web.Mvc;

namespace LMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

   
    public class AuthorizationFilter : AuthorizeAttribute
    {
        override
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }

            // Check for authorization
            if (HttpContext.Current.Session["loginDetails"] == null)
            {
                filterContext.Result = filterContext.Result = new HttpUnauthorizedResult();
            }

            
        }


    }
}
