using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;
using IS.Web.Components;
using IS.Web.Components.ViewModels;

namespace IS.Services.Interfaces
{
    public interface ISafetyZoneService
    {
        SafetyZonesViewModel GetSafetyZonesViewModel(string accountId);
        SafetyZoneDto GetSafetyZoneViewModel(string safetyZoneId);
        void UpdateSafetyZone(SafetyZoneDto model);
        void AddNewSafetyZone(string accountId, string name);
        void DeleteSafetyZone(string id);
        void AddNewDeparment(string accountId, string name, string safetyZoneId);
        List<SafetyZone> GetDrillSafetyZones(string accountId);
    }
}
