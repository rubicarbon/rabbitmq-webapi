using rabbitmq_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace rabbitmq_webapi.DataAccess;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Email> Emails { get; set; }

}
