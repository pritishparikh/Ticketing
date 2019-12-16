using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Filters
{
    public class Authorization : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            string recivedtoken = filterContext.HttpContext.Request.Headers["token"];

            //if (!ValidateToken(filterContext.HttpContext.Request.Headers["token"]))
            //{
            //    filterContext.Result = new UnauthorizedResult();
            //}
        }
    }
}
