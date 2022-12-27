using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Core.Domain.Entities;
using EMarket.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EMarket.Infrastructure.Persistence.Repositories
{
    public class AdvertRepository : GenericRepository<Advert>, IAdvertRepository
    {
        private readonly ApplicationContext _dbContext;

        public AdvertRepository (ApplicationContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }

      


    }
}
