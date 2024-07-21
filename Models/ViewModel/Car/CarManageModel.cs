using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCIDentity.Models.ViewModel.Car
{
    public class CarManageModel
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }
        public List<SelectListItem> UsersSelectList { get; internal set; }
    }
}
