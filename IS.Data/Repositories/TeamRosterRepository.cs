using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.DbContexts;
using IS.Data.Interfaces;
using IS.Data.Migrations;
using IS.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IS.Data.Repositories
{
    public class TeamRosterRepository : ITeamRosterRepository
    {
        private IsDbContext _context;
        private ILogger<TeamRosterRepository> _logger;
        private IHttpContextAccessor _httpContextAccessor;
        private UserManager<IgnitionUser> _userManager;

        public TeamRosterRepository( IsDbContext context, ILogger<TeamRosterRepository> logger,
            IHttpContextAccessor httpContextAccessor, UserManager<IgnitionUser> userManager )
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public List<RosterMember> GetRoster(string firstLetterOfLastName, string accountId)
        {
            return _context.RosterMembers.Where(e => e.AccountId == accountId).ToList();
        }

        public RosterMember GetMember(string id, string accountId)
        {
            return _context.RosterMembers.FirstOrDefault(e => e.Id == id && e.AccountId == accountId);
        }

        public void UpdateMember(RosterMember rosterMember)
        {
            var user = _userManager.GetUserAsync( _httpContextAccessor.HttpContext.User );
            rosterMember.AccountId = user.Result.AccountId;
            _context.Update(rosterMember);
            _context.SaveChanges();
        }

        public void AddMember(RosterMember rosterMember)
        {
            var user = _userManager.GetUserAsync( _httpContextAccessor.HttpContext.User );
            rosterMember.AccountId = user.Result.AccountId;
            _context.RosterMembers.Add(rosterMember);
            _context.SaveChanges();
        }

        public void DeleteMember(string id)
        {
            var user = _userManager.GetUserAsync( _httpContextAccessor.HttpContext.User );
            var member = _context.RosterMembers.FirstOrDefault(e => e.AccountId == user.Result.AccountId &&
                                                                    e.Id == id);

            if (member == null)
                return;

            _context.RosterMembers.Remove(member);
            _context.SaveChanges();
        }

        public bool AnyMembersInDepartment(string departmentId)
        {
            return _context.RosterMembers.Any(e => e.DepartmentId == departmentId);
        }

        public List<RosterMember> GetMembersOfSafetyZone(string accountId, string safetyZoneId)
        {
            var departments = _context.Departments.Where(e => e.SafetyZoneId == safetyZoneId).Select( e => e.Id);

            var members = _context.RosterMembers.Where(e => departments.Contains(e.DepartmentId)).OrderBy( e => e.LastName).OrderBy( e => e.FirstName);

            return members.ToList();
        }

        public void ChangeStatus(string memberId, int memberStatus)
        {
            var member = _context.MembersStatuses.FirstOrDefault(e => e.MemberId == memberId && !e.Locked);

            if (member == null)
            {
                var newMember = new MembersStatus()
                {
                    MemberId = memberId,
                    Status = memberStatus,
                    Locked = false
                };

                _context.MembersStatuses.Add(newMember);
                _context.SaveChanges();
            }
            else
            {
                member.Status = memberStatus;
                member.Locked = false;
                _context.SaveChanges();
            }
        }

        public List<string> PresentMembers(List<RosterMember> safetyZoneMembers)
        {  
            var ids = safetyZoneMembers.Select(e => e.Id).ToList();

            var statusRecords = _context.MembersStatuses.Where(e => ids.Contains(e.MemberId)  && e.Status == 1).ToList();
            return statusRecords.Select( e => e.MemberId).ToList();
        }
    }
}
