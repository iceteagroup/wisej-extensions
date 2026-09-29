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
	/// Information about the authenticator.
	/// </summary>
	/// <remarks>
	/// Returned in <see cref="CredentialsResponse.AuthenticatorData"/>.
	/// <see cref="WebAuthn.CreateAsync"/> fills <see cref="PublicKey"/>; <see cref="WebAuthn.GetAsync"/> fills
	/// <see cref="RPIDHash"/>, the user flags and <see cref="Base64"/>.
	/// See <see href="https://w3c.github.io/webauthn/#sctn-authenticator-data"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// CredentialsResponse response = await WebAuthn.GetAsync(challenge, allowCredentials, 60000);
	/// AuthenticatorData data = response.AuthenticatorData;
	///
	/// if (!data.UserPresent || !data.UserVerified)
	/// {
	/// 	AlertBox.Show("The user was not verified by the authenticator.");
	/// 	return;
	/// }
	///
	/// bool valid = WebAuthn.Validate(publicKey, data.Base64, response.ClientData.Base64, response.Signature);
	/// ]]></code>
	/// </example>
	[ApiCategory("WebAuthn")]
	public class AuthenticatorData
	{
		/// <summary>
		/// Represents a hash of the Relaying Party ID (RPID). Only available in <see cref="WebAuthn.GetAsync"/>.
		/// </summary>
		public byte[] RPIDHash { get; set; }

		/// <summary>
		/// Represents the public key received from the client. Only available in <see cref="WebAuthn.CreateAsync"/>.
		/// </summary>
		public PublicKey PublicKey { get; set; }

		/// <summary>
		/// Defines whether the user is said to be "present".
		/// </summary>
		public bool UserPresent { get; set; }

		/// <summary>
		/// Defines whether the user is said to be "verified".
		/// </summary>
		public bool UserVerified { get; set; }

		/// <summary>
		/// Defines whether the public key credential source is allowed to be backed up.
		/// </summary>
		public bool BackupEligibility { get; set; }

		/// <summary>
		/// The authenticator data in base64 format.
		/// </summary>
		public string Base64 { get; set; }
	}
}
