using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Film.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Newtonsoft.Json;

namespace Film.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private  SignInManager<User> _signInManager;
        private  UserManager<User> _userManager;
        private  ILogger<RegisterModel> _logger;
        private  IEmailSender _emailSender;




        public  RegisterController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }
        //objeto dinamico
        //public IActionResult Post([FromBody] object token)
        //{
        //    //Convierte el object que se pasa 
        //    String t = ((dynamic)JObject.Parse(token.ToString())).token;


        //[BindProperty]
        //public InputModel Input { get; set; }

        //public string ReturnUrl { get; set; }

        //public class InputModel
        //{
        //    [Required]
        //    [EmailAddress]
        //    [Display(Name = "Email")]
        //    public string Email { get; set; }

        //    [Required]
        //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //    [DataType(DataType.Password)]
        //    [Display(Name = "Password")]
        //    public string Password { get; set; }

        //    [DataType(DataType.Password)]
        //    [Display(Name = "Confirm password")]
        //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //    public string ConfirmPassword { get; set; }
        //}

        //public void OnGet(string returnUrl = null)
        //{
        //    ReturnUrl = returnUrl;
        //}

        /// <summary>
        /// Registro de un nuevo usuario
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User User)
        {


                var user = new User { UserName = User.Email, Email = User.Email };
                var result = await _userManager.CreateAsync(user, User.Password);
                if (result.Succeeded)
                {
                //llamamos al token de acceso
                

                _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = 
                    //Url.Page(
                    //    "/api/Register",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);
                //string callbackUrl = Url.Action("Register", "api", new { userId = user.Id, code = code });

                string host = HttpContext.Request.Host.ToString();

                var route = Url.RouteUrl("ConfirmEmail", new { userId = user.Id, code = code });
                host = "https://"+host + route;

                    try
                    {
                        await _emailSender.SendEmailAsync(User.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{host}'>clicking here</a>.");

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Ok("Revisa tu correo para confirmar el email");
                    }
                    catch (Exception e) {
                        await _userManager.DeleteAsync(user);
                        return BadRequest("Ha habido en error en su registro intentelo de nuevo");
                    }
                }         
            
            return BadRequest(result.Errors);
            // If we got this far, something failed, redisplay form

        }
        //[HttpGet("ConfirmEmail/{userId}/{code}")]
        /// <summary>
        /// Confirmación de correo electrónico
        /// </summary>
        [Route("register-email", Name = "ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId,  string code )
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }
            //remplazar en code /
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");

            }
            
            // var decbuff = HttpServerUtility.UrlTokenDecode(code);
           var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            return RedirectToPage("/Index");
        }
    }
}
