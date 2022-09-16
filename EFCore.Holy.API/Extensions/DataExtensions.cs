using EFCore.Holy.Business;
using EFCore.Holy.Data.Context;
using EFCore.Holy.Data.Interfaces;
using EFCore.Holy.Data.Repository;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddScoped(typeof(IChurchBusiness), typeof(ChurchBusiness));
            builder.Services.AddScoped(typeof(IChurchRepository), typeof(ChurchRepository));

            return builder;
        }
    }
}
