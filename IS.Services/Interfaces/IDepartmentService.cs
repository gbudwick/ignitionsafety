using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Web.Components;
using IS.Web.Components.Dtos;
using IS.Web.Components.ViewModels;

namespace IS.Services.Interfaces
{
    public interface IDepartmentService
    {
        DepartmentsViewModel GetDepartmentsForAccount(string accountId);
        DepartmentDto GetDepartment(string id);
        void DeleteDepartment(string modelId);
        void AddNewDeparment(string accountId, string name, string safetyZoneId);
        void UpdateDepartment(DepartmentDto model);
    }
}
