using API.PaymentSystem.Data;
using API.PaymentSystem.Data.Models;
using API.PaymentSystem.DTOs.Csv;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Mapster;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly PaymentDbContext _context;

        public UsersController(PaymentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        [HttpPost("import")] // POST: api/users/import
        public async Task<IActionResult> Import(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
                return BadRequest("No file uploaded.");

            var config = new TypeAdapterConfig();
            config.ForType<UserDTO, User>()
                .Map(dest => dest.PasswordHash, src => src.Password)
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Role, src => src.Role)
                .IgnoreNonMapped(true);

            //UserMapper.Configure(config); // Call the UserMappings.Configure() method to configure the mapping

            //config.NameMatchingStrategy(NameMatchingStrategy.Flexible);

            using (var stream = csvFile.OpenReadStream())
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var usersDto = csv.GetRecords<UserDTO>();

                var users = usersDto.Adapt<List<User>>(config);

                await _context.Users.AddRangeAsync(users);
                await _context.SaveChangesAsync();

                return Ok(users.Count + " users imported successfully");
            }
        }
    }
}
