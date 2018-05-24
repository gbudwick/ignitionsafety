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
    public class SafetyZoneService : ISafetyZoneService
    {
        private ISafetyZoneRepository _safetyZoneRepository;
        private IDepartmentRepository _departmentRepository;

        public SafetyZoneService(ISafetyZoneRepository safetyZoneRepository, IDepartmentRepository departmentRepository)
        {
            _safetyZoneRepository = safetyZoneRepository;
            _departmentRepository = departmentRepository;
        }

        public List<SafetyZone> GetDrillSafetyZones(string accountId)
        {
            var safetyZones = _safetyZoneRepository.GetSafetyZonesForAccount(accountId);
            return safetyZones.ToList();
        }


        public SafetyZonesViewModel GetSafetyZonesViewModel(string accountId)
        {
            try
            {
                var safetyZones = _safetyZoneRepository.GetSafetyZonesForAccount(accountId);

                var viewModel = new SafetyZonesViewModel();

                foreach (var safetyZone in safetyZones)
                {
                    var departments = _departmentRepository.GetDepartmentsForSafetyZone(safetyZone.Id);
                    var departmentString = "";
                    foreach (var department in departments)
                    {
                        departmentString += department.Name + ", ";
                    }
                    departmentString = departmentString.TrimEnd( ' ' );
                    departmentString = departmentString.TrimEnd( ',' );
                    viewModel.SafetyZones.Add(new SafetyZoneDto(){ Id = safetyZone.Id, Name = safetyZone.Name, Departments = departmentString});
                }

                return viewModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public SafetyZoneDto GetSafetyZoneViewModel(string safetyZoneId)
        {
            var safetyZone = _safetyZoneRepository.GetSafetyZone(safetyZoneId);
            return new SafetyZoneDto {Id = safetyZone.Id, Name = safetyZone.Name};
        }

        public void UpdateSafetyZone(SafetyZoneDto model)
        {
           
            _safetyZoneRepository.UpdateSafetyZone(model.Id, model.Name);
        }

        public void AddNewSafetyZone(string accountId, string name)
        {
            var safetyZone = new SafetyZone(){AccountId = accountId, Name = name};
            _safetyZoneRepository.AddNewSafetyZone(safetyZone);
        }

        public void DeleteSafetyZone(string id)
        {

            var safetyZoneInUse = _departmentRepository.GetDepartmentsForSafetyZone( id ).Any( );

            if ( safetyZoneInUse )
                throw new ForeignKeyError( "This safety zone is assigned to at least one department and cannot be deleted until the departments are assigned to another safety zone." );

            _safetyZoneRepository.DeleteSafetyZone(id);
        }

        public void AddNewDeparment(string accountId, string name, string safetyZoneId)
        {
            throw new NotImplementedException();
        }

        public List<RosterCheckIn> GetDrillRoster(string safetyZoneId)
        {
            var members = _safetyZoneRepository.GetSafetyZoneMembers(safetyZoneId);

            var rosterMembers = (from m in members
                select new RosterCheckIn()
                {
                    RosterId = m.Id,
                    Name = m.LastName + ", " + m.FirstName,
                }).ToList();

            return rosterMembers;
        }
    }
}
