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
	/// Names and identifier for the user account performing the registration.
	/// </summary>
	/// <remarks>
	/// See <see href="https://w3c.github.io/webauthn/#dom-publickeycredentialcreationoptions-user"/>.
	/// </remarks>
	[ApiCategory("WebAuthn")]
	public class PublicKeyCredentialUserEntity
    {
        /// <summary>
        /// The user handle of the user account.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A name for the user account.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A human-palatable name for the user account, intended only for display.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="PublicKeyCredentialUserEntity"/>.
        /// </summary>
        /// <example>
        /// <code><![CDATA[
        /// var user = new PublicKeyCredentialUserEntity
        /// {
        /// 	Id = "user-1234",
        /// 	Name = "jane.doe@example.com",
        /// 	DisplayName = "Jane Doe"
        /// };
        /// ]]></code>
        /// </example>
        public PublicKeyCredentialUserEntity()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="PublicKeyCredentialUserEntity"/> with the given configuration.
        /// </summary>
        /// <param name="id">The user handle. Use a stable, opaque identifier without personal information; it is returned in <see cref="CredentialsResponse.UserHandle"/> on login.</param>
        /// <param name="name">The user name, e.g. an email address.</param>
        /// <param name="displayName">The name shown to the user by the authenticator.</param>
        /// <example>
        /// <code><![CDATA[
        /// var user = new PublicKeyCredentialUserEntity("user-1234", "jane.doe@example.com", "Jane Doe");
        ///
        /// CredentialsResponse response = await WebAuthn.CreateAsync(
        /// 	challenge, rp, user, parameters, selection, 60000, AttestationConveyancePreference.None);
        /// ]]></code>
        /// </example>
        public PublicKeyCredentialUserEntity(string id, string name, string displayName)
        {
            this.Id = id;
            this.Name = name;
            this.DisplayName = displayName;
        }
    }
}