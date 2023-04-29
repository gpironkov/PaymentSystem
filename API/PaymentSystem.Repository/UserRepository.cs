using API.PaymentSystem.Data;
using API.PaymentSystem.Data.Enums;
using API.PaymentSystem.Data.Models;
using API.PaymentSystem.DTOs;
using CsvDto = API.PaymentSystem.DTOs.Csv;
using API.PaymentSystem.Repository.Interfaces;
using CsvHelper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

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
            config.ForType<CsvDto.UserDTO, User>()
                .Map(dest => dest.PasswordHash, src => src.Password)
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Role, src => src.Role) //src => Enum.GetName(typeof(Role)))
                .IgnoreNonMapped(true);

            //UserMapper.Configure(config); 

            using (var stream = csvFile.OpenReadStream())
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var usersDto = csv.GetRecords<CsvDto.UserDTO>();

                users = usersDto.Adapt<List<User>>(config);

                await CheckExistingUsername(users);
                HashPassword(users);

                await _context.Users.AddRangeAsync(users);
                await _context.SaveChangesAsync();
            }

            return users;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();            

            return users;
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

        private async Task CheckExistingUsername(List<User> users)
        {
            foreach (var user in users)
            {
                var existingUsername = await _context.Users.Where(u => u.Username == user.Username).FirstOrDefaultAsync();

                if (existingUsername != null)
                {
                    throw new ArgumentException($"Username '{user.Username}' already exists.");
                }
            }
        }

        private void HashPassword(List<User> users)
        {
            foreach (var user in users)
            {
                byte[] salt = GenerateRandomSalt(32); // generate a random salt

                byte[] passwordBytes = Encoding.UTF8.GetBytes(user.PasswordHash);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                var sha256 = SHA256.Create();
                byte[] hashedPassword = sha256.ComputeHash(saltedPassword);

                user.PasswordHash = Convert.ToBase64String(hashedPassword);
            }
        }

        private static byte[] GenerateRandomSalt(int size)
        {
            var random = RandomNumberGenerator.Create();
            byte[] salt = new byte[size];
            random.GetBytes(salt);

            return salt;
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
