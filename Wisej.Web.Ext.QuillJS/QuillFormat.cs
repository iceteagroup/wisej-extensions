namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// Represents custom formats for the QuillJS editor.
	/// </summary>
	public class QuillFormat
	{
		/// <summary>
		/// Predefined formats supported by QuillJS.
		/// </summary>
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
		/// Predefined sizes for text.
		/// </summary>
		public static class Sizes
		{
			public const string Small = "small";
			public const string Normal = "normal";
			public const string Large = "large";
			public const string Huge = "huge";
		}

		/// <summary>
		/// Predefined alignment options.
		/// </summary>
		public static class Alignments
		{
			public const string Left = "left";
			public const string Center = "center";
			public const string Right = "right";
			public const string Justify = "justify";
		}

		/// <summary>
		/// Script types for superscript and subscript.
		/// </summary>
		public static class Scripts
		{
			public const string Sub = "sub";
			public const string Super = "super";
		}

		/// <summary>
		/// List types.
		/// </summary>
		public static class Lists
		{
			public const string Ordered = "ordered";
			public const string Bullet = "bullet";
			public const string Check = "check";
		}

		/// <summary>
		/// Direction options for text.
		/// </summary>
		public static class Directions
		{
			public const string Rtl = "rtl";
			public const string Ltr = "ltr";
		}

		/// <summary>
		/// Predefined font families.
		/// </summary>
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
		/// Predefined font sizes.
		/// </summary>
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
	public class CustomFormat
	{
		/// <summary>
		/// Gets or sets the tag name for the format.
		/// </summary>
		public string TagName { get; set; }

		/// <summary>
		/// Gets or sets the class name for the format.
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// Gets or sets whether this format is inline.
		/// </summary>
		public bool IsInline { get; set; }

		/// <summary>
		/// Gets or sets whether this format allows nested formats.
		/// </summary>
		public bool AllowNested { get; set; }

		/// <summary>
		/// Gets or sets the allowed attributes for this format.
		/// </summary>
		public string[] AllowedAttributes { get; set; }

		/// <summary>
		/// Gets or sets whether to add this format to the toolbar.
		/// </summary>
		public bool AddToToolbar { get; set; }

		/// <summary>
		/// Creates a new custom format for a specific tag.
		/// </summary>
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
