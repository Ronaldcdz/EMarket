using EMarket.Core.Application.Helpers;
using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Core.Application.ViewModels.Users;
using EMarket.Core.Domain.Entities;
using EMarket.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);
            return await base.AddAsync(entity);
        }

        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypty = PasswordEncryption.ComputeSha256Hash(loginVm.Password);
            User user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.Username == loginVm.Username && user.Password == passwordEncrypty);
            return user;
        }
    }
}
