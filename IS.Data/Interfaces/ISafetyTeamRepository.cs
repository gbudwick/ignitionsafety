using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IS.Data.Interfaces
{
    public interface ISafetyTeamRepository
    {
        List<IgnitionUser> GetSafetyTeam(string accountId);
        IgnitionUser GetMember(string id, string accountId);
        List<IdentityUserClaim<string>> GetClaimsForUser(string id);
        void UpdateMember(IgnitionUser existingMember);
        void SetClaim(string claimName, bool isTrue, string userId);
        
    }
}
