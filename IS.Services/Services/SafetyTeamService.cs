using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IS.Data.Interfaces;
using IS.Data.Model;
using IS.Services.Interfaces;
using IS.Web.Components.Dtos;
using IS.Web.Components.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IS.Services.Services
{
    public class SafetyTeamService : ISafetyTeamService
    {
        private readonly ISafetyTeamRepository _safetyTeamRepository;
        private readonly UserManager<IgnitionUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IOptions<EmailSettingsModel> _configOptions;

        #region Properties

        private string AccountId
        {
            get
            {
                var user = _userManager.GetUserAsync( _httpContextAccessor.HttpContext.User );
                return user.Result.AccountId;
            }
        }


        #endregion

        #region Constructor

        public SafetyTeamService(ISafetyTeamRepository safetyTeamRepository, UserManager<IgnitionUser> userManager
            , IHttpContextAccessor httpContextAccessor, IOptions<EmailSettingsModel> configOptions )
        {
            _safetyTeamRepository = safetyTeamRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _configOptions = configOptions;
        }

        #endregion

        #region ISafetyTeamService Implementation

        public List<SafetyTeamMemberDto> GetTeam()
        {
            var users = _safetyTeamRepository.GetSafetyTeam(AccountId);
            var safetyTeamUsers = new List<SafetyTeamMemberDto>();

            foreach (var ignitionUser in users)
            {
                var safetyTeamMember = Mapper.Map<SafetyTeamMemberDto>( ignitionUser );
                safetyTeamUsers.Add( safetyTeamMember );
            }

            return safetyTeamUsers;
        }

        public SafetyTeamMemberDto GetMembers(string id)
        {
            var ignitionUser = _safetyTeamRepository.GetMember(id, AccountId );
            
            var safetyTeamMember = Mapper.Map<SafetyTeamMemberDto>( ignitionUser );

            var claims = _safetyTeamRepository.GetClaimsForUser(id);

            safetyTeamMember.IsManager = claims.Any(e => e.ClaimType == "AccountManager" && e.ClaimValue == "True");
            safetyTeamMember.IsOwner = claims.Any(e => e.ClaimType == "AccountOwner" && e.ClaimValue == "True" );


            return safetyTeamMember;
        }

        public void UpdateMember(SafetyTeamMemberDto model)
        {
            var user = _userManager.GetUserAsync( _httpContextAccessor.HttpContext.User );
            var existingMember = _safetyTeamRepository.GetMember(model.Id, user.Result.AccountId );

            if (existingMember == null)
                return;

            existingMember.Email = model.Email;
            existingMember.FirstName = model.FirstName;
            existingMember.LastName = model.LastName;

            if (model.Id != user.Result.Id)
            {
                _safetyTeamRepository.SetClaim("AccountOwner", model.IsOwner, model.Id);
                _safetyTeamRepository.SetClaim("AccountManager", model.IsManager, model.Id);
            }




            _safetyTeamRepository.UpdateMember(existingMember);

        }

        public async Task<IgnitionUser> AddNewSafetyTeamMember(SafetyTeamMemberDto model)
        {
            var newUser = Mapper.Map<IgnitionUser>(model);

            var user = _userManager.GetUserAsync( _httpContextAccessor.HttpContext.User );

            newUser.UserName = model.Email;
            newUser.AccountId = user.Result.AccountId;

            try
            {
                var result = await _userManager.CreateAsync( newUser, "TempP255word!" );

                if ( result.Succeeded )
                {

                    if ( model.IsManager )
                        await _userManager.AddClaimAsync( newUser, new Claim( "AccountManager", "True" ) );

                    if ( model.IsOwner )
                        await _userManager.AddClaimAsync( newUser, new Claim( "AccountOwner", "True" ) );

                    string code = await _userManager.GeneratePasswordResetTokenAsync( newUser );


                    return newUser;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return newUser;
        }

        public void SendInvite(string callbackUrl, string email)
        {
            var emailManager = new EmailService( _configOptions );
            emailManager.SendInviteEmail( callbackUrl, email );
        }

        #endregion
    }
}
