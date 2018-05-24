using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;

namespace IS.Data.Interfaces
{
    public interface ISafetyZoneRepository
    {
        void ClearAllZonesForAccount(string accountId);
        string InsertSafetyZone(string accountId, string safetyZone);
        List<SafetyZone> GetSafetyZonesForAccount(string accountId);
        SafetyZone GetSafetyZone(string safetyZoneId);
        void UpdateSafetyZone(string modelId, string modelName);
        void AddNewSafetyZone(SafetyZone safetyZone);
        void DeleteSafetyZone(string id);
        string GetSafetyZoneNameForDepartment(string accountId, string departmentId);
        List<RosterMember> GetSafetyZoneMembers( string safetyZoneId );
    }
}
