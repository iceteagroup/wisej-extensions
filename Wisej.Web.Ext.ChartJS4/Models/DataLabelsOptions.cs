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
using System.Text.Json.Serialization;

namespace Wisej.Web.Ext.ChartJS4.Models
{
	/// <summary>
	/// Data labels plugin options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class DataLabelsOptions : OptionsBase
	{
		private bool _display;
		private string? _anchor;
		private string? _align;
		private int _offset = 4;
		private object? _backgroundColor;
		private object? _borderColor;
		private int _borderWidth;
		private int _borderRadius;
		private object? _color;
		private FontOptions? _font;
		private int _padding = 4;

		/// <summary>
		/// Display data labels.
		/// </summary>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.Never)]
		[DefaultValue(false)]
		[Description("Display data labels.")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Anchor point: 'start', 'center', 'end'.
		/// </summary>
		[JsonPropertyName("anchor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Anchor point.")]
		public string? Anchor
		{
			get => _anchor;
			set => SetProperty(ref _anchor, value);
		}

		/// <summary>
		/// Alignment: 'start', 'center', 'end', 'left', 'right', 'top', 'bottom'.
		/// </summary>
		[JsonPropertyName("align")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Alignment.")]
		public string? Align
		{
			get => _align;
			set => SetProperty(ref _align, value);
		}

		/// <summary>
		/// Distance (in pixels) to pull the label away from the anchor point.
		/// </summary>
		[JsonPropertyName("offset")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(4)]
		[Description("Offset distance.")]
		public int Offset
		{
			get => _offset;
			set => SetProperty(ref _offset, value);
		}

		/// <summary>
		/// Background color.
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
		/// Border color.
		/// </summary>
		[JsonPropertyName("borderColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Border color.")]
		public object? BorderColor
		{
			get => _borderColor;
			set => SetProperty(ref _borderColor, value);
		}

		/// <summary>
		/// Border width.
		/// </summary>
		[JsonPropertyName("borderWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Border width.")]
		public int BorderWidth
		{
			get => _borderWidth;
			set => SetProperty(ref _borderWidth, value);
		}

		/// <summary>
		/// Border radius.
		/// </summary>
		[JsonPropertyName("borderRadius")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Border radius.")]
		public int BorderRadius
		{
			get => _borderRadius;
			set => SetProperty(ref _borderRadius, value);
		}

		/// <summary>
		/// Text color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Text color.")]
		public object? Color
		{
			get => _color;
			set => SetProperty(ref _color, value);
		}

		/// <summary>
		/// Font configuration.
		/// </summary>
		[JsonPropertyName("font")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Font configuration.")]
		public FontOptions? Font
		{
			get
			{
				if (_font == null)
					_font = new FontOptions { Chart = Chart };
				return _font;
			}
			set => SetProperty(ref _font, value);
		}

		/// <summary>
		/// Padding.
		/// </summary>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Padding.")]
		[DefaultValue(4)]
		public int Padding
		{
			get => _padding;
			set => SetProperty(ref _padding, value);
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
		/// <summary>
		/// Determines whether the Display property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeDisplay() => Display != false;

		/// <summary>
		/// Resets the Display property to its default value.
		/// </summary>
		public void ResetDisplay() => Display = true;

		/// <summary>
		/// Determines whether the Anchor property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeAnchor() => Anchor != null;

		/// <summary>
		/// Resets the Anchor property to its default value.
		/// </summary>
		public void ResetAnchor() => Anchor = null;

		/// <summary>
		/// Determines whether the Align property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeAlign() => Align != null;

		/// <summary>
		/// Resets the Align property to its default value.
		/// </summary>
		public void ResetAlign() => Align = null;

		/// <summary>
		/// Determines whether the Offset property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeOffset() => Offset != 4;

		/// <summary>
		/// Resets the Offset property to its default value.
		/// </summary>
		public void ResetOffset() => Offset = 4;

		/// <summary>
		/// Determines whether the BackgroundColor property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBackgroundColor() => BackgroundColor != null;

		/// <summary>
		/// Resets the BackgroundColor property to its default value.
		/// </summary>
		public void ResetBackgroundColor() => BackgroundColor = null;

		/// <summary>
		/// Determines whether the BorderColor property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBorderColor() => BorderColor != null;

		/// <summary>
		/// Resets the BorderColor property to its default value.
		/// </summary>
		public void ResetBorderColor() => BorderColor = null;

		/// <summary>
		/// Determines whether the BorderWidth property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBorderWidth() => BorderWidth != 0;

		/// <summary>
		/// Resets the BorderWidth property to its default value.
		/// </summary>
		public void ResetBorderWidth() => BorderWidth = 0;

		/// <summary>
		/// Determines whether the BorderRadius property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBorderRadius() => BorderRadius != 0;

		/// <summary>
		/// Resets the BorderRadius property to its default value.
		/// </summary>
		public void ResetBorderRadius() => BorderRadius = 0;

		/// <summary>
		/// Determines whether the Color property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeColor() => Color != null;

		/// <summary>
		/// Resets the Color property to its default value.
		/// </summary>
		public void ResetColor() => Color = null;

		/// <summary>
		/// Determines whether the Font property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeFont() => Font != null && !Font.IsDefault;

		/// <summary>
		/// Resets the Font property to its default value.
		/// </summary>
		public void ResetFont() => Font = null;

		/// <summary>
		/// Determines whether the Padding property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializePadding() => !Padding.Equals(4);

		/// <summary>
		/// Resets the Padding property to its default value.
		/// </summary>
		public void ResetPadding() => Padding = 4;

		/// <inheritdoc/>
		protected override void OnChartChanged()
		{
			if (_font != null)
				_font.Chart = Chart;
		}

	}
}
