using EMarket.Core.Application.ViewModels.Adverts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.Interfaces.Services
{
    public interface IAdvertService : IGenericService<SaveAdvertViewModel, AdvertViewModel>
    {

        Task<List<AdvertViewModel>> GetAllViewModeWithFilters(FilterAdvertViewModel filters);

        Task<DetailViewModel> GetDetailViewAsync(int id, string categoryName);
    }
}
