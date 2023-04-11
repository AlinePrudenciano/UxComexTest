using System;

namespace UxComexTest.MVC.Models
{
    public abstract class BaseModel
    {
        public BaseModel()
        { 
        }

        public int Id { get; set; }
    }
}
