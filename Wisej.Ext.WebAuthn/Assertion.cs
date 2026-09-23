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
	/// An authenticator's response to a client's request for generation of a new authentication assertion.
	/// </summary>
	/// <remarks>
	/// See <see href="https://w3c.github.io/webauthn/#iface-authenticatorassertionresponse"/>.
	/// </remarks>
	[ApiCategory("WebAuthn")]
	public class Assertion
	{
		/// <summary>
		/// The raw signature returned from the authenticator.
		/// </summary>
		public byte[] Signature { get; internal set; }

		/// <summary>
		/// The JSON-compatible serialization of client data.
		/// </summary>
		public string ClientDataJSON { get; internal set; } 

		/// <summary>
		/// The authenticator data returned by the authenticator.
		/// </summary>
		public string AuthenticatorData { get; internal set; }


		/// <summary>
		/// Creates a new instance of <see cref="Assertion"/>.
		/// </summary>
		/// <remarks>
		/// The <see cref="Signature"/>, <see cref="ClientDataJSON"/> and <see cref="AuthenticatorData"/>
		/// properties can only be set internally; use the
		/// <see cref="Assertion(byte[], string, string)"/> overload to initialize them.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var assertion = new Assertion();
		///
		/// // All properties are null until they are assigned by the WebAuthn extension.
		/// bool isEmpty = assertion.Signature == null;
		/// ]]></code>
		/// </example>
		public Assertion()
        {
        }

		/// <summary>
		/// Creates a new instance of <see cref="Assertion"/> with the given configuration.
		/// </summary>
		/// <param name="signature">The raw signature returned by the authenticator.</param>
		/// <param name="clientDataJSON">The JSON-compatible serialization of the client data.</param>
		/// <param name="authenticatorData">The authenticator data returned by the authenticator.</param>
		/// <example>
		/// <code><![CDATA[
		/// // Build an Assertion from the values returned by WebAuthn.GetAsync().
		/// CredentialsResponse response = await WebAuthn.GetAsync(challenge, allowCredentials, 60000);
		///
		/// var assertion = new Assertion(
		/// 	response.Signature,
		/// 	response.ClientData.Base64,
		/// 	response.AuthenticatorData.Base64);
		/// ]]></code>
		/// </example>
		public Assertion(byte[] signature, string clientDataJSON, string authenticatorData)
        {
            this.Signature = signature;
            this.ClientDataJSON = clientDataJSON;
            this.AuthenticatorData = authenticatorData;
        }
    }
}
