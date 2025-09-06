using Application.DTOs.MovieDTOs;
using Application.Exceptions;
using Application.Interfaces.MovieInterfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services.MovieServices
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<AddedMovieResponseDto> AddMovieAsync(AddOrEditMovieRequestDto requestDto)
        {
            var movieToAdd = _mapper.Map<Movie>(requestDto);

            await _movieRepository.AddAsync(movieToAdd);

            return _mapper.Map<AddedMovieResponseDto>(movieToAdd);
        }

        public async Task<GetMovieResponseDto> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetAsync(id, m => m.Genre);

            if (movie is null) throw new NotFoundExcpetion("Movie", id);

            return _mapper.Map<GetMovieResponseDto>(movie);
        }

        public async Task<IEnumerable<GetMovieResponseDto>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetAllAsync(m => m.Genre);

            return _mapper.Map<IEnumerable<GetMovieResponseDto>>(movies);
        }

        public async Task<GetMovieResponseDto> UpdateMovieAsync(int id, AddOrEditMovieRequestDto requestDto)
        {
            var movie = await _movieRepository.GetAsync(id, m => m.Genre);

            if (movie is null) throw new NotFoundExcpetion("Movie", id);

            _mapper.Map(requestDto, movie);

            _movieRepository.Update(movie);

            return _mapper.Map<GetMovieResponseDto>(movie);
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _movieRepository.GetAsync(id);

            if (movie is null) throw new NotFoundExcpetion("Movie", id);

            _movieRepository.Delete(movie);
        }

    }
}
