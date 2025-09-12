using System;

namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// Event arguments for the SelectionChanged event.
	/// </summary>
	public class SelectionChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the index of the selection.
		/// </summary>
		public int Index { get; }

		/// <summary>
		/// Gets the length of the selection.
		/// </summary>
		public int Length { get; }

		/// <summary>
		/// Initializes a new instance of the SelectionChangedEventArgs class.
		/// </summary>
		public SelectionChangedEventArgs(int index, int length)
		{
			Index = index;
			Length = length;
		}
	}
}
