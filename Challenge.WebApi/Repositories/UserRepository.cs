using Challenge.WebApi.Data;
using Challenge.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.WebApi.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<IEnumerable<User>> GetAllAsync() => await context.Users.ToListAsync();

        public async Task<User?> GetByIdAsync(int id) => await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> AddAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
