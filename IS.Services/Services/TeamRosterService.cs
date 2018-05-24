using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IS.Data.Interfaces;
using IS.Data.Model;
using IS.Services.Interfaces;
using IS.Web.Components.Dtos;
using IS.Web.Components.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace IS.Services.Services
{
    public class TeamRosterService : ITeamRosterService
    {
        private readonly ITeamRosterRepository _teamRosterRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IgnitionUser> _userManager;
        private readonly IDepartmentRepository _departmentRepository;
        private ISafetyZoneRepository _safetyZoneRepository;

        public TeamRosterService( ITeamRosterRepository teamRoster, IHttpContextAccessor httpContextAccessor,
            UserManager<IgnitionUser> userManager, IDepartmentRepository departmentRepository,
            ISafetyZoneRepository safetyZoneRepository)
        {
            _teamRosterRepository = teamRoster;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _departmentRepository = departmentRepository;
            _safetyZoneRepository = safetyZoneRepository;
        }


        public async Task<List<TeamRosterMemberDto>> GetTeamMembers( string firstLetterOfLastName, int page)
        {
            var user = await _userManager.GetUserAsync( _httpContextAccessor.HttpContext.User );
            var team = _teamRosterRepository.GetRoster(firstLetterOfLastName, user.AccountId);
            var departments = _departmentRepository.GetDepartments(user.AccountId);

            var skip = page - 1 * 25;

            if (firstLetterOfLastName == "*")
            {
                return (from t in team
                join d in departments on t.DepartmentId equals d.Id
                        where t.AccountId == user.AccountId
                select new TeamRosterMemberDto
                {
                    Department = d.Name,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    ContactPhone = t.ContactPhone,
                    Id = t.Id
                }).ToList();
            }
            else if (firstLetterOfLastName != "xyz")
            {
                return ( from t in team
                         join d in departments on t.DepartmentId equals d.Id
                         where t.LastName.ToLower().StartsWith(firstLetterOfLastName.ToLower())
                         select new TeamRosterMemberDto
                         {
                             Department = d.Name,
                             FirstName = t.FirstName,
                             LastName = t.LastName,
                             ContactPhone = t.ContactPhone,
                             Id = t.Id
                         } ).ToList( );
            }
            else
            {
                return ( from t in team
                         join d in departments on t.DepartmentId equals d.Id
                         where t.LastName.ToLower( ).StartsWith( "x" ) ||
                         t.LastName.ToLower( ).StartsWith( "y" ) ||
                         t.LastName.ToLower( ).StartsWith( "z" ) 
                         select new TeamRosterMemberDto
                         {
                             Department = d.Name,
                             FirstName = t.FirstName,
                             LastName = t.LastName,
                             ContactPhone = t.ContactPhone,
                             Id = t.Id
                         } ).ToList( );
            }


        }

        public TeamRosterMemberDto GetTeamMember(string id, string accountId)
        {
            var member = _teamRosterRepository.GetMember(id, accountId);
            return Mapper.Map<TeamRosterMemberDto>(member);
        }

        public void UpdateMember(TeamRosterMemberDto model)
        {
            var rosterMember = Mapper.Map<RosterMember>(model);
            _teamRosterRepository.UpdateMember(rosterMember);
        }

        public void AddNewTeamMember(TeamRosterMemberDto model)
        {
            var rosterMember = Mapper.Map<RosterMember>( model );
            _teamRosterRepository.AddMember( rosterMember );
        }

        public void DeleteMember(string id)
        {
            _teamRosterRepository.DeleteMember(id);
        }

        public List<EmergencyTeamMemberDto> GetMembersOfSafetyTeam(string accountId, string safetyZoneId)
        {
            var safetyZoneMembers = _teamRosterRepository.GetMembersOfSafetyZone(accountId, safetyZoneId);

            return safetyZoneMembers.Select(Mapper.Map<EmergencyTeamMemberDto>).ToList();
        }

        public void UpdateStatus(string memberId, int memberStatus)
        {
            _teamRosterRepository.ChangeStatus(memberId, memberStatus);
        }

        public RosterCheckOffViewModel GetRosterCheckOffModel(string accountId, string safetyZoneId)
        {
            var nameOfSafetyZone = _safetyZoneRepository.GetSafetyZone(safetyZoneId).Name;
            var safetyZoneMembers = _teamRosterRepository.GetMembersOfSafetyZone(accountId, safetyZoneId);

            var presentMembers = _teamRosterRepository.PresentMembers(safetyZoneMembers);
            

            var members = safetyZoneMembers.Select(Mapper.Map<EmergencyTeamMemberDto>).ToList();

            foreach (var member in members)
            {
                if (presentMembers.Contains(member.Id))
                    member.Status = 1;
            }


            var rosterViewModel = new RosterCheckOffViewModel()
            {
                Members = members,
                SafetyZoneName = nameOfSafetyZone
            };

            return rosterViewModel;
        }

        public bool TeamMembersExistInDepartment(string departmentId)
        {
            return _teamRosterRepository.AnyMembersInDepartment(departmentId);
        }
    }
}
