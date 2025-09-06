using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class LogConfigurations : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasOne(l => l.User)
                .WithMany(u => u.Logs)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Movie)
                .WithMany(m => m.Logs)
                .HasForeignKey(l => l.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(l => l.Rating)
                .HasColumnType("decimal(2, 1)");

            builder.HasOne(l => l.MovieDiary)
                .WithMany(md => md.Logs)
                .HasForeignKey(l => l.MovieDiaryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
