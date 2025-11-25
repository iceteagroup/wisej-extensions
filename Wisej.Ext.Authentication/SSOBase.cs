using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Wisej.Ext.Authentication
{
	/// <summary>
	/// Abstract base class providing OAuth2 user-based authentication functionality.
	/// </summary>
	public abstract class SSOBase
	{
		protected readonly string ClientId;
		protected readonly string ClientSecret;
		protected readonly HttpClient HttpClient;
		protected string AccessToken;

		private TaskCompletionSource<string> _authCompletionSource;
		private string _oauthState;

		protected SSOBase(string clientId, string clientSecret)
		{
			ClientId = clientId;
			ClientSecret = clientSecret;
			HttpClient = new HttpClient();
			HttpClient.Timeout = TimeSpan.FromMinutes(60);
			HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd("WisejApp");
		}

		/// <summary>
		/// Authorization URL provided by the OAuth2 provider.
		/// </summary>
		protected virtual string AuthorizationUrl { get; set; }

		/// <summary>
		/// Access token exchange URL provided by the OAuth2 provider.
		/// </summary>
		protected virtual string TokenUrl { get; set; }

		/// <summary>
		/// Scope required for OAuth2 authentication.
		/// </summary>
		protected virtual string Scope { get; set; }

		/// <summary>
		/// Initiates the OAuth2 authentication flow.
		/// </summary>
		public virtual async Task<bool> AuthenticateAsync()
		{
			Wisej.Web.Application.ApplicationRefresh += OAuthCallbackHandler;

			_oauthState = Guid.NewGuid().ToString();
			var redirectUri = HttpUtility.UrlEncode(Wisej.Web.Application.Url);
			var authUrl = $"{AuthorizationUrl}?client_id={ClientId}&redirect_uri={redirectUri}&state={_oauthState}&scope={HttpUtility.UrlEncode(Scope)}";

			_authCompletionSource = new TaskCompletionSource<string>();
			Wisej.Web.Application.Navigate(authUrl);

			AccessToken = await _authCompletionSource.Task;

			return !string.IsNullOrWhiteSpace(AccessToken);
		}

		public virtual async void OAuthCallbackHandler(object sender, EventArgs e)
		{
			Wisej.Web.Application.ApplicationRefresh -= OAuthCallbackHandler;

			var state = Wisej.Web.Application.QueryString["state"];
			if (state != _oauthState)
				throw new InvalidOperationException("Invalid OAuth state (CSRF detected).");

			var code = Wisej.Web.Application.QueryString["code"];
			if (string.IsNullOrWhiteSpace(code))
				throw new InvalidOperationException("Authorization code missing.");

			Wisej.Web.Application.Navigate(Wisej.Web.Application.StartupUrl);

			AccessToken = await ExchangeCodeForTokenAsync(code);
			HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

			_authCompletionSource.SetResult(AccessToken);
		}

		/// <summary>
		/// Generic implementation of exchanging authorization code for an OAuth2 access token.
		/// </summary>
		public virtual async Task<string> ExchangeCodeForTokenAsync(string code)
		{
			var postData = new Dictionary<string, string>
			{
				{ "client_id", ClientId },
				{ "client_secret", ClientSecret },
				{ "code", code },
				{ "redirect_uri", Wisej.Web.Application.StartupUrl }
			};

			var response = await HttpClient.PostAsync(TokenUrl, new FormUrlEncodedContent(postData));
			response.EnsureSuccessStatusCode();

			var responseContent = await response.Content.ReadAsStringAsync();
			var parsedContent = HttpUtility.ParseQueryString(responseContent);

			return parsedContent["access_token"];
		}
	}
}
