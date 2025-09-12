using Jose;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace Wisej.Ext.Authentication
{
	/// <summary>
	/// Abstract base class for authenticating with OAuth2 service accounts using JWT bearer tokens.
	/// Designed for compatibility with .NET Framework using the Jose.JWT library for signing.
	/// </summary>
	public abstract class ServiceBase
	{
		/// <summary>
		/// The client email from the service account credentials.
		/// </summary>
		protected string ClientEmail;

		/// <summary>
		/// The private key in PEM format used to sign JWT assertions.
		/// </summary>
		protected string PrivateKey;

		/// <summary>
		/// The HTTP client used for making authenticated requests.
		/// </summary>
		protected readonly HttpClient HttpClient;

		/// <summary>
		/// The last retrieved OAuth2 access token.
		/// </summary>
		protected string AccessToken;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceBase"/> class.
		/// </summary>
		/// <param name="clientEmail">Service account email (issuer for the JWT).</param>
		/// <param name="privateKey">PEM-encoded private key to sign JWT assertions.</param>
		/// <exception cref="ArgumentNullException">Thrown if either parameter is null.</exception>
		protected ServiceBase(string clientEmail, string privateKey)
		{
			ClientEmail = clientEmail ?? throw new ArgumentNullException(nameof(clientEmail));
			PrivateKey = privateKey ?? throw new ArgumentNullException(nameof(privateKey));

			HttpClient = new HttpClient
			{
				Timeout = TimeSpan.FromMinutes(5)
			};
			HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd("WisejApp");
		}

		/// <summary>
		/// Gets the token endpoint URL used to exchange the JWT for an access token.
		/// </summary>
		protected abstract string TokenUrl { get; }

		/// <summary>
		/// Gets the OAuth2 scope used for authorization.
		/// </summary>
		protected abstract string Scope { get; set; }

		/// <summary>
		/// Authenticates the service account by generating a signed JWT and exchanging it for an access token.
		/// </summary>
		/// <returns><c>true</c> if authentication succeeds; otherwise, <c>false</c>.</returns>
		public virtual async Task<bool> AuthenticateAsync()
		{
			var jwt = CreateSignedJwt();

			var postData = new Dictionary<string, string>
			{
				{ "grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer" },
				{ "assertion", jwt }
			};

			var response = await HttpClient.PostAsync(TokenUrl, new FormUrlEncodedContent(postData));
			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();
			var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(json);

			AccessToken = tokenResponse.AccessToken;
			HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

			return !string.IsNullOrWhiteSpace(AccessToken);
		}

		/// <summary>
		/// Creates a signed JWT assertion using the service account credentials.
		/// </summary>
		/// <returns>A JWT string signed with RS256 using the private key.</returns>
		protected virtual string CreateSignedJwt()
		{
			var now = DateTimeOffset.UtcNow;

			var payload = new Dictionary<string, object>
			{
				{ "iss", ClientEmail },
				{ "scope", Scope },
				{ "aud", TokenUrl },
				{ "exp", now.ToUnixTimeSeconds() + 3600 },
				{ "iat", now.ToUnixTimeSeconds() }
			};

			using (var rsa = LoadRsaPrivateKey(PrivateKey))
			{
				return JWT.Encode(payload, rsa, JwsAlgorithm.RS256);
			}
		}

		/// <summary>
		/// Loads an RSA instance from a PEM-encoded private key string.
		/// Supports PKCS#8 and PKCS#1 formats.
		/// </summary>
		/// <param name="pem">The PEM-encoded private key string.</param>
		/// <returns>An <see cref="RSA"/> instance initialized with the key.</returns>
		/// <exception cref="PlatformNotSupportedException">Thrown on platforms that don't support private key import.</exception>
		private static RSA LoadRsaPrivateKey(string pem)
		{
			var keyText = pem
				.Replace("-----BEGIN PRIVATE KEY-----", "")
				.Replace("-----END PRIVATE KEY-----", "")
				.Replace("-----BEGIN RSA PRIVATE KEY-----", "")
				.Replace("-----END RSA PRIVATE KEY-----", "")
				.Replace("\r", "")
				.Replace("\n", "")
				.Trim();

			var keyBytes = Convert.FromBase64String(keyText);

			var rsa = RSA.Create();

#if NETCOREAPP
			try
			{
				// Try importing as PKCS#8
				rsa.ImportPkcs8PrivateKey(new ReadOnlySpan<byte>(keyBytes), out _);
			}
			catch
			{
				// Fallback to PKCS#1
				rsa.ImportRSAPrivateKey(new ReadOnlySpan<byte>(keyBytes), out _);
			}
#else
			throw new PlatformNotSupportedException("ImportPkcs8PrivateKey");
#endif
			return rsa;
		}

		/// <summary>
		/// Represents the structure of the token response from the OAuth2 token endpoint.
		/// </summary>
		private class TokenResponse
		{
			/// <summary>
			/// Gets or sets the access token issued by the server.
			/// </summary>
			[JsonProperty("access_token")]
			public string AccessToken { get; set; }

			/// <summary>
			/// Gets or sets the type of the token (typically "Bearer").
			/// </summary>
			[JsonProperty("token_type")]
			public string TokenType { get; set; }

			/// <summary>
			/// Gets or sets the lifetime in seconds of the access token.
			/// </summary>
			[JsonProperty("expires_in")]
			public int ExpiresIn { get; set; }
		}
	}
}
