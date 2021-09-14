///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
	/// Determines the barcode symbology to display display.
	/// </summary>
	[ApiCategory("Barcode")]
	public enum BarcodeType
	{
		/// <summary>Aztec 2D barcode format.</summary>
		Aztec = 1,

		/// <summary>CODABAR 1D format.</summary>
		Codabar = 2,

		/// <summary>Code 39 1D format.</summary>
		Code_39 = 4,

		/// <summary>Code 128 1D format.</summary>
		Code_128 = 16,

		/// <summary>Data Matrix 2D barcode format.</summary>
		DataMatrix = 32,

		/// <summary>EAN-8 1D format.</summary>
		EAN_8 = 64,

		/// <summary>EAN-13 1D format.</summary>
		EAN_13 = 128,

		/// <summary>ITF (Interleaved Two of Five) 1D format.</summary>
		ITF = 256,

		/// <summary>PDF417 format.</summary>
		PDF_417 = 1024,

		/// <summary>QR Code 2D barcode format.</summary>
		QR = 2048,

		/// <summary>UPC-A 1D format.</summary>
		UPC_A = 16384,

		/// <summary>MSI</summary>
		MSI = 131072,

		/// <summary>Plessey</summary>
		Plessey = 262144,
	}
}