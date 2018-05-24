using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;
using IS.Services.Interfaces;
using IS.Web.Components.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IS.Web.Controllers
{
    [Route("api")]
    public class EmergencyController  : Controller
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
        public EmergencyController(ILogger<AccountController> logger, IAccountService accountService,
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


        [Route("safetyzones")]
        public IActionResult GetSafetyZonesForAccount()
        {
            var user = _userManager.GetUserAsync(HttpContext.User);
            var accountId = user.Result.AccountId;
            var model = _safetyZoneService.GetSafetyZonesViewModel(accountId);

            return Json(model);
        }

        [Route("safetyzones/members/{safetyZoneId}")]
        public IActionResult MembersOfSafetyGroup(string safetyZoneId)
        {
            var user = _userManager.GetUserAsync(HttpContext.User);
            var accountId = user.Result.AccountId;
            accountId = "24732de7-afcf-4627-b66c-131664732724";

            var model = _teamRosterService.GetRosterCheckOffModel(accountId, safetyZoneId);

           

            return Json(model);
        }

        [Route("members/changestatus")]
        [HttpPost]
        public IActionResult PostStatusChange([FromBody] MemberStatusDto memberStatus)
        {
            _teamRosterService.UpdateStatus(memberStatus.MemberId, memberStatus.Status);
            return StatusCode(200);
        }
    }
}
