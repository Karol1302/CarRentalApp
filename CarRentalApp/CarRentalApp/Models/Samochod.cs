using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApp.Models
{
    public class Samochod
    {
        [Key]
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public decimal CenaZaGodzine { get; set; }
        public string Opis { get; set; }
        public string TypSamochodu { get; set; }
    }
}
