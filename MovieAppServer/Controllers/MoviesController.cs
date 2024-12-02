using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly MovieService _movieService;

    public MoviesController(MovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _movieService.GetMoviesAsync();
        return Ok(movies);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateMovie(MovieDto movieDto)
    {
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return Unauthorized();
        }

        try
        {
            await _movieService.CreateMovieAsync(movieDto, userEmail);
            return Ok(new { message = "Movie added successfully!" });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetMovie(int id)
    {
        try
        {
            var movie = await _movieService.GetMovieAsync(id);
            return Ok(movie);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateMovie(int id, MovieDto movieDto)
    {
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return Unauthorized();
        }

        try
        {
            await _movieService.UpdateMovieAsync(id, movieDto, userEmail);
            return Ok(new { message = "Movie updated successfully!" });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return Unauthorized();
        }

        try
        {
            await _movieService.DeleteMovieAsync(id, userEmail);
            return Ok(new { message = "Movie deleted successfully!" });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetMoviesForUser()
    {
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return Unauthorized();
        }

        try
        {
            var movies = await _movieService.GetMoviesForUserAsync(userEmail);
            return Ok(movies);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}
