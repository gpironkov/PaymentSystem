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
            TypeAdapterConfig<User, UserDTO>.NewConfig()
                .Map(dest => dest.Role, src => Enum.GetName(typeof(UserRoles), src.Role));

            //TypeAdapterConfig<UserDTO, User>.NewConfig()
            //    .Map(dest => dest.Role, src => (UserRoles)Enum.Parse(typeof(UserRoles), src.Role, true));
            //TypeAdapterConfig<UserDTO, User>.NewConfig()
            //    .Map(dest => dest.PasswordHash, src => src.Password)
            //    .IgnoreNonMapped(true);
        }

        //public static UserRoles MapFromCsv()
        //{

        //    var line = File.ReadAllLines();
        //    //    //var users = new List<UserDTO>();
        //    //    //foreach (var line in lines.Skip(1)) // skip the  header line
        //    //    //{
        //    //        var user = UserMapper.MapFromCsv(line);
        //    //        //users.Add(user);
        //    //    //}
        //    var parts = csvLine.Split(',');
        //    if (parts.Length != 3)
        //    {
        //        throw new Exception("Invalid CSV line format.");
        //    }

        //    return MapRole(parts[2]); //new UserDTO
        //    //{
        //    //    Username = parts[0],
        //    //    Password = parts[1],
        //    //    Role = MapRole(parts[2])
        //    //};
        //}

        //private static UserRoles MapRole(string role)
        //{
        //    switch (role.Trim().ToLower())
        //    {
        //        case "admin":
        //            return UserRoles.Admin;
        //        case "merchant":
        //            return UserRoles.Merchant;
        //        default:
        //            throw new Exception("Invalid role value in CSV line.");
        //    }
        //}
    }
}
