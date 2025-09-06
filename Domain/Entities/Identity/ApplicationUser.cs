using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public int age { get; set; }
        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
        public ICollection<MovieDiary> MovieDiaries { get; set; } = new List<MovieDiary>();
        public virtual ICollection<MovieList> MovieLists { get; set; } = new List<MovieList>();
    }
}
