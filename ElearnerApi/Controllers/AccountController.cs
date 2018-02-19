using ElearnerApi.Data.DataContext;
using ElearnerApi.Data.Models;
using ElearnerApi.Data.Models.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElearnerApi.Controllers
{
    [Route( "[controller]/[action]" )]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [TempData]
        public string ErrorMessage
        {
            get; set;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync( IdentityConstants.ExternalScheme );

            ViewData["ReturnUrl"] = returnUrl;
            return View( );
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result 
                    = await _signInManager
                        .PasswordSignInAsync( model.Email, model.Password, model.RememberMe, lockoutOnFailure: false );

                if (result.Succeeded)
                {
                    return RedirectToLocal( returnUrl );
                }
            }

            TempData["CustomResponseAlert"] = CustomResponseAlert.GetStringResponse( ResponseStatusEnum.Danger, "Unable to log in!" );

            return View( model );
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View( );
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                var result = await _userManager.CreateAsync( user, model.Password );

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync( user );

                    var claim = new Claim( "DefaultUserClaim", "DefaultUserAuthorization" );

                    var addClaimResult = await _userManager.AddClaimAsync( user, claim );

                    await _signInManager.SignInAsync( user, isPersistent: false );

                    TempData["CustomResponseAlert"] = CustomResponseAlert.GetStringResponse( ResponseStatusEnum.Success, "Thank you for registering!" );

                    return RedirectToLocal( returnUrl );
                }

                AddErrors( result );
            }

            return View( model );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync( );
            return RedirectToAction( nameof( HomeController.Index ), "Home" );
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError( string.Empty, error.Description );
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl( returnUrl ))
            {
                return Redirect( returnUrl );
            }
            else
            {
                return RedirectToAction( nameof( HomeController.Index ), "Home" );
            }
        }

        #endregion Helpers
    }
}