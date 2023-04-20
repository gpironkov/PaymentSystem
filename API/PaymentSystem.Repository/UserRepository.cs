using API.PaymentSystem.Data;
using API.PaymentSystem.Data.Enums;
using API.PaymentSystem.Data.Models;
using API.PaymentSystem.DTOs.Csv;
using API.PaymentSystem.Repository.Interfaces;
using CsvHelper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace API.PaymentSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        //private readonly string _filePath;
        private readonly PaymentDbContext _context;

        public UserRepository(PaymentDbContext context)
        {
            _context = context;
        }        

        public async Task<List<User>> ImportUsersAsync(IFormFile csvFile)
        {
            var config = new TypeAdapterConfig();
            var users = new List<User>();
            config.ForType<UserDTO, User>()
                .Map(dest => dest.PasswordHash, src => src.Password)
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Role, src => src.Role) //src => Enum.GetName(typeof(Role)))
                .IgnoreNonMapped(true);

            //UserMapper.Configure(config); // Call the UserMappings.Configure() method to configure the mapping

            using (var stream = csvFile.OpenReadStream())
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var usersDto = csv.GetRecords<UserDTO>();

                users = usersDto.Adapt<List<User>>(config);

                await _context.Users.AddRangeAsync(users);
                await _context.SaveChangesAsync();
            }

            return users;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        //public UserRoles GetUserRole()
        //{
        //    var line = File.ReadLines(_filePath);
        //    //var users = new List<UserDTO>();
        //    //foreach (var line in lines.Skip(1)) // skip the  header line
        //    //{
        //        var user = UserMapper.MapFromCsv(line);
        //        //users.Add(user);
        //    //}

        //    return user;
        //}
    }
}
