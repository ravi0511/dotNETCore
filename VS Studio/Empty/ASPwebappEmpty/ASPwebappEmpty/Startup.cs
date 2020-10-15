using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPwebappEmpty.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASPwebappEmpty
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContent>(option =>
            {
                option.UseSqlServer(_config.GetConnectionString("EmployeeDBString"));
            });
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContent>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 3;
                //options.Password.RequireDigit = false;
                //options.Password.RequireLowercase = false;
                //options.Password.RequireNonAlphanumeric = false;
            });
            services.AddMvc(option => option.EnableEndpointRouting = false).AddJsonOptions(e => e.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddScoped<IEmployeeInterface, SQLEmployeeRepository>();
            // services.AddTransient<IEmployeeInterface, IEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync(_config["MyKey"]);
            //    });
            //});
            //FileServerOptions fileop = new FileServerOptions();
            //fileop.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileop.DefaultFilesOptions.DefaultFileNames.Add("pageone.html");
            app.UseStaticFiles();
            app.UseAuthentication();


            //app.UseMvcWithDefaultRoute();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller}/{action}/{id}");
            //});

            app.UseRouting();
            app.UseEndpoints(endPoint =>
            {
                //endPoint.MapControllerRoute("default", "{controller=Home}/{action=Index}/{?id}");
                endPoint.MapDefaultControllerRoute();
            });


            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World");
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World");
            //});
        }
    }
}
