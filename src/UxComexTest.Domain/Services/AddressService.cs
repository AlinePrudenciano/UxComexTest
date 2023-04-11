using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UxComexTest.Domain.Entities;
using UxComexTest.Domain.Interfaces.Services;
using UxComexTest.Domain.Repositories;

namespace UxComexTest.Domain.Services
{
    public class AddressService : Service<Address>, IAddressService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository repository, IUserRepository userRepository) : base(repository)
        {
            _userRepository = userRepository;
            _addressRepository = repository;
        }

        public async Task<Address> Add(int userId, Address address, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(userId, cancellationToken);
            if (user == null)
                throw new ArgumentException("User not found");

            address.UserId = userId;
            return await Add(address, cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetByUser(int userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(userId, cancellationToken);
            if (user == null)
                throw new ArgumentException("User not found");

            var result = await _addressRepository.GetByUser(userId, cancellationToken);

            return result;
        }

        public async Task<Address> GetCep(string cep, CancellationToken cancellationToken)
        {
            var result = await _addressRepository.GetCep(cep, cancellationToken);
            return result;
        }
    }
}
