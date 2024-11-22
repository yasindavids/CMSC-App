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
            if (ModelState.IsValid)
            {
                // Validate file upload
                if (model.FileUpload == null || model.FileUpload.Length == 0)
                {
                    ModelState.AddModelError("FileUpload", "A file must be uploaded.");
                }
                else
                {
                    string fileName = model.FileUpload.FileName;
                    string filePath = Path.Combine("uploads", fileName); // Relative path
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(stream);
                    }

                    // Create a new Claim instance
                    var claim = new Claim
                    {
                   
                    HoursWorked = model.HoursWorked,
                        HourlyRate = model.HourlyRate,
                        Notes = model.Notes,
                        FileName = fileName,
                        FilePath = filePath // Store relative path
                    };

                    ClaimStorage.Claims.Add(claim);

                    return RedirectToAction("Index");  // Redirect on success
                }
            }

            // If validation fails, return the view with validation errors
            return View("Dashboard", model);
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
