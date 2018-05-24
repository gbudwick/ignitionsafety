using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Interfaces;
using IS.Services.Interfaces;

namespace IS.Services.Services
{
    public class UploadService : IUploadService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRosterRepository _rosterRepository;
        private ISafetyZoneRepository _safetyZoneRepository;

        public UploadService(IDepartmentRepository departmentRepository, IRosterRepository rosterRepository,
            ISafetyZoneRepository safetyZoneRepository)
        {
            _departmentRepository = departmentRepository;
            _rosterRepository = rosterRepository;
            _safetyZoneRepository = safetyZoneRepository;
        }



        public void SaveRosterLine(string line, string accountId)
        {
            var departments = new Dictionary<string, string>();
            var safetyZones = new Dictionary<string, string>( );

            try
            {
                var columns = line.Split('\t');

                var firstName = columns[0];
                var lastName = columns[1];
                var contactPhone = columns[2];
                var department = columns[3];
                var safetyZone = columns[4];

                var departmentId = string.Empty;
                var safetyZoneId = string.Empty;

                safetyZoneId = _safetyZoneRepository.InsertSafetyZone( accountId, safetyZone );

                departmentId = _departmentRepository.InsertDepartment( accountId, department, safetyZoneId );
               
                
               

               // _safetyZoneRepository.AddDepartmentSafetyZone(accountId, departmentId, safetyZoneId);

                _rosterRepository.AddEntry(firstName, lastName, contactPhone, departmentId, accountId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void ClearRoster(string accountId)
        {
            try
            {
                _rosterRepository.ClearAllMembersForAccount(accountId);
                _departmentRepository.ClearDepartmentsForAccount(accountId);
                _safetyZoneRepository.ClearAllZonesForAccount( accountId );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
