using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UxComexTest.Domain.Entities;

namespace UxComexTest.Domain.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<IEnumerable<Address>> GetByUser(int userId, CancellationToken cancellationToken);
        Task<Address> GetCep(string cep, CancellationToken cancellationToken);
    }
}
