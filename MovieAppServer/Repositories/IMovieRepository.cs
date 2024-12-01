public interface IMovieRepository
{
    Task<IEnumerable<MovieModel>> GetMoviesAsync();

    Task CreateMovieAsync(MovieModel movie);
}
