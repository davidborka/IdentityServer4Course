using IdentityModel.Client;

namespace Movies.Client.HttpHandler
{
	public class AuthenticationDelegateHandler:DelegatingHandler
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ClientCredentialsTokenRequest _tokenRequest;

		public AuthenticationDelegateHandler(IHttpClientFactory httpClientFactory, ClientCredentialsTokenRequest tokenRequest)
		{
			_httpClientFactory = httpClientFactory;
			_tokenRequest = tokenRequest;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var httpClient = _httpClientFactory.CreateClient("IDPClient");

			var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(_tokenRequest);

			if (tokenResponse.IsError)
			{
				throw new HttpRequestException("Something went wrong while requestin the access token");
			}
			request.SetBearerToken(tokenResponse.AccessToken);
			return await base.SendAsync(request, cancellationToken);
		}
	}
}
