using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Interfaces;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Academies.Commands.UploadAcademyPhotos;

public class UploadAcademyPhotosCommandHandler(ILogger<UploadAcademyPhotosCommandHandler> logger,
    IAcademiesRepository academiesRepository,
    IAcademyAuthorizationService academyAuthorizationService,
    IBlobStorageService blobStorageService
    ) : IRequestHandler<UploadAcademyPhotosCommand>
{
    public async Task Handle(UploadAcademyPhotosCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Uploading academy photos for academy id: {AcademyId}", request.AcademyId);

        var academy = await academiesRepository.GetByIdAsync(request.AcademyId)
            ?? throw new NotFoundException(nameof(Academy), request.AcademyId.ToString());

        if (!academyAuthorizationService.Authorize(academy, Domain.Constants.ResourceOperationEnm.Update))
            throw new ForbidExeption();


        foreach (var file in request.Files)
        {
            using var stream = file.OpenReadStream();

            var photoUrl = await blobStorageService.UploadAcademyPhotoAsync(
                stream,
                $"{request.AcademyId}/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}");

            await academiesRepository.AddPhotoUrlAsync(request.AcademyId, photoUrl);
        }

        await academiesRepository.SaveChangesAsync();

    }

}
