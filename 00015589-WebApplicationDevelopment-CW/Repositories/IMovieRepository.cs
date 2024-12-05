using _00015589_WebApplicationDevelopment_CW.Models;

namespace _00015589_WebApplicationDevelopment_CW.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieModel>> GetMoviesAsync();

        Task CreateMovieAsync(MovieModel movie);

        Task<MovieModel> GetMovieAsync(int id);

        Task UpdateMovieAsync(MovieModel movie);

        Task DeleteMovieAsync(int id);

        Task<IEnumerable<MovieModel>> GetMoviesForUserAsync(int userId);
    }
}
