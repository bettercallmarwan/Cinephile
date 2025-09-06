using Application.DTOs.LogDTOs;
using Application.Exceptions;
using Application.Interfaces.LogInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cinephile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpPost("CreateLog")]
        [Authorize(Roles ="User")]
        public async Task<ActionResult<AddLogResponseDto>> AddLog(AddLogRequestDto requestDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                throw new UnAuthorizedException();

            var log = await _logService.AddLogAsync(requestDto, userId);
            return Ok(log);
        }


        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadFile(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("No file uploaded.");

        //    var filePath = Path.Combine("wwwroot/uploads", file.FileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    return Ok(new { FilePath = filePath });
        //}

    }
}
