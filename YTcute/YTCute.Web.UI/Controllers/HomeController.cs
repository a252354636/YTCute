using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YTCute.Web.UI.Filter;
using YTCute.Web.UI.Models;
using YTCute.Web.UI.Services;

namespace YTCute.Web.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ITestService _testSvc;
        private readonly IIdentityParser<ApplicationUser> _appUserParser;
        public HomeController(ITestService testSvc, IIdentityParser<ApplicationUser> appUserParser)
        {
            _appUserParser = appUserParser;
            _testSvc = testSvc;
        }
        public async Task<IActionResult> Index()
        {
            var user = _appUserParser.Parse(HttpContext.User);
            var str = await _testSvc.GetTest(user, 1.ToString());
            ViewData["service"] = str;
            return View();
        }
        //private async Task<string> GetUserTokenAsync()
        //{
        //    var context = _httpContextAccesor.HttpContext;
        //    return await context.GetTokenAsync("access_token");
        //}
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
