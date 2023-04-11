using Dapper;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using UxComexTest.Domain.Entities;
using UxComexTest.Domain.Repositories;

namespace UxComexTest.Infra.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(IDbConnection dbConnection) : base("Addresses", dbConnection)
        {
        }

        public override async Task<Address> Add(Address address, CancellationToken cancellationToken)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            var id = await _dbConnection.QueryFirstOrDefaultAsync<int>(
                sql: $"insert into {_table} (addressname, cep, city, state, userid) " +
                     $"values (@AddressName, @Cep, @City, @State, @UserId); " +
                     $"select cast(scope_identity() as int) as id;",
                param: new { address.AddressName, address.Cep, address.City, address.State, address.UserId },
                commandType: CommandType.Text);

            if (_dbConnection.State != ConnectionState.Closed &&
               _dbConnection.State != ConnectionState.Executing)
                _dbConnection.Close();

            address.Id = id;
            return address;

        }

        public override async Task Update(Address address, CancellationToken cancellationToken)
        {

            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            await _dbConnection.ExecuteAsync(
                sql: $"update {_table} set addressname  = @AddressName, " +
                     $"                    cep          = @Cep, " +
                     $"                    city         = @City, " +
                     $"                    state        = @State " +
                     $"where id = @Id ",
                param: new { address.AddressName, address.Cep, address.City, address.State, address.UserId, address.Id },
                commandType: CommandType.Text);

            if (_dbConnection.State != ConnectionState.Closed &&
               _dbConnection.State != ConnectionState.Executing)
                _dbConnection.Close();
        }

        public async Task<IEnumerable<Address>> GetByUser(int userId, CancellationToken cancellationToken)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

            var result = await _dbConnection.QueryAsync<Address>(
                sql: $"select * from {_table} where userid = @userId",
                param: new { userId },
                commandType: CommandType.Text);

            if (_dbConnection.State != ConnectionState.Closed &&
               _dbConnection.State != ConnectionState.Executing)
                _dbConnection.Close();

            return result;
        }

        public async Task<Address> GetCep(string cep, CancellationToken cancellationToken)
        {
            var address = new Address();

            using (var client = new RestClient("https://viacep.com.br/ws/"))
            {
                var request = new RestRequest($"{cep}/json/", Method.Get);
                var result = client.Execute(request);

                var data = JsonConvert.DeserializeObject<dynamic>(result.Content);

                if (data.erro != null)
                    throw new ArgumentException("Cep not foud");

                address.AddressName = data.logradouro;
                address.Cep = data.cep;
                address.City = data.localidade;
                address.State = data.uf;
            }

            return address;

        }
    }
}
