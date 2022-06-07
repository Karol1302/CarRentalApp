namespace ATHCarRentNetworkSystem.ViewModels
{
    public class SamochodsDetailsViewModel
    {
        public int Id { get; set; }

        //[DataType(DataType.EmailAddress)]
        public string Nazwa { get; set; }
        public float CenaZaGodzine { get; set; }
        public string Opis { get; set; }



        //public virtual ICollection<Wypozyczenie> Wypozyczenia { get; set; } do podmiany z viewmodelem jak bedzie gotowy
        public string TypSamochodu { get; set; }
    }
}
