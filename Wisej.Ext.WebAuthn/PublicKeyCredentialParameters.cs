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
	/// <See href="https://www.w3.org/TR/webauthn-2/#dictdef-publickeycredentialparameters"/>
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
		public PublicKeyCredentialParameters()
        {
        }

		/// <summary>
		/// Creates a new instance of <see cref="PublicKeyCredentialParameters"/> with the given configuration.
		/// </summary>
		/// <param name="type">The type of credential key (i.e. "public-key").</param>
		/// <param name="alg"></param>
		public PublicKeyCredentialParameters(COSEAlgorithmIdentifier alg, string type="public-key")
        {
            this.Type = type;
            this.Alg = alg;
        }

		/// <summary>
		/// Returns a JSON representation of the current object.
		/// </summary>
		/// <returns>The JSON object representation.</returns>
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
