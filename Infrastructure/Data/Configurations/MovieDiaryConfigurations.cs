using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastructure.Data.Configurations
{
    internal class MovieDiaryConfigurations : IEntityTypeConfiguration<MovieDiary>
    {
        public void Configure(EntityTypeBuilder<MovieDiary> builder)
        {
            builder.HasOne(md => md.User)
                .WithMany(u => u.MovieDiaries)
                .HasForeignKey(md => md.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(md => md.Movie)
                .WithMany(m => m.MovieDiaries)
                .HasForeignKey(md => md.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(md => md.Logs)
                .WithOne(l => l.MovieDiary)
                .HasForeignKey(l => l.MovieDiaryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
