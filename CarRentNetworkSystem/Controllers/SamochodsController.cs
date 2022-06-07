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
using AutoMapper;
using ATHCarRentNetworkSystem.ViewModels;
using ATHCarRentNetworkSystem.Repositories;
using ATHCarRentNetworkSystem.Services;

namespace ATHCarRentNetworkSystem.Controllers
{
    public class SamochodsController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly RepositoryService<Samochod> _samochods;
        private readonly RepositoryService<TypSamochodu> _typSamochodus;
        private readonly RepositoryService<Wypozyczalnia> _wypozyczalnias;
        IAuthorizationInitializer _authorizationInitializer;

        //public SamochodsController(ApplicationDbContext context, IMapper mapper)
        public SamochodsController(ApplicationDbContext context, IMapper mapper,
                                   IAuthorizationInitializer authorizationInitializer)
        {
            _mapper = mapper;
            _samochods = new(context);

            _typSamochodus = new(context);
            _wypozyczalnias = new(context);

            _authorizationInitializer= authorizationInitializer;
        }

        // GET: Samochods
        public async Task<IActionResult> Index()
        {
            /*            var applicationDbContext = _context.samochods.Include(s => s.TypSamochodu);
                        var Samochods = applicationDbContext.ToList();
                        var viewModel = Samochods.Select(r => _mapper.Map<SamochodsIndexViewModel>(r));
                        return View(viewModel);*/

            var Samochods = _samochods.GetAllRecords().Include(r=>r.TypSamochodu).ToList();
            var viewModel = Samochods.Select(r => _mapper.Map<SamochodsIndexViewModel>(r));
            return View(viewModel);
        }

        // GET: Samochods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samochod = await _samochods.GetAllRecords()
                .Include(s => s.TypSamochodu)
                .FirstOrDefaultAsync(m => m.Id == id);
            var viewModel = _mapper.Map<SamochodsDetailsViewModel>(samochod);
            if (samochod == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Samochods/Create
        public IActionResult Create()
        {
            SamochodsCreateViewModel viewModel = new SamochodsCreateViewModel()
            {
                TypSamochodu = new SelectList(_typSamochodus.GetAllRecords(), "Id", "Nazwa"),
                Wypozyczalnia = new SelectList(_wypozyczalnias.GetAllRecords(), "Id", "Name")
            };
           // ViewData["TypSamochoduId"] = new SelectList(_typSamochodus.GetAllRecords(), "Id", "Id");
            //ViewData[""]
            return View(viewModel);
        }

        // POST: Samochods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,CenaZaGodzine,Opis,TypSamochoduId")] Samochod samochod)
        {
          //  if (ModelState.IsValid)
            {
                _samochods.Add(samochod);
                _samochods.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypSamochoduId"] = new SelectList(_typSamochodus.GetAllRecords(), "Id", "Id", samochod.TypSamochoduId);
            return View(samochod);
        }

        // GET: Samochods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var samochod = _samochods.GetSingle((int) id);

            var viewModel = _mapper.Map<SamochodsEditViewModel>(samochod);
            viewModel.TypSamochoduSelectList = new SelectList(_typSamochodus.GetAllRecords(), "Id", "Nazwa");
            if (samochod == null)
            {
                return NotFound();
            }
            ViewData["TypSamochoduId"] = new SelectList(_typSamochodus.GetAllRecords(), "Id", "Id", samochod.TypSamochoduId);
            return View(viewModel);
        }

        // POST: Samochods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SamochodsEditViewModel viewModel)
        {
            var samochod = _samochods.GetSingle(viewModel.Id);
            _mapper.Map<SamochodsEditViewModel, Samochod>(viewModel, samochod);
            //if (ModelState.IsValid)
            {
                try
                {
                    _samochods.Edit(samochod);
                    _samochods.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SamochodExists(samochod.Id))
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
            ViewData["TypSamochoduId"] = new SelectList(_typSamochodus.GetAllRecords(), "Id", "Id", samochod.TypSamochoduId);
            return View(viewModel);
        }

        // GET: Samochods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samochod = await _samochods.GetAllRecords()
                .Include(s => s.TypSamochodu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (samochod == null)
            {
                return NotFound();
            }

            return View(samochod);
        }

        // POST: Samochods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var samochod = _samochods.GetSingle(id);
            _samochods.Delete(samochod);
            _samochods.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool SamochodExists(int id)
        {
            return _samochods.Exists(id);
        }
    }
}
