using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateCore.WebUI.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTestMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TestMiddleware>();
        }
        public static IApplicationBuilder UseSecurityMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SecurityMiddleware>();
        }
    }
}
