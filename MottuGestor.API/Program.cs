using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MottuGestor.Domain.Entities;
using MottuGestor.Infrastructure.Repositories;

namespace GestMottu.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables();

            builder.Services.AddControllers()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.WriteIndented = true;
                });;

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration["Swagger:Title"] ?? "GestMottu API",
                    Description = "API RESTful para gestão de motos com Clean Architecture e DDD",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe MottuGestor",
                        Email = "contato@mottu.com.br"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();
            
            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapGet("/", () => Results.Redirect("/swagger/index.html"));
            app.Run();
        }
    }
}