using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using Pizzaria.Persistence.Context;
using Pizzaria.Application.Contratos;
using Pizzaria.Application;
using Pizzaria.Persistence.Contratos;
using Pizzaria.Persistence.ContratosImpls;
using AutoMapper;

namespace Pizzaria.API
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
            services.AddDbContext<PizzariaContext> (
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );

            services.AddControllers();/*     Se der o Newtonsoft Loop infinito, adicionar o Microsoft.AspNeTCore.Mvc.NewtonsoftJson e adicionar essa linha:
                    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );     */

            services.AddScoped<IPizzaService, PizzaService>();
            services.AddScoped<IGeralPersist, GeralPersistImpl>();
            services.AddScoped<IPizzaPersist, PizzaPersistImpl>(); 

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors(); //falta la em baixo

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pizzaria.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizzaria.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
