using Microsoft.EntityFrameworkCore;
using rabbitmq_receiver.Models;

namespace rabbitmq_receiver.DataAccess;
public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Email> Emails { get; set; }

}
