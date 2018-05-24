using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Web.Components.Dtos;
using IS.Web.Components.ViewModels;

namespace IS.Services.Interfaces
{
    public interface ITeamRosterService
    {
        Task<List<TeamRosterMemberDto>> GetTeamMembers(string firstLetter, int page );
        TeamRosterMemberDto GetTeamMember(string id, string accountId);
        void UpdateMember(TeamRosterMemberDto model);
        void AddNewTeamMember(TeamRosterMemberDto model);
        void DeleteMember(string id);
        List<EmergencyTeamMemberDto> GetMembersOfSafetyTeam(string accountId, string safetyZoneId);
        void UpdateStatus(string memberId, int memberStatus);
         RosterCheckOffViewModel GetRosterCheckOffModel(string accountId, string safetyZoneId);
    }
}
