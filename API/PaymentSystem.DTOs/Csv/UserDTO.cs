using API.PaymentSystem.Data.Enums;

namespace API.PaymentSystem.DTOs.Csv
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public UserRoles Role { get; set; }

        //public UserRoles GetRole(IFormFile csvFile)
        //{
        //    using (var stream = csvFile.OpenReadStream())
        //    using (var reader = new StreamReader(stream))
        //    { 
        //        string contents = reader.ReadToEnd();
        //    }
        //}
    }
}
