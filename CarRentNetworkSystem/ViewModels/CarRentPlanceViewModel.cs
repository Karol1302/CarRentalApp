using System.ComponentModel.DataAnnotations;

namespace ATHCarRentNetworkSystem.ViewModels
{
    public class CarRentPlanceViewModel
    {
        public int _id { get; set; }
        [Required]
        public string _model { get; set; }
        public string _color { get; set; }
        public float _rentPrice { get; set; }

        public CarRentPlanceViewModel(int id, string model, string color, float rentPrice)
        {
            _id = id;
            _model = model;
            _color = color;
            _rentPrice = rentPrice;
        }

        public CarRentPlanceViewModel()
        { }
    }
}
