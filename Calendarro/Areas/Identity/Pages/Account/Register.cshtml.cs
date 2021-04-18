using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Calendarro.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Calendarro.Models.Database;

namespace Calendarro.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<CalendarroUser> _signInManager;
        private readonly UserManager<CalendarroUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly CalendarroDBContext _calendarroContext;

        public RegisterModel(
            UserManager<CalendarroUser> userManager,
            SignInManager<CalendarroUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            CalendarroDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _calendarroContext = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Imię")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nazwisko")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Miasto")]
            public string City { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Ulica")]
            public string Street { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Numer domu")]
            public string HouseNumber { get; set; }

            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Numer telefonu")]
            public string PhoneNumber { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Opis")]
            public string Description { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Hasło musi mieć przynajmniej {2}.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Hasło")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potwierz hasło")]
            [Compare("Password", ErrorMessage = "Proszę podać takie samo hasło.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new CalendarroUser
                {
                    UserName = Input.Email,
                    Email = Input.Email
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    var calUser = new CalendarroUsers
                    {
                        Token = user.Id,
                        CreateDate = DateTime.Now,
                        City = Input.City,
                        Description = Input.Description,
                        EMail = Input.Email,
                        Name = Input.FirstName,
                        HouseNumber = Input.HouseNumber,
                        PhoneNumber = Input.PhoneNumber,
                        SurName = Input.LastName,
                        Street = Input.Street
                    };

                    await _calendarroContext.CalendarroUsers.AddAsync(calUser);
                    await _calendarroContext.SaveChangesAsync();
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
