using ATHCarRentNetworkSystem.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATHCarRentNetworkSystem.Models
{
    public class Samochod : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //[DataType(DataType.EmailAddress)]
        public string Nazwa { get; set; }
        public float CenaZaGodzine { get; set; }
        public string Opis { get; set; }


        
        public virtual ICollection<Wypozyczenie> Wypozyczenia { get; set; }

        [ForeignKey("TypSamochodu")]
        public virtual int TypSamochoduId { get; set; } 
        public virtual TypSamochodu TypSamochodu { get; set; }


        public int WypozyczalniaId { get; set; }
        public Wypozyczalnia Wpozyczalnia { get; set; }
    }
}
