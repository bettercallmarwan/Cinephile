using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.MovieDTOs
{
    public class AddedMovieResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Range(1890, 2040, ErrorMessage = "Release year must be between 1890 and 2040")]
        public int ReleaseYear { get; set; }
        [MaxLength(50)]
        public string Director { get; set; }

    }
}
