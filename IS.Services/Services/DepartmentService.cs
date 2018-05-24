using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IS.Data.Interfaces;
using IS.Data.Model;
using IS.Services.Interfaces;
using IS.Web.Components;
using IS.Web.Components.Dtos;
using IS.Web.Components.Exceptions;
using IS.Web.Components.ViewModels;

namespace IS.Services.Services
{
    public class DepartmentService : IDepartmentService
    {

        private ISafetyZoneRepository _safetyZoneRepository;
        private IDepartmentRepository _departmentRepository;
        private ITeamRosterRepository _teamRepository;

        public DepartmentService( ISafetyZoneRepository safetyZoneRepository, ITeamRosterRepository teamRosterRepository, IDepartmentRepository departmentRepository )
        {
            _safetyZoneRepository = safetyZoneRepository;
            _departmentRepository = departmentRepository;
            _teamRepository = teamRosterRepository;
        }


        public DepartmentsViewModel GetDepartmentsForAccount(string accountId)
        {
            var departments = _departmentRepository.GetDepartments(accountId);

            var model = new DepartmentsViewModel();

            foreach (var department in departments)
            {
                var safetyZoneName = _safetyZoneRepository.GetSafetyZoneNameForDepartment(accountId, department.Id);

                var departmentDto = new DepartmentDto(){ Id = department.Id, Name = department.Name, SafetyZoneName = safetyZoneName };
                model.Departments.Add(departmentDto);
            }

            return model;

        }

        public DepartmentDto GetDepartment(string id)
        {
            var department = _departmentRepository.GetDepartment(id);

            return Mapper.Map<DepartmentDto>(department);
        }

        public void DeleteDepartment(string departmentId)
        {
            if ( _teamRepository.AnyMembersInDepartment( departmentId ) )
            throw new ForeignKeyError("Team members are assigned to the department.  It cannot be deleted " +
                                       "until they are assigned to another department.");

            _departmentRepository.DeleteDepartment(departmentId);

            throw new NotImplementedException();
        }

        public void AddNewDeparment(string accountId, string name, string safetyZoneId)
        {
            var department = new Department()
            {
                AccountId = accountId,
                Name = name,
                SafetyZoneId = safetyZoneId
            };
            _departmentRepository.AddNewDepartment(department);
        }

        public void UpdateDepartment(DepartmentDto model)
        {
            _departmentRepository.UpdateDepartment(model.Id, model.Name, model.SafetyZoneId);
        }
    }
}
