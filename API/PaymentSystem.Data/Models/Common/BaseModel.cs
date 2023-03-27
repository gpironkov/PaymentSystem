using System.ComponentModel.DataAnnotations;

namespace API.PaymentSystem.Data.Models.Common
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
