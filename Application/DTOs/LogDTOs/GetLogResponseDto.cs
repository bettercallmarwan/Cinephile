using Domain.Entities.Identity;
using Domain.Entities;

namespace Application.DTOs.LogDTOs
{
    public class GetLogResponseDto
    {
        public string User { get; set; }
        public string Movie { get; set; } 
        public decimal? Rating { get; set; }
        public string? Review { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
