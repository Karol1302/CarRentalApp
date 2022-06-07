using Microsoft.AspNetCore.Identity;

namespace ATHCarRentNetworkSystem.Areas.MainAdmin.ViewModels.ApplicationUsers
{
    public class ApplicationUsersDetailsViewModel : IdentityUser
    {
        //public string Id { get; set; }
        //public string UserName { get; set; }
        //public string Email { get; set; }
        public int? WypozyczalniaId { get; set; }
        public string Wypozyczalnia { get; set; }
    }
}
