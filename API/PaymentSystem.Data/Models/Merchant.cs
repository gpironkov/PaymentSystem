using API.PaymentSystem.Data.Enums;
using API.PaymentSystem.Data.Models.Common;

namespace API.PaymentSystem.Data.Models
{
    public class Merchant : BaseModel
    {
        public Merchant()
        {
            this.Status = MerchantStatus.Active;
            this.CreatedOn = DateTime.UtcNow;
        }        

        public virtual User User { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public MerchantStatus Status { get; set; }

        public decimal TotalTransactionSum { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
