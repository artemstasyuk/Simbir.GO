using Microsoft.AspNetCore.Http;

namespace Simbir.GO.Server.Contracts.Vfs;

public record PutObjectRequest(
    Guid FolderId,
    string Title,
    IFormFile File
);