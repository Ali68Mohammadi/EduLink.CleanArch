

//using EduLink.Domain.Interfaces;
using EduLink.Application.Users;
using EduLink.Domain.Constants;
using EduLink.Domain.Entities;
using EduLink.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EduLink.Infrastructure.Authorization.Services;

public class AcademyAuthorizationService(ILogger<AcademyAuthorizationService> logger,
    IUserContext userContext) : IAcademyAuthorizationService
{

    public bool Authorize(Academy academy, ResourceOperationEnm resourceOpertionEnm)
    {
        var currentUser = userContext.GetCurrenUser();

        logger.LogInformation("Authorizing user {UserEmail}, to {opertaion} for Academy {AcademyName} ",
            currentUser.Email,
            resourceOpertionEnm,
            academy.Name);

        if (resourceOpertionEnm == ResourceOperationEnm.Read || resourceOpertionEnm == ResourceOperationEnm.Create)
        {
            logger.LogInformation("create/read operation -  successfull authorization ");
            return true;
        }

        if (resourceOpertionEnm == ResourceOperationEnm.Delete && currentUser.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("AdminUser , delete operation -  successfull authorization ");
            return true;
        }

        if ((resourceOpertionEnm == ResourceOperationEnm.Delete || resourceOpertionEnm == ResourceOperationEnm.Update
            )&& currentUser.Id == academy.ManagerId)
        {
            logger.LogInformation("Acadmey manager -  successfull authorization ");
            return true;
        }

        return false;
    }
}
