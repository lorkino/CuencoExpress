using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Film.Controllers;
using Film.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Film.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private UserManager<User> _userManager;

        public LoginController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //[BindProperty]
        //public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        //public class InputModel
        //{
        //    [Required]
        //    [EmailAddress]
        //    public string Email { get; set; }

        //    [Required]
        //    [DataType(DataType.Password)]
        //    public string Password { get; set; }

        //    [Display(Name = "Remember me?")]
        //    public bool RememberMe { get; set; }
        //}
        
        [HttpGet]
        [Authorize]
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }
        [HttpPost]
        public async Task<JsonResult> Login([FromBody] User user)
        {

            //// This doesn't count login failures towards account lockout
            //// To enable password failures to trigger account lockout, set lockoutOnFailure: true
            
           
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, lockoutOnFailure: true);
                User userComplete = await _userManager.FindByEmailAsync(user.Email);

          
            
            if (userComplete != null)
                {
                    
                    if (result.Succeeded)
                    {
                    
                   //llamamos al token de acceso
                    Tuple<string, DateTime> token = Film.Controllers.Account.BuildToken(user);
                    User userSecure = new User
                    {
                        Admin = userComplete.Admin,
                        Apellidos = userComplete.Apellidos,
                        Nombre = userComplete.Nombre,
                        Email = userComplete.Email,
                        EmailConfirmed = userComplete.EmailConfirmed,
                        UserDates = userComplete.UserDates,
                        AccessFailedCount = userComplete.AccessFailedCount,
                        RememberMe = userComplete.RememberMe,
                        Token = token.Item1,
                        TokenExpiration = token.Item2
                    };
                    return Json(userSecure);
                    }
                   
                    //if (result.RequiresTwoFactor)
                    //{
                    //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    //}
                    if (result.IsLockedOut)
                    {

                        return Json("Blocked user");

                    }
                }
           

            //// If we got this far, something failed, redisplay form
            return Json("Incorrect User");
        }
    }
}
