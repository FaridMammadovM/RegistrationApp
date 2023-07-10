using AddressBook.Api.Infrastructure.ExternalServices.AuthenticationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AddressBook.Api.Infrastructure.Filters
{
    public class CheckAccess : ActionFilterAttribute
    {
        private readonly string _connectionString = new ConfigurationBuilder().AddJsonFile($@"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json").Build().GetSection("ConnectionStrings")["AppCon"];
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (SkipAuthorization(actionContext)) return;

            if (string.IsNullOrEmpty(actionContext.HttpContext.Request.Headers["Authorization"]))
            {
                actionContext.Result = new StatusCodeResult(401);
            }
            else
            {
                var token = actionContext.HttpContext.Request.Headers["Authorization"];
                AuthenticationServices service = new AuthenticationServices();
                var userId = service.GetUserInfo(token);
                if (string.IsNullOrEmpty(userId))
                {
                    actionContext.Result = new StatusCodeResult(401);
                }
            }
        }
        private bool SkipAuthorization(ActionExecutingContext actionContext)
        {
            var controllerActionDescriptor = actionContext.ActionDescriptor as ControllerActionDescriptor;
            return controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                    .Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
        }


    }

}
