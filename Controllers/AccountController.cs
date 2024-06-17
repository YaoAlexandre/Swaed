using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Swaed.Helpers;
using Swaed.Models;
using Swaed.Services;
using Swaed.ViewModels;
using System.Text;
using System.Web;

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
                    //HttpContext.Session.SetString("UserMail", user.Email);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var emailSubject = "Confirmation de votre adresse e-mail";
                    var emailBody = await GenerateConfirmationEmailLink(user, token, "https", model.AccountType);
                    await _emailSender.SendEmailAsync(user.Email, emailSubject, emailBody);

					TempData["SuccessMessage"] = "Account successfully created! Please confirm your email to continue.";
                    return View();

				}
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    TempData["ErrorMessage"] = HttpUtility.HtmlEncode(error.Description);
                }
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult VolunteerRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VolunteerRegister(Volunteer model, string id)
        {
            if (ModelState.IsValid)
            {
                //var userMail = HttpContext.Session.GetString("UserMail");
                var user = await _userManager.FindByIdAsync(id) as Volunteer;
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
						TempData["SuccessMessage"] = "Information successfully saved.";
						return RedirectToAction("Index", "Volunteer");
                    }
                    else
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
							TempData["ErrorMessage"] = HttpUtility.HtmlEncode(error.Description);
						}
                    }
                }
                else
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        TempData["ErrorMessage"] = HttpUtility.HtmlEncode(error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult OrganizationRegister(string email)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrganizationRegister(Organization model, string id)
        {
            if (ModelState.IsValid)
            {
                //var userMail = HttpContext.Session.GetString("UserMail");
                var user = await _userManager.FindByIdAsync(id) as Organization;
                if (user == null)
                {
                    return NotFound();
                }
                user.FullNameEn = model.FullNameEn;
                user.FullNameAr = model.FullNameAr;
                user.Phone = model.Phone;
                user.RefNumber = model.RefNumber;
                user.Logo = model.Logo;
                user.Sector = model.Sector;
                user.Description = model.Description;
                user.ContactNameEn = model.ContactNameEn;
                user.ContactNameAr = model.ContactNameAr;
                user.ContactPhone = model.ContactPhone;
                user.City = model.City;
                user.Address = model.Address;
                user.Website = model.Website;
                user.Categories = model.Categories;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "Organization");
                    if (roleResult.Succeeded)
                    {
						TempData["SuccessMessage"] = "Information successfully saved.";
						return RedirectToAction("Index", "Organization");
                    }
                    else
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
							TempData["ErrorMessage"] = HttpUtility.HtmlEncode(error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
						TempData["ErrorMessage"] = HttpUtility.HtmlEncode(error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token, string accountType)
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
                if (accountType == "Volunteer")
                {
                    // Redirection vers la page de complétion d'informations du volontaire
                    return RedirectToAction("VolunteerRegister", "Account", new {id = user.Id});
                }
                else if (accountType == "Organization")
                {
                    // Redirection vers la page de complétion d'informations de l'organisation
                    return RedirectToAction("OrganizationRegister", "Account", new { id = user.Id });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                // Gérer l'erreur, par exemple rediriger vers une page d'erreur
                return RedirectToAction("Error", "Home");
            }
        }

        private async Task<string> GenerateConfirmationEmailLink(ApplicationUser user, string token, string scheme, string accountType)
        {
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token, accountType = accountType }, scheme);

            var emailBody = new StringBuilder();
            emailBody.Append("<p>Veuillez confirmer votre adresse e-mail en cliquant sur le lien ci-dessous :</p>");
            emailBody.Append("<p><a href='" + confirmationLink + "'>Lien de confirmation</a></p>");

            return emailBody.ToString();
        }

    }
}
