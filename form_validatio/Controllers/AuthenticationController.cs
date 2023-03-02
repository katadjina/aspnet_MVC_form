using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using form_validation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace form_validation.Controllers
{
    public class AuthenticationController : Controller
    {



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login(LoginForm form)
        {

            ValidationLoginCustom(form, ModelState);

            if (!ModelState.IsValid) return View(form);
            return RedirectToAction("Index", "Home");
        }



        private static void ValidationLoginCustom (LoginForm form, ModelStateDictionary modelstate)
        {
            //first, verification of input fields are empty
            if (string.IsNullOrEmpty(form.Email))
            {
                modelstate.AddModelError(nameof(form.Email), "Email can not be empty");
            }
            if (string.IsNullOrEmpty(form.Password))
            {
                modelstate.AddModelError(nameof(form.Password), "Password can not be empty");
                return;
            }

            //additional rules for the password
            if (!Regex.IsMatch(form.Password, "[0-9]"))
            {
                modelstate.AddModelError(nameof(form.Password), "Password has to contain at least one number");
            }
            if (!Regex.IsMatch(form.Password, "[a-z]"))
            {
                modelstate.AddModelError(nameof(form.Password), "Password has to contain at least one lowercase letter");
            }
            if (!Regex.IsMatch(form.Password, "[A-Z]"))
            {
                modelstate.AddModelError(nameof(form.Password), "Password has to contain at least one uppercase letter");
            }
            if (!Regex.IsMatch(form.Password, "[=+/-/.//?*]"))
            {
                modelstate.AddModelError(nameof(form.Password), "Password has to contain at least one special character");
            }
            if (form.Password.Length < 8)
            {
                modelstate.AddModelError(nameof(form.Password), "Password has to be at least 8 characters long");
            }
            if (form.Password.Length > 32)
            {
                modelstate.AddModelError(nameof(form.Password), "Password is too long");
            }
        }






    }
}

