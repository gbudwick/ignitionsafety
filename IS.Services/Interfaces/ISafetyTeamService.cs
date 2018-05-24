using System.Collections.Generic;
using System.Threading.Tasks;
using IS.Data.Model;
using IS.Web.Components.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IS.Services.Interfaces
{
    public interface ISafetyTeamService
    {
        List<SafetyTeamMemberDto> GetTeam();
        SafetyTeamMemberDto GetMembers(string id);
        void UpdateMember(SafetyTeamMemberDto model);
        Task<IgnitionUser> AddNewSafetyTeamMember(SafetyTeamMemberDto model);
        void SendInvite(string link, string email );
    }
}
