using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;

namespace IS.Data.Interfaces
{
    public interface ITeamRosterRepository
    {
        List<RosterMember> GetRoster(string firstLetterOfLastName, string resultAccountId);
        RosterMember GetMember(string id, string accountId);
        void UpdateMember(RosterMember rosterMember);
        void AddMember(RosterMember rosterMember);
        void DeleteMember(string id);
        bool AnyMembersInDepartment(string departmentId);
        List<RosterMember> GetMembersOfSafetyZone(string accountId, string safetyZoneId);
        void ChangeStatus(string memberId, int memberStatus);
        List<string> PresentMembers(List<RosterMember> safetyZoneMembers);
    }
}
