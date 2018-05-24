using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;
using IS.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IS.Web.Controllers
{
    public class DrillController : Controller
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
        public DrillController( ILogger<AccountController> logger, IAccountService accountService,
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

        public IActionResult SafetyZones()
        {
            var user = _userManager.GetUserAsync( HttpContext.User );
            var accountId = user.Result.AccountId;
            var model = _safetyZoneService.GetSafetyZonesViewModel( accountId );

            return View(model);
        }
    }
}
