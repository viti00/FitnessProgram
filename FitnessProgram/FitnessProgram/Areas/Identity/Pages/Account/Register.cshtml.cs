namespace FitnessProgram.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using FitnessProgram.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using static FitnessProgram.Global.GlobalConstants;

    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string UserName { get; set; }

            [Required]
            [StringLength(UserConstants.PasswordMaxLegth, MinimumLength = UserConstants.PasswordMinLegth)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [FromForm]
            [NotMapped]
            public IFormFile? File { get; set; }
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (signInManager.IsSignedIn(User))
            {
                Response.Redirect("/");
            }
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                ProfilePhoto profilePicture = null;

                if(Input.File != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Input.File.CopyToAsync(memoryStream);

                        if (memoryStream.Length < 2097152)
                        {
                            var newPhoto = new ProfilePhoto()
                            {
                                Bytes = memoryStream.ToArray(),
                                Description = Input.File.FileName,
                                FileExtension = Path.GetExtension(Input.File.FileName),
                                Size = Input.File.Length
                            };

                            profilePicture = newPhoto;
                        }

                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                        }
                    }
                }
                var user = new User
                {
                    Email = Input.Email,
                    UserName = Input.UserName,
                    ProfilePicture = profilePicture
                };

                var result = await userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
