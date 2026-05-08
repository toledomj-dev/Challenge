using Challenge.WebApi.Models;
using Challenge.WebApi.Repositories;
using Microsoft.AspNetCore.Builder;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/users", async (IUserRepository repo) =>
            Results.Ok(await repo.GetAllAsync()));

        routes.MapGet("/users/{id:int}", async (int id, IUserRepository repo) =>
        {
            var user = await repo.GetByIdAsync(id);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        });

        routes.MapPost("/users", async (User user, IUserRepository repo) =>
        {
            var created = await repo.AddAsync(user);
            return Results.Created($"/users/{created.Id}", created);
        });

        routes.MapPut("/users/{id:int}", async (int id, User user, IUserRepository repo) =>
        {
            if (id != user.Id)
                return Results.BadRequest();
            else if (await repo.GetByIdAsync(id) is null)
                return Results.NotFound();

            await repo.UpdateAsync(user);
            return Results.NoContent();
        });

        routes.MapDelete("/users/{id:int}", async (int id, IUserRepository repo) =>
        {       
            if (await repo.GetByIdAsync(id) is null) 
                return Results.NotFound();

            await repo.DeleteAsync(id);
            return Results.NoContent();
        });

        return routes;
    }
}
