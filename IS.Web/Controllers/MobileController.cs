using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;
using IS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IS.Web.Components.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IS.Web.Controllers
{
    [Authorize]
    public class MobileController : Controller
    {
        private readonly ITeamRosterService _teamRosterService;
        private readonly ISafetyZoneService _safetyZoneService;
        private UserManager<IgnitionUser> _userManager;

        public MobileController(ITeamRosterService teamRosterService,
            ISafetyZoneService safetyZoneService, UserManager<IgnitionUser> userManager)
        {
            _teamRosterService = teamRosterService;
            _safetyZoneService = safetyZoneService;
            _userManager = userManager;

        }

        public IActionResult Index(string safetyZoneId)
        {
            var user = _userManager.GetUserAsync(HttpContext.User);
            var accountId = user.Result.AccountId;

            var model = _safetyZoneService.GetDrillSafetyZones(accountId);

            //var model = _teamRosterService.GetRosterCheckOffModel(accountId, safetyZoneId);
            return View(model);
        }

        [Route("members/changestatus")]
        [HttpPost]
        public IActionResult PostStatusChange([FromBody] MemberStatusDto memberStatus)
        {
            _teamRosterService.UpdateStatus(memberStatus.MemberId, memberStatus.Status);
            return StatusCode(200);
        }

        public IActionResult SafetyZone(string id)
        {
            
        }
    }
}
