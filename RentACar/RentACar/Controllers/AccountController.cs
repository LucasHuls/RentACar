using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACar.Models;
using RentACar.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentACar.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logoff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Login()
        {
            if (!_roleManager.RoleExistsAsync(Utility.Helper.Admin).GetAwaiter().GetResult())
            {
                //await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.Supervisor));
                await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.User));
            }
            if (!_db.Users.Any()) // If no users exists create default admin
            {
                ApplicationUser defaultUser = new()
                {
                    UserName = "beheerder@gmail.com",
                    Email = "beheerder@gmail.com",
                    FirstName = "Beheerder",
                    MiddleName = null,
                    LastName = "Account",
                    City = "Hengelo",
                    Adress = "Gieterij 200",
                    ZipCode = "1234DD",
                    BirthDate = DateTime.Now
                };
                var result = await _userManager.CreateAsync(defaultUser, "DefaultBeheerder");
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(defaultUser, "Supervisor");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Inloggen mislukt");
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!_roleManager.RoleExistsAsync(Utility.Helper.Admin).GetAwaiter().GetResult())
            {
                //await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.Supervisor));
                await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.User));
            }
            if (ValidateIfUserIs18(model.Geboortedatum))
            {
                Console.WriteLine("18+");
            }
            if (ModelState.IsValid)
            {
                if (ValidateIfUserIs18(model.Geboortedatum))
                {
                    if (ValidateZipCode(model.Postcode))
                    {
                        ApplicationUser user = new ApplicationUser()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FirstName = model.Voorletters,
                            MiddleName = model.Tussenvoegsels,
                            LastName = model.Achternaam,
                            City = model.Woonplaats,
                            Adress = model.Adres,
                            ZipCode = model.Postcode,
                            BirthDate = model.Geboortedatum,
                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, model.RoleName);
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("Login", "Account");
                        }
                        // Add all errors to the modelstate
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Postcode is niet geldig! Gebruik bijvoorveeld: 1234AB");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Voor het aanmaken van een account moet je 18 jaar of ouder zijn! Met de opgegeven gegevens is dit niet het geval.");
                }
            }
            return View();
        }

        private static bool ValidateZipCode(string zipcode)
        {
            if (Regex.IsMatch(zipcode.ToUpper(), @"[1-9]\d{3}[A-Za-z]{2}")) //Check the zipcode with the following format:
            {
                return true;
            }
            else
                return false;
        }

        private static bool ValidateIfUserIs18(DateTime birthday)
        {
            if (birthday.AddYears(18) <= DateTime.Now) //Check the zipcode with the following format:
            {
                return true;
            }
            else
                return false;
        }
    }
}
