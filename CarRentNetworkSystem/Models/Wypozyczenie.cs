using ATHCarRentNetworkSystem.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATHCarRentNetworkSystem.Models
{
    public class Wypozyczenie : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataUtworzenia { get; set; }
        public DateTime DataRozpoczecia { get; set; }
        public StatusWypozyczenia status { get; set; }

        [ForeignKey("Samochod")]
        public virtual int SamochodId { get; set; }
        public virtual Samochod Samochod { get; set; }

        public int WypozyczalniaId { get; set; }
        public Wypozyczalnia Wpozyczalnia { get; set; }
    }

    public enum StatusWypozyczenia
    {
        Rezerwacja,
        Aktywne,
        Zakonczone
    }
}
