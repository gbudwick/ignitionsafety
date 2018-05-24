using System.Collections.Generic;
using IS.Web.Components.Dtos;

namespace IS.Web.Components.ViewModels
{
    public class DepartmentsViewModel
    {
        public DepartmentsViewModel()
        {
            Departments = new List<DepartmentDto>();
        }

        public List<DepartmentDto> Departments { get; set; }
    }
}
