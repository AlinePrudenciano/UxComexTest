using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UxComexTest.Domain.Entities;

namespace UxComexTest.Domain.Interfaces.Services
{
    public interface IAddressService : IService<Address>
    {
        Task<Address> Add(int userId, Address address, CancellationToken cancellationToken);
        Task<IEnumerable<Address>> GetByUser(int userId, CancellationToken cancellationToken);
        Task<Address> GetCep(string cep, CancellationToken cancellationToken);
    }
}
