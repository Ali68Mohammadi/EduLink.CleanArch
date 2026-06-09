using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using EduLink.Domain.Interfaces;
using EduLink.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace EduLink.Infrastructure.Storage;



internal class BlobStorgeService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;
    public async Task<string> UploadToBlobAsync(Stream date, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);

        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(date);

        var blobUrl = blobClient.Uri.ToString();
        return blobUrl;

    }

    public async Task<string> UploadAcademyPhotoAsync(Stream data, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);

        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.AcademyMediaContainerName);

        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(data);

        return fileName;
    }

    public string? GetBlobSasUrl(string? blobUrl)
    {
        if (string.IsNullOrEmpty(blobUrl))
            return null;

        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = _blobStorageSettings.LogosContainerName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
            BlobName = GetBlobNameFromUrl(blobUrl),
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);

        var sasToken = sasBuilder.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(
              blobServiceClient.AccountName,
             _blobStorageSettings.AccountKey)).ToString();
        return $"{blobUrl}?{sasToken}";
    }

    private string GetBlobNameFromUrl(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        return uri.Segments.Last();
    }

 
}
