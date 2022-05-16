using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApp.Models
{
    public class Wypozyczenie
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataUtworzenia { get; set; }
        public DateTime DataRozpoczecia { get; set; }
        public DateTime DataZakonczenia { get; set; }
        public StatusWypozyczenia Status { get; set; }
        public string Klient { get; set; }

    }
}
