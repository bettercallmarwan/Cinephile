    namespace Domain.Entities
    {
        public class MovieList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
        }
    }
