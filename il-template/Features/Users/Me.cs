using System.Security.Claims;

using FastEndpoints;

using il_template.Data;
using il_template.Models;
using il_template.Shared;

using Microsoft.EntityFrameworkCore;

namespace il_template.Features.Users;

public record UserResponse(string FirstName, string LastName, string Email);

public class Me(ApplicationContext context, IHttpContextAccessor http) : EndpointWithoutRequest<UserResponse>
{
    public override void Configure()
    {
        Get("/api/users/me");
        Description(d => d
            .Produces<UserResponse>(contentType: "application/json")
            .Produces(404));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        ClaimsIdentity? identity = http.HttpContext!.User.Identity as ClaimsIdentity;
        User userFromToken = UserData.GetFromToken(identity);
        
        User? user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == userFromToken.Email, ct);

        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        var response = new UserResponse(user.FirstName, user.LastName, user.Email);

        await SendOkAsync(response, ct);
    }
}