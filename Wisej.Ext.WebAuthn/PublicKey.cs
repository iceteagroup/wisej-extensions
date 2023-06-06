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

using System;

namespace Wisej.Ext.WebAuthn
{
	/// <summary>
	/// Represents information about a public key retrieved from the client.
	/// </summary>
	[ApiCategory("WebAuthn")]
	public class PublicKey
    {
        /// <summary>
        /// The credential ID returned from the client.
        /// </summary>
        public string CredentialID { get; set; }

        /// <summary>
        /// The public key data returned from the client.
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// The public key algorithm used.
        /// </summary>
        public COSEAlgorithmIdentifier Algorithm
        {
            get => (COSEAlgorithmIdentifier)Data["3"];
        }

        /// <summary>
        /// Creates a new instance of <see cref="PublicKey"/>.
        /// </summary>
        public PublicKey()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="PublicKey"/> with the given configuration.
        /// </summary>
        /// <param name="credentialID">The credential id received upon registration.</param>
        /// <param name="data">The dynamic public key object.</param>
        public PublicKey(string credentialID, dynamic data)
        {
            this.CredentialID = credentialID;
            this.Data = data;
        }

        /// <summary>
        /// Creates a new instance of <see cref="PublicKey"/> with the given JSON configuration.
        /// </summary>
        /// <param name="json">The serialized JSON string of the <see cref="PublicKey"/> object.</param>
        public PublicKey(string json)
        {
            var obj = JSON.Parse(json);

            this.CredentialID = obj.CredentialID;
            this.Data = obj.Data;
        }
    }
}
