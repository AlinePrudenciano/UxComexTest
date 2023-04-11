using UxComexTest.Domain.Entities;
using UxComexTest.Domain.Interfaces.Services;
using UxComexTest.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UxComexTest.Domain.Services
{
    public abstract class Service<T> : IService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;
        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<List<T>> Get(CancellationToken cancellationToken)
        {
            return await _repository.Get(cancellationToken);
        }

        public async Task<T> Get(int id, CancellationToken cancellationToken)
        {
            return await _repository.Get(id, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var obj = await _repository.Get(id, cancellationToken);

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            await _repository.Delete(obj, cancellationToken);
        }

        public async Task<T> Add(T obj, CancellationToken cancellationToken)
        {
            var dbObj = await _repository.Get(obj.Id, cancellationToken);

            if (dbObj != null)
                throw new ArgumentNullException(nameof(obj));

            return await _repository.Add(obj, cancellationToken);
        }

        public async Task Update(T obj, int id, CancellationToken cancellationToken)
        {
            var dbObj = await _repository.Get(id, cancellationToken);

            if (dbObj == null)
                throw new ArgumentNullException(nameof(obj));

            obj.Id = id;
            
            await _repository.Update(obj, cancellationToken);
        }

    }
}
