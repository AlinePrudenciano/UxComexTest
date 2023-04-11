using UxComexTest.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UxComexTest.Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> Get(CancellationToken cancellationToken);
        Task<T> Get(int id, CancellationToken cancellationToken);
        Task<T> Add(T obj, CancellationToken cancellationToken);
        Task Update(T obj, CancellationToken cancellationToken);
        Task Delete(T obj, CancellationToken cancellationToken);
    }
}
