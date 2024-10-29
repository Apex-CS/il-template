using System.Security.Claims;

using il_template.Data;
using il_template.Models;

using Microsoft.EntityFrameworkCore;

namespace il_template.Shared;

public abstract class UserData
{
    public static User GetFromToken(ClaimsIdentity? identity)
    {
        if (identity is null)
        {
            return default!;
        }

        List<Claim> user = identity.Claims.ToList();

        return new User
        {
            Email = identity.Name!,
            FirstName =
                user.FirstOrDefault(
                    x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")!.Value,
            LastName = user.FirstOrDefault(x =>
                x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")!.Value,
        };
    }
}

public class UserDataFilter(ApplicationContext db) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (!context.HttpContext.User.Identity!.IsAuthenticated)
        {
            return await next(context);
        }
        
        ClaimsIdentity? identity = context.HttpContext.User.Identity as ClaimsIdentity;
        User userFromToken = UserData.GetFromToken(identity);
        User? user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == userFromToken.Email);

        if (user is not null)
        {
            return await next(context);
        }

        await db.Users.AddAsync(userFromToken);
        await db.SaveChangesAsync();

        return await next(context);
    }
}