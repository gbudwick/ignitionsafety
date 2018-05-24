using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Web.Components.Dtos;

namespace IS.Web.Components.ViewModels
{
    public class CheckOffViewModel
    {
        public List<RosterCheckIn> RosterCheckIns { get; set; }
        public List<SafetyZoneDto> SafetyZoneDtos { get; set; }
    }
}
