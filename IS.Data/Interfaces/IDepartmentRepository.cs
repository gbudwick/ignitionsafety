using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;

namespace IS.Data.Interfaces
{
    public interface IDepartmentRepository
    {
        string InsertDepartment(string accountId, string department, string safetyZoneId);
        void ClearDepartmentsForAccount(string accountId);
        List<Department> GetDepartmentsForSafetyZone(string safetyZoneId);
        List<Department> GetDepartments(string accountId);
        void AddNewDepartment(Department department);
        Department GetDepartment(string id);
        void UpdateDepartment(string id, string name, string safetyZoneId);
        void DeleteDepartment(string departmentId);
    }
}
