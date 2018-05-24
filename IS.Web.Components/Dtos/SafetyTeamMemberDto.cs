using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Web.Components.Dtos
{
    public class SafetyTeamMemberDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "A first name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "A last name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "An email address is required.")]
        public string Email { get; set; }
        public bool IsManager { get; set; }
        public bool IsOwner { get; set; }
    }
}
