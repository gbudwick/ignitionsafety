using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.DbContexts;
using IS.Data.Interfaces;
using IS.Data.Migrations;
using IS.Data.Model;
using Microsoft.Extensions.Logging;
using SafetyZone = IS.Data.Model.SafetyZone;

namespace IS.Data.Repositories
{
    public class SafetyZoneRepository : ISafetyZoneRepository
    {
        private IsDbContext _context;
        private ILogger<SafetyZoneRepository> _logger;

        public SafetyZoneRepository(IsDbContext context, ILogger<SafetyZoneRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void ClearAllZonesForAccount(string accountId)
        {
            var zones = _context.SafetyZones.Where(e => e.AccountId == accountId);
            _context.SafetyZones.RemoveRange(zones);
            _context.SaveChanges();
        }

        public string InsertSafetyZone(string accountId, string safetyZoneName)
        {
            var existingZone =
                _context.SafetyZones.FirstOrDefault(e => e.AccountId == accountId && e.Name == safetyZoneName);
            if (existingZone != null)
                return existingZone.Id;

            var safetyZone = new SafetyZone()
            {
                Name = safetyZoneName,
                AccountId = accountId
            };
            _context.SafetyZones.Add(safetyZone);
            _context.SaveChanges();
            return safetyZone.Id;
        }

        
        public List<SafetyZone> GetSafetyZonesForAccount(string accountId)
        {
            try
            {
                return _context.SafetyZones.Where(e => e.AccountId == accountId).OrderBy(e => e.Name).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public SafetyZone GetSafetyZone(string safetyZoneId)
        {
            try
            {
                return _context.SafetyZones.FirstOrDefault(e => e.Id == safetyZoneId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateSafetyZone(string id, string name)
        {
            var safetyZone = _context.SafetyZones.FirstOrDefault(e => e.Id == id);
            safetyZone.Name = name;
            _context.SaveChanges();
        }

        public void AddNewSafetyZone(SafetyZone safetyZone)
        {
            _context.SafetyZones.Add(safetyZone);
            _context.SaveChanges();
        }

        public void DeleteSafetyZone(string id)
        {
            var safetyZone = _context.SafetyZones.FirstOrDefault(e => e.Id == id);
            _context.SafetyZones.Remove(safetyZone);
            _context.SaveChanges();

        }

        public string GetSafetyZoneNameForDepartment(string accountId, string departmentId)
         {
            var name = (from dept in _context.Departments   
                join sz in _context.SafetyZones on dept.SafetyZoneId equals sz.Id
                where dept.AccountId == accountId && dept.Id == departmentId
                select sz.Name).FirstOrDefault();

            return name;
        }

        public List<RosterMember> GetSafetyZoneMembers( string safetyZoneId )
        {
            var departments = _context.Departments.Where(e => e.SafetyZoneId == safetyZoneId).Select( e => e.Id).ToList();
            var members = _context.RosterMembers.Where(e => departments.Contains(e.DepartmentId)).OrderBy( e => e.LastName).ThenBy( e => e.FirstName);
            return members.ToList();
        }
    }
}
