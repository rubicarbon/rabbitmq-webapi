using rabbitmq_receiver.Models;

namespace rabbitmq_receiver.Repositories;
public interface IUserRepository
{
    Task AddUser(User user);
    Task DeleteUser(int id);
    Task UpdateUser(User user);
}