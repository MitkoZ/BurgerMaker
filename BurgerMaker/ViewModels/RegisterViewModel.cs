using BurgerMaker.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BurgerMaker.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Username must be at least 5 symbols")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 symbols")]
        [Password]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Password]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}