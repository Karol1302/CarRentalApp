using ATHCarRentNetworkSystem.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATHCarRentNetworkSystem.Models
{
    public class TypSamochodu : IEntity<int>
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public virtual ICollection<Samochod> Samochod { get; set; }
    }
}
