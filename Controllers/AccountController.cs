using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swaed.Helpers;
using Swaed.Models;
using Swaed.ViewModels;

namespace Swaed.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("UserMail", user.Email);
                    if (model.AccountType == "Volunteer")
                    {
                        // Redirection vers la page de complétion d'informations du volontaire
                        return RedirectToAction("VolunteerRegister", new { userId = user.Id });
                    }
                    else if (model.AccountType == "Organization")
                    {
                        // Redirection vers la page de complétion d'informations de l'organisation
                        return RedirectToAction("OrganizationRegister", new { userId = user.Id });
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
                var user = await _userManager.FindByEmailAsync(userMail);
                if (user == null)
                {
                    return NotFound();
                }

                var volunteer = new Volunteer
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstNameEn = model.FirstNameEn,
                    FirstNameAr = model.FirstNameAr,
                    LastNameEn = model.LastNameEn,
                    LastNameAr = model.LastNameAr,
                    Phone = model.Phone,
                    EmirateId = model.EmirateId,
                    EmirateIdExpiryDate = model.EmirateIdExpiryDate,
                    Nationality = model.Nationality,
                    Residency = model.Residency,
                    Address = model.Address,
                    Dob = model.Dob,
                    Gender = model.Gender
                };

                var updateResult = await _userManager.UpdateAsync(volunteer);

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
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
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
    }
}
