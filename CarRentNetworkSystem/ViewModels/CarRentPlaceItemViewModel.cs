namespace ATHCarRentNetworkSystem.ViewModels
{
    public class CarRentPlaceItemViewModel
    {
        public List<CarRentPlanceViewModel> carRentPlanceViewModels { get; set; } = new List<CarRentPlanceViewModel>();

        public CarRentPlaceItemViewModel()
        {
            carRentPlanceViewModels.Add(new CarRentPlanceViewModel(1, "Fiat", "Blue", 200.99f));
            carRentPlanceViewModels.Add(new CarRentPlanceViewModel(2, "Opel", "Black", 150.47f));
        }

    }
}




