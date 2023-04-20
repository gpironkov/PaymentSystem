using API.PaymentSystem.Data.Enums;

namespace API.PaymentSystem.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        
        public string Username { get; set; }

        public UserRoles Role { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
