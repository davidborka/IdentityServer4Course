using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.API.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MoviesContext>(options =>
{
	options.UseInMemoryDatabase("Movies");
});
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
	options.Authority = "https://localhost:5005";
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateAudience = false,
	};
});
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "movieClient"));
});
var app = builder.Build();
MovieContextSeed.SeedDatabase(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();


