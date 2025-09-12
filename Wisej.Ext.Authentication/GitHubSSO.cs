using System;

namespace Wisej.Ext.Authentication
{
	/// <summary>
	/// Provides GitHub Single Sign-On (SSO) functionality using OAuth2.
	/// Inherits from <see cref="SSOBase"/> and predefines GitHub-specific
	/// authorization and token endpoints.
	/// </summary>
	public class GitHubSSO : SSOBase
	{
		/// <summary>
		/// Gets the GitHub authorization URL used to initiate the OAuth2 flow.
		/// </summary>
		protected override string AuthorizationUrl => "https://github.com/login/oauth/authorize";

		/// <summary>
		/// Gets the GitHub token URL used to exchange the authorization code for an access token.
		/// </summary>
		protected override string TokenUrl => "https://github.com/login/oauth/access_token";

		/// <summary>
		/// Gets or sets the OAuth2 scope requested during the authorization process.
		/// Defaults to: <c>repo read:user user:email</c>.
		/// </summary>
		protected override string Scope => "repo read:user user:email";

		/// <summary>
		/// Initializes a new instance of the <see cref="GitHubSSO"/> class with
		/// the specified <paramref name="clientId"/> and <paramref name="clientSecret"/>.
		/// Uses the default GitHub scope.
		/// </summary>
		/// <param name="clientId">The client ID of the GitHub OAuth app.</param>
		/// <param name="clientSecret">The client secret of the GitHub OAuth app.</param>
		public GitHubSSO(string clientId, string clientSecret)
			: base(clientId, clientSecret)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GitHubSSO"/> class with
		/// the specified <paramref name="clientId"/>, <paramref name="clientSecret"/>,
		/// and a custom <paramref name="scope"/>.
		/// </summary>
		/// <param name="clientId">The client ID of the GitHub OAuth app.</param>
		/// <param name="clientSecret">The client secret of the GitHub OAuth app.</param>
		/// <param name="scope">A custom OAuth2 scope string to override the default.</param>
		public GitHubSSO(string clientId, string clientSecret, string scope)
			: base(clientId, clientSecret)
		{
			this.Scope = scope;
		}
	}
}
