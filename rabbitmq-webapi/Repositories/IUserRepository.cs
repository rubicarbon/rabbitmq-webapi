using rabbitmq_webapi.Models;

namespace rabbitmq_webapi.Repositories;
public interface IUserRepository
{
    Task<User?> GetUser(int id);
    Task<List<User>> GetUsers();
}