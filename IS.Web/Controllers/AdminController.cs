using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;
using IS.Services.Interfaces;
using IS.Web.Components;
using IS.Web.Components.Dtos;
using IS.Web.Components.Exceptions;
using IS.Web.Components.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IS.Web.Controllers
{
    [Authorize( Policy = "AccountManagers" )]
    public partial class AdminController : Controller
    {

        #region Members

        private IAccountService _accountService;
        private ILogger<AccountController> _logger;
        private UserManager<IgnitionUser> _userManager;
        private SignInManager<IgnitionUser> _signInManager;
        private readonly IHostingEnvironment _environment;
        private IUploadService _uploadService;
        private ISafetyZoneService _safetyZoneService;
        private IDepartmentService _departmentService;
        private IDownloadService _downloadService;
        private ITeamRosterService _teamRosterService;
        private ISafetyTeamService _safetyTeamService;

        #endregion
        public AdminController( ILogger<AccountController> logger, IAccountService accountService,
             UserManager<IgnitionUser> userManager, SignInManager<IgnitionUser> signInManager,
             IHostingEnvironment environment, IUploadService uploadService,
             ISafetyZoneService safetyZoneService, IDepartmentService departmentService,
             IDownloadService downloadService,
             ITeamRosterService teamRosterService,
             ISafetyTeamService safetyTeamService
            )
        {
            _accountService = accountService;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
            _uploadService = uploadService;
            _safetyZoneService = safetyZoneService;
            _departmentService = departmentService;
            _downloadService = downloadService;
            _teamRosterService = teamRosterService;
            _safetyTeamService = safetyTeamService;
        }



        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult UploadRoster()
        {
            return View();
        }

        [HttpPost]
        [Authorize( Policy = "AccountManagers" )]
        public async Task<IActionResult> UploadRoster( ICollection<IFormFile> files )
        {
            var uploads = Path.Combine( _environment.WebRootPath, "uploads" );
            foreach ( var file in files )
            {
                if ( file.Length > 0 )
                {
                    using ( var saveFileStream = new FileStream( Path.Combine( uploads, file.FileName ), FileMode.Create ) )
                    {
                       
                        await file.CopyToAsync( saveFileStream );
                    }
                    string line;
                    var user = _userManager.GetUserAsync( HttpContext.User );

                   // _uploadService.ClearRoster(user.Result.AccountId);

                    var fileStream = new FileStream( Path.Combine( uploads, file.FileName ), FileMode.Open );
                    using ( var reader = new StreamReader( fileStream ) )
                    {
                        //Read in header
                        reader.ReadLine();
                        while ( ( line = reader.ReadLine( ) ) != null )
                        {
                            Console.WriteLine( line );
                            _uploadService.SaveRosterLine(line, user.Result.AccountId);
                        }
                    }

                    System.IO.File.Delete( Path.Combine( uploads, file.FileName ) );
                }
            }
            return View( );
        }

        public IActionResult SafetyZones()
        {
            var user = _userManager.GetUserAsync( HttpContext.User );
            var model = _safetyZoneService.GetSafetyZonesViewModel(user.Result.AccountId);

            return View(model);
        }

        [Route("admin/safetyzones/{id}")]
        public IActionResult SafetyZonesEdit(string id)
        {
            var model = _safetyZoneService.GetSafetyZoneViewModel(id);
            return View("SafetyZoneEdit", model );
        }

        [Route("admin/safetyzones/add")]
        public IActionResult SafetyZoneAdd()
        {
            return View("SafetyZoneAdd");
        }

        [HttpPost]
        [Route( "admin/safetyzones/add" )]
        public IActionResult SafetyZoneAdd( SafetyZoneDto model )
        {
            if ( !ModelState.IsValid )
                return View( "SafetyZoneAdd", model );

            var user = _userManager.GetUserAsync( HttpContext.User );
            _safetyZoneService.AddNewSafetyZone(user.Result.AccountId, model.Name);

            return RedirectToAction("SafetyZones");
        }

        [Route("admin/safetyzones/delete/{id}")]
        public IActionResult SafetyZoneDelete(string id)
        {
            var model = _safetyZoneService.GetSafetyZoneViewModel( id );
            return View( "SafetyZoneDelete", model );
        }

        [HttpPost]
        [Route("admin/safetyzones/delete/delete")]
        public IActionResult DeleteSafetyZone( SafetyZoneDto model )
        {
            try
            {
                _safetyZoneService.DeleteSafetyZone( model.Id );
                return RedirectToAction( "SafetyZones" );
            }
            catch (ForeignKeyError exception)
            {
                ModelState.AddModelError( "SafetyZoneInUse", exception.Message );
                return View( "SafetyZoneDelete", model );
            }
        }

        [HttpPost]
        [Route("admin/safetyzones/edit")]
        public IActionResult SafetyZonesEditSave( SafetyZoneDto model )
        {

            if (!ModelState.IsValid)
                return View( "SafetyZoneEdit", model );



            _safetyZoneService.UpdateSafetyZone(model);
            return RedirectToAction("SafetyZones");
        }

        public IActionResult Departments()
        {
            var user = _userManager.GetUserAsync( HttpContext.User );
            var model = _departmentService.GetDepartmentsForAccount(user.Result.AccountId);
            return View(model);
        }

        [Route("admin/departments/{id}")]
        public IActionResult EditDepartment(string id)
        {
            var model = _departmentService.GetDepartment(id);
            return View( "DepartmentEdit", model );
        }

        [HttpPost]
        [Route( "admin/departments/edit" )]
        public IActionResult DepartmentEditSave( DepartmentDto model )
        {
            if ( !ModelState.IsValid )
                return View( "DepartmentEdit", model );

            _departmentService.UpdateDepartment( model );
            return RedirectToAction( "Departments" );
        }

        [HttpPost]
        [Route( "admin/departments/add" )]
        public IActionResult DepartmentAdd( DepartmentDto model )
        {
            if ( !ModelState.IsValid )
                return View( "DepartmentAdd", model );

            var user = _userManager.GetUserAsync( HttpContext.User );
            _departmentService.AddNewDeparment( user.Result.AccountId, model.Name, model.SafetyZoneId );

            return RedirectToAction( "Departments" );
        }

        [Route( "admin/departments/delete/{id}" )]
        public IActionResult DepartmentDelete( string id )
        {
            var model = _departmentService.GetDepartment( id );
            return View( "DepartmentDelete", model );
        }

        [HttpPost]
        [Route( "admin/departments/delete/delete" )]
        public IActionResult DeleteEpartment( DepartmentDto model )
        {
            try
            {
                _departmentService.DeleteDepartment( model.Id );
                return RedirectToAction( "Departments" );
            }
            catch (ForeignKeyError exception)
            {
                ModelState.AddModelError( "DepartmentInUse", exception.Message );
                return View( "DepartmentDelete", model );

            }
            _departmentService.DeleteDepartment(model.Id);
            return RedirectToAction( "Departments" );
        }

        [Route( "admin/departments/add" )]
        public IActionResult DepartmentAdd( )
        {
            return View( "DepartmentAdd" );
        }

        public IActionResult Download()
        {
            return View();
        }

        public FileResult DownloadFile( )
        {
            var user = _userManager.GetUserAsync( HttpContext.User );
            var fileName = _downloadService.DownloadFile(user.Result.AccountId);
            byte[ ] fileBytes = System.IO.File.ReadAllBytes( fileName );
            System.IO.File.Delete( fileName );
            return File( fileBytes, "text/plain", "IgnitionSafety.txt" );
        }

        public FileResult DownloadSampleFile( )
        {
            var user = _userManager.GetUserAsync( HttpContext.User );
            var fileName = _downloadService.GetSampleFileName();
            byte[ ] fileBytes = System.IO.File.ReadAllBytes( fileName );
            return File( fileBytes, "text/plain", "IgnitionSafetySample.txt" );
        }


        public async Task<IActionResult> TeamRoster( string li = "*", int page = 1 )
        {
           // var user = _userManager.GetUserAsync( HttpContext.User );
           
            var model = await _teamRosterService.GetTeamMembers(li, page);
            return View( model );
        }

        [Route( "admin/teamroster/{id}" )]
        public IActionResult EditRoster( string id )
        {
            var user = _userManager.GetUserAsync( HttpContext.User );
            var accountId = user.Result.AccountId;
            var model = _teamRosterService.GetTeamMember(id, accountId);
            return View( "TeamRosterEdit", model );
        }

        [HttpPost]
        [Route( "admin/teamroster/edit" )]
        public IActionResult RosterEditSave( TeamRosterMemberDto model )
        {
            if ( !ModelState.IsValid )
                return View( "TeamRosterEdit", model );

            _teamRosterService.UpdateMember(model);
            return RedirectToAction( "TeamRoster" );
        }

        [Route( "admin/teamroster/add" )]
        public IActionResult TeamMemberAdd( )
        {
            return View( "TeamRosterAdd" );
        }

        [HttpPost]
        [Route( "admin/teamroster/add" )]
        public IActionResult TeamMemberAdd( TeamRosterMemberDto model )
        {
            if ( !ModelState.IsValid )
                return View( "TeamRosterAdd", model );

            _teamRosterService.AddNewTeamMember(model);

            return RedirectToAction( "TeamRoster" );
        }

        [Route( "admin/teamroster/delete/{id}" )]
        public IActionResult TeamRosterDelete( string id )
        {
            var user = _userManager.GetUserAsync( HttpContext.User );
            var accountId = user.Result.AccountId;
            var model = _teamRosterService.GetTeamMember( id, accountId );
            return View( "TeamRosterDelete", model );
        }

        [HttpPost]
        [Route( "admin/teamroster/delete/delete" )]
        public IActionResult DeleteMember( string id )
        {
            _teamRosterService.DeleteMember(id);
            return RedirectToAction( "TeamRoster" );
        }

        [Route("admin/safetyteam")]
        public IActionResult SafetyTeam()
        {
            var model = _safetyTeamService.GetTeam();
            return View(model);
        }

        [Route( "admin/safetyteam/{id}" )]
        public IActionResult SafetyTeamEdit( string id )
        {
            var model = _safetyTeamService.GetMembers(id);
            return View( model );
        }

        [Route( "admin/safetyteam/add" )]
        public IActionResult SafetyTeamAdd( )
        {
            return View( "SafetyTeamAdd" );
        }

        [HttpPost]
        [Route( "admin/safetyteam/add" )]
        public async Task<IActionResult> SafetyTeamAddNew( SafetyTeamMemberDto model )
        {
            if ( !ModelState.IsValid )
                return View( "SafetyTeamAdd", model );

           var user = await _safetyTeamService.AddNewSafetyTeamMember( model );

            string code = await _userManager.GeneratePasswordResetTokenAsync( user );
            var callbackUrl = Url.Action( "ResetPassword", "Account", new { userId = user.Id, code = code } );

            _safetyTeamService.SendInvite(callbackUrl, model.Email);

            return RedirectToAction( "SafetyTeam" );
        }

        [HttpPost]
        [Route( "admin/safetyteam/edit" )]
        public IActionResult SafetyTeamEdit( SafetyTeamMemberDto model )
        {
            if (!ModelState.IsValid)
                return View(model);

            _safetyTeamService.UpdateMember( model );
            return RedirectToAction("SafetyTeam");
        }
    }
}
