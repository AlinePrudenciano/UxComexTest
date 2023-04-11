using System.ComponentModel.DataAnnotations;

namespace UxComexTest.Domain.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        { 
        }

        [Key]
        public int Id { get; set; }
    }
}
