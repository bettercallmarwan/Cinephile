using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Contexts;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieDiaryRepository : GenericRepository<MovieDiary>, IMovieDiaryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieDiaryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MovieDiary?> GetByUserAndMovieAsync(int userId, int movieId)
        {
            return await _dbContext.MovieDiaries
                .FirstOrDefaultAsync(md => md.UserId == userId && md.MovieId == movieId);
        }
    }
}
