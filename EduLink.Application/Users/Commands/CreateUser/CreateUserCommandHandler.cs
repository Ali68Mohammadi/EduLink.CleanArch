using AutoMapper;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace EduLink.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger,
        IUserRepository userRepository,
        IMapper mapper) : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Create a new User");

        if (await userRepository.IsEmailExistAsync(request.Email))
            throw new BadRequestException("Email Exist");

        if (await userRepository.IsPhoneNumberExistAsync(request.PhoneNumber))
            throw new BadRequestException("Phone Number Exist");

        var user = mapper.Map<User>(request);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        user.ActivationCode = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        user.Email = request.Email.ToLower();
        var id = await userRepository.Create(user);
        logger.LogInformation("User successfully created with ID: {UserId}", id);
        return id;
    }
}