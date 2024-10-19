using _10269809_PROG6212_POE.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace _10269809_PROG6212_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static readonly List<DashBoardModel> _claims = new List<DashBoardModel>();

        public ActionResult DashBoard()
        {

            return View(new DashBoardModel());
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

                model.UploadedFileName = model.FileUpload.FileName;

                _claims.Add(model);

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

        [HttpGet]
        public ActionResult ManagerDash()
        {

            return View(_claims);
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
