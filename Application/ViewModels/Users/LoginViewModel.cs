using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.ViewModels.Users
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "You must type the username")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must type the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
