using EduLink.Domain.Entities;

namespace EduLink.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(User user);
}
