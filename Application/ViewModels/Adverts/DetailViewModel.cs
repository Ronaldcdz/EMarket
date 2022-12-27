using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.ViewModels.Adverts
{
    public class DetailViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        public double? Price { get; set; }

        public string? CategoryName { get; set; }

        public string? CreatedDate { get; set; }

        public string? UserName { get; set; }

        public string? UserEmail { get; set; }

        public string? UserPhone { get; set; }


    }
}
