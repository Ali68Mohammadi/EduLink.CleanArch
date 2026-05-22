namespace EduLink.Domain.Interfaces;


public interface IProfileAuthorizationService
{
    bool CanEditProfile(string profileManagerId);
}
