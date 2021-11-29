using IdentityModel.Client;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices
{
	public class MovieService : IMovieApiService
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public MovieService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<Movie> CreateMovie(Movie movie)
		{
			throw new NotImplementedException();
		}

		public Task DeleteMovie(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Movie> GetMovie(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Movie>> GetMovies()
		{
			var httpClient = _httpClientFactory.CreateClient("MoviesAPIClient");

			var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies");

			var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			List<Movie> movieList = JsonConvert.DeserializeObject<List<Movie>>(content);

			return movieList;
			//var apiClientCredentials = new ClientCredentialsTokenRequest
			//{
			//	Address = "https://localhost:5005/connect/token",
			//	ClientId = "movieClient",
			//	ClientSecret = "secret",
			//	Scope = "movieAPI"
			//};
			//var httpClient = new HttpClient();

			//var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5005");

			//if (disco.IsError)
			//{
			//	return null;
			//}
			//var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(apiClientCredentials);

			//if (tokenResponse.IsError)
			//{
			//	return null;
			//}


			//var apiClient = new HttpClient();
			//apiClient.SetBearerToken(tokenResponse.AccessToken);

			//var response = await apiClient.GetAsync("https://localhost:5001/api/movies");
			//response.EnsureSuccessStatusCode();

			//var content = await response.Content.ReadAsStringAsync();
			//List<Movie> movieList = JsonConvert.DeserializeObject<List<Movie>>(content);


			//return await Task.FromResult(movieList);
		}

		public Task<Movie> UpdateMovie(Movie movie)
		{
			throw new NotImplementedException();
		}
	}
}
