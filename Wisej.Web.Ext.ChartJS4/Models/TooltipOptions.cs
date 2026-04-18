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

using System.ComponentModel;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Wisej.Web.Ext.ChartJS4.Models
{
	/// <summary>
	/// Tooltip options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class TooltipOptions : OptionsBase
	{
		private bool _enabled = true;
		private string? _mode;
		private string? _position;
		private bool _intersect = true;
		private object? _backgroundColor;
		private object? _titleColor;
		private Font? _titleFont;
		private int _titleSpacing = 2;
		private int _titleMarginBottom = 6;
		private object? _bodyColor;
		private FontOptions? _bodyFont;
		private int _bodySpacing = 2;
		private int _padding = 6;
		private int _caretPadding = 2;
		private int _caretSize = 5;
		private int _cornerRadius = 6;
		private bool _displayColors = true;
		private int _boxWidth = 40;
		private int _boxHeight;
		private bool _usePointStyle;

		/// <summary>
		/// Are tooltips enabled?
		/// </summary>
		[JsonPropertyName("enabled")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Are tooltips enabled?")]
		public bool Enabled
		{
			get => _enabled;
			set => SetProperty(ref _enabled, value);
		}

		/// <summary>
		/// Mode for positioning: 'point', 'nearest', 'index', 'dataset', 'x', 'y'.
		/// </summary>
		[JsonPropertyName("mode")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Positioning mode.")]
		public string? Mode
		{
			get => _mode;
			set => SetProperty(ref _mode, value);
		}

		/// <summary>
		/// The tooltip position mode. Defines the mode used to determine the position of the tooltip.
		/// Accepted values: 'average' | 'nearest' | 'bottom'. Defaults to 'average'.
		/// </summary>
		[JsonPropertyName("position")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The tooltip position mode ('average', 'nearest', 'bottom').")]
		[DefaultValue(null)]
		public string? Position
		{
			get => _position;
			set => SetProperty(ref _position, value);
		}

		/// <summary>
		/// Determines whether the Position property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializePosition() => Position != null;

		/// <summary>
		/// Resets the Position property to its default value.
		/// </summary>
		public void ResetPosition() => Position = null;

		/// <summary>
		/// if true, the tooltip mode applies only when the mouse position intersects with an element.
		/// </summary>
		[JsonPropertyName("intersect")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Intersect mode.")]
		public bool Intersect
		{
			get => _intersect;
			set => SetProperty(ref _intersect, value);
		}

		/// <summary>
		/// Background color of the tooltip.
		/// </summary>
		[JsonPropertyName("backgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Background color.")]
		public object? BackgroundColor
		{
			get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		/// <summary>
		/// Color of title text.
		/// </summary>
		[JsonPropertyName("titleColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Title color.")]
		public object? TitleColor
		{
			get => _titleColor;
			set => SetProperty(ref _titleColor, value);
		}

		/// <summary>
		/// Font for title.
		/// </summary>
		[JsonPropertyName("titleFont")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Title font.")]
		[DefaultValue(null)]
		public Font? TitleFont
		{
			get => _titleFont;
			set => SetProperty(ref _titleFont, value);
		}

		/// <summary>
		/// Spacing to add to top and bottom of each title line.
		/// </summary>
		[JsonPropertyName("titleSpacing")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(2)]
		[Description("Title spacing.")]
		public int TitleSpacing
		{
			get => _titleSpacing;
			set => SetProperty(ref _titleSpacing, value);
		}

		/// <summary>
		/// Margin to add on bottom of title section.
		/// </summary>
		[JsonPropertyName("titleMarginBottom")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(6)]
		[Description("Title bottom margin.")]
		public int TitleMarginBottom
		{
			get => _titleMarginBottom;
			set => SetProperty(ref _titleMarginBottom, value);
		}

		/// <summary>
		/// Color of body text.
		/// </summary>
		[JsonPropertyName("bodyColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Body color.")]
		public object? BodyColor
		{
			get => _bodyColor;
			set => SetProperty(ref _bodyColor, value);
		}

		/// <summary>
		/// Font for body.
		/// </summary>
		[JsonPropertyName("bodyFont")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Body font.")]
		public FontOptions? BodyFont
		{
			get
			{
				if (_bodyFont == null)
					_bodyFont = new FontOptions { Chart = Chart };
				return _bodyFont;
			}
			set => SetProperty(ref _bodyFont, value);
		}

		/// <summary>
		/// Spacing to add to top and bottom of each tooltip item.
		/// </summary>
		[JsonPropertyName("bodySpacing")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(2)]
		[Description("Body spacing.")]
		public int BodySpacing
		{
			get => _bodySpacing;
			set => SetProperty(ref _bodySpacing, value);
		}

		/// <summary>
		/// Padding inside the tooltip.
		/// </summary>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(6)]
		[Description("Tooltip padding.")]
		public int Padding
		{
			get => _padding;
			set => SetProperty(ref _padding, value);
		}

		/// <summary>
		/// Extra distance to move the end of the tooltip arrow away from the tooltip point.
		/// </summary>
		[JsonPropertyName("caretPadding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(2)]
		[Description("Caret padding.")]
		public int CaretPadding
		{
			get => _caretPadding;
			set => SetProperty(ref _caretPadding, value);
		}

		/// <summary>
		/// Size, in px, of the tooltip arrow.
		/// </summary>
		[JsonPropertyName("caretSize")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(5)]
		[Description("Caret size.")]
		public int CaretSize
		{
			get => _caretSize;
			set => SetProperty(ref _caretSize, value);
		}

		/// <summary>
		/// Radius of tooltip corner curves.
		/// </summary>
		[JsonPropertyName("cornerRadius")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(6)]
		[Description("Corner radius.")]
		public int CornerRadius
		{
			get => _cornerRadius;
			set => SetProperty(ref _cornerRadius, value);
		}

		/// <summary>
		/// If true, color boxes are shown in the tooltip.
		/// </summary>
		[JsonPropertyName("displayColors")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Display color boxes.")]
		public bool DisplayColors
		{
			get => _displayColors;
			set => SetProperty(ref _displayColors, value);
		}

		/// <summary>
		/// Width of the color box if displayColors is true.
		/// </summary>
		[JsonPropertyName("boxWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(40)]
		[Description("Color box width.")]
		public int BoxWidth
		{
			get => _boxWidth;
			set => SetProperty(ref _boxWidth, value);
		}

		/// <summary>
		/// Height of the color box if displayColors is true.
		/// </summary>
		[JsonPropertyName("boxHeight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Color box height.")]
		public int BoxHeight
		{
			get => _boxHeight;
			set => SetProperty(ref _boxHeight, value);
		}

		/// <summary>
		/// If true, the tooltip will use the point style from the dataset rather than the default square.
		/// </summary>
		[JsonPropertyName("usePointStyle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("If true, use the point style for tooltip items.")]
		public bool UsePointStyle
		{
			get => _usePointStyle;
			set => SetProperty(ref _usePointStyle, value);
		}

		/// <summary>
		/// Additional custom properties that can be serialized to JSON.
		/// </summary>
		[JsonExtensionData]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <inheritdoc/>
		protected override void OnChartChanged()
		{
			if (_bodyFont != null)
				_bodyFont.Chart = Chart;
		}

		/// <summary>
		/// Determines whether the Enabled property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeEnabled() => Enabled != true;

		/// <summary>
		/// Resets the Enabled property to its default value.
		/// </summary>
		public void ResetEnabled() => Enabled = true;

		/// <summary>
		/// Determines whether the Mode property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeMode() => Mode != null;

		/// <summary>
		/// Resets the Mode property to its default value.
		/// </summary>
		public void ResetMode() => Mode = null;

		/// <summary>
		/// Determines whether the Intersect property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeIntersect() => Intersect != true;

		/// <summary>
		/// Resets the Intersect property to its default value.
		/// </summary>
		public void ResetIntersect() => Intersect = true;

		/// <summary>
		/// Determines whether the BackgroundColor property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBackgroundColor() => BackgroundColor != null;

		/// <summary>
		/// Resets the BackgroundColor property to its default value.
		/// </summary>
		public void ResetBackgroundColor() => BackgroundColor = null;

		/// <summary>
		/// Determines whether the TitleColor property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTitleColor() => TitleColor != null;

		/// <summary>
		/// Resets the TitleColor property to its default value.
		/// </summary>
		public void ResetTitleColor() => TitleColor = null;

		/// <summary>
		/// Determines whether the TitleFont property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTitleFont() => TitleFont != null;

		/// <summary>
		/// Resets the TitleFont property to its default value.
		/// </summary>
		public void ResetTitleFont() => TitleFont = null;

		/// <summary>
		/// Determines whether the TitleSpacing property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTitleSpacing() => TitleSpacing != 2;

		/// <summary>
		/// Resets the TitleSpacing property to its default value.
		/// </summary>
		public void ResetTitleSpacing() => TitleSpacing = 2;

		/// <summary>
		/// Determines whether the TitleMarginBottom property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTitleMarginBottom() => TitleMarginBottom != 6;

		/// <summary>
		/// Resets the TitleMarginBottom property to its default value.
		/// </summary>
		public void ResetTitleMarginBottom() => TitleMarginBottom = 6;

		/// <summary>
		/// Determines whether the BodyColor property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBodyColor() => BodyColor != null;

		/// <summary>
		/// Resets the BodyColor property to its default value.
		/// </summary>
		public void ResetBodyColor() => BodyColor = null;

		/// <summary>
		/// Determines whether the BodyFont property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBodyFont() => BodyFont != null;

		/// <summary>
		/// Resets the BodyFont property to its default value.
		/// </summary>
		public void ResetBodyFont() => SetProperty(ref _bodyFont, null);

		/// <summary>
		/// Determines whether the BodySpacing property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBodySpacing() => BodySpacing != 2;

		/// <summary>
		/// Resets the BodySpacing property to its default value.
		/// </summary>
		public void ResetBodySpacing() => BodySpacing = 2;

		/// <summary>
		/// Determines whether the Padding property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializePadding() => Padding != 6;

		/// <summary>
		/// Resets the Padding property to its default value.
		/// </summary>
		public void ResetPadding() => Padding = 6;

		/// <summary>
		/// Determines whether the CaretPadding property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeCaretPadding() => CaretPadding != 2;

		/// <summary>
		/// Resets the CaretPadding property to its default value.
		/// </summary>
		public void ResetCaretPadding() => CaretPadding = 2;

		/// <summary>
		/// Determines whether the CaretSize property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeCaretSize() => CaretSize != 5;

		/// <summary>
		/// Resets the CaretSize property to its default value.
		/// </summary>
		public void ResetCaretSize() => CaretSize = 5;

		/// <summary>
		/// Determines whether the CornerRadius property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeCornerRadius() => CornerRadius != 6;

		/// <summary>
		/// Resets the CornerRadius property to its default value.
		/// </summary>
		public void ResetCornerRadius() => CornerRadius = 6;

		/// <summary>
		/// Determines whether the DisplayColors property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeDisplayColors() => DisplayColors != true;

		/// <summary>
		/// Resets the DisplayColors property to its default value.
		/// </summary>
		public void ResetDisplayColors() => DisplayColors = true;

		/// <summary>
		/// Determines whether the BoxWidth property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBoxWidth() => BoxWidth != 40;

		/// <summary>
		/// Resets the BoxWidth property to its default value.
		/// </summary>
		public void ResetBoxWidth() => BoxWidth = 40;

		/// <summary>
		/// Determines whether the BoxHeight property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBoxHeight() => BoxHeight != 0;

		/// <summary>
		/// Resets the BoxHeight property to its default value.
		/// </summary>
		public void ResetBoxHeight() => BoxHeight = 0;

	}
}
