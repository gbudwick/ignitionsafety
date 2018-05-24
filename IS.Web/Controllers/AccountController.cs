using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;
using IS.Services.Interfaces;
using IS.Web.Components.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IS.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Members

        private IAccountService _accountService;
        private ILogger<AccountController> _logger;
        private UserManager<IgnitionUser> _userManager;
        private SignInManager<IgnitionUser> _signInManager;
        private IOptions<EmailSettingsModel> _configOptions;

        #endregion

        public AccountController(ILogger<AccountController> logger, IAccountService accountService, 
            UserManager<IgnitionUser> userManager, SignInManager<IgnitionUser> signInManager,
            IOptions<EmailSettingsModel> configOptions )
        {
            _accountService = accountService;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _configOptions = configOptions;
        }


        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp (RegisterViewModel model)
        {
            if ( !ModelState.IsValid )
                return View ( model );

            try
            {
                // check that email is not registered
                var user = await _userManager.FindByNameAsync ( model.Email );

                if ( user != null )
                {
                    // throw new ConflictException( "User is already signed up.");
                }

                var newUser = new IgnitionUser( )
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                var result = await _userManager.CreateAsync ( newUser, model.Password );

                if ( result.Succeeded )
                {
                    await _signInManager.SignInAsync ( newUser, isPersistent: false );
                    await _userManager.AddClaimAsync ( newUser, new Claim ( "AccountOwner", "True" ) );
                    await _userManager.AddClaimAsync( newUser, new Claim( "AccountManager", "True" ) );
                    await _signInManager.PasswordSignInAsync ( model.Email, model.Password, false, false );
                }

                var accountId = _accountService.RegisterNewAccount ( model );

                newUser.AccountId = accountId;

                await _userManager.UpdateAsync ( newUser );

                return RedirectToAction("index", new { controller = "admin"});
            }
            catch (Exception ex)
            {
                throw;
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[RequireHttps]
        public async Task<IActionResult> Login( LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            try
            {
                if (ModelState.IsValid)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        //Logger.LogInformation(1, "User logged in.");
                        return RedirectToAction("index", new {controller = "admin"});
                    }
                    else
                    {
                        ModelState.AddModelError( string.Empty, "Invalid user name or password." );
                        model.Password = string.Empty;
                        return View ( model );
                    }
                }
                else
                {
                    return View(model);
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> IsEmailAddressSignedUp ( string email )
        {

            var user = await _userManager.FindByNameAsync ( email );
            var userDoesNotExist = user == null;

                return Json ( userDoesNotExist ?
                    "true" : string.Format ( "An account for address {0} already exists.", email ) );
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword( )
        {
            return View( );
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword( ForgotViewModel model )
        {
            if ( ModelState.IsValid )
            {
                var user = await _userManager.FindByNameAsync( model.Email );
                if ( user == null || !( await _userManager.IsEmailConfirmedAsync( user ) ) )
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    //return View( "ForgotPasswordConfirmation" );
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link

                string code = await _userManager.GeneratePasswordResetTokenAsync(user);


                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code } );
                var emailManager = new Email.Email( _configOptions );
                emailManager.SendPasswordResetEmail(callbackUrl, model.Email);
                 return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View( model );
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword( string code )
        {
            return code == null ? View( "Error" ) : View( );
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword( ResetPasswordViewModel model )
        {
            if ( !ModelState.IsValid )
            {
                return View( model );
            }
            var user = await _userManager.FindByNameAsync( model.Email );
            if ( user == null )
            {
                // Don't reveal that the user does not exist
                return RedirectToAction( "ResetPasswordConfirmation", "Account" );
            }
            var result = await _userManager.ResetPasswordAsync( user, model.Code, model.Password );
            if ( result.Succeeded )
            {
                return RedirectToAction( "ResetPasswordConfirmation", "Account" );
            }
            //AddErrors( result );
            return View( );
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation( )
        {
            return View( );
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation( )
        {
            return View( );
        }
    }
}
