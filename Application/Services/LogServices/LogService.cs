using Application.DTOs.LogDTOs;
using Application.Interfaces.LogInterfaces;
using Application.Interfaces.MovieDiaryInterfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services.LogServices
{
    public class LogService : ILogService
    {
        #region Dependencies and Constructors
        private readonly ILogRepository _logRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieDiaryService _movieDiaryService;


        private readonly IMapper _mapper;

        public LogService(ILogRepository logRepository, IMovieRepository movieRepository, IMovieDiaryService movieDiaryService, IMapper mapper)
        {
            _logRepository = logRepository;
            _movieRepository = movieRepository;
            _movieDiaryService = movieDiaryService;
            _mapper = mapper;
        } 
        #endregion

        public async Task<AddLogResponseDto> AddLogAsync(AddLogRequestDto requestDto, int userId)
        {
            await _logRepository.BeginTransactionAsync();

            try
            {
                var movieDiary = await _movieDiaryService.CreateOrUpdateMovieDiary(userId, requestDto.MovieId);

                var logToAdd = _mapper.Map<Log>(requestDto);
                logToAdd.MovieDiaryId = movieDiary.Id;
                logToAdd.UserId = userId;

                var movie = await _movieRepository.GetAsync(requestDto.MovieId);

                if (movieDiary.LogCount == 1) movie!.MembersCount++;

                if (requestDto.Rating is not null)
                {
                    var lastLogWithRating = movie!.Logs.LastOrDefault(l => l.UserId == userId && l.Rating is not null);

                    if (lastLogWithRating is not null)
                    {
                        movie.RatingSum -= (decimal)lastLogWithRating.Rating!;
                        movie.RatingSum += (decimal)requestDto.Rating!;
                        movie.AverageRating = movie.RatingSum / movie.MembersCount;
                    }
                    else
                    {
                        movie.RatingSum += (decimal)requestDto.Rating!;
                        movie.AverageRating = movie.RatingSum / movie.MembersCount;
                    }
                }

                _movieRepository.Update(movie);
                await _logRepository.AddAsync(logToAdd);

                await _logRepository.SaveChangesAsync();
                await _logRepository.CommitTransactionAsync();

                return _mapper.Map<AddLogResponseDto>(logToAdd);

            }
            catch (Exception)
            {
                await _logRepository.RollbackTransactionAsync(); 
                throw;
            }
        }


    }
}
