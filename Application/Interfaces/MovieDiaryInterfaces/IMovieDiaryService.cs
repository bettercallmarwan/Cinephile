using Application.DTOs.MovieDiaryDTOs;
using Domain.Entities;

namespace Application.Interfaces.MovieDiaryInterfaces
{
    public interface IMovieDiaryService
    {
        Task<MovieDiary> CreateOrUpdateMovieDiary(int userId, int movieId);

        Task<IEnumerable<GetMovieDiaryResponse>> GetMovieDiariesOfUser(int userId);
    }
}
