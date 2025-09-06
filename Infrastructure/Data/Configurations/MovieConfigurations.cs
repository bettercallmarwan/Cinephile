using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class MovieConfigurations : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.MovieLists)
                .WithMany(ml => ml.Movies);

            builder.Property(m => m.AverageRating)
                .HasColumnType("decimal(2, 1)");

            builder.Property(m => m.RatingSum)
                .HasColumnType("decimal(2, 1)");

        }
    }
}
