using CarRentalApp.Data;
using CarRentalApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApp.Controllers
{
    public class SamochodController : Controller
    {

        private readonly ApplicationDbContext _db;

        public SamochodController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Samochod> objList = _db.Samochody;
            return View(objList);
        }
    }
}
