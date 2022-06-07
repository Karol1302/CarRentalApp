using ATHCarRentNetworkSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ATHCarRentNetworkSystem.Areas.MainAdmin.ViewModels
{
    public class WypozyczenieCreateViewModel
    {
        public int Id { get; set; }

        public DateTime DataUtworzenia { get; set; }
        public DateTime DataRozpoczecia { get; set; }
        public StatusWypozyczenia status { get; set; }
        public int SamochodId { get; set; }
        public SelectList Samochod { get; set; }
    }
}
