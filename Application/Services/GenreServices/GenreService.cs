using Application.DTOs.GenreDTOs;
using Application.Exceptions;
using Application.Interfaces.GenreInterfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services.GenreServices
{
    public class GenreService : IGenreService
    {
        private readonly IGenericRepository<Genre> _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenericRepository<Genre> genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<GenreDto> AddGenreAsync(GenreDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);

            await _genreRepository.AddAsync(genre);

            return genreDto;
        }

        public async Task DeleteGenre(int id)
        {
            var genre = await _genreRepository.GetAsync(id);

            if (genre is null) throw new NotFoundExcpetion("Genre", id);

            _genreRepository.Delete(genre);
        }

        public async Task<IEnumerable<GenreDto>> GetGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }
    }
}
