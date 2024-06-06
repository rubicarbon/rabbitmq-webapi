using rabbitmq_webapi.Models;
using rabbitmq_webapi.Repositories;

namespace rabbitmq_webapi.Endponts;

public static class UserEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/users").WithTags("Users");

        group.MapGet("", async (IUserRepository repository) =>
        {
            return await repository.GetUsers();
        }).WithOpenApi();

        group.MapGet("/{id}", async (IUserRepository repository, int id) =>
        {
            return await repository.GetUser(id);
        }).WithOpenApi();

        group.MapPost("", async (User user) =>
        {
            return true;
        }).WithOpenApi();

        group.MapPut("", async (User user) =>
        {
            return true;
        }).WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            return true;
        }).WithOpenApi();

    }
}
