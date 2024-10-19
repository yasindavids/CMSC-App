using _10269809_PROG6212_POE.Models;
using _10269809_PROG6212_POE.Views.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace _10269809_PROG6212_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        [HttpPost]
        public async Task<IActionResult> Upload(DashBoardModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the file is not null and has content
                if (model.FileUpload != null && model.FileUpload.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", model.FileUpload.FileName);

                    // Using OpenReadStream() to read the file contents
                    using (var stream = model.FileUpload.OpenReadStream())
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await stream.CopyToAsync(fileStream);  
                        }
                    }
                }

                return View("Success");
            }


            return View(model);
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
