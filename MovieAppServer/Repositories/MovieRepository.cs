using Microsoft.EntityFrameworkCore;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _ctx;

    public MovieRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<MovieModel>> GetMoviesAsync()
    {
        return await _ctx.Movies.ToListAsync();
    }

    public async Task CreateMovieAsync(MovieModel movie)
    {
        _ctx.Movies.Add(movie);
        await _ctx.SaveChangesAsync();
    }
}
