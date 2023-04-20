using API.PaymentSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using API.PaymentSystem.Repository.Interfaces;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _repository.GetUsersAsync());
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        { 
            return await _repository.GetUserByUsernameAsync(username);
        }

        [HttpPost("import")] // POST: api/users/import
        public async Task<IActionResult> Import(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
                return BadRequest("No file uploaded.");

            var users = await _repository.ImportUsersAsync(csvFile);

            return Ok(users.Count + " users imported successfully");
        }
    }
}
