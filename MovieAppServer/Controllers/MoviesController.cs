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

}
