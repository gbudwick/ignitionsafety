using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Web.Components
{
    public class SafetyZoneDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "A safety zone name is required.")]
        [MaxLength(20, ErrorMessage = "Names can be not more than 20 characters.")]
        public string Name { get; set; }
        public string Departments { get; set; }
    }
}
