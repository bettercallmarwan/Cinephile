using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }

        public int UserId { get; set; } // done
        public virtual ApplicationUser User { get; set; }

        public int MovieId { get; set; } // done
        public virtual Movie Movie { get; set; }

        public decimal? Rating { get; set; }
        public string? Review { get; set; }

        public int MovieDiaryId { get; set; }
        public virtual MovieDiary MovieDiary { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}