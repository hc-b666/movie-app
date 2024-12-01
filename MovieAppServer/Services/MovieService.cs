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
}
