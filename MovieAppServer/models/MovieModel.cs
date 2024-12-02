public class MovieModel
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string Genre { get; set; }

    public required string ImageUrl { get; set; }

    public required string ReleaseDate { get; set; }

    public required string Rating { get; set; }

    public required string Country { get; set; }

    public required string Cast { get; set; }

    public int UserId { get; set; } // Foreign key
}
