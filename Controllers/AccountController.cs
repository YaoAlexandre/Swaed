using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Swaed.Helpers;
using Swaed.Models;
using Swaed.Services;
using Swaed.ViewModels;
using System.Text;

namespace Swaed.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Services.IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 Services.IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        private async Task<string> GenerateConfirmationEmailLink(ApplicationUser user, string token, string scheme)
        {
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, scheme);

            var emailBody = new StringBuilder();
            emailBody.Append("<p>Veuillez confirmer votre adresse e-mail en cliquant sur le lien ci-dessous :</p>");
            emailBody.Append("<p><a href='" + confirmationLink + "'>Lien de confirmation</a></p>");

            return emailBody.ToString();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var roles = await _userManager.GetRolesAsync(user);


                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("AdminDashboard", "Admin");
                    }
                    else if (roles.Contains("Volunteer"))
                    {
                        return RedirectToAction("VolunteerDashboard", "Volunteer");
                    }
                    else if (roles.Contains("Organization"))
                    {
                        return RedirectToAction("OrganizationDashboard", "Organization");
                    }
                    else
                    {
                        // Redirection par défaut pour les utilisateurs sans rôle spécifique
                        return RedirectToAction("DefaultDashboard", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user;

                if (model.AccountType == "Volunteer")
                {
                    user = new Volunteer { UserName = model.Email, Email = model.Email };
                }
                else if (model.AccountType == "Organization")
                {
                    user = new Organization { UserName = model.Email, Email = model.Email };
                }
                else
                {
                    // Gérer le cas où le type de compte n'est pas valide
                    return BadRequest("Invalid account type");
                }

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("UserMail", user.Email);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var emailSubject = "Confirmation de votre adresse e-mail";
                    var emailBody = await GenerateConfirmationEmailLink(user, token, "https");
                    await _emailSender.SendEmailAsync(user.Email, emailSubject, emailBody);


                    if (model.AccountType == "Volunteer")
                    {
                        // Redirection vers la page de complétion d'informations du volontaire
                        return RedirectToAction("VolunteerRegister", "Account", new { userId = user.Id });
                    }
                    else if (model.AccountType == "Organization")
                    {
                        // Redirection vers la page de complétion d'informations de l'organisation
                        return RedirectToAction("OrganizationRegister", "Account", new { userId = user.Id });
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult VolunteerRegister(string email)
        {
            var model = new Volunteer { Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VolunteerRegister(Volunteer model)
        {
            if (ModelState.IsValid)
            {
                var userMail = HttpContext.Session.GetString("UserMail");
                var user = await _userManager.FindByEmailAsync(userMail) as Volunteer;
                if (user == null)
                {
                    return NotFound();
                }

                user.FirstNameEn = model.FirstNameEn;
                user.FirstNameAr = model.FirstNameAr;
                user.LastNameEn = model.LastNameEn;
                user.LastNameAr = model.LastNameAr;
                user.Phone = model.Phone;
                user.EmirateId = model.EmirateId;
                user.EmirateIdExpiryDate = model.EmirateIdExpiryDate;
                user.Nationality = model.Nationality;
                user.Residency = model.Residency;
                user.Address = model.Address;
                user.Dob = model.Dob;
                user.Gender = model.Gender;

                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "Volunteer");
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult OrganizationRegister(string email)
        {
            var model = new Organization { Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> OrganizationRegister(Organization model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Email);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "Organization");
                    if (roleResult.Succeeded)
                    {
                        // Logique d'inscription de l'organisation ici
                        // ...
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                // Gérer l'erreur, par exemple rediriger vers une page d'erreur
                return RedirectToAction("Error", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // Gérer l'erreur, par exemple rediriger vers une page d'erreur
                return RedirectToAction("Error", "Home");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                // Rediriger vers la page de complétion d'informations
                return RedirectToAction("CompleteProfile", "Account");
            }
            else
            {
                // Gérer l'erreur, par exemple rediriger vers une page d'erreur
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
