using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NumerosPrimosAPI.Src.Contexto;
using NumerosPrimosAPI.Src.Repositorio;
using NumerosPrimosAPI.Src.Repositorio.Implemtacoes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NumerosPrimosAPI
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
            services.AddDbContext<NumeroContexto>(opt =>
            opt.UseSqlServer(Configuration["ConnectionStringsDev:DefaultConnection"]));

            services.AddScoped<INumeroRepositorio, NumeroRepositorio>();

            services.AddCors();
            services.AddControllers();

            services.AddSwaggerGen(
            s =>
             {
                 s.SwaggerDoc("v1", new OpenApiInfo
                 {
                     Title = "NumerosPrimos",
                     Version = "v1"
                 });

                 s.AddSecurityRequirement(
                     new OpenApiSecurityRequirement
                     {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new List<string>()
                            }
                     }
                 );

                 var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                 var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                 s.IncludeXmlComments(xmlPath);
             }
        );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, NumeroContexto contexto)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NumerosPrimos");
                    c.RoutePrefix = string.Empty;
                });
            }

            contexto.Database.EnsureCreated();

            app.UseRouting();

            app.UseCors(c => c
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
