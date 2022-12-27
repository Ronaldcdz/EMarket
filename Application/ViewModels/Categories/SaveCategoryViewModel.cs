using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.ViewModels.Categories
{
    public class SaveCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must type the Category's name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must type the Category's description")]
        [DataType(DataType.Text)]
        [MaxLength(120)]
        public string Description { get; set; }
    }
}
