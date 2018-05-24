using System.Collections.Generic;

namespace IS.Web.Components.ViewModels
{
    public class SafetyZonesViewModel
    {
        public SafetyZonesViewModel()
        {
            SafetyZones = new List<SafetyZoneDto>();
        }

        public List<SafetyZoneDto> SafetyZones { get; set; }
    }
}
