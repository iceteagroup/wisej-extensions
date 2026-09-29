namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// Provides the names and values of the formats supported by the QuillJS editor.
	/// </summary>
	/// <remarks>
	/// Use the constants in the nested classes instead of string literals when calling
	/// <see cref="QuillJSEditor.Format"/>, <see cref="QuillJSEditor.FormatText"/> or <see cref="QuillJSEditor.FormatLine"/>,
	/// or when building the <see cref="QuillJSEditor.Toolbar"/> configuration.
	/// Some formats (i.e. <see cref="Formats.Formula"/>, <see cref="Formats.Table"/>, <see cref="Formats.Mention"/>)
	/// require additional QuillJS modules that are not included in the default packages.
	/// </remarks>
	public class QuillFormat
	{
		/// <summary>
		/// Provides the names of the formats supported by QuillJS.
		/// </summary>
		/// <remarks>
		/// <see cref="Bold"/>, <see cref="Italic"/>, <see cref="Underline"/>, <see cref="Strike"/>, <see cref="Script"/>,
		/// <see cref="Size"/>, <see cref="Color"/>, <see cref="Background"/>, <see cref="Font"/> and <see cref="Link"/> are inline formats;
		/// <see cref="Header"/>, <see cref="Blockquote"/>, <see cref="Code"/>, <see cref="List"/>, <see cref="Indent"/>,
		/// <see cref="Direction"/> and <see cref="Align"/> are line (block) formats.
		/// <see cref="Clean"/> is only used as a toolbar button that removes the formatting.
		/// </remarks>
		public static class Formats
		{
			public const string Bold = "bold";
			public const string Italic = "italic";
			public const string Underline = "underline";
			public const string Strike = "strike";
			public const string Script = "script";
			public const string Header = "header";
			public const string Blockquote = "blockquote";
			public const string Code = "code-block";
			public const string List = "list";
			public const string Bullet = "bullet";
			public const string Indent = "indent";
			public const string Direction = "direction";
			public const string Size = "size";
			public const string Color = "color";
			public const string Background = "background";
			public const string Font = "font";
			public const string Align = "align";
			public const string Link = "link";
			public const string Image = "image";
			public const string Video = "video";
			public const string Formula = "formula";
			public const string Table = "table";
			public const string Mention = "mention";
			public const string Clean = "clean";
		}

		/// <summary>
		/// Provides the values of the <see cref="Formats.Size"/> format supported by the default QuillJS configuration.
		/// </summary>
		public static class Sizes
		{
			public const string Small = "small";
			public const string Normal = "normal";
			public const string Large = "large";
			public const string Huge = "huge";
		}

		/// <summary>
		/// Provides the values of the <see cref="Formats.Align"/> format.
		/// </summary>
		public static class Alignments
		{
			public const string Left = "left";
			public const string Center = "center";
			public const string Right = "right";
			public const string Justify = "justify";
		}

		/// <summary>
		/// Provides the values of the <see cref="Formats.Script"/> format (subscript and superscript).
		/// </summary>
		public static class Scripts
		{
			public const string Sub = "sub";
			public const string Super = "super";
		}

		/// <summary>
		/// Provides the values of the <see cref="Formats.List"/> format.
		/// </summary>
		public static class Lists
		{
			public const string Ordered = "ordered";
			public const string Bullet = "bullet";
			public const string Check = "check";
		}

		/// <summary>
		/// Provides the values of the <see cref="Formats.Direction"/> format.
		/// </summary>
		public static class Directions
		{
			public const string Rtl = "rtl";
			public const string Ltr = "ltr";
		}

		/// <summary>
		/// Provides common font family names for the <see cref="Formats.Font"/> format.
		/// </summary>
		/// <remarks>
		/// The default QuillJS configuration only accepts the "serif" and "monospace" font values; these names
		/// can be used only when the font format has been registered with a matching whitelist on the client.
		/// </remarks>
		public static class Fonts
		{
			public const string Arial = "Arial";
			public const string TimesNewRoman = "Times New Roman";
			public const string Helvetica = "Helvetica";
			public const string Verdana = "Verdana";
			public const string Courier = "Courier";
			public const string Georgia = "Georgia";
			public const string Tahoma = "Tahoma";
			public const string Impact = "Impact";
		}

		/// <summary>
		/// Provides CSS font size names for the <see cref="Formats.Size"/> format.
		/// </summary>
		/// <remarks>
		/// The default QuillJS configuration only accepts the values in <see cref="Sizes"/>; these names can be used
		/// only when the size format has been registered with a matching whitelist on the client.
		/// </remarks>
		public static class FontSizes
		{
			public const string XSmall = "x-small";
			public const string Small = "small";
			public const string Normal = "normal";
			public const string Large = "large";
			public const string XLarge = "x-large";
			public const string XXLarge = "xx-large";
		}
	}

	/// <summary>
	/// Represents a custom format definition for QuillJS.
	/// </summary>
	/// <remarks>
	/// This class only describes a format: it's not used by <see cref="QuillJSEditor"/> and a custom format
	/// must be registered on the client using the QuillJS API.
	/// </remarks>
	public class CustomFormat
	{
		/// <summary>
		/// Returns or sets the HTML tag name for the format, i.e. "span" or "div".
		/// </summary>
		public string TagName { get; set; }

		/// <summary>
		/// Returns or sets the CSS class name for the format.
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// Returns or sets a value indicating whether this format is inline (true) or a block format (false).
		/// </summary>
		public bool IsInline { get; set; }

		/// <summary>
		/// Returns or sets a value indicating whether this format allows nested formats.
		/// </summary>
		public bool AllowNested { get; set; }

		/// <summary>
		/// Returns or sets the names of the HTML attributes allowed for this format.
		/// </summary>
		public string[] AllowedAttributes { get; set; }

		/// <summary>
		/// Returns or sets a value indicating whether to add this format to the toolbar.
		/// </summary>
		public bool AddToToolbar { get; set; }

		/// <summary>
		/// Creates a new custom format for a specific tag.
		/// </summary>
		/// <param name="tagName">The HTML tag name of the format.</param>
		/// <param name="isInline">Whether the format is inline. The default is true.</param>
		/// <param name="allowNested">Whether the format allows nested formats. The default is true.</param>
		/// <param name="addToToolbar">Whether to add the format to the toolbar. The default is false.</param>
		/// <param name="allowedAttributes">The names of the allowed HTML attributes.</param>
		/// <returns>A new <see cref="CustomFormat"/> instance.</returns>
		/// <example>
		/// Describing an inline format rendered as a <c>mark</c> element:
		/// <code><![CDATA[
		/// var highlight = CustomFormat.CreateTag("mark", true, true, true, "title");
		/// ]]></code>
		/// </example>
		public static CustomFormat CreateTag(string tagName, bool isInline = true, bool allowNested = true, bool addToToolbar = false, params string[] allowedAttributes)
		{
			return new CustomFormat
			{
				TagName = tagName,
				IsInline = isInline,
				AllowNested = allowNested,
				AllowedAttributes = allowedAttributes,
				AddToToolbar = addToToolbar
			};
		}

		/// <summary>
		/// Creates a new custom format with a class.
		/// </summary>
		/// <param name="tagName">The HTML tag name of the format.</param>
		/// <param name="className">The CSS class name of the format.</param>
		/// <param name="isInline">Whether the format is inline. The default is true.</param>
		/// <param name="allowNested">Whether the format allows nested formats. The default is true.</param>
		/// <param name="addToToolbar">Whether to add the format to the toolbar. The default is false.</param>
		/// <param name="allowedAttributes">The names of the allowed HTML attributes.</param>
		/// <returns>A new <see cref="CustomFormat"/> instance.</returns>
		/// <example>
		/// Describing an inline format rendered as a <c>span</c> element with a CSS class:
		/// <code><![CDATA[
		/// var warning = CustomFormat.CreateClass("span", "text-warning");
		/// ]]></code>
		/// </example>
		public static CustomFormat CreateClass(string tagName, string className, bool isInline = true, bool allowNested = true, bool addToToolbar = false, params string[] allowedAttributes)
		{
			return new CustomFormat
			{
				TagName = tagName,
				ClassName = className,
				IsInline = isInline,
				AllowNested = allowNested,
				AllowedAttributes = allowedAttributes,
				AddToToolbar = addToToolbar
			};
		}

		/// <summary>
		/// Creates a new custom format for a block element.
		/// </summary>
		/// <param name="tagName">The HTML tag name of the block element.</param>
		/// <param name="className">The optional CSS class name of the format.</param>
		/// <param name="addToToolbar">Whether to add the format to the toolbar. The default is false.</param>
		/// <param name="allowedAttributes">The names of the allowed HTML attributes.</param>
		/// <returns>A new block (not inline) <see cref="CustomFormat"/> instance that allows nested formats.</returns>
		/// <example>
		/// Describing a block format rendered as an <c>aside</c> element:
		/// <code><![CDATA[
		/// var note = CustomFormat.CreateBlock("aside", "note");
		/// ]]></code>
		/// </example>
		public static CustomFormat CreateBlock(string tagName, string className = null, bool addToToolbar = false, params string[] allowedAttributes)
		{
			return new CustomFormat
			{
				TagName = tagName,
				ClassName = className,
				IsInline = false,
				AllowNested = true,
				AllowedAttributes = allowedAttributes,
				AddToToolbar = addToToolbar
			};
		}
	}
}
