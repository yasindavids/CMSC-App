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

        private static int lastClaimId = 0;

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = ClaimStorage.Claims.FirstOrDefault(c => c.Id == id);

            if (claim != null)
            {
  
                ClaimStorage.Claims.Remove(claim);
            }

            return RedirectToAction("ManagerDash"); 
        }

        [HttpPost]
        public IActionResult DownloadReport(int id)
        {
            var claim = ClaimStorage.Claims.FirstOrDefault(c => c.Id == id);

            if (claim == null)
            {
             
                return NotFound();
            }

            var reportContent = $"Claim Report\n\n" +
                                $"Hours Worked: {claim.HoursWorked}\n" +
                                $"Hourly Rate: {claim.HourlyRate}\n" +
                                $"Total Pay: {claim.HourlyRate * claim.HoursWorked}\n" +
                                $"Additional Notes: {claim.Notes}\n" +
                                $"File: {claim.FileName}";

            var fileBytes = System.Text.Encoding.UTF8.GetBytes(reportContent);


            return File(fileBytes, "text/plain", $"{claim.Id}_Claim_Report.txt");
        }



        [HttpPost]
        public async Task<IActionResult> SubmitClaim(DashBoardModel model)
        {
            string[] allowedExtensions = { ".doc", ".pdf", ".xlsx", ".docx", ".txt" };
            string fileName = null;
            string filePath = null;

            if (ModelState.IsValid)
            {
                if (model.FileUpload != null && model.FileUpload.Length > 0)
                {
                    
                    var fileExtension = Path.GetExtension(model.FileUpload.FileName).ToLower();

                    
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("FileUpload", "Only .doc, .pdf, or .xlsx files are allowed.");
                        return View("Dashboard", model); 
                    }

                    fileName = model.FileUpload.FileName;
                    filePath = Path.Combine("uploads", fileName);
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(stream);
                    }
                }

                
                var claim = new Claim
                {
                    HoursWorked = model.HoursWorked,
                    HourlyRate = model.HourlyRate,
                    Notes = model.Notes,
                    FileName = fileName,
                    FilePath = filePath
                };

                ClaimStorage.Claims.Add(claim);

                return RedirectToAction("Index");
            }

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
