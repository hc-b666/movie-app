public interface IUserRepository
{
    Task<UserModel> GetUserByEmailAsync(string email);

    Task CreateUserAsync(UserModel user);

    Task<UserDto> GetUserByIdAsync(int id);
}
