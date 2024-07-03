using JuliePro.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JuliePro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index","Trainer");
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            var cookie = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));
            var name = CookieRequestCultureProvider.DefaultCookieName;

            Response.Cookies.Append(name, cookie, new CookieOptions()
            {
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            });

            return LocalRedirect(returnUrl);
        }
    }
}