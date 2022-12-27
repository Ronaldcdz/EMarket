using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.ViewModels.Adverts;
using EMarket.Core.Application.ViewModels.Users;
using EMarket.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Login(LoginViewModel loginVm)
        {
            User user = await _userRepository.LoginAsync(loginVm);

            if(user == null)
            {
                return null;
            }

            UserViewModel userVm = new();
            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.Password = user.Password;
            userVm.Email = user.Email;
            userVm.Phone = user.Phone;

            return userVm;
        }

        public async Task<SaveUserViewModel> AddAsync(SaveUserViewModel vm)
        {
            User user= new();
            user.Username = vm.Username;
            user.Name = vm.Name;
            user.Password = vm.Password;
            user.Email = vm.Email;
            user.Phone = vm.Phone;

            user = await _userRepository.AddAsync(user);

            SaveUserViewModel userVm = new();

            userVm.Id = user.Id;
            userVm.Username = user.Username;
            userVm.Name = user.Name;
            userVm.Password = user.Password;
            userVm.Email = user.Email;
            userVm.Phone = user.Phone;

            return userVm;
        }

        public async Task UpdateAsync(SaveUserViewModel vm)
        {
            User user = await _userRepository.GetByIdAsync(vm.Id);
            user.Id = vm.Id;
            user.Username = vm.Username;
            user.Name = vm.Name;
            user.Password = vm.Password;
            user.Email = vm.Email;
            user.Phone = vm.Phone;

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(user);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            SaveUserViewModel vm = new();
            user.Id = vm.Id;
            user.Username = vm.Username;
            user.Password = vm.Password;
            user.Email = vm.Email;
            user.Phone = vm.Phone;

            return vm;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await _userRepository.GetAllWithIncludeAsync(new List<string> { "Adverts" });

            return userList.Select(user => new UserViewModel
            {

                Id = user.Id,
                Username = user.Username,
                Password = user.Password,   
                Name = user.Name,
                Email = user.Email, 
                Phone = user.Phone


            }).ToList();
        }

        public async Task<bool> UserExists(SaveUserViewModel userVm)
        {
            var userList = await _userRepository.GetAllWithIncludeAsync(new List<string> { "Adverts" });

            if (userList.Exists(user => user.Username == userVm.Username))
            {
                return true;
            }

            else
            {
                return false;
            }

        }
    }
}
