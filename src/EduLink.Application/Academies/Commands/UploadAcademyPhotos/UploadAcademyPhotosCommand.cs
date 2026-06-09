using MediatR;
using Microsoft.AspNetCore.Http;

namespace EduLink.Application.Academies.Commands.UploadAcademyPhotos;

public class UploadAcademyPhotosCommand :IRequest
{
    public int AcademyId { get; set; }
    public List<IFormFile> Files { get; set; } = [];

}
