using Application.DTOs.GenreDTOs;

namespace Application.Interfaces.GenreInterfaces
{
    public interface IGenreService
    {
        Task<GenreDto> AddGenreAsync(GenreDto genreDto);
        Task<IEnumerable<GenreDto>> GetGenresAsync();
        Task DeleteGenre(int id);
    }
}
