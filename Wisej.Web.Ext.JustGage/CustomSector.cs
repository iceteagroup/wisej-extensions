///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// Author: Nic Adams
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
using System.Drawing;
using Wisej.Base;

namespace Wisej.Web.Ext.JustGage
{
	/// <summary>
	/// Represents a custom color sector of a <see cref="JustGage"/> control.
	/// </summary>
	/// <remarks>
	/// Custom sectors are assigned to the <see cref="JustGage.CustomSectors"/> property. When the gauge value
	/// falls between <see cref="Lo"/> and <see cref="Hi"/> (inclusive), the gauge level is drawn using <see cref="Color"/>.
	/// </remarks>
	[ApiCategory("JustGage")]
	public class CustomSector
    {
        /// <summary>
        /// Returns or sets the low boundary (inclusive) of this sector.
        /// </summary>
        /// <remarks>
        /// The boundary is an absolute gauge value, in the same units as <see cref="JustGage.Value"/>, not a percentage.
        /// </remarks>
        [SRDescription("JustGageCustomSectorLoDescr")]
        public int Lo { get; set; }

        /// <summary>
        /// Returns or sets the high boundary (inclusive) of this sector.
        /// </summary>
        /// <remarks>
        /// The boundary is an absolute gauge value, in the same units as <see cref="JustGage.Value"/>, not a percentage.
        /// </remarks>
        [SRDescription("JustGageCustomSectorHiDescr")]
        public int Hi { get; set; }

        /// <summary>
        /// Returns or sets the color used to draw the gauge level when the value is within this sector.
        /// </summary>
        [SRDescription("JustGageCustomSectorColorDescr")]
        public Color Color { get; set; }
    }
}
