using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
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

        public int GenreId { get; set; } // config done
        public virtual Genre Genre { get; set; }

        public int MembersCount { get; set; }
        public int MembersRatedCount { get; set; }

        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
        public virtual ICollection<MovieDiary> MovieDiaries { get; set; } = new List<MovieDiary>();

        public virtual ICollection<MovieList> MovieLists { get; set; } = new List<MovieList>();


        public decimal RatingSum { get; set; }
    }
}
