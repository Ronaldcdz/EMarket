using EMarket.Core.Application.Helpers;
using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.ViewModels.Adverts;
using EMarket.Core.Application.ViewModels.Categories;
using EMarket.Core.Application.ViewModels.Users;
using EMarket.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        public CategoryService(ICategoryRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = repository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<SaveCategoryViewModel> AddAsync(SaveCategoryViewModel vm)
        {
            Category category = new();
            category.Name = vm.Name;
            category.Description = vm.Description;

            category = await _categoryRepository.AddAsync(category);

            SaveCategoryViewModel categoryVm = new();
            categoryVm.Id = category.Id;
            categoryVm.Name = category.Name;
            categoryVm.Description = category.Description;

            return categoryVm;
        }

        public async Task UpdateAsync(SaveCategoryViewModel vm)
        {
            Category category = await _categoryRepository.GetByIdAsync(vm.Id);
            category.Id = vm.Id;
            category.Name = vm.Name;
            category.Description = vm.Description;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<List<CategoryViewModel>> GetAllViewModel()
        {
            var categoryList = await _categoryRepository.GetAllWithIncludeAsync(new List<string> { "Adverts"});

            return categoryList.Select(category => new CategoryViewModel
            {

                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                AdvertsQuantity = category.Adverts.Count(),
            }).ToList();
        }

        public async Task<SaveCategoryViewModel> GetByIdSaveViewModel(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            SaveCategoryViewModel vm = new();
            vm.Id = category.Id;
            vm.Name = category.Name;
            vm.Description = category.Description;

            return vm;
        }


    }
}
