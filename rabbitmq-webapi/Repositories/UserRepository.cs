using Microsoft.EntityFrameworkCore;
using rabbitmq_webapi.DataAccess;
using rabbitmq_webapi.Models;

namespace rabbitmq_webapi.Repositories;

public class UserRepository(UserContext _context) : IUserRepository
{
    public async Task<List<User>> GetUsers()
    {
        return await _context.Users
            .Include(a => a.Emails)
            .ToListAsync();
    }

    public async Task<User?> GetUser(int id)
    {
        return await _context.Users
            .Include(a => a.Emails)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}
