using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<UserModel> Users { get; set; }
    public DbSet<MovieModel> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<MovieModel>()
            .HasOne(m => m.User)
            .WithMany(u => u.FavoriteMovies)
            .HasForeignKey(m => m.UserId);
    }
}
