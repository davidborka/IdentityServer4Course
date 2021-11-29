using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Movies.Client.ApiServices;
using Movies.Client.HttpHandler;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => {
	options.Authority = "https://localhost:5005";
	options.ClientId = "movies_mvc_client";
	options.ClientSecret = "secret";
	options.ResponseType = "code";
	options.Scope.Add("openid");
	options.Scope.Add("profile");
	options.SaveTokens = true;
	options.GetClaimsFromUserInfoEndpoint = true;

});


builder.Services.AddTransient<IMovieApiService, MovieService>();
builder.Services.AddTransient<AuthenticationDelegateHandler>();
builder.Services.AddHttpClient("MoviesAPIClient", client =>
{
	client.BaseAddress = new Uri("https://localhost:5001/");
	client.DefaultRequestHeaders.Clear();
	client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddHttpMessageHandler<AuthenticationDelegateHandler>();

builder.Services.AddHttpClient("IDPClient", client =>
{
	client.BaseAddress = new Uri("https://localhost:5005/");
	client.DefaultRequestHeaders.Clear();
	client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddSingleton(new ClientCredentialsTokenRequest
{
	Address = "https://localhost:5005/connect/token",
	ClientId = "movieClient",
	ClientSecret = "secret",
	Scope = "movieAPI"
});

// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
