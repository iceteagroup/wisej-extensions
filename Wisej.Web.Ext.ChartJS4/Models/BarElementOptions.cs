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
	/// Bar element styling options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class BarElementOptions : OptionsBase
	{
		/// <summary>
		/// Bar background color.
		/// </summary>
		[JsonPropertyName("backgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Bar background color.")]
		public object? BackgroundColor { get; set; }

		/// <summary>
		/// Bar border width.
		/// </summary>
		[JsonPropertyName("borderWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Bar border width.")]
		public int BorderWidth { get; set; }

		/// <summary>
		/// Bar border radius in pixels.
		/// </summary>
		[JsonPropertyName("borderRadius")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Bar border radius.")]
		public int BorderRadius { get; set; }

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
		/// Determines whether the BackgroundColor property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBackgroundColor() => BackgroundColor != null;

		/// <summary>
		/// Resets the BackgroundColor property to its default value.
		/// </summary>
		public void ResetBackgroundColor() => BackgroundColor = null;

		/// <summary>
		/// Determines whether the BorderWidth property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBorderWidth() => BorderWidth != default;

		/// <summary>
		/// Resets the BorderWidth property to its default value.
		/// </summary>
		public void ResetBorderWidth() => BorderWidth = 0;

		/// <summary>
		/// Determines whether the BorderRadius property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBorderRadius() => BorderRadius != default;

		/// <summary>
		/// Resets the BorderRadius property to its default value.
		/// </summary>
		public void ResetBorderRadius() => BorderRadius = 0;

	}
}
