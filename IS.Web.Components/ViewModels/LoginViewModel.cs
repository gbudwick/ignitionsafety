using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Web.Components.ViewModels
{
    public class LoginViewModel
    {
        [Required( ErrorMessage = "Your user name is required" )]
        public string UserName { get; set; }
        [Required( ErrorMessage = "A password is required" )]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
