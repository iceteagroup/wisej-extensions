using System;

namespace Wisej.Ext.Authentication
{
	/// <summary>
	/// Google Service Account OAuth2 authentication provider.
	/// </summary>
	public class GoogleService : ServiceBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GoogleService"/> class.
		/// </summary>
		/// <param name="clientEmail">The service account client email.</param>
		/// <param name="privateKey">The private key in PEM format.</param>
		/// <param name="scope">The OAuth2 scope for Google API access.</param>
		public GoogleService(string clientEmail, string privateKey, string scope)
			: base(clientEmail, privateKey)
		{
			this.Scope = scope ?? throw new ArgumentNullException(nameof(scope));
		}

		/// <summary>
		/// OAuth2 token URL for Google service accounts.
		/// </summary>
		protected override string TokenUrl => "https://oauth2.googleapis.com/token";

		/// <summary>
		/// OAuth2 scope required for access.
		/// </summary>
		protected override string Scope { get; set; }
	}
}
