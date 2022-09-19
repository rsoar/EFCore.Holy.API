using EFCore.Holy.Business;
using EFCore.Holy.Data.Config;
using EFCore.Holy.Data.Context;
using EFCore.Holy.Data.Interfaces;
using EFCore.Holy.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EFCore.Holy.API.Extensions
{
    public static class DataExtensions
    {
        public static WebApplicationBuilder AddDbContext(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DatabaseContext>(o =>
            {
                o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });

            return builder;
        }
        public static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
            builder.Services.AddScoped<IChurchRepository, ChurchRepository>();
            builder.Services.AddScoped<IChurchBusiness, ChurchBusiness>();

            return builder;
        }
        public static WebApplicationBuilder AddAuthenticate(WebApplicationBuilder builder)
        {
            byte[] key = Encoding.ASCII.GetBytes(TokenSettings.Secret);

            builder.Services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.RequireHttpsMetadata = false;
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false, // oauth2
                        ValidateAudience = false, // oauth2
                    };

                    jwtOptions.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("Token inválido" + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("Token válido" + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                });

            return builder;
        }
    }
}