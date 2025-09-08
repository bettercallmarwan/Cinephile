using Application.DTOs.MovieDiaryDTOs;
using Application.Interfaces.MovieDiaryInterfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services.MovieDiaryServices
{
    public class MovieDiaryService : IMovieDiaryService
    {
        private readonly IMovieDiaryRepository _movieDiaryRepository;
        private readonly IMapper _mapper;

        public MovieDiaryService(IMovieDiaryRepository movieDiaryRepository, IMapper mapper)
        {
            _movieDiaryRepository = movieDiaryRepository;
            _mapper = mapper;
        }
        public async Task<MovieDiary> CreateOrUpdateMovieDiary(int userId, int movieId)
        {
            var movieDiary = await _movieDiaryRepository.GetByUserAndMovieAsync(userId, movieId);

            if(movieDiary is null)
            {
                movieDiary = new MovieDiary()
                {
                    UserId = userId,
                    MovieId = movieId,
                    LogCount = 1
                };

                await _movieDiaryRepository.AddAsync(movieDiary);
            }
            else
            {
                movieDiary.LogCount++;

                _movieDiaryRepository.Update(movieDiary);
            }

            return movieDiary;
        }

        public async Task<IEnumerable<GetMovieDiaryResponse>> GetMovieDiariesOfUser(int userId)
        {
            var diaries = await _movieDiaryRepository.GetAllAsync();
            diaries = diaries.Where(d => d.UserId == userId);

            return _mapper.Map<IEnumerable<GetMovieDiaryResponse>>(diaries);
        }
    }
}
