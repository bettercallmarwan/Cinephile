using Application.DTOs.LogDTOs;
using Application.Interfaces.LogInterfaces;
using Application.Interfaces.MovieDiaryInterfaces;
using Application.Interfaces.MovieInterfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services.LogServices
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        private readonly IMovieDiaryService _movieDiaryService;
        private readonly IMovieService _movieService;

        private readonly IMapper _mapper;

        public LogService(ILogRepository logRepository, IMovieDiaryService movieDiaryService, IMovieService movieService, IMapper mapper)
        {
            _logRepository = logRepository;
            _movieDiaryService = movieDiaryService;
            _movieService = movieService;
            _mapper = mapper;
        }

        public async Task<AddLogResponseDto> AddLogAsync(AddLogRequestDto requestDto, int userId)
        {
            await _logRepository.BeginTransactionAsync();

            try
            {
                var movieDiary = await _movieDiaryService.CreateOrUpdateMovieDiary(userId, requestDto.MovieId);

                var logToAdd = _mapper.Map<Log>(requestDto);
                logToAdd.UserId = userId;
                logToAdd.MovieDiaryId = movieDiary.Id;

                await _movieService.UpdateMovieRatingAndMembers(movieDiary, requestDto);

                await _logRepository.AddAsync(logToAdd);
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