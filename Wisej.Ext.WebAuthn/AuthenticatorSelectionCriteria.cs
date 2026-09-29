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
	/// Manages requirements regarding authenticator attributes.
	/// </summary>
	/// <remarks>
	/// See <see href="https://w3c.github.io/webauthn/#dom-authenticatorselectioncriteria-authenticatorattachment"/>.
	/// </remarks>	
	[ApiCategory("WebAuthn")]
	public class AuthenticatorSelectionCriteria
    {
        /// <summary>
        /// If this member is present, eligible authenticators are filtered to be only 
        /// those authenticators attached with the specified authenticator attachment modality.
        /// </summary>
        public AuthenticatorAttachment AuthenticatorAttachment { get; set; }

        /// <summary>
        /// Specifies the extent to which the Relying Party desires to create a client-side discoverable credential.
        /// </summary>
        public string ResidentKey { get; set; } = "";

        /// <summary>
        /// This member is retained for backwards compatibility with WebAuthn Level 1 and, for historical reasons, 
        /// its naming retains the deprecated “resident” terminology for discoverable credentials.
        /// </summary>
        public bool RequireResidentKey { get; set; } = false;

        /// <summary>
        /// This member specifies the Relying Party's requirements regarding user verification.
        /// </summary>
        public ResidentKeyRequirement UserVerification { get; set; } = ResidentKeyRequirement.Preferred;

        /// <summary>
        /// Creates a new instance of <see cref="AuthenticatorSelectionCriteria"/> with the given configuration.
        /// </summary>
        /// <param name="authenticatorAttachment">The attachment modality: <see cref="F:Wisej.Ext.WebAuthn.AuthenticatorAttachment.Platform"/> (e.g. Windows Hello) or <see cref="F:Wisej.Ext.WebAuthn.AuthenticatorAttachment.CrossPlatform"/> (e.g. a security key).</param>
        /// <param name="residentKey">The extent to which a client-side discoverable credential is desired: "discouraged", "preferred" or "required". Empty by default.</param>
        /// <param name="requireResidentKey">Legacy WebAuthn Level 1 flag; set to true only when <paramref name="residentKey"/> is "required". False by default.</param>
        /// <param name="userVerification">The Relying Party's user verification requirement. <see cref="ResidentKeyRequirement.Preferred"/> by default.</param>
        /// <example>
        /// <code><![CDATA[
        /// // Built-in authenticator, discoverable credential, user verification required.
        /// var selection = new AuthenticatorSelectionCriteria(
        /// 	AuthenticatorAttachment.Platform,
        /// 	residentKey: "required",
        /// 	requireResidentKey: true,
        /// 	userVerification: ResidentKeyRequirement.Required);
        ///
        /// CredentialsResponse response = await WebAuthn.CreateAsync(
        /// 	challenge, rp, user, parameters, selection, 60000, AttestationConveyancePreference.None);
        /// ]]></code>
        /// </example>
        public AuthenticatorSelectionCriteria(AuthenticatorAttachment authenticatorAttachment, string residentKey="", bool requireResidentKey=false, ResidentKeyRequirement userVerification=ResidentKeyRequirement.Preferred)
        {
            this.AuthenticatorAttachment = authenticatorAttachment;
            this.RequireResidentKey = requireResidentKey;
            this.UserVerification = userVerification;
            this.ResidentKey = residentKey;
        }
    }
}
