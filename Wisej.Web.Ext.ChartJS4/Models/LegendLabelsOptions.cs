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
	/// Legend labels options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class LegendLabelsOptions : OptionsBase
	{
		/// <summary>
		/// Width of colored box.
		/// </summary>
		[JsonPropertyName("boxWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Width of colored box.")]
		public int BoxWidth { get; set; } = 0;

		/// <summary>
		/// Height of colored box.
		/// </summary>
		[JsonPropertyName("boxHeight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Height of colored box.")]
		[DefaultValue(0)]
		public int BoxHeight { get; set; } = 0;

		/// <summary>
		/// Color of label and the strikethrough.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Label color.")]
		public object? Color { get; set; }

		/// <summary>
		/// Font configuration.
		/// </summary>
		[JsonPropertyName("font")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Font configuration.")]
		public FontOptions? Font
		{
			get => _font ??= new FontOptions();
			set => _font = value;
		}
		private FontOptions? _font;

		/// <summary>
		/// Padding between rows of colored boxes.
		/// </summary>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(10)]
		[Description("Padding between rows.")]
		public int Padding { get; set; } = 10;

		/// <summary>
		/// If true, the legend items will use the point style from the dataset rather than the default square.
		/// </summary>
		[JsonPropertyName("usePointStyle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("If true, use the point style for legend items.")]
		public bool UsePointStyle { get; set; }

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
		/// Determines whether the BoxWidth property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBoxWidth() => BoxWidth != 0;

		/// <summary>
		/// Resets the BoxWidth property to its default value.
		/// </summary>
		public void ResetBoxWidth() => BoxWidth = 0;

		/// <summary>
		/// Determines whether the BoxHeight property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBoxHeight() => BoxHeight != 0;

		/// <summary>
		/// Resets the BoxHeight property to its default value.
		/// </summary>
		public void ResetBoxHeight() => BoxHeight = 0;

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
		public bool ShouldSerializeFont() => Font != null;

		/// <summary>
		/// Resets the Font property to its default value.
		/// </summary>
		public void ResetFont() => Font = null;

		/// <summary>
		/// Determines whether the Padding property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializePadding() => Padding != 10;

		/// <summary>
		/// Resets the Padding property to its default value.
		/// </summary>
		public void ResetPadding() => Padding = 10;

	}
}
