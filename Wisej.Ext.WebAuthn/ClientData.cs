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
	/// The client data represents the contextual bindings of both the WebAuthn Relying Party and the client.
	/// </summary>
	/// <remarks>
	/// <See href="https://w3c.github.io/webauthn/#dictionary-client-data"/>
	/// </remarks>	
	[ApiCategory("WebAuthn")]
	public class ClientData
    {
        /// <summary>
        /// This member contains the string "webauthn.create" when creating new credentials, 
        /// and "webauthn.get" when getting an assertion from an existing credential.
        /// </summary>
        /// <remarks>
        /// The purpose of this member is to prevent certain types of signature confusion attacks 
        /// (where an attacker substitutes one legitimate signature for another).
        /// </remarks>
        public string Type { get; set; }

        /// <summary>
        /// This member contains the fully qualified origin of the requester, as provided to the authenticator by the client, 
        /// in the syntax defined by [RFC6454].
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// The challenge returned from the client.
        /// </summary>
        public byte[] Challenge { get; set; }

        /// <summary>
        /// The client data in base64 format.
        /// </summary>
        public string Base64 { get; set; }
    }
}
