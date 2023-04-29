using API.PaymentSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using API.PaymentSystem.Repository.Interfaces;
using API.PaymentSystem.DTOs;
using Mapster;

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
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _repository.GetUsersAsync();

            var userDtos = users.Adapt<List<UserDTO>>();

            return Ok(userDtos);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserDTO>> GetUser(string username)
        {
            var user = await _repository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = user.Adapt<UserDTO>();

            return Ok(userDto);
        }

        [HttpPost("import")] // POST: api/users/import
        public async Task<IActionResult> Import(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                var users = await _repository.ImportUsersAsync(csvFile);

                return Ok(users.Count + " users imported successfully");
            }
            catch (ArgumentException ex)
            { 
                return BadRequest(ex.Message); 
            }
        }
    }
}
