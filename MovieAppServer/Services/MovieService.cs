public class MovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUserRepository _userRepository;

    public MovieService(IMovieRepository movieRepository, IUserRepository userRepository)
    {
        _movieRepository = movieRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<MovieModel>> GetMoviesAsync()
    {
        return await _movieRepository.GetMoviesAsync();
    }

    public async Task CreateMovieAsync(MovieDto movieDto, string userEmail)
    {
        var user = await _userRepository.GetUserByEmailAsync(userEmail) ?? throw new UnauthorizedAccessException("User not found.");
        var userId = user.Id;

        if (string.IsNullOrWhiteSpace(movieDto.Title) || string.IsNullOrWhiteSpace(movieDto.Description) || string.IsNullOrWhiteSpace(movieDto.Rating))
        {
            throw new ArgumentException("Title, description, and rating are required.");
        }

        var movie = new MovieModel
        {
            Title = movieDto.Title,
            Description = movieDto.Description,
            Genre = movieDto.Genre,
            ImageUrl = movieDto.ImageUrl,
            ReleaseDate = movieDto.ReleaseDate,
            Rating = movieDto.Rating,
            Country = movieDto.Country,
            Cast = movieDto.Cast,
            UserId = userId,
        };

        await _movieRepository.CreateMovieAsync(movie);
    }

    public async Task<MovieModel> GetMovieAsync(int id)
    {
        return await _movieRepository.GetMovieAsync(id);
    }

    public async Task UpdateMovieAsync(int id, MovieDto movieDto, string userEmail)
    {
        var user = await _userRepository.GetUserByEmailAsync(userEmail) ?? throw new UnauthorizedAccessException("User not found.");
        var userId = user.Id;

        var movie = await _movieRepository.GetMovieAsync(id);

        if (movie.UserId != userId)
        {
            throw new UnauthorizedAccessException("User is not authorized to update this movie.");
        }

        movie.Title = movieDto.Title;
        movie.Description = movieDto.Description;
        movie.Genre = movieDto.Genre;
        movie.ImageUrl = movieDto.ImageUrl;
        movie.ReleaseDate = movieDto.ReleaseDate;
        movie.Rating = movieDto.Rating;
        movie.Country = movieDto.Country;
        movie.Cast = movieDto.Cast;

        await _movieRepository.UpdateMovieAsync(movie);
    }

    public async Task DeleteMovieAsync(int id, string userEmail)
    {
        var user = await _userRepository.GetUserByEmailAsync(userEmail) ?? throw new UnauthorizedAccessException("User not found.");
        var userId = user.Id;

        var movie = await _movieRepository.GetMovieAsync(id);

        if (movie.UserId != userId)
        {
            throw new UnauthorizedAccessException("User is not authorized to delete this movie.");
        }

        await _movieRepository.DeleteMovieAsync(id);
    }

    public async Task<IEnumerable<MovieModel>> GetMoviesForUserAsync(string userEmail)
    {
        var user = await _userRepository.GetUserByEmailAsync(userEmail) ?? throw new UnauthorizedAccessException("User not found.");
        var userId = user.Id;

        return await _movieRepository.GetMoviesForUserAsync(userId);
    }
}
