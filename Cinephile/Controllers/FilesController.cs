using Application.DTOs.FileDTOs;
using Application.Services.CommonServices;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly FileService _fileService;

    public FilesController(FileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(10_000_000)]
    public async Task<IActionResult> Upload([FromForm] FileUploadRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            return BadRequest("No file uploaded.");

        var filePath = await _fileService.Upload(request.File, request.FolderName);

        var url = $"{Request.Scheme}://{Request.Host}/{filePath}";

        return Created(url, new
        {
            url,
            size = request.File.Length,
            contentType = request.File.ContentType
        });
    }
}

