using Application.DTOs.LogDTOs;
using Application.DTOs.MovieDTOs;

namespace Application.Interfaces.LogInterfaces
{
    public interface ILogService
    {
        Task<AddLogResponseDto> AddLogAsync(AddLogRequestDto requestDto, int userId);
    }
}
