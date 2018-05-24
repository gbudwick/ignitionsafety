using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IS.Web.Components.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "A company name is required.")]
        public string  CompanyName { get; set; }


        [Required ( ErrorMessage = "Your email address is required." )]
        [MaxLength ( 256, ErrorMessage = "Email addresses can't be over 256 characters." )]
        [Remote ( "IsEmailAddressSignedUp", "Account" )]
        public string Email { get; set; }

        [Required ( ErrorMessage = "A password is required." )]
        [MaxLength ( 50, ErrorMessage = "Your password can't be over 50 characters." )]
        public string Password { get; set; }

        [Required ( ErrorMessage = "A confirmation password is required." )]
        [Compare ( "Password", ErrorMessage = "Passwords do not match." )]
        public string ConfirmPassword { get; set; }

        [Required ( ErrorMessage = "Your first name is required." )]
        [MaxLength ( 50, ErrorMessage = "Your first name can't be over 50 characters." )]
        public string FirstName { get; set; }

        [Required ( ErrorMessage = "Your last name is required." )]
        [MaxLength ( 50, ErrorMessage = "Your last name can't be over 50 characters." )]
        public string LastName { get; set; }
    }
}
