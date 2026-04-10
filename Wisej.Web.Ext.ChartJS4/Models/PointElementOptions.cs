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
	/// Point element styling options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class PointElementOptions : OptionsBase
	{

		/// <summary>
		/// Point radius.
		/// </summary>
		[JsonPropertyName("radius")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(3)]
		[Description("Point radius.")]
		public int Radius { get; set; } = 3;

		/// <summary>
		/// Point style: 'circle', 'cross', 'crossRot', 'dash', 'line', 'rect', 'rectRounded', 'rectRot', 'star', 'triangle'.
		/// </summary>
		[JsonPropertyName("pointStyle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Point style.")]
		public string? PointStyle { get; set; }

		/// <summary>
		/// Point rotation in degrees.
		/// </summary>
		[JsonPropertyName("rotation")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Point rotation in degrees.")]
		public double? Rotation { get; set; }

		/// <summary>
		/// Point background color.
		/// </summary>
		[JsonPropertyName("backgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Point background color.")]
		public object? BackgroundColor { get; set; }

		/// <summary>
		/// Point border width.
		/// </summary>
		[JsonPropertyName("borderWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(1)]
		[Description("Point border width.")]
		public int BorderWidth { get; set; } = 1;

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
		/// Determines whether the Radius property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeRadius() => Radius != 3;

		/// <summary>
		/// Resets the Radius property to its default value.
		/// </summary>
		public void ResetRadius() => Radius = 3;

		/// <summary>
		/// Determines whether the PointStyle property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializePointStyle() => PointStyle != null;

		/// <summary>
		/// Resets the PointStyle property to its default value.
		/// </summary>
		public void ResetPointStyle() => PointStyle = null;

		/// <summary>
		/// Determines whether the Rotation property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeRotation() => Rotation != null;

		/// <summary>
		/// Resets the Rotation property to its default value.
		/// </summary>
		public void ResetRotation() => Rotation = null;

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
		public bool ShouldSerializeBorderWidth() => BorderWidth != 1;

		/// <summary>
		/// Resets the BorderWidth property to its default value.
		/// </summary>
		public void ResetBorderWidth() => BorderWidth = 1;

	}
}
