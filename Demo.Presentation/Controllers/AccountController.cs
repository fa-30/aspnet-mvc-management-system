using Demo.DAL.Models.IdentityModels;
using Demo.Presentation.Helpers;
using Demo.Presentation.Utilities;
using Demo.Presentation.ViewModels.Auth;
using MailKit;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager , SignInManager<ApplicationUser> _signInManager , IMailServices mailService) : Controller
    {
        public IActionResult Register() => View();
        [HttpPost]

        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var User = new ApplicationUser()
            {
                FristName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
            };
            var Result = _userManager.CreateAsync(User, viewModel.Password).Result;
            if (Result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null)
            {
                if (_userManager.CheckPasswordAsync(user, model.Password).Result)
                {
                    var result = _signInManager.PasswordSignInAsync
                    (user,model.Password, model.RememberMe,false).Result;
                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }

            }
            ModelState.AddModelError(string.Empty, "Invalid Email or Password");
            return View(model);
        }

        public IActionResult GoogleLogin()
        {
            var prop = new AuthenticationProperties { RedirectUri = Url.Action(action: "GoogleResponse") };

            return Challenge(prop, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault()
                       .Claims.Select(claim => new
                       {
                           claim.Issuer,
                           claim.OriginalIssuer,
                           claim.Type,
                           claim.Value,
                       });
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ForgetPassword() => View();

        [HttpPost]

        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (User is not null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(User).Result;
                    // BaseUrl/Account/ResetPassword?email=aliaatarek@gmail.com&Token=
                    var ResetPasswordLink = Url.Action("ResetPassword","Account",new { email = viewModel.Email, Token },Request.Scheme);
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordLink // TODO
                    };
                    //EmailSettings.SendEmail(email);
                    mailService.Send(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            ModelState.AddModelError(string.Empty,"Invalid Operation");
            return View(nameof(ForgetPassword), model: viewModel);
        }

        public IActionResult CheckYourInbox() => View();

        public IActionResult ResetPassword(string email, string Token) 
        {
            TempData["email"]=email;
            TempData["Token"]=Token;
            return View();
        }

        [HttpPost]

        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(model: viewModel);

            string email = TempData[key: "email"] as string ?? string.Empty;
            string Token = TempData[key: "Token"] as string ?? string.Empty;

            var User = _userManager.FindByEmailAsync(email).Result;
            if (User is not null)
            {
                var Result = _userManager.ResetPasswordAsync(user: User, token: Token, newPassword: viewModel.Password).Result;
                if (Result.Succeeded)
                    return RedirectToAction(actionName: nameof(Login));
                else
                {
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError(key: string.Empty, errorMessage: error.Description);
                }
            }
            return View(viewName: nameof(ResetPassword), viewModel);
        }
    }
}
