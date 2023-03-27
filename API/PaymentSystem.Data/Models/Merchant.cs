using API.PaymentSystem.Data.Enums;
using API.PaymentSystem.Data.Models.Common;

namespace API.PaymentSystem.Data.Models
{
    public class Merchant : BaseModel
    {
        public Merchant()
        {
            //this.Id = id;
            this.CreatedOn = DateTime.UtcNow;
            this.Status = MerchantStatus.Active;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public MerchantStatus Status { get; set; }

        public decimal TotalTransactionSum { get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
