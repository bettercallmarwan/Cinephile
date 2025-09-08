using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Contexts;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LogRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<decimal?> GetUserLastRatingAsync(int userId, int movieId)
        {
            return _dbContext.Logs
                .Where(l => l.UserId == userId && l.MovieId == movieId && l.Rating.HasValue)
                .OrderByDescending(l => l.CreationDate)
                .Select(l => l.Rating)
                .FirstOrDefaultAsync();
        }
    }
}
