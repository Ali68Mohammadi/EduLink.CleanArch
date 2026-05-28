using EduLink.Application.Users;
using EduLink.Domain.Constants;
using EduLink.Domain.Interfaces;

namespace EduLink.Infrastructure.Authorization.Services;

public class ProfileAuthorizationService(IUserContext userContext) : IProfileAuthorizationService
{
    public bool CanEditProfile(string profileManagerId)
    {
        var currentUser = userContext.GetCurrenUser();
        if (currentUser == null) return false;

        if (currentUser.IsInRole(UserRoles.Admin))
        {
            return true;
        }

        if (currentUser.Id == profileManagerId)
        {
            return true;
        }

        return false;
    }
}