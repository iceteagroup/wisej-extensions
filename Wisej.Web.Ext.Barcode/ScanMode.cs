///////////////////////////////////////////////////////////////////////////////
//
// (C) 2020 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.Barcode
{
	/// <summary>
	/// Specifies the mode in which the scanner should detect barcodes.
	/// </summary>
	[ApiCategory("Barcode")]
	public enum ScanMode
	{
		/// <summary>
		/// Continuously scans the environment for barcodes.
		/// </summary>
		Automatic,

		/// <summary>
		/// Stops scanning after one successful barcode detection.
		/// </summary>
		/// <remarks>Reset the scanner by calling <see cref=""/></remarks>
		AutomaticOnce,

		/// <summary>
		/// Requires the user to call the <see cref="BarcodeReader.ScanImage"/> method.
		/// </summary>
		Manual
	}
}
