using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 25))));

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/", () => "o/, its meee!");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/api/v1/auth/register", async (ApplicationDbContext dbContext, UserDto userDto) =>
{
    if (string.IsNullOrWhiteSpace(userDto.Username) || string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password))
    {
        return Results.BadRequest("Username, email, and password are required.");
    }

    if (await dbContext.Users.AnyAsync(u => u.Email == userDto.Email))
    {
        return Results.BadRequest("Email is already registered.");
    }

    var user = new UserModel
    {
        Username = userDto.Username,
        Email = userDto.Email,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
    };

    dbContext.Users.Add(user);
    await dbContext.SaveChangesAsync();

    return Results.Ok("User registered successfully!");
});

app.MapPost("/api/v1/auth/login", async (ApplicationDbContext dbContext, UserDto userDto) =>
{
    if (string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password))
    {
        return Results.BadRequest("Email and password are required.");
    }

    var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == userDto.Email);

    if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
    {
        return Results.Unauthorized();
    }

    await dbContext.SaveChangesAsync();

    var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
        };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: jwtSettings["Issuer"],
        audience: jwtSettings["Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: creds);

    var tokenHandler = new JwtSecurityTokenHandler();
    var jwt = tokenHandler.WriteToken(token);

    await dbContext.SaveChangesAsync();

    return Results.Ok(new { token = jwt });
});

app.MapGet("/api/v1/movies", async (ApplicationDbContext dbContext) =>
{
    var movies = await dbContext.Movies.ToArrayAsync();

    return Results.Ok(movies);
}).RequireAuthorization();

app.MapPost("/api/v1/movies", async (HttpContext httpContext, ApplicationDbContext dbContext, MovieDto movieDto) =>
{
    var userEmailClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
    if (userEmailClaim == null)
    {
        return Results.Unauthorized();
    }

    var userEmail = userEmailClaim.Value;

    var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == userEmail);
    if (user == null)
    {
        return Results.Unauthorized();
    }

    var userId = user.Id;

    if (string.IsNullOrWhiteSpace(movieDto.Title) || string.IsNullOrWhiteSpace(movieDto.Description) || string.IsNullOrWhiteSpace(movieDto.Rating))
    {
        return Results.BadRequest("Title, description, and rating are required.");
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

    dbContext.Movies.Add(movie);
    await dbContext.SaveChangesAsync();

    return Results.Ok("Movie added successfully!");
}).RequireAuthorization();

app.Run();
