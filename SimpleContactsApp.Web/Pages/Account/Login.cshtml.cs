using System.ComponentModel.DataAnnotations;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleContactsApp.Domain.Entities;
using SimpleContactsApp.Web.Models;

namespace SimpleContactsApp.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;

        public LoginModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }
        
        [BindProperty]
        public InputModel Input { get; set; }

        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                // Redirect to home page if already logged in
                return RedirectToPage("/Index"); 
            }

            // Clear session data
            HttpContext.Session.Clear();
            
            return Page();
        }

        /*
         * Here we will try to log in using the username and password provided by user,
         * if successful we will use the user name to retrieve user information and store the userId to share it with the backend via Azure Redis cache. 
         */
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //After successful login, store the userId into the current session
                    var user = await _signInManager.UserManager.FindByNameAsync(Input.Username);
                    HttpContext.Session.SetString("UserId", user.Id.ToString());

                    // Redirect to home page on successful login
                    return RedirectToPage("/Index"); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return Page();
        }
    }
}