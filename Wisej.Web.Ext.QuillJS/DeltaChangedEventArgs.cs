using System;

namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// Event arguments for the DeltaChanged event.
	/// </summary>
	public class DeltaChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the old Delta state.
		/// </summary>
		public QuillDelta OldDelta { get; }

		/// <summary>
		/// Gets the new Delta state.
		/// </summary>
		public QuillDelta NewDelta { get; }

		/// <summary>
		/// Gets the source of the change.
		/// </summary>
		public string Source { get; }

		/// <summary>
		/// Initializes a new instance of the DeltaChangedEventArgs class.
		/// </summary>
		public DeltaChangedEventArgs(QuillDelta oldDelta, QuillDelta newDelta, string source)
		{
			OldDelta = oldDelta;
			NewDelta = newDelta;
			Source = source;
		}
	}
}
