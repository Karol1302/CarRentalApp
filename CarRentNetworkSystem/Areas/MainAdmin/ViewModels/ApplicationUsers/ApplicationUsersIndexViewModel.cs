using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ATHCarRentNetworkSystem.Areas.MainAdmin.ViewModels.ApplicationUsers
{
    public class ApplicationUsersIndexViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Wypozyczalnia { get; set; }
    }
}
