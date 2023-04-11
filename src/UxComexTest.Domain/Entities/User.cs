using System;
using System.Collections.Generic;

namespace UxComexTest.Domain.Entities
{
    public class User : BaseEntity
    {
        public User() : base()
        {
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Cpf { get; set; }
    
        public List<Address> Addresses { get; set; }
    }
}
