namespace EduLink.Domain.Interfaces;

public interface IBlobStorageService 
{
    string? GetBlobSasUrl(string? blobUrl);
    Task<string> UploadToBlobAsync(Stream date  , string fileName );
    Task<string> UploadAcademyPhotoAsync(Stream data, string fileName);

}
