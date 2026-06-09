using MediatR;

namespace EduLink.Application.Academies.Commands.UploadAcademyLogo;

public class UploadAcademyLogoCommand : IRequest
{
    public int AcademyId { get; set; }
    public string FileName { get; set; }=default!;
    public Stream File { get; set; }=default!;
}
