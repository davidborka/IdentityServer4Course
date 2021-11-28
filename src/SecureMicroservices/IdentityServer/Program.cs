using IdentityServer4.Models;
using IdentityServer4.Test;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddIdentityServer()
	.AddInMemoryClients(IdentityServer.Config.Clients)
	.AddInMemoryIdentityResources(IdentityServer.Config.IdentityResources)
	.AddInMemoryApiResources(IdentityServer.Config.ApiResources)
	.AddInMemoryApiScopes(IdentityServer.Config.ApiScopes)
	.AddTestUsers(IdentityServer.Config.TestUsers)
	.AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.MapRazorPages();

app.Run();