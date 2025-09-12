using System.ComponentModel;

namespace Wisej.Ext.Tesseract
{
	/// <summary>
	/// Contains information about the text detected from the image
	/// </summary>
	[ApiCategory("Tesseract")]
	public class ScanResult
	{
		/// <summary>
		/// Creates a new instance of <see cref="ScanResult"/> with the given configuration.
		/// </summary>
		/// <param name="confidence">The confidence of the recognition. Generally a higher confidence indicates that the result is more accurate.</param>
		/// <param name="text">The text discovered.</param>
		/// <param name="words">An array of strings containing the words discovered.</param>
		public ScanResult(int confidence, string text, string[] words)
		{
			this.Confidence = confidence;
			this.Words = words;
			this.Text = text;
		}
		/// <summary>
		/// Returns the confidence level of the scan event.
		/// </summary>
		/// <remarks>
		/// Generally a higher confidence indicates that the result is more accurate.
		/// </remarks>
		public int Confidence { get; }

		/// <summary>
		/// Returns the text detected during the scan event.
		/// </summary>
		public string Text { get; }

		/// <summary>
		/// Returns a list of words that were detected during the scan event.
		/// </summary>
		public string[] Words { get; }
	}
}
