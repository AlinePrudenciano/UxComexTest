using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UxComexTest.Domain.Entities;
using UxComexTest.Domain.Interfaces.Services;
using UxComexTest.MVC.Models;

namespace UxComexTest.MVC.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressController(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        // GET: Address
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<AddressModel>>(await _addressService.Get(CancellationToken.None)));
        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressModel = _mapper.Map<AddressModel>(await _addressService.Get(id.Value, CancellationToken.None));
            if (addressModel == null)
            {
                return NotFound();
            }

            return View(addressModel);
        }

        // GET: Address/Create
        public IActionResult Create(int userid)
        {
            var address = new AddressModel() { UserId = userid };
            return View(address);
        }

        // POST: Address/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,AddressName,Cep,City,State,Id")] AddressModel addressModel)
        {
            if (ModelState.IsValid)
            {
                await _addressService.Add(addressModel.UserId, _mapper.Map<Address>(addressModel), CancellationToken.None);
                return Redirect($"~/User/Edit/{addressModel.UserId}");
            }
            return View(addressModel);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressModel = _mapper.Map<AddressModel>(await _addressService.Get(id.Value, CancellationToken.None));
            if (addressModel == null)
            {
                return NotFound();
            }
            return View(addressModel);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,AddressName,Cep,City,State,Id")] AddressModel addressModel)
        {
            if (id != addressModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _addressService.Update(_mapper.Map<Address>(addressModel), id, CancellationToken.None);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AddressModelExists(addressModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect($"~/User/Edit/{addressModel.UserId}");
            }
            return View(addressModel);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressModel = _mapper.Map<AddressModel>(await _addressService.Get(id.Value, CancellationToken.None));
            if (addressModel == null)
            {
                return NotFound();
            }

            return View(addressModel);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _addressService.Get(id, CancellationToken.None);
            await _addressService.Delete(id, CancellationToken.None);

            return Redirect($"~/User/Edit/{address.UserId}");
        }

        private async Task<bool> AddressModelExists(int id)
        {
            return (await _addressService.Get(id, CancellationToken.None)) != null;
        }
    }
}
