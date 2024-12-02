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

    public async Task<MovieModel> GetMovieAsync(int id)
    {
        return await _ctx.Movies.FindAsync(id) ?? throw new Exception("Movie not found");
    }

    public async Task UpdateMovieAsync(MovieModel movie)
    {
        _ctx.Movies.Update(movie);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteMovieAsync(int id)
    {
        var movie = await _ctx.Movies.FindAsync(id) ?? throw new Exception("Movie not found");
        _ctx.Movies.Remove(movie);
        await _ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<MovieModel>> GetMoviesForUserAsync(int userId)
    {
        return await _ctx.Movies.Where(m => m.UserId == userId).ToListAsync();
    }
}
