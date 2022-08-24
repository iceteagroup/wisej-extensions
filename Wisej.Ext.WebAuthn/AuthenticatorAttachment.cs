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
    /// This enumeration’s values describe authenticators' attachment modalities.
    /// </summary>
    /// <remarks>
    /// See: https://w3c.github.io/webauthn/#enum-attachment.
    /// </remarks>
    public enum AuthenticatorAttachment
    {
        /// <summary>
        /// A platform authenticator is attached using a client device-specific transport, called platform attachment, and is usually not removable from the client device.
        /// </summary>
        Platform,

        /// <summary>
        /// A roaming authenticator is attached using cross-platform transports, called cross-platform attachment. Authenticators of this class are removable from, and can "roam" between, client devices.
        /// </summary>
        CrossPlatform
    }
}
