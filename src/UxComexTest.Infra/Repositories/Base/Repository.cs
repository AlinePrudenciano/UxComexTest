using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UxComexTest.Domain.Entities;
using UxComexTest.Domain.Repositories;
using static Dapper.SqlMapper;

namespace UxComexTest.Infra.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IDbConnection _dbConnection;
        protected readonly string _table;

        public Repository(string table, IDbConnection dbConnection)
        {
            _table = table;
            _dbConnection = dbConnection;
        }

        public async Task<List<T>> Get(CancellationToken cancellationToken)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            var result = _dbConnection.Query<T>(
                sql: $"select * from {_table}",
                commandType: CommandType.Text);

            if (_dbConnection.State != ConnectionState.Closed &&
               _dbConnection.State != ConnectionState.Executing)
                _dbConnection.Close();

            return result.ToList();
        }

        public async Task<T> Get(int id, CancellationToken cancellationToken)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            var result = _dbConnection.QueryFirstOrDefault<T>(
                sql: $"select * from {_table} where id = @id",
                param: new { id },
                commandType: CommandType.Text);

            if (_dbConnection.State != ConnectionState.Closed &&
               _dbConnection.State != ConnectionState.Executing)
                _dbConnection.Close();

            return result;
        }

        public async Task Delete(T entity, CancellationToken cancellationToken)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            _dbConnection.Execute(
                sql: $"delete from {_table} where id = @Id",
                param: new { entity.Id },
                commandType: CommandType.Text);

            if (_dbConnection.State != ConnectionState.Closed &&
               _dbConnection.State != ConnectionState.Executing)
                _dbConnection.Close();
        }

        public abstract Task<T> Add(T entity, CancellationToken cancellationToken);

        public abstract Task Update(T entity, CancellationToken cancellationToken);
    }
}
