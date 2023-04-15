using API.PaymentSystem.DTOs.Csv;

namespace API.PaymentSystem.Repository.Interfaces
{
    public interface IUserRepository
    {
        List<UserDTO> GetAll();

        void Add(UserDTO user);
    }
}
