using System;

namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// Provides data for the <see cref="QuillJSEditor.DeltaChanged"/> event.
	/// </summary>
	public class DeltaChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Returns the content of the editor before the change, as a <see cref="QuillDelta"/>.
		/// </summary>
		public QuillDelta OldDelta { get; }

		/// <summary>
		/// Returns the delta describing the change, or the new content of the editor.
		/// </summary>
		public QuillDelta NewDelta { get; }

		/// <summary>
		/// Returns the source of the change.
		/// </summary>
		/// <remarks>
		/// QuillJS uses "user" for changes made by the user, "api" for changes made programmatically and "silent"
		/// for programmatic changes that don't fire events.
		/// </remarks>
		public string Source { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DeltaChangedEventArgs"/> class.
		/// </summary>
		/// <param name="oldDelta">The content of the editor before the change.</param>
		/// <param name="newDelta">The delta describing the change, or the new content of the editor.</param>
		/// <param name="source">The source of the change: "user", "api" or "silent".</param>
		public DeltaChangedEventArgs(QuillDelta oldDelta, QuillDelta newDelta, string source)
		{
			OldDelta = oldDelta;
			NewDelta = newDelta;
			Source = source;
		}
	}
}
