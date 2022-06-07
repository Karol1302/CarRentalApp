using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ATHCarRentNetworkSystem.Areas.MainAdmin.ViewModels.ApplicationUsers
{
    public class ApplicationUsersEditViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public int  WypozyczalniaId{ get; set; }
        public SelectList WypozyczalniaSelectList { get; set; } 
        //public MultiSelectList? RolesSelectList { get; set; }
    }
}
