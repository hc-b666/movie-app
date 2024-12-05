using _00015589_WebApplicationDevelopment_CW.Dtos;
using _00015589_WebApplicationDevelopment_CW.Models;
using Microsoft.EntityFrameworkCore;

namespace _00015589_WebApplicationDevelopment_CW.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _ctx;

        public UserRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _ctx.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task CreateUserAsync(UserModel user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _ctx.Users.SingleOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
