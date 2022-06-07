using ATHCarRentNetworkSystem.Repositories;

namespace ATHCarRentNetworkSystem.Models
{
    public class Wypozyczalnia : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Samochod> Samochody { get; set; }=new List<Samochod>();
        public ICollection<ApplicationUser> Admini { get; set; }=new List<ApplicationUser>();
        public ICollection<Wypozyczenie> Wypozyczenia { get; set; }=new List<Wypozyczenie>();
    }
}
