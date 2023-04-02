using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebApplicationDemo.Models;

namespace WebApplicationDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindModel _context;

        // Basic tutorial to follow during Holy Week and try some stuff
        // https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application


        /*
        // Be careful when adding context. Only can be one constructor.
        // https://social.msdn.microsoft.com/Forums/en-US/8b4e4d69-7416-4a06-a86b-9081c7a70a0e/one-or-more-constructors-in-home-controller-error?forum=aspdotnetcore
        public HomeController(NorthwindModel context)
        {
            _context = context;
        }
        */

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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