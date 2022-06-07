using ATHCarRentNetworkSystem.Areas.MainAdmin.ViewModels.ApplicationUsers;
using ATHCarRentNetworkSystem.Data;
using ATHCarRentNetworkSystem.Models;
using ATHCarRentNetworkSystem.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ATHCarRentNetworkSystem.Areas.MainAdmin.Controllers
{
    [Area("MainAdmin")]
    public class ApplicationUsersController : Controller
    {
        private readonly StringRepositoryService<ApplicationUser> _applicationUsers;
        private readonly RepositoryService<Wypozyczalnia> _wypozyczalnias;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public ApplicationUsersController(ApplicationDbContext context, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _applicationUsers = new(context);
            _wypozyczalnias = new(context);
            _mapper = mapper;
            _roleManager = roleManager;
        }

        // GET: ApplicationUsersController
        public ActionResult Index()
        {
            var users = _applicationUsers.GetAllRecords().Include(r => r.Wypozyczalnia).ToList();
            var viewModel = users.Select(r => _mapper.Map<ApplicationUsersIndexViewModel>(r));
            return View(viewModel);
        }

        // GET: ApplicationUsersController/Details/5
        public ActionResult Details(string? id)
        {
            if (id is null)
                return NotFound();

            var user = _applicationUsers.GetAllRecords().Include(r => r.Wypozyczalnia).FirstOrDefault(r => r.Id == id);
            var viewModel = _mapper.Map<ApplicationUsersDetailsViewModel>(user);
            return View(viewModel);
        }

        // GET: ApplicationUsersController/Edit/5
        public ActionResult Edit(string id)
        {
            if (id is null)
                return NotFound();

            var user = _applicationUsers.GetSingle(id);
            var viewModel = _mapper.Map<ApplicationUsersEditViewModel>(user);

/*            ApplicationUsersEditViewModel2 viewModel = new ApplicationUsersEditViewModel2()
            {
                Id= user.Id,
                Email= user.Email
            };*/

            //viewModel.WypozyczalniaId = user.WypozyczalniaId.HasValue  ?  user.WypozyczalniaId.Value: 1;

            viewModel.WypozyczalniaSelectList = new SelectList(_wypozyczalnias.GetAllRecords().ToList(), "Id", "Name");
            //, _wypozyczalnias.GetAllRecords().First()
            //viewModel.RolesSelectList = new MultiSelectList(_roleManager.Roles, "Id", "Name");


            return View(viewModel);
        }

        // POST: ApplicationUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUsersEditViewModel viewModel)
        {
            var appUser = _applicationUsers.GetSingle(viewModel.Id);
            _mapper.Map<ApplicationUsersEditViewModel, ApplicationUser>(viewModel, appUser);
            //var model = _mapper.Map<ApplicationUser>(viewModel);
            _applicationUsers.Edit(appUser);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationUsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApplicationUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
