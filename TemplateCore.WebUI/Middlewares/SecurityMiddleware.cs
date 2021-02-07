using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateCore.WebUI.Middlewares
{
    public class SecurityMiddleware
    {
        RequestDelegate next;
        public SecurityMiddleware(RequestDelegate _next)
        {
            next = _next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated) context.Response.Redirect("/login");
            else await next(context);
        }
    }
}
