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
    /// Relying Party's rerquirements for client-side discoverable credentials.
    /// </summary>
    /// <remarks>
    /// See: https://www.w3.org/TR/webauthn-2/#enum-residentKeyRequirement.
    /// </remarks>
    public enum ResidentKeyRequirement
    {
        /// <summary>
        /// This value indicates the Relying Party prefers creating a server-side credential, 
        /// but will accept a client-side discoverable credential.
        /// </summary>
        Discouraged,

        /// <summary>
        /// This value indicates the Relying Party strongly prefers creating a client-side discoverable credential, 
        /// but will accept a server-side credential.
        /// </summary>
        Preferred,

        /// <summary>
        /// This value indicates the Relying Party requires a client-side discoverable credential, and is prepared 
        /// to receive an error if a client-side discoverable credential cannot be created.
        /// </summary>
        Required
    }
}
