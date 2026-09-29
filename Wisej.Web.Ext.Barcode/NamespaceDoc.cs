///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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


namespace Wisej.Web.Ext.Barcode
{
	/// <summary>
	/// <para>
	/// BarCode component. Displays all sorts of bar codes using the ZXing library.
	/// </para>
	/// </summary>
	/// <example>
	/// The following example shows a QR code and reads barcodes from a camera:
	/// <code><![CDATA[
	/// var qrCode = new Barcode
	/// {
	///     BarcodeType = BarcodeType.QR,
	///     Text = "https://wisej.com",
	///     ShowLabel = false,
	///     Size = new Size(150, 150)
	/// };
	/// this.Controls.Add(qrCode);
	///
	/// var reader = new BarcodeReader(this.components) { Camera = this.camera1 };
	/// reader.ScanSuccess += (s, e) => AlertBox.Show($"Scanned: {e.Data}");
	/// ]]></code>
	/// </example>
	internal class NamespaceDoc
	{
	}
}
