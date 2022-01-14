using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ApiControllers.Models;
using Microsoft.Extensions.Hosting;

namespace ApiControllers
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:8080", "http://172.18.53.209", "http://24.23.95.208", "http://192.168.1.4:8080/", "https://annetteprojects.z13.web.core.windows.net")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });

            services.AddSingleton<IRepository, MemoryRepository>();
            services.AddHttpClient();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction())
            {
                //app.UseExceptionHandler("/Error");
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}
