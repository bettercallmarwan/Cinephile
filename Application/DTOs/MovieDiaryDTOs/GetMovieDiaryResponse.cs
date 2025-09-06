using Application.DTOs.LogDTOs;
using Domain.Entities;

namespace Application.DTOs.MovieDiaryDTOs
{
    public class GetMovieDiaryResponse
    {

        public string Movie { get; set; }
        public int LogCount { get; set; }
        public virtual ICollection<GetLogResponseDto> Logs { get; set; } = new List<GetLogResponseDto>();
    }
}
