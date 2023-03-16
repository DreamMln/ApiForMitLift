using ApiForMitLift.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace ApiForMitLift
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiForMitLift", Version = "v1" });
            });
            // CORS add to restbik
            //vi har lavet en policy
            services.AddCors(options => options.AddPolicy("allowAll",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

            //DBContext tilføjet
            services.AddDbContext<CorolabPraktikDBContext>(opt => opt.UseSqlServer(CorolabPraktikDBContext.Connectionstring));

            //add authentification - Cookie
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options => {
            //        options.Cookie.Name = "Example-cookie";
            //        //life-time, session udløber efter 10 sec
            //        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            //    });
            //Session/cookie
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiForMitLift v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            // CORS allowall
            //This will define the default CORS policy for your REST service.
            //In order to specify individual CORS policies for the individual methods in your controller class, you can add the following tag to the methed:
            //[EnableCors("allowAll")]
            app.UseCors("allowAll");

            app.UseAuthorization();
            //Use the session
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
