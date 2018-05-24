using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using IS.Web.Components.Dtos;

namespace IS.Web.Components.ViewModels
{
    public class RosterCheckOffViewModel
    {
        public string SafetyZoneName { get; set; }
        public List<EmergencyTeamMemberDto> Members { get; set; }
    }
}
