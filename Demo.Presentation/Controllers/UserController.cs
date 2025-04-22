using System.Net.NetworkInformation;
using Demo.DAL.Models.IdentityModels;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
namespace Demo.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string searchInput)
        {
            var users = Enumerable.Empty<UserViewModel>();

            if (string.IsNullOrEmpty(searchInput))
            {
                users = await _userManager.Users.Select(U => new UserViewModel()
                {
                    Id = U.Id,
                    FirstName = U.FristName,
                    LastName = U.LastName,
                    Email = U.Email,
                    PhoneNumber=U.PhoneNumber
                }).ToListAsync();

                foreach (var user in users)
                {
                    user.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
                }
            }
            else
            {
                users = await _userManager.Users.Where(U => U.Email
                    .ToLower()
                    .Contains(searchInput.ToLower()))
                    .Select(U => new UserViewModel()
                    {
                        Id = U.Id,
                        FirstName = U.FristName,
                        LastName = U.LastName,
                        Email = U.Email,
                        PhoneNumber = U.PhoneNumber
                    }).ToListAsync();

                foreach (var user in users)
                {
                    user.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
                }
            }

            return View(users);
        }
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();//error 400

            var UserFromDb = await _userManager.FindByIdAsync(id);

            if (UserFromDb == null)
                return NotFound(); // 404

            var user = new UserViewModel()
            {
                Id = UserFromDb.Id,
                FirstName = UserFromDb.FristName,
                LastName = UserFromDb.LastName,
                Email = UserFromDb.Email,
                PhoneNumber = UserFromDb.PhoneNumber
            };

            user.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));

            return View(ViewName, user);
        }
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//btmn3 ay request mn app khargy zy el postman msln
        public async Task<IActionResult> Edit([FromRoute] string id, UserEditeViewModel model)
        {

            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByIdAsync(id);
                if (userFromDb == null)
                    return NotFound(); // 404

                userFromDb.FristName = model.FirstName;
                userFromDb.LastName = model.LastName;
                userFromDb.PhoneNumber = model.PhoneNumber;

                await _userManager.UpdateAsync(userFromDb);

                return RedirectToAction("Index");

            }

            return View(model);
        }

        public Task<IActionResult> Delete(string? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//btmn3 ay request mn app khargy zy el postman msln
        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByIdAsync(id);
                if (userFromDb == null)
                    return NotFound(); // 404

                await _userManager.DeleteAsync(userFromDb);

                return RedirectToAction("Index");

            }

            return View(model);
        }
    }
}
