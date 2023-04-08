using API.PaymentSystem.Data.Enums;
using API.PaymentSystem.Data.Models.Common;

namespace API.PaymentSystem.Data.Models
{
    public class User : BaseModel
    {
        public User()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Username { get; set; }

        public string PasswordHash { get; set; }
         
        public UserRoles Role { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}
