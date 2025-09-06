using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class MovieDiary
    {
        public int Id { get; set; }

        public int UserId { get; set; } 
        public virtual ApplicationUser User { get; set; }

        public int MovieId { get; set; } 
        public virtual Movie Movie { get; set; }

        public int LogCount { get; set; }
        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    }
}
