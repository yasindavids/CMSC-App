using _10269809_PROG6212_POE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _10269809_PROG6212_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(DashBoardModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.FileUpload != null && model.FileUpload.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", model.FileUpload.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(stream);
                    }
                }

                return RedirectToAction("DashBoard"); // This should redirect to the DashBoard action
            }

            return View("Index", model); // Return to the view with the same model if there are validation errors
        }





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

       


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
