using _10269809_PROG6212_POE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _10269809_PROG6212_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult ManagerDash()
        {
            return View();
        }

        public ActionResult SignUp()
        {
           
            return View();
        }

        
        public ActionResult DashBoard()
        {
          
            return View(); 
        }

        public IActionResult Index()
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
