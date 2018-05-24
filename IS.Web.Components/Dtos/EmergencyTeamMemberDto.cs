using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Web.Components.Dtos
{
    public class EmergencyTeamMemberDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPhone { get; set; }
        public int Status { get; set; }
    }
}
