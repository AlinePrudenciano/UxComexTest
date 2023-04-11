using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UxComexTest.Domain.Entities;
using UxComexTest.Domain.Interfaces.Services;
using UxComexTest.MVC.Models;

namespace UxComexTest.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IAddressService addressService, IMapper mapper)
        {
            _userService = userService;
            _addressService = addressService;
            _mapper = mapper;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<UserModel>>(await _userService.Get(CancellationToken.None)));
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = _mapper.Map<UserModel>(await _userService.Get(id.Value, CancellationToken.None));
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Phone,Cpf,Id")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.Add(_mapper.Map<User>(userModel), CancellationToken.None);
                return Redirect($"{nameof(Edit)}/{user.Id}");
            }
            return View(userModel);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = _mapper.Map<UserModel>(await _userService.Get(id.Value, CancellationToken.None));
            if (userModel == null)
            {
                return NotFound();
            }

            var addresses = _mapper.Map<IEnumerable<AddressModel>>(await _addressService.GetByUser(id.Value, CancellationToken.None));
            if (addresses == null)
            {
                return NotFound();
            }

            userModel.Addresses = addresses.ToList();

            return View(userModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Phone,Cpf,Id")] UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.Update(_mapper.Map<User>(userModel), id, CancellationToken.None);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserModelExists(userModel.Id))
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
            return View(userModel);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = _mapper.Map<UserModel>(await _userService.Get(id.Value, CancellationToken.None));
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.Delete(id, CancellationToken.None);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserModelExists(int id)
        {
            return (await _userService.Get(id, CancellationToken.None)) != null;
        }
    }
}
