using API.PaymentSystem.Data.Enums;
using API.PaymentSystem.Data.Models;
using Mapster;
using System.Data;

namespace API.PaymentSystem.DTOs.Csv
{
    public static class UserMapper
    {
        public static void Configure()
        {
            TypeAdapterConfig<UserDTO, User>.NewConfig()
                .Map(dest => dest.PasswordHash, src => src.Password)
                .IgnoreNonMapped(true);
        }

        public static UserDTO MapFromCsv(string csvLine)
        {
            var parts = csvLine.Split(',');
            if (parts.Length != 3)
            {
                throw new Exception("Invalid CSV line format.");
            }

            return new UserDTO
            {
                Username = parts[0],
                Password = parts[1],
                Role = MapRole(parts[2])
            };
        }

        public static User MapToEntity(UserDTO dto)
        {
            return new User
            {
                Username = dto.Username,
                PasswordHash = dto.Password,
                Role = dto.Role
            };
        }

        private static UserRoles MapRole(string role)
        {
            switch (role.Trim().ToLower())
            {
                case "admin":
                    return UserRoles.Admin;
                case "merchant":
                    return UserRoles.Merchant;
                default:
                    throw new Exception("Invalid role value in CSV line.");
            }
        }
    }
}
