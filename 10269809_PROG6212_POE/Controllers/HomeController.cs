using _10269809_PROG6212_POE.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace _10269809_PROG6212_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;



        public ActionResult DashBoard()
        {

            return View(new DashBoardModel());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(DashBoardModel model)
        {
            string fileName = null;
            string filePath = null; // Declare a variable for the file path
            if (ModelState.IsValid)
            {
                if (model.FileUpload != null && model.FileUpload.Length > 0)
                {
                    fileName = model.FileUpload.FileName;
                    filePath = Path.Combine("uploads", fileName); // Store relative path
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(stream);
                    }
                }

                // Create a new Claim instance
                var claim = new Claim
                {
                    HoursWorked = model.HoursWorked,
                    HourlyRate = model.HourlyRate,
                    Notes = model.Notes,
                    FileName = fileName,
                    FilePath = filePath // Store the relative path
                };

                ClaimStorage.Claims.Add(claim);

                return RedirectToAction("Index"); // Return to homepage if successful
            }

            return View("Dashboard", model);  // Stay on Dashboard if unsuccessful
        }


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public ActionResult LogIn()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ManagerDash()
        {

            var claims = ClaimStorage.Claims;
            return View(claims);
        }

        public ActionResult SignUp()
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
