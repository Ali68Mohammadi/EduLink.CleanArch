using EduLink.Application.Courses.Command.UpdateCourse;
using EduLink.Application.Users;
using EduLink.Domain.Constants;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Interfaces;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Academies.Commands.UploadAcademyLogo;

public class UploadAcademyLogoCommandHandler(
    ILogger<UploadAcademyLogoCommandHandler> logger,
    IAcademiesRepository academiesRepository,
    IAcademyAuthorizationService academyAuthorizationService,
    IBlobStorageService blobStorageService)
    : IRequestHandler<UploadAcademyLogoCommand>
{
    public async Task Handle(UploadAcademyLogoCommand request, CancellationToken cancellationToken)
    {

        logger.LogInformation("uploading academy logo for id :{AcademyId}", request.AcademyId);

        var academy = await academiesRepository.GetByIdAsync(request.AcademyId)
            ?? throw new NotFoundException(nameof(Academy), request.AcademyId.ToString());

        if (!academyAuthorizationService.Authorize(academy, ResourceOperationEnm.Update))
            throw new ForbidExeption();


        var logoUrl = await blobStorageService.UploadToBlobAsync(request.File, request.FileName);
        academy.LogoUrl = logoUrl;

        await academiesRepository.SaveChangesAsync();
    }
}
