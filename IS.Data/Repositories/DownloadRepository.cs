using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.DbContexts;
using IS.Data.Interfaces;
using IS.Data.Model;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;

namespace IS.Data.Repositories
{
    public class DownloadRepository : IDownloadRepository
    {
        private IsDbContext _context;
        private ILogger<DownloadRepository> _logger;

        public DownloadRepository( IsDbContext context, ILogger<DownloadRepository> logger )
        {
            _context = context;
            _logger = logger;
        }

        public List<OutputFileDto> GetDownloadContents(string accountId)
        {
            return (from rst in _context.RosterMembers
                join dept in _context.Departments on rst.DepartmentId equals dept.Id
                join sz in _context.SafetyZones on dept.SafetyZoneId equals  sz.Id
                    where (rst.AccountId == accountId)
                select new OutputFileDto()
                {
                    FirstName = rst.FirstName, LastName = rst.LastName,
                    ContactPhone = rst.ContactPhone, Department = dept.Name, SafetyZone = sz.Name
                }).ToList();

           
        }
    }
}
