using MediatR;

namespace EduLink.Application.Authentication.LogOut;

public class LogOutCommand(int userId) : IRequest<bool>
{
    public int UserId { get; } = userId;
}
