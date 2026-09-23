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
	/// <remarks>
	/// Returned by <see cref="WebAuthn.CreateAsync"/> and <see cref="WebAuthn.GetAsync"/>.
	/// <see cref="Signature"/> and <see cref="UserHandle"/> are only set by <see cref="WebAuthn.GetAsync"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// // Registration: keep the public key.
	/// CredentialsResponse created = await WebAuthn.CreateAsync(
	/// 	challenge, rp, user, parameters, selection, 60000, AttestationConveyancePreference.None);
	/// PublicKey publicKey = created.AuthenticatorData.PublicKey;
	///
	/// // Login: validate the signature with the stored public key.
	/// CredentialsResponse assertion = await WebAuthn.GetAsync(challenge2, allowCredentials, 60000);
	/// bool valid = WebAuthn.Validate(
	/// 	publicKey,
	/// 	assertion.AuthenticatorData.Base64,
	/// 	assertion.ClientData.Base64,
	/// 	assertion.Signature);
	/// ]]></code>
	/// </example>
	[ApiCategory("WebAuthn")]
	public class CredentialsResponse
	{
		/// <summary>
		/// The authenticator data provided by the client.
		/// </summary>
		/// <remarks>
		/// See <see href="https://w3c.github.io/webauthn/#authenticator-data"/>.
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
