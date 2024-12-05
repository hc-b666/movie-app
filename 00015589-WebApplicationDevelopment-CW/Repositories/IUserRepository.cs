using _00015589_WebApplicationDevelopment_CW.Dtos;
using _00015589_WebApplicationDevelopment_CW.Models;

namespace _00015589_WebApplicationDevelopment_CW.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByEmailAsync(string email);

        Task CreateUserAsync(UserModel user);

        Task<UserDto> GetUserByIdAsync(int id);
    }
}
