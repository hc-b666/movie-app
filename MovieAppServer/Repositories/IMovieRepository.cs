public interface IMovieRepository
{
    Task<IEnumerable<MovieModel>> GetMoviesAsync();

    Task CreateMovieAsync(MovieModel movie);

    Task<MovieModel> GetMovieAsync(int id);

    Task UpdateMovieAsync(MovieModel movie);

    Task DeleteMovieAsync(int id);

    Task<IEnumerable<MovieModel>> GetMoviesForUserAsync(int userId);
}
