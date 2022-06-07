using ATHCarRentNetworkSystem.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ATHCarRentNetworkSystem.Models
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        public int? WypozyczalniaId { get; set; }
        public Wypozyczalnia? Wypozyczalnia { get; set; }
    }
}
