using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using reSports_Proyect_MM.Models;
using reSports_Proyect_MM.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace reSports_Proyect_MM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("/login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login(reSportsModel.Auth.UserAuth credentials)
        {
            var user = await APIServices.Login(credentials);
            if (user == null)
            {
                return View(credentials);
            }
            var claims = new List<Claim>
            {
                new Claim("username", user.Username),
                new Claim("TokenAPI", user.Token)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            APIServices.token = user.Token;
            return RedirectToAction("Index", "Publicacions");
        }

        [Route("/register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> Register(reSportsModel.Register usuarionuevo)
        {
            var user = await APIServices.Register(usuarionuevo);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("username", user.Username),
                    new Claim("TokenAPI", user.Token)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                APIServices.token = user.Token;
                return RedirectToAction("Index", "Publicacions");
            }
            return View(usuarionuevo);
        }



        [Route("/logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}