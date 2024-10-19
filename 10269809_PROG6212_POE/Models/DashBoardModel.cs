using System.ComponentModel.DataAnnotations;
using System.Security.Claims;


namespace _10269809_PROG6212_POE.Models
{
    public class DashBoardModel
    {
        
            [Required(ErrorMessage = "Please enter hours worked.")]
            [Range(1, int.MaxValue, ErrorMessage = "Hours worked must be a number.")]
            public int HoursWorked { get; set; }

            [Required(ErrorMessage = "Please enter an hourly rate.")]
            [Range(1, int.MaxValue, ErrorMessage = "Hourly rate must be a number.")]
            public int HourlyRate { get; set; }

            public string Notes { get; set; }

            public IFormFile FileUpload { get; set; }


    }


}
