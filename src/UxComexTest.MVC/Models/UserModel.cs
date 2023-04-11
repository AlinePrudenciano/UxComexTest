using System.Collections.Generic;

namespace UxComexTest.MVC.Models
{
    public class UserModel : BaseModel
    {
        public UserModel() : base()
        {
        }


        public string Name { get; set; }
        public string Phone { get; set; }
        public string Cpf { get; set; }

        public List<AddressModel> Addresses { get; set; }
    }
}
