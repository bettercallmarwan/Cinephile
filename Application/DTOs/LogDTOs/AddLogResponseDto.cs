namespace Application.DTOs.LogDTOs
{
    public class AddLogResponseDto
    {
        public int UserId { get; set; } 
        public int MovieId { get; set; } 
        public decimal? Rating { get; set; }
        public string? Review { get; set; }
    }
}
