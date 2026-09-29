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

using System;
using System.ComponentModel;

namespace Wisej.Ext.WebAuthn
{
	/// <summary>
	/// Contains information about the desired properties of the credential to be created.
	/// </summary>
	/// <remarks>
	/// See <see href="https://www.w3.org/TR/webauthn-2/#dictdef-publickeycredentialparameters"/>.
	/// </remarks>
	[ApiCategory("WebAuthn")]
	public class PublicKeyCredentialParameters
	{
		/// <summary>
		/// This member specifies the type of credential to be created (i.e. "public-key").
		/// </summary>
		public string Type { get; set; } = "public-key";

		/// <summary>
		/// Specifies the cryptographic signature algorithm with which the newly generated 
		/// credential will be used, and thus also the type of asymmetric key pair to be generated, 
		/// e.g., RSA or Elliptic Curve.
		/// </summary>
		public COSEAlgorithmIdentifier Alg { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="PublicKeyCredentialParameters"/>.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var parameter = new PublicKeyCredentialParameters
		/// {
		/// 	Alg = COSEAlgorithmIdentifier.ES256
		/// };
		/// ]]></code>
		/// </example>
		public PublicKeyCredentialParameters()
        {
        }

		/// <summary>
		/// Creates a new instance of <see cref="PublicKeyCredentialParameters"/> with the given configuration.
		/// </summary>
		/// <param name="alg">The signature algorithm the new credential will use.</param>
		/// <param name="type">The type of credential key. Defaults to "public-key".</param>
		/// <example>
		/// <code><![CDATA[
		/// // List the accepted algorithms in order of preference.
		/// var parameters = new[]
		/// {
		/// 	new PublicKeyCredentialParameters(COSEAlgorithmIdentifier.ES256),
		/// 	new PublicKeyCredentialParameters(COSEAlgorithmIdentifier.RS256)
		/// };
		///
		/// CredentialsResponse response = await WebAuthn.CreateAsync(
		/// 	challenge, rp, user, parameters, selection, 60000, AttestationConveyancePreference.None);
		/// ]]></code>
		/// </example>
		public PublicKeyCredentialParameters(COSEAlgorithmIdentifier alg, string type="public-key")
        {
            this.Type = type;
            this.Alg = alg;
        }

        /// <summary>
        /// Returns a JSON representation of the current object.
        /// </summary>
        /// <returns>A JSON string with the <c>type</c> and <c>alg</c> members, where <c>alg</c> is the numeric COSE identifier.</returns>
        /// <example>
        /// <code><![CDATA[
        /// var parameter = new PublicKeyCredentialParameters(COSEAlgorithmIdentifier.ES256);
        ///
        /// string json = parameter.ToJSON();
        /// // {"type":"public-key","alg":-7}
        /// ]]></code>
        /// </example>
        public string ToJSON() 
		{
			return new
			{
				type = Type,
				alg = (int)Alg
			}.ToJSON();
		}
	}
}
