using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateCore.WebUI.Middlewares
{
    public class TestMiddleware
    {
        RequestDelegate next;
        public TestMiddleware(RequestDelegate _next)
        {
            next = _next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Query["deneme"] == "test") context.Response.Redirect("/sepetim");
            else await next(context);
        }
    }
}
