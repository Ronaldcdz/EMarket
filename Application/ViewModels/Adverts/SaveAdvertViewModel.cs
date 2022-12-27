using EMarket.Core.Application.ViewModels.Categories;
using EMarket.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.ViewModels.Adverts
{
    public class SaveAdvertViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "You must type the Advert's name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must type the Advert's description")]
        [DataType(DataType.MultilineText)]
        [MaxLength(200)]
        public string Description { get; set; }

        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "You must choose the Advert's price")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose a Category")]
        public int CategoryId { get; set; }

        public List<CategoryViewModel>? Categories { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
