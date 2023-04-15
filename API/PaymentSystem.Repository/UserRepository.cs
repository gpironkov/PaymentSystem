using API.PaymentSystem.DTOs.Csv;
using API.PaymentSystem.Repository.Interfaces;

namespace API.PaymentSystem.Repository
{
    public class CsvUserRepository : IUserRepository
    {
        private readonly string _filePath;

        public CsvUserRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<UserDTO> GetAll()
        {
            var lines = File.ReadAllLines(_filePath);
            var users = new List<UserDTO>();
            foreach (var line in lines.Skip(1)) // skip the header line
            {
                var user = UserMapper.MapFromCsv(line);
                users.Add(user);
            }

            return users;
        }

        public void Add(UserDTO user)
        {
            var line = $"{user.Username},{user.Password},{user.Role}";
            File.AppendAllLines(_filePath, new[] { line });
        }
    }

}
