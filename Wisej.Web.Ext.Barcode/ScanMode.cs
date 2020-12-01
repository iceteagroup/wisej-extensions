
namespace Wisej.Web.Ext.Barcode
{
	/// <summary>
	/// Specifies the mode in which the scanner should detect barcodes.
	/// </summary>
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
