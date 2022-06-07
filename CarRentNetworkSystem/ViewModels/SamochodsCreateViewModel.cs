using Microsoft.AspNetCore.Mvc.Rendering;

namespace ATHCarRentNetworkSystem.ViewModels
{
    public class SamochodsCreateViewModel
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public float CenaZaGodzine { get; set; }
        public string Opis { get; set; }
        public int TypSamochoduId { get; set; }
        public SelectList TypSamochodu { get; set; }
        public int WypozyczalniaId { get; set; }
        public SelectList Wypozyczalnia { get; set; }
    }
}
