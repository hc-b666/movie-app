public class MovieDto
{

    public required string Title { get; set; }

    public required string Description { get; set; }

    public string? Genre { get; set; }

    public required string ImageUrl { get; set; }

    public string? ReleaseDate { get; set; }

    public required string Rating { get; set; }

    public string? Country { get; set; }

    public string? Cast { get; set; }
}
