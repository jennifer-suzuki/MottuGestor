using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MottuGestor.Application.Interfaces;
using MottuGestor.Application.Services;
using MottuGestor.Domain.Entities;
using MottuGestor.Infrastructure.Data;
using MottuGestor.Infrastructure.Repositories;

namespace GestMottu.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var config = builder.Configuration;
                var connString = config.GetConnectionString("MongoDb") ?? "mongodb://localhost:27017";
                return new MongoClient(connString);
            });

            builder.Services.AddSingleton<MongoDbContext>(sp =>
            {
                var config = builder.Configuration;
                var client = sp.GetRequiredService<IMongoClient>();
                var dbName = "MottuGestor";
                return new MongoDbContext(client, dbName);
            });
            
            builder.Services.AddScoped<IMotoRepository, MotoRepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IPatioRepository, PatioRepository>();
            builder.Services.AddScoped<IMotoService, MotoService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IPatioService, PatioService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Challenge - Aplicativo de Gestão para Mottu",
                    Version = "v1",
                    Description = "Uma aplicação para Mottu que faz a gestão das motos, pátios e usuários.",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe MottuGestor",
                        Email = "mottugestor@gmail.com"
                    }
                });

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Challenge - Aplicativo de Gestão para Mottu",
                    Version = "v2",
                    Description = "MottuGestor - versão 2.",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe MottuGestor",
                        Email = "mottugestor@gmail.com"
                    }
                });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization utilizando Bearer. Exemplo: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);
            });
            
            builder.Services.AddHealthChecks()
                .AddMongoDb(
                    sp => sp.GetRequiredService<IMongoClient>(),
                    name: "mongodb",
                    timeout: TimeSpan.FromSeconds(3),
                    tags: new[] { "db", "mongo" }
                );
            
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "API MottuGestor v1");
                    opt.SwaggerEndpoint("/swagger/v2/swagger.json", "API MottuGestor v2");
                    opt.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health");

            app.Run();
        }
    }
}