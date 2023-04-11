using System.Collections.Generic;

namespace UxComexTest.Api.Models
{
    public class AddressModel : BaseModel
    {
        public AddressModel() : base()
        {

        }

        public int UserId { get; set; }
        public string AddressName { get; set; }
        public string Cep { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
