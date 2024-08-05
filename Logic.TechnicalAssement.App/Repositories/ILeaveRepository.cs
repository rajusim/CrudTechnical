using Logic.TechnicalAssement.App.Models;
using Newtonsoft.Json;

namespace Logic.TechnicalAssement.App.Repositories
{
    public interface ILeaveRepository
    {
        List<LeaveViewModel>? GetAllLeaveRequests();
        int UpdateLeaveRequests(LeaveViewModel leaveViewModel);
        LeaveViewModel? GetLeaveViewModel(int leaveId);
        void RemoveLeaveViewModel(int leaveId);
    }
    public class LeaveRepository : ILeaveRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LeaveRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<LeaveViewModel>? GetAllLeaveRequests()
        {
            var lvm = new List<LeaveViewModel>();
            string lvmString = _httpContextAccessor.HttpContext.Session.GetString("LeaveRequests") ?? string.Empty;
            if (!string.IsNullOrEmpty(lvmString))
                lvm = (JsonConvert.DeserializeObject<List<LeaveViewModel>>(lvmString)).OrderBy(c => c.Id).ToList();

            return lvm;
        }

        public LeaveViewModel? GetLeaveViewModel(int leaveId)
        {
            LeaveViewModel lvm = null;
            var lvmList = GetAllLeaveRequests();
            if (lvmList != null && lvmList.Count > 0)
                lvm = lvmList.FirstOrDefault(x => x.Id == leaveId);
            return lvm;
        }

        public void RemoveLeaveViewModel(int leaveId)
        {
            var lvmList = GetAllLeaveRequests();
            if (lvmList != null && lvmList.Count > 0)
                lvmList.RemoveAll(x => x.Id == leaveId);

            var leaveRequestsString = JsonConvert.SerializeObject(lvmList);
            if (leaveRequestsString != null && _httpContextAccessor.HttpContext != null)
                _httpContextAccessor.HttpContext.Session.SetString("LeaveRequests", leaveRequestsString);
        }


        public int UpdateLeaveRequests(LeaveViewModel leaveViewModel)
        {
            if (leaveViewModel.Id > 0)
                RemoveLeaveViewModel(leaveViewModel.Id);

            var lvmList = GetAllLeaveRequests();
            if (leaveViewModel.Id == 0)
            {
                int mx = 0;
                if (lvmList.Count > 0)
                    mx = lvmList.Max(x => x.Id);
                leaveViewModel.Id = mx + 1;
            }
            lvmList.Add(leaveViewModel);
            var leaveRequestsString = JsonConvert.SerializeObject(lvmList);
            if (leaveRequestsString != null && _httpContextAccessor.HttpContext != null)
                _httpContextAccessor.HttpContext.Session.SetString("LeaveRequests", leaveRequestsString);
            return leaveViewModel.Id;
        }

    }

}
