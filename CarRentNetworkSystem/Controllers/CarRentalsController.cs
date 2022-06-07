using ATHCarRentNetworkSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ATHCarRentNetworkSystem.Controllers
{
    public class CarRentalsController : Controller
    {
        public static CarRentPlaceItemViewModel _model { get; set; } = new CarRentPlaceItemViewModel();
        
        [HttpGet]
        public IActionResult Index()
        {
            return View(_model);
        }

        [HttpGet("id")]
        public IActionResult Details(int id)
        {
            return View(_model.carRentPlanceViewModels.FirstOrDefault(x=>x._id == id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CarRentPlanceViewModel model)
        {
            _model.carRentPlanceViewModels.Add(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
