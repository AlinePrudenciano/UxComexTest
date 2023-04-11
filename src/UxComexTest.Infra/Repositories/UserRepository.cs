using Dapper;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using UxComexTest.Domain.Entities;
using UxComexTest.Domain.Repositories;

namespace UxComexTest.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDbConnection dbConnection) : base("Users", dbConnection)
        {
        }

        public override async Task<User> Add(User user, CancellationToken cancellationToken)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            var id = await _dbConnection.QueryFirstOrDefaultAsync<int>(
                sql: $"insert into {_table} (name, phone, cpf) " +
                     $"values (@Name, @Phone, @Cpf); " +
                     $"select cast(scope_identity() as int) as id;",
                param: new { user.Name, user.Phone, user.Cpf },
                commandType: CommandType.Text);

            if (_dbConnection.State != ConnectionState.Closed &&
               _dbConnection.State != ConnectionState.Executing)
                _dbConnection.Close();

            user.Id = id;
            return user;
        }

        public override async Task Update(User user, CancellationToken cancellationToken)
        {

            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            await _dbConnection.ExecuteAsync(
                sql: $"update {_table} set name  = @Name, " +
                     $"                    phone = @Phone, " +
                     $"                    cpf   = @Cpf " +
                     $"where id = @Id ",
                param: new { user.Name, user.Phone, user.Cpf, user.Id },
                commandType: CommandType.Text);

            if (_dbConnection.State != ConnectionState.Closed &&
               _dbConnection.State != ConnectionState.Executing)
                _dbConnection.Close();
        }
    }
}
