using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Core.Domain.Entities;
using EMarket.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationContext _dbContext;

        public CategoryRepository (ApplicationContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
