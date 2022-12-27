using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Core.Application.ViewModels.Users
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must type the username")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must type the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords must be the same")]
        [Required(ErrorMessage = "You must type the password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "You must type the name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must type the email")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must type the phone number")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
    }
}
