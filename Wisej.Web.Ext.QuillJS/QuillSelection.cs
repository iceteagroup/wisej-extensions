namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// Represents a selection within the QuillJS editor.
	/// </summary>
	/// <remarks>
	/// Instances are returned by <see cref="QuillJSEditor.GetSelectionAsync"/>. Positions are zero-based character
	/// offsets in the editor content, where each embed (image, video) counts as one character.
	/// </remarks>
	public class QuillSelection
	{
		/// <summary>
		/// Returns or sets the zero-based starting index of the selection.
		/// </summary>
		public int Index { get; set; }

		/// <summary>
		/// Returns or sets the length of the selection. A length of 0 indicates the position of the caret.
		/// </summary>
		public int Length { get; set; }
	}
}
