namespace EduLink.Domain.Exceptions;

public class NotFoundException(string resourceType, string? resourceIdentifier = null)
    : Exception(resourceIdentifier is null
        ? $"{resourceType} was not found."
        : $"{resourceType} with id {resourceIdentifier} doesn't exist.")
{
}

