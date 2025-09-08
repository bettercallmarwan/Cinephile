using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ILogRepository : IGenericRepository<Log>
    {
        Task<decimal?> GetUserLastRatingAsync(int userId, int movieId);
    }
}
