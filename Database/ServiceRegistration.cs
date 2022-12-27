using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Infrastructure.Persistence.Contexts;
using EMarket.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Infrastructure.Persistence
{
    //Extension method - decorator pattern
    public static class ServiceRegistration
    {

        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            #region contexts

            if(configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(db => db.UseInMemoryDatabase("TestDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }




            #endregion

            #region repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAdvertRepository, AdvertRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
        }

    }
}
