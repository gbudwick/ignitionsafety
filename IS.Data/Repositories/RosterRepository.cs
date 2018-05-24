using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.DbContexts;
using IS.Data.Interfaces;
using IS.Data.Model;
using Microsoft.Extensions.Logging;

namespace IS.Data.Repositories
{
    public class RosterRepository : IRosterRepository
    {
        private IsDbContext _context;
        private ILogger<RosterRepository> _logger;

        public RosterRepository( ILogger<RosterRepository> logger, IsDbContext context )
        {
            _context = context;
            _logger = logger;
        }

        public void AddEntry(string firstName, string lastName, string contactPhone, string departmentId, string accountId)
        {
            try
            {
                if ( _context.RosterMembers.Any(e => e.FirstName == firstName && e.LastName == lastName && e.DepartmentId == departmentId))
                    return;

                var rosterMember = new RosterMember( )
                {
                    FirstName = firstName,
                    LastName = lastName,
                    ContactPhone = contactPhone,
                    DepartmentId = departmentId,
                    AccountId = accountId
                };
                _context.RosterMembers.Add(rosterMember);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void ClearAllMembersForAccount(string accountId)
        {
            var members = _context.RosterMembers.Where(e => e.AccountId == accountId);
            _context.RemoveRange(members);
            _context.SaveChanges();
        }
    }
}
