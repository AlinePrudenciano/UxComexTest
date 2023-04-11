using System.Collections.Generic;

namespace UxComexTest.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Address() : base()
        {

        }

        public int UserId { get; set; }
        public string AddressName { get; set; }
        public string Cep { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
