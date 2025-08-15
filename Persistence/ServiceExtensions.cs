using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repository;

namespace Persistence
{
   public static class ServiceExtensions
    {
        //matricular servicios
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                //corre los migrations
                b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)
                ));

            #region Repositories
            //se matricula el patron repositorio
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            #endregion

            #region Caching
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration =configuration.GetValue<string>("Caching:RedisConnection");
            });
            #endregion
        }
    }
}
