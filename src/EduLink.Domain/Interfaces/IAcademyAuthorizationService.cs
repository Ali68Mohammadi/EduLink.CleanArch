using EduLink.Domain.Constants;
using EduLink.Domain.Entities;

namespace EduLink.Domain.Interfaces;

public interface IAcademyAuthorizationService
{
    bool Authorize(Academy academy, ResourceOperationEnm resourceOpertionEnm);
}