using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region services

            services.AddTransient<IAdvertService, AdvertService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();

            #endregion
        }
    }
}
