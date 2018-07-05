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

using System.Drawing;
using Wisej.Base;

namespace Wisej.Web.Ext.JustGage
{
	/// <summary>
	/// Represents custom sectors as used by JustGauge
	/// </summary>
	public class CustomSector
    {
        /// <summary>
        /// The low boundary of this sector
        /// </summary>
        [SRDescription("JustGageCustomSectorLoDescr")]
        public int Lo { get; set; }

        /// <summary>
        /// The high boundary of this sector
        /// </summary>
        [SRDescription("JustGageCustomSectorHiDescr")]
        public int Hi { get; set; }

        /// <summary>
        /// The color this sector should be drawn in
        /// </summary>
        [SRDescription("JustGageCustomSectorColorDescr")]
        public Color Color { get; set; }
    }
}
