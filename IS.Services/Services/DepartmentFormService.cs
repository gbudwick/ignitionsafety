using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Interfaces;
using IS.Data.Model;
using IS.Web.Components.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IS.Services.Services
{
    public class DepartmentFormService
    {
        
        private readonly UserManager<IgnitionUser> _userManager;

        private IHttpContextAccessor _httpContextAccessor;
        private ISafetyZoneRepository _safetyZoneRepository;
        private IDepartmentRepository _departmentRepository;

        public DepartmentFormService( ISafetyZoneRepository safetyZoneRepository, UserManager<IgnitionUser> userManager,
            IHttpContextAccessor httpContextAccessor, IDepartmentRepository departmentRepository )
        {
            _safetyZoneRepository = safetyZoneRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _departmentRepository = departmentRepository;
        }

        public List<SelectListItem> GetSafetyZones()
        {
            var user = _userManager.GetUserAsync( _httpContextAccessor.HttpContext.User );
            var safetyZones = _safetyZoneRepository.GetSafetyZonesForAccount(user.Result.AccountId).OrderBy( e => e.Name);

            var safetyZoneList = new List<SelectListItem>();

            foreach (var safetyZone in safetyZones )
            {
                var item = new SelectListItem(){ Text = safetyZone.Name, Value = safetyZone.Id};
                safetyZoneList.Add(item);
            }

            return safetyZoneList;
        }

        public List<SelectListItem> GetDepartments( )
        {
            var user = _userManager.GetUserAsync( _httpContextAccessor.HttpContext.User );
            var departments = _departmentRepository.GetDepartments( user.Result.AccountId ).OrderBy( e => e.Name );

            var departmentList = new List<SelectListItem>( );

            foreach ( var department in departments )
            {
                var item = new SelectListItem( ) { Text = department.Name, Value = department.Id };
                departmentList.Add( item );
            }

            return departmentList;
        }
    }
}
