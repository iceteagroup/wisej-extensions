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
	/// User account parameters for credential generation.
	/// </summary>
	/// <remarks>
	/// <See href="https://w3c.github.io/webauthn/#dictionary-rp-credential-params"/>
	/// </remarks>
	[ApiCategory("WebAuthn")]
	public class RelyingParty
	{
		/// <summary>
		/// The user handle of the user account.
		/// </summary>
		public string ID { get; set; }

		/// <summary>
		/// A human-palatable name for the entity.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="RelyingParty"/> with the given configuration.
		/// </summary>
		/// <param name="id">The RP ID.</param>
		/// <param name="name">The RP name.</param>
		public RelyingParty(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
        public override string ToString()
        {
			return new
			{
				id = ID,
				name = Name,
			}.ToJSON();
        }
    }
}
