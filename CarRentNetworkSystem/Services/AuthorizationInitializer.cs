using ATHCarRentNetworkSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace ATHCarRentNetworkSystem.Services
{
    public interface IAuthorizationInitializer
    {
        void GenerateAdminAndRoles();
    }
    public class AuthorizationInitializer : IAuthorizationInitializer
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly RoleManager<IdentityRole> _roleManager;
        public AuthorizationInitializer(UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public void GenerateAdminAndRoles()
        {
            var admin = _userManager.FindByEmailAsync("admin@ath.eu").Result;
            if (admin == null)
            {
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "admin@ath.eu",
                    Email = "admin@ath.eu",
                }, "Az123456$");

                admin = _userManager.FindByEmailAsync("admin@ath.eu").Result;
                var code = _userManager.GenerateEmailConfirmationTokenAsync(admin).Result;
                var result = _userManager.ConfirmEmailAsync(admin, code);

            }
            if (_roleManager.Roles.Count() == 0)
            {
                _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                _roleManager.CreateAsync(new IdentityRole() { Name = "Moderator" });
                _roleManager.CreateAsync(new IdentityRole() { Name = "Klient" });
                _roleManager.CreateAsync(new IdentityRole() { Name = "Staff" });

                _userManager.AddToRoleAsync(admin, "Admin");
                _userManager.AddToRoleAsync(admin, "Staff");
            }
        }
    }
}
