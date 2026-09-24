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

using System;
using System.ComponentModel;

namespace Wisej.Web.Ext.Barcode
{
	/// <summary>
	/// Represents the method that will handle the <see cref="BarcodeReader.ScanSuccess"/> event.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="ScanEventArgs" /> that contains the event data.</param>
	/// <remarks>
	/// The same delegate is also used for the <see cref="BarcodeReader.ScanError"/> event.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.barcodeReader1.ScanSuccess += new ScanEventHandler(barcodeReader1_Scan);
	/// this.barcodeReader1.ScanError += new ScanEventHandler(barcodeReader1_Scan);
	///
	/// private void barcodeReader1_Scan(object sender, ScanEventArgs e)
	/// {
	///     this.labelStatus.Text = e.Success
	///         ? $"Scanned: {e.Data}"
	///         : $"Scan failed: {e.Data}";
	/// }
	/// ]]></code>
	/// </example>
	[ApiCategory("Barcode")]
	public delegate void ScanEventHandler(object sender, ScanEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="BarcodeReader.ScanSuccess"/> event of the <see cref="BarcodeReader"/> component.
	/// </summary>
	[ApiCategory("Barcode")]
	public class ScanEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ScanEventArgs"/> class.
		/// </summary>
		/// <param name="data">The data encoded in the barcode, or the error message when the scan failed.</param>
		/// <param name="success">true if a barcode was detected; otherwise false.</param>
		/// <example>
		/// The following example fires the <see cref="BarcodeReader.ScanSuccess"/> event from a derived reader,
		/// for example to simulate a scan in a test:
		/// <code><![CDATA[
		/// public class TestBarcodeReader : BarcodeReader
		/// {
		///     public void SimulateScan(string data)
		///     {
		///         OnScanSuccess(new ScanEventArgs(data, true));
		///     }
		/// }
		/// ]]></code>
		/// </example>
		public ScanEventArgs(string data, bool success)
		{
			this.Data = data;
			this.Success = success;
		}

		/// <summary>
		/// Returns the scanned data.
		/// </summary>
		public string Data
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns whether a barcode was detected.
		/// </summary>
		public bool Success
		{
			get;
			private set;
		}
	}
}
