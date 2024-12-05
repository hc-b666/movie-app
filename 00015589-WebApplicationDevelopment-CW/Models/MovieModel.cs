namespace _00015589_WebApplicationDevelopment_CW.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public string ImageUrl { get; set; }

        public string ReleaseDate { get; set; }

        public string Rating { get; set; }

        public string Country { get; set; }

        public string Cast { get; set; }

        public int UserId { get; set; } // Foreign key
    }
}
