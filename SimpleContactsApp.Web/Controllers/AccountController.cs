using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleContactsApp.Domain.Entities;
using SimpleContactsApp.Web.Models;

namespace SimpleContactsApp.Web.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly string aspBridgeUrl = "https://simplecontactswebapp-asp.azurewebsites.net/asp2netbridge.asp";
        private readonly HttpClient _httpClient;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IHttpClientFactory httpClientFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(InputModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    // Get the user ID after successful login
                    var user = await _signInManager.UserManager.FindByNameAsync(loginModel.Username);
                    HttpContext.Session.SetString("userId", user.Id.ToString());

                    //share the useId to the asp session using the asp bridge
                    var  bridgeRedirectUrl = $"{aspBridgeUrl}?userId={user.Id.ToString()}";
                    var aspResponse =  await _httpClient.GetAsync(bridgeRedirectUrl);
                    
                    return Ok(user.Id.ToString());
                }
            }

            return BadRequest();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(InputModel model)
        {
            var user = new User { UserName = model.Username };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var newUser = await _signInManager.UserManager.FindByNameAsync(model.Username);

                //After successful registration, share the useId to the asp session using the asp bridge
                var bridgeRedirectUrl = $"{aspBridgeUrl}?userId={user.Id.ToString()}";
                await _httpClient.GetAsync(bridgeRedirectUrl);

                return Ok(new { Success = true ,userId = newUser.Id.ToString() });
            }
            else
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                return BadRequest(errors);
            }
        }
    }

}
