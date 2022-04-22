using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace MvcTrabajoMaster.Filters
{
    public class AuthorizeJugadoresAttribute : AuthorizeAttribute,
          IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                RouteValueDictionary routeLogin =
                new RouteValueDictionary(new
                {
                    controller = "Managed",
                    action = "LogIn"
                });
                RedirectToRouteResult result =
                new RedirectToRouteResult(routeLogin);
                context.Result = result;
            }
        }
    }

}