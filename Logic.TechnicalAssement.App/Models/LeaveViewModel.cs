using System.ComponentModel.DataAnnotations;

namespace Logic.TechnicalAssement.App.Models
{
    public class LeaveViewModel
    {
        public int Id { get; set; }
        [Required]
        public LeaveTypeEnum LeaveType { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public bool IsHalfDay {  get; set; }
    }

   
    public enum LeaveTypeEnum
    {
        [Display(Name = "Annual Leave")]
        AnnualLeave,
        [Display(Name = "Sick Leave")]
        SickLeave,
        [Display(Name = "Compassionate Leave")]
        CompassionateLeave,
        [Display(Name = "Unpaid Leave")]
        UnpaidLeave,
    }
}
