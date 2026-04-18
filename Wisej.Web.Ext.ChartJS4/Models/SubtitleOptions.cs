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
	/// Subtitle options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class SubtitleOptions : OptionsBase
	{
		private bool _display;
		private object? _text;
		private object? _color;
		private FontOptions? _font;
		private int _padding;

		/// <summary>
		/// Is the subtitle shown?
		/// </summary>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Is the subtitle shown?")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Subtitle text (can be string or array of strings).
		/// </summary>
		[JsonPropertyName("text")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Subtitle text.")]
		public object? Text
		{
			get => _text;
			set => SetProperty(ref _text, value);
		}

		/// <summary>
		/// Color of text.
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
		/// Padding to apply around the subtitle.
		/// </summary>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Padding around subtitle.")]
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

		/// <inheritdoc/>
		protected override void OnChartChanged()
		{
			if (_font != null)
				_font.Chart = Chart;
		}

		/// <summary>
		/// Determines whether the Display property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeDisplay() => Display != false;

		/// <summary>
		/// Resets the Display property to its default value.
		/// </summary>
		public void ResetDisplay() => Display = false;

		/// <summary>
		/// Determines whether the Text property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeText() => Text != null;

		/// <summary>
		/// Resets the Text property to its default value.
		/// </summary>
		public void ResetText() => Text = null;

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
		public void ResetFont() => SetProperty(ref _font, null);

		/// <summary>
		/// Determines whether the Padding property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializePadding() => Padding != 0;

		/// <summary>
		/// Resets the Padding property to its default value.
		/// </summary>
		public void ResetPadding() => Padding = 0;

	}
}
