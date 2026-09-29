///////////////////////////////////////////////////////////////////////////////
//
// (C) 2022 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// 
//
// ALL INFORMATION CONTAINED HEREIN IS, AND REMAINS
// THE PROPERTY OF ICE TEA GROUP LLC AND ITS SUPPLIERS, IF ANY.
// THE INTELLECTUAL PROPERTY AND TECHNICAL CONCEPTS CONTAINED
// HEREIN ARE PROPRIETARY TO ICE TEA GROUP LLC AND ITS SUPPLIERS
// AND MAY BE COVERED BY U.S. AND FOREIGN PATENTS, PATENT IN PROCESS, AND
// ARE PROTECTED BY TRADE SECRET OR COPYRIGHT LAW.
//
// DISSEMINATION OF THIS INFORMATION OR REPRODUCTION OF THIS MATERIAL
// IS STRICTLY FORBIDDEN UNLESS PRIOR WRITTEN PERMISSION IS OBTAINED
// FROM ICE TEA GROUP LLC.
//
///////////////////////////////////////////////////////////////////////////////

using System.ComponentModel;

namespace Wisej.Ext.WebAuthn
{
	/// <summary>
	/// Identifies the credential to be retrieved from the client.
	/// </summary>
	/// <remarks>
	/// See <see href="https://www.w3.org/TR/webauthn-2/#dictdef-publickeycredentialdescriptor"/>.
	/// </remarks>
	[ApiCategory("WebAuthn")]
	public class PublicKeyCredentialDescriptor
	{
		/// <summary>
		/// The credential ID.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// The type of credential to retrieve.
		/// </summary>
		public string Type { get; set; } = "public-key";

		/// <summary>
		/// This OPTIONAL member contains a hint as to how the client might communicate 
		/// with the managing authenticator of the public key credential the caller is referring to.
		/// </summary>
		public AuthenticatorTransport[] Transports { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="PublicKeyCredentialDescriptor"/>.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var descriptor = new PublicKeyCredentialDescriptor
		/// {
		/// 	Id = storedCredentialID,
		/// 	Transports = new[] { AuthenticatorTransport.Internal }
		/// };
		/// ]]></code>
		/// </example>
		public PublicKeyCredentialDescriptor()
        {
        }

		/// <summary>
		/// Creates a new instance of <see cref="PublicKeyCredentialDescriptor"/> with the given configuration.
		/// </summary>
		/// <param name="id">The base64 credential ID, as returned in <see cref="PublicKey.CredentialID"/> upon registration.</param>
		/// <param name="type">The type of credential to retrieve. Defaults to "public-key".</param>
		/// <param name="transports">Optional hints for how the client can reach the authenticator.</param>
		/// <example>
		/// <code><![CDATA[
		/// // Ask the user to authenticate with the credential registered earlier.
		/// var allowCredentials = new[]
		/// {
		/// 	new PublicKeyCredentialDescriptor(storedCredentialID)
		/// };
		///
		/// CredentialsResponse response = await WebAuthn.GetAsync(challenge, allowCredentials, 60000);
		/// ]]></code>
		/// </example>
		public PublicKeyCredentialDescriptor(string id, string type="public-key", AuthenticatorTransport[] transports=null)
        {
            this.Id = id;
            this.Type = type;
            this.Transports = transports;
        }
    }
}
