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

namespace Wisej.Ext.WebAuthn
{
	/// <summary>
	/// An authenticator's response to a client's request for generation of a new authentication assertion.
	/// </summary>
	/// <remarks>
	/// See: https://w3c.github.io/webauthn/#iface-authenticatorassertionresponse.
	/// </remarks>
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
		public Assertion()
        {
        }

		/// <summary>
		/// Creates a new instance of <see cref="Assertion"/> with the given configuration.
		/// </summary>
		/// <param name="signature">The authenticator's signature.</param>
		/// <param name="clientDataJSON">Client data.</param>
		/// <param name="authenticatorData">Authenticator data.</param>
		public Assertion(byte[] signature, string clientDataJSON, string authenticatorData)
        {
            this.Signature = signature;
            this.ClientDataJSON = clientDataJSON;
            this.AuthenticatorData = authenticatorData;
        }
    }
}
