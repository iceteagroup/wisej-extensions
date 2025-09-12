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
	/// Represents the result of a request for credentials from the client.
	/// </summary>
	[ApiCategory("WebAuthn")]
	public class CredentialsResponse
	{
		/// <summary>
		/// The authenticator data provided by the client.
		/// </summary>
		/// <remarks>
		/// <See href="https://w3c.github.io/webauthn/#authenticator-data"/>
		/// </remarks>
		public AuthenticatorData AuthenticatorData { get; set; }

		/// <summary>
		/// The client data provided by the client.
		/// </summary>
		/// <remarks>
		/// See: <see href="https://w3c.github.io/webauthn/#dictdef-collectedclientdata"/>.
		/// </remarks>
		public ClientData ClientData { get; set; }

		/// <summary>
		/// Represents an assertion by the authenticator that the user has consented to a specific transaction.
		/// </summary>
		/// <remarks>
		/// See: <see href="https://w3c.github.io/webauthn/#webauthn-signature"/>.
		/// </remarks>
		public byte[] Signature { get; set; }

		/// <summary>
		/// The user handle associated when this public key credential source was created.
		/// </summary>
		/// <remarks>
		/// See: <see href="https://w3c.github.io/webauthn/#public-key-credential-source-userhandle"/>
		/// </remarks>
		public string UserHandle { get; set; }

		/// <summary>
		/// The number of successful calls to authenticatorGetAssertion(). Used for detecting cloned authenticators.
		/// </summary>
		/// <remarks>
		/// See: <see href="https://w3c.github.io/webauthn/#signature-counter"/>.
		/// </remarks>
		public int SignatureCounter { get; set; }
	}
}
