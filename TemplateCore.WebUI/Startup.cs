using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using TemplateCore.BL.Repositories;
using TemplateCore.DAL.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TemplateCore.WebUI
{
    public class Startup
    {
        IConfiguration configuration;
        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<WebContext>(op => op.UseSqlServer(configuration.GetConnectionString("WebCS")));
            services.AddScoped(typeof(WebRepository<>));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt => {
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                opt.LoginPath = "/admin/login";
                opt.LogoutPath = "/admin/logout";
                opt.AccessDeniedPath = "/admin/login?access=denied";
            });
            services.AddSingleton(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA }));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseStatusCodePagesWithReExecute("/hata", "?sCode={0}");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
