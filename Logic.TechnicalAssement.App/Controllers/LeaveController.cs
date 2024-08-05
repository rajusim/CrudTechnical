using Logic.TechnicalAssement.App.Models;
using Logic.TechnicalAssement.App.Repositories;
using Logic.TechnicalAssement.App.Services;
using Logic.TechnicalAssement.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Logic.TechnicalAssement.App.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILogger<LeaveController> _logger;
        private readonly ILeaveRepository _repository;
        private readonly IValidationRule _validationRule;

        public LeaveController(ILogger<LeaveController> logger, ILeaveRepository repository, IValidationRule validationRule)
        {
            _logger = logger;
            _repository = repository;
            _validationRule = validationRule;
        }
        [Route("")]
        [Route("Leave")]
        [Route("Leave/Index")]
        public IActionResult Index()
        {
            var lvmCollection = new LeaveDetailViewModel();
            lvmCollection.PageTitle = "Leaves";
            lvmCollection.LeaveViewModel.Id = 0;
            var lvmList = _repository.GetAllLeaveRequests();
            if (lvmList != null && lvmList.Count > 0)
                lvmCollection.LeaveRequests = lvmList;

            return View(lvmCollection);
        }
        [Route("Leave/Index/{id}")]
        public IActionResult Index(int id)
        {
            var lvmCollection = new LeaveDetailViewModel();
            lvmCollection.PageTitle = "Leaves";
            var lvmList = _repository.GetAllLeaveRequests();
            if (lvmList.Count > 0 && id > 0)
                lvmCollection.LeaveViewModel = _repository.GetLeaveViewModel(id);
            else
                lvmCollection.LeaveViewModel.Id = 0;
            if (lvmList != null && lvmList.Count > 0)
                lvmCollection.LeaveRequests = lvmList;

            return View(lvmCollection);
        }

        public IActionResult Remove(int id)
        {
            _repository.RemoveLeaveViewModel(id);

            var lvmCollection = new LeaveDetailViewModel();
            var lvmList = _repository.GetAllLeaveRequests();
            if (lvmList.Count > 0)
                lvmCollection.LeaveViewModel = _repository.GetLeaveViewModel(id);
            _logger.LogInformation("Leave Request Removed");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RegisterLeave(LeaveDetailViewModel ldvm)
        {
            string message = string.Empty;
            if (ldvm == null)
                message = "No Data" ;

            if (_validationRule.IsValidEmail(ldvm.LeaveViewModel.Email))
            {
                message = "Enter Valid Email";
                _logger.LogInformation(message);
                return Json(new { result = message });

            }
            message = _validationRule.IsValidDates(ldvm.LeaveViewModel.StartDate, 
                                                    ldvm.LeaveViewModel.EndDate, 
                                                    ldvm.LeaveViewModel.IsHalfDay);

            if (message != "Valid")
            {
                _logger.LogInformation(message);
                return Json(new { result = message });
            }
            var id = _repository.UpdateLeaveRequests(ldvm.LeaveViewModel);

            var lvmCollection = new LeaveDetailViewModel();
            lvmCollection.PageTitle = "Leaves";
            var lvm = _repository.GetAllLeaveRequests();
            if (lvm != null && lvm.Count > 0)
                lvmCollection.LeaveRequests = lvm;

            _logger.LogInformation("Leave Request Registred");

            return Json(new { result = "Redirect", Id = string.Format("{0}", id) });
        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult GetLeaveRequests()
        {
            return PartialView("_LeaveRequestList");
        }

    }
}
