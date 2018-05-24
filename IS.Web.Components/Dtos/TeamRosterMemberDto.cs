using System.ComponentModel.DataAnnotations;

namespace IS.Web.Components.Dtos
{
    public class TeamRosterMemberDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "A first name is required.")]
        [MaxLength(50, ErrorMessage = "First name is 50 characters max.")]
        public string FirstName { get; set; }
        [Required( ErrorMessage = "A last name is required." )]
        [MaxLength( 50, ErrorMessage = "Last name is 50 characters max." )]
        public string LastName { get; set; }
        public string ContactPhone { get; set; }
        public string Department { get; set; }
        public string DepartmentId { get; set; }
    }
}
