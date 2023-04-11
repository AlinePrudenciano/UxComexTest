using System.Threading.Tasks;
using System.Threading;
using System;
using UxComexTest.Domain.Entities;
using UxComexTest.Domain.Interfaces.Services;
using UxComexTest.Domain.Repositories;

namespace UxComexTest.Domain.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;

        public UserService(IUserRepository repository, IAddressRepository addressRepository) : base(repository)
        {
            _userRepository = repository;
            _addressRepository = addressRepository;
        }

        public new async Task Delete(int id, CancellationToken cancellationToken)
        {
            var obj = await _userRepository.Get(id, cancellationToken);

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            foreach (var address in await _addressRepository.GetByUser(id, cancellationToken))
                await _addressRepository.Delete(address, cancellationToken);

            await _userRepository.Delete(obj, cancellationToken);
        }
    }
}
