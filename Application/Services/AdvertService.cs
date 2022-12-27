using EMarket.Core.Application.Helpers;
using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.ViewModels.Adverts;
using EMarket.Core.Application.ViewModels.Users;
using EMarket.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;


        public AdvertService(IAdvertRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _advertRepository = repository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<SaveAdvertViewModel> AddAsync(SaveAdvertViewModel vm)
        {
            Advert advert = new();
            advert.Name = vm.Name;
            advert.Description = vm.Description;
            advert.ImagePath = vm.ImagePath;
            advert.CategoryId = vm.CategoryId;
            advert.Price = vm.Price;
            advert.UserId = userViewModel.Id;

            advert = await _advertRepository.AddAsync(advert);

            SaveAdvertViewModel advertVm = new();
            advertVm.Id = advert.Id;
            advertVm.Name = advert.Name;
            advertVm.Description = advert.Description;
            advertVm.ImagePath = advert.ImagePath;
            advertVm.CategoryId = advert.CategoryId;
            advertVm.Price = advert.Price;

            return advertVm;
        }

        public async Task UpdateAsync(SaveAdvertViewModel vm)
        {
            Advert advert = await _advertRepository.GetByIdAsync(vm.Id);
            advert.Id = vm.Id;
            advert.Name = vm.Name;
            advert.Description = vm.Description;
            advert.ImagePath = vm.ImagePath;
            advert.CategoryId = vm.CategoryId;
            advert.Price = vm.Price;

            await _advertRepository.UpdateAsync(advert);
        }

        public async Task DeleteAsync(int id)
        {
            var advert = await _advertRepository.GetByIdAsync(id);
            await _advertRepository.DeleteAsync(advert);
        }

        public async Task<SaveAdvertViewModel> GetByIdSaveViewModel(int id)
        {
            var advert = await _advertRepository.GetByIdAsync(id);

            SaveAdvertViewModel vm = new();
            vm.Id = advert.Id;
            vm.Name = advert.Name;
            vm.Description = advert.Description;
            vm.ImagePath = advert.ImagePath;
            vm.CategoryId = advert.CategoryId;
            vm.Price = advert.Price;

            return vm;
        }


        public async Task<List<AdvertViewModel>> GetAllViewModel()
        {
            var advertList = await _advertRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            return advertList.Where(advert => advert.UserId == userViewModel.Id).Select(advert => new AdvertViewModel { 
            
                Id = advert.Id,
                Name = advert.Name,
                Description = advert.Description,
                ImagePath = advert.ImagePath,
                Price = advert.Price,
                CategoryName = advert.Category.Name,
                CategoryDescription = advert.Category.Description,
                CategoryId = advert.Category.Id


            }).ToList();
        }

        public async Task<List<AdvertViewModel>> GetAllViewModeWithFilters(FilterAdvertViewModel filters)
        {
            var advertList = await _advertRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            var listViewModels = advertList.Where(advert => advert.UserId != userViewModel.Id).Select(advert => new AdvertViewModel
            {

                Id = advert.Id,
                Name = advert.Name,
                Description = advert.Description,
                ImagePath = advert.ImagePath,
                Price = advert.Price,
                CategoryName = advert.Category.Name,
                CategoryDescription = advert.Category.Description,
                CategoryId = advert.Category.Id

            }).ToList();


            if (filters.CategoryListId != null)
            {
                if(filters.CategoryListId.Contains(0)) 
                {

                }

               else
                {

                    for (int i = 0; i < filters.CategoryListId.Count; i++)
                    {
                        listViewModels = listViewModels.Where(advert => advert.CategoryId == filters.CategoryListId[i]).ToList();
                    }
                }


            }

            else if (filters.AdvertName != null)
            {
                listViewModels = listViewModels.Where(advert => advert.Name == filters.AdvertName).ToList();

            }

            return listViewModels;
        }

       public async Task<DetailViewModel> GetDetailViewAsync(int id, string categoryName)
        {
            var advert = await _advertRepository.GetByIdAsync(id);

            DetailViewModel advertVm = new();
            advertVm.Id = advert.Id;
            advertVm.Name = advert.Name;
            advertVm.Price = advert.Price;
            advertVm.ImagePath = advert.ImagePath;
            advertVm.Description = advert.Description;
            advertVm.CategoryName = categoryName;
            advertVm.CreatedDate = advert.Created.Value.ToShortDateString();
            advertVm.UserName = userViewModel.Name;
            advertVm.UserEmail = userViewModel.Email;
            advertVm.UserPhone = userViewModel.Phone;

            return advertVm;

        }


    }
}
