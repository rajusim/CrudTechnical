using Logic.TechnicalAssement.App.Models;

namespace Logic.TechnicalAssement.App.ViewModels
{
    public class LeaveDetailViewModel
    {
        public LeaveViewModel LeaveViewModel { get; set; } = new LeaveViewModel();
        public string PageTitle { get; set; } = string.Empty;
        public List<LeaveViewModel> LeaveRequests { get; set; } = new List<LeaveViewModel>();

    }
}
