
namespace FitnessProgram.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using FitnessProgram.Data.Models;

    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public LoginModel(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }


        public class InputModel
        {

            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            //[Display(Name = "Remember Me?")]
            //public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (signInManager.IsSignedIn(User))
            {
                Response.Redirect("/");
            }
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(Input.UserName);

                var result = await signInManager.PasswordSignInAsync(Input.UserName, Input.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (await userManager.IsInRoleAsync(user, WebConstants.AdministratorRoleName))
                    {
                        returnUrl = Url.Content("~/Admin");
                    }
                    return LocalRedirect(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            return Page();
        }
    }
}
