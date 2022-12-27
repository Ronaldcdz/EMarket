using EMarket.Core.Application.ViewModels.Adverts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel>
        where SaveViewModel : class
        where ViewModel : class
    {
        Task<SaveViewModel> AddAsync(SaveViewModel entity);

        Task UpdateAsync(SaveViewModel entity);

        Task DeleteAsync(int id);

        Task<List<ViewModel>> GetAllViewModel();

        Task<SaveViewModel> GetByIdSaveViewModel(int id);

    }
}
