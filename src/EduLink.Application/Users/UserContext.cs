using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EduLink.Application.Users;

public interface IUserContext
{
    CurrenUser? GetCurrenUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrenUser? GetCurrenUser()
    {
        var user = (httpContextAccessor?.HttpContext?.User) 
            ?? throw new InvalidOperationException("User context is not present!");

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        var dateOfBirthString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;

        DateOnly? dateOfBirth;

        if (dateOfBirthString == null)
        {
            dateOfBirth = null;
        }
        else
        {
            dateOfBirth = DateOnly.ParseExact(dateOfBirthString, "yyyy-MM-dd");
        }

        return new CurrenUser(userId, email, roles, nationality, dateOfBirth);

    }


}
