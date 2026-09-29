using System;

namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// Provides data for the <see cref="QuillJSEditor.SelectionChanged"/> event.
	/// </summary>
	public class SelectionChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Returns the zero-based starting index of the selection.
		/// </summary>
		public int Index { get; }

		/// <summary>
		/// Returns the length of the selection. A length of 0 indicates the position of the caret.
		/// </summary>
		public int Length { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SelectionChangedEventArgs"/> class.
		/// </summary>
		/// <param name="index">The zero-based starting index of the selection.</param>
		/// <param name="length">The length of the selection.</param>
		public SelectionChangedEventArgs(int index, int length)
		{
			Index = index;
			Length = length;
		}
	}
}
