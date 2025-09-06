using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.MovieDTOs
{
    public class GetMovieResponseDto
    {
        public string Name { get; set; }
        [Range(1890, 2040, ErrorMessage = "Release year must be between 1890 and 2040")]
        public int ReleaseYear { get; set; }
        [MaxLength(50)]
        public string Director { get; set; }
        [MaxLength(300)]
        public string Plot { get; set; }
        public string PosterUrl { get; set; }
        [Range(0.5, 5, ErrorMessage = "Averge rating range is wrong")]
        public decimal? AverageRating { get; set; }
        [Range(5, 500, ErrorMessage = "Runtime range is wrong")]
        public int Runtime { get; set; }

        public string Genre { get; set; }

    }
}
