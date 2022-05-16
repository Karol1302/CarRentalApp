using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApp.Models
{
    public class TypSamochodu
    {
        [Key]
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public ICollection<Samochod> Samochody { get; set; }
    }
}
