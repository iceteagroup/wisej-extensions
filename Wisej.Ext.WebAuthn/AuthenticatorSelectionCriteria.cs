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
    /// Manages requirements regarding authenticator attributes.
    /// </summary>
    /// <remarks>
    /// See: https://w3c.github.io/webauthn/#dom-authenticatorselectioncriteria-authenticatorattachment.
    /// </remarks>
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
        /// <param name="authenticatorAttachment">The attachment modality.</param>
        /// <param name="residentKey"></param>
        /// <param name="requireResidentKey"></param>
        /// <param name="userVerification"></param>
        public AuthenticatorSelectionCriteria(AuthenticatorAttachment authenticatorAttachment, string residentKey="", bool requireResidentKey=false, ResidentKeyRequirement userVerification=ResidentKeyRequirement.Preferred)
        {
            this.AuthenticatorAttachment = authenticatorAttachment;
            this.RequireResidentKey = requireResidentKey;
            this.UserVerification = userVerification;
            this.ResidentKey = residentKey;
        }
    }
}
