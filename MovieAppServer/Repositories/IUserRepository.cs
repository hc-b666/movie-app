public interface IUserRepository
{
    Task<UserModel> GetUserByEmailAsync(string email);

    Task CreateUserAsync(UserModel user);
}
