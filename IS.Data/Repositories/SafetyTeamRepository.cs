using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IS.Data.DbContexts;
using IS.Data.Interfaces;
using IS.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IS.Data.Repositories
{
    public class SafetyTeamRepository : ISafetyTeamRepository
    {
        private readonly IsDbContext _context;
            

        #region Constructor

        public SafetyTeamRepository(IsDbContext context)
        {
            _context = context;
        }

        #endregion

        public List<IgnitionUser> GetSafetyTeam(string accountId)
        {
            return _context.Users.Where( e => e.AccountId == accountId).ToList();
        }

        public IgnitionUser GetMember(string id, string accountId)
        {
            return _context.Users.FirstOrDefault(e => e.Id == id && e.AccountId == accountId);
        }

        public List<IdentityUserClaim<string>> GetClaimsForUser(string id)
        {
            return _context.UserClaims.Where(e => e.UserId == id).ToList();
                
        }

        public void UpdateMember(IgnitionUser existingMember)
        {
            _context.Users.Update(existingMember);
            _context.SaveChanges();
        }

        public void SetClaim(string claimName, bool isTrue, string userId)
        {
            var claim = _context.UserClaims.FirstOrDefault(e => e.UserId == userId && e.ClaimType == claimName);
            var value = isTrue ? "True" : "False";


            if (claim == null)
            {
                var newClaim = new IdentityUserClaim<string>
                {
                    ClaimType = claimName,
                    ClaimValue = value,
                    UserId = userId
                };

                _context.UserClaims.Add(newClaim);
            }
            else
            {
                claim.ClaimValue = value;
            }


            _context.SaveChanges();
        }
    }
}
