using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AdvertsQuantity { get; set; }

        public int AdvertsUsersQuantity { get; set; }

    }
}
