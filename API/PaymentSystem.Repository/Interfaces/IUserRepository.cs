using API.PaymentSystem.Data.Models;
using API.PaymentSystem.DTOs.Csv;
using System.Collections;

namespace API.PaymentSystem.Repository.Interfaces
{
    public interface IUserRepository
    {
        //List<UserDTO> GetAll();

        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByUsernameAsync(string username);

        void UpdateUser(User user);

        Task<List<User>> ImportUsersAsync(IFormFile csvFile);
    }
}
