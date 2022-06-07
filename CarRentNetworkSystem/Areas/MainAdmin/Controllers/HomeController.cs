#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ATHCarRentNetworkSystem.Data;
using ATHCarRentNetworkSystem.Models;
using Microsoft.AspNetCore.Authorization;
using ATHCarRentNetworkSystem.Areas.MainAdmin.ViewModels;
using AutoMapper;
using ATHCarRentNetworkSystem.Repositories;

namespace ATHCarRentNetworkSystem.Areas.MainAdmin.Controllers
{
    [Area("MainAdmin")]
    [Authorize (Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly RepositoryService<Wypozyczenie> _wypozyczenies;
        private readonly RepositoryService<Samochod> _samochods;

        public HomeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _wypozyczenies = new RepositoryService<Wypozyczenie>(context);
            _samochods = new RepositoryService<Samochod>(context);
        }

        // GET: MainAdmin/Home
        public async Task<IActionResult> Index()
        {
            var wypozyczenies = _wypozyczenies.GetAllRecords().Include(r=> r.Samochod);
            var viewModel = wypozyczenies.Select(r => _mapper.Map<WypozyczenieIndexViewModel>(r));
            //var applicationDbContext = _context.wypozyczenies.Include(w => w.Samochod);
            return View(viewModel);
        }

        // GET: MainAdmin/Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var wypozyczenie = await _wypozyczenies.GetAllRecords()
                .Include(w => w.Samochod)
                .FirstOrDefaultAsync(m => m.Id == id);

            var viewModel = _mapper.Map<WypozyczenieDetailsViewModel>(wypozyczenie);

            if (wypozyczenie == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: MainAdmin/Home/Create
        public IActionResult Create()
        {
            List<StatusWypozyczenia> statusWypozyczenias = new List<StatusWypozyczenia>();
            statusWypozyczenias.Add(StatusWypozyczenia.Aktywne);
            statusWypozyczenias.Add(StatusWypozyczenia.Rezerwacja);
            statusWypozyczenias.Add(StatusWypozyczenia.Zakonczone);
            var viewModel = new WypozyczenieCreateViewModel()
            {
                Samochod = new SelectList(_samochods.GetAllRecords(), "Id", "Nazwa")
                
            };
            ViewData["Samochod"] = new SelectList(_samochods.GetAllRecords(), "Id", "Nazwa");

            return View(viewModel);
        }

        // POST: MainAdmin/Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,DataUtworzenia,DataRozpoczecia,status,SamochodId")] Wypozyczenie wypozyczenie)
        public async Task<IActionResult> Create(Wypozyczenie wypozyczenie)
        {
            //if (ModelState.IsValid)
            {
                _wypozyczenies.Add(wypozyczenie);
                _wypozyczenies.Save();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SamochodId"] = new SelectList(_context.samochods, "Id", "Nazwa", wypozyczenie.SamochodId);
            return View(wypozyczenie);
        }

        // GET: MainAdmin/Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wypozyczenie = _wypozyczenies.GetSingle((int)id);
            var viewModel = _mapper.Map<WypozyczenieEditViewModel>(wypozyczenie);
            viewModel.SamochodSelectList = new SelectList(_samochods.GetAllRecords(), "Id", "Nazwa");

            if (wypozyczenie == null)
            {
                return NotFound();
            }

            ViewData["SamochodId"] = new SelectList(_samochods.GetAllRecords(), "Id", "Id", wypozyczenie.SamochodId);
            return View(viewModel);
        }

        // POST: MainAdmin/Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(WypozyczenieEditViewModel viewModel)
        {
            var wypozyczenie = _wypozyczenies.GetSingle(viewModel.Id);
            _mapper.Map<WypozyczenieEditViewModel, Wypozyczenie>(viewModel, wypozyczenie);

            //if (ModelState.IsValid)
            {
                try
                {
                    _wypozyczenies.Edit(wypozyczenie);
                    //_context.Update(wypozyczenie);
                    _wypozyczenies.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WypozyczenieExists(wypozyczenie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SamochodId"] = new SelectList(_wypozyczenies.GetAllRecords(), "Id", "Nazwa", wypozyczenie.SamochodId);
            return View(viewModel);
        }

        // GET: MainAdmin/Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wypozyczenie = await _context.wypozyczenies
                .Include(w => w.Samochod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wypozyczenie == null)
            {
                return NotFound();
            }

            return View(wypozyczenie);
        }

        // POST: MainAdmin/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wypozyczenie = await _context.wypozyczenies.FindAsync(id);
            _context.wypozyczenies.Remove(wypozyczenie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WypozyczenieExists(int id)
        {
            return _context.wypozyczenies.Any(e => e.Id == id);
        }
    }
}
