using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
using UsersStateService.Interfaces;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
		private readonly IUsersStateService userStateService;

		public HomeController(IUsersStateService userStateService)
		{
			this.userStateService = userStateService;
		}

        public async Task<IActionResult> Index()
		{
			ViewData["State"] = await userStateService.GetUserStateAsync();
			await userStateService.SetUserStateAsync("Last visited page was Index");

			return View();
        }

        public async Task<IActionResult> About()
        {
			ViewData["State"] = await userStateService.GetUserStateAsync();
			await userStateService.SetUserStateAsync("Last visited page was About");

			ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> Contact()
        {
			ViewData["State"] = await userStateService.GetUserStateAsync();
			await userStateService.SetUserStateAsync("Last visited page was Contact");

			ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
