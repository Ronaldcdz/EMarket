using EMarket.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.ViewModels.Adverts
{
    public class AdvertViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public double Price { get; set; }

        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }

        public int? CategoryId { get; set; }


    }
}
