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
    public class DepartmentRepository : IDepartmentRepository
    {
        private IsDbContext _context;
        private ILogger<DepartmentRepository> _logger;

        public DepartmentRepository(IsDbContext context, ILogger<DepartmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }



        public string InsertDepartment(string accountId, string departmentName, string safetyZoneId)
        {
            try
            {
                var existingDepartment =
                    _context.Departments.FirstOrDefault(e => e.AccountId == accountId && e.Name == departmentName);
                if (existingDepartment != null)
                    return existingDepartment.Id;



                var department = new Department()
                {
                    AccountId = accountId,
                    Name = departmentName,
                    SafetyZoneId = safetyZoneId
                };
                _context.Departments.Add(department);
                _context.SaveChanges();
                return department.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void ClearDepartmentsForAccount(string accountId)
        {
            var departments = _context.Departments.Where(e => e.AccountId == accountId);
            _context.Departments.RemoveRange(departments);
            _context.SaveChanges();
        }

        public List<Department> GetDepartmentsForSafetyZone(string safetyZoneId)
        {
            try
            {
                return _context.Departments.Where(e => e.SafetyZoneId == safetyZoneId).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Department> GetDepartments(string accountId)
        {
            return _context.Departments.Where(e => e.AccountId == accountId).OrderBy(e => e.Name).ToList();
        }

        public void AddNewDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public Department GetDepartment(string id)
        {
            return _context.Departments.First(e => e.Id == id);
        }

        public void UpdateDepartment(string id, string name, string safetyZoneId)
        {
            var department = _context.Departments.First(e => e.Id == id);
            department.Name = name;
            department.SafetyZoneId = safetyZoneId;
            _context.SaveChanges();
        }

        public void DeleteDepartment(string departmentId)
        {
            var department = GetDepartment(departmentId);
            _context.Departments.Remove(department);
            _context.SaveChanges();
        }
    }
}
