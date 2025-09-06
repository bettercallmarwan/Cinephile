namespace Application.DTOs.LogDTOs
{
    public class AddLogRequestDto
    {
        public int MovieId { get; set; }
        public decimal? Rating { get; set; }
        public string? Review { get; set; }
    }
}
