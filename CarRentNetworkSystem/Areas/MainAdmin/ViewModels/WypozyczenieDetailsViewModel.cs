namespace ATHCarRentNetworkSystem.Areas.MainAdmin.ViewModels
{
    public class WypozyczenieDetailsViewModel
    {
        public int Id { get; set; }

        public DateTime DataUtworzenia { get; set; }
        public DateTime DataRozpoczecia { get; set; }
        public string status { get; set; }
        public string Samochod { get; set; }
    }
}
