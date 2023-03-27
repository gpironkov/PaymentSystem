using API.PaymentSystem.Data.Enums;
using API.PaymentSystem.Data.Models.Common;

namespace API.PaymentSystem.Data.Models
{
    public class Transaction : BaseModel
    {
        public Transaction() //: base()
        {
            //this.Id = id;
            this.CreatedOn = DateTime.UtcNow;
            this.Status = TransactionStatus.Error;
        }

        public Guid MerchantId { get; set; }

        public virtual Merchant Merchant { get; set; }

        public Guid GUID { get; set; }

        public decimal Amount { get; set; }

        public TransactionStatus Status { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }
    }
}
