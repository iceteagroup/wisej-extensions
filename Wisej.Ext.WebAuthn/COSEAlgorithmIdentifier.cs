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
    /// Identifies a cryptographic algorithm.
    /// </summary>
    /// <remarks>
    /// See: https://www.w3.org/TR/webauthn-2/#typedefdef-cosealgorithmidentifier
    /// </remarks>
    public enum COSEAlgorithmIdentifier
    {
        /// <summary>
        /// ES256 algorithm.
        /// </summary>
        ES256 = -7,

        /// <summary>
        /// ES384 algorithm.
        /// </summary>
        ES384 = -35,

        /// <summary>
        /// ES512 algorithm.
        /// </summary>
        ES512 = -36,

        /// <summary>
        /// EdDSA algorithm.
        /// </summary>
        EdDSA = -8,

        /// <summary>
        /// RS256 algorithm.
        /// </summary>
        RS256 = -257,

    }
}
