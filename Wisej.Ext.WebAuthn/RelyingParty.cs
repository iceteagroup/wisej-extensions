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
	/// See <see href="https://w3c.github.io/webauthn/#dictionary-rp-credential-params"/>.
	/// </remarks>
	[ApiCategory("WebAuthn")]
	public class RelyingParty
	{
		/// <summary>
		/// The Relying Party identifier (RP ID), typically the domain of the application.
		/// </summary>
		public string ID { get; set; }

		/// <summary>
		/// A human-palatable name for the entity.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="RelyingParty"/> with the given configuration.
		/// </summary>
		/// <param name="id">The RP ID: the domain of the application (e.g. "example.com"). It must match the current origin or a registrable suffix of it.</param>
		/// <param name="name">The human-palatable name of the Relying Party, shown to the user.</param>
		/// <example>
		/// <code><![CDATA[
		/// var rp = new RelyingParty("example.com", "Example Inc.");
		///
		/// CredentialsResponse response = await WebAuthn.CreateAsync(
		/// 	challenge, rp, user, parameters, selection, 60000, AttestationConveyancePreference.None);
		/// ]]></code>
		/// </example>
		public RelyingParty(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A JSON string with the <c>id</c> and <c>name</c> members.</returns>
        /// <example>
        /// <code><![CDATA[
        /// var rp = new RelyingParty("example.com", "Example Inc.");
        ///
        /// string json = rp.ToString();
        /// // {"id":"example.com","name":"Example Inc."}
        /// ]]></code>
        /// </example>
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
