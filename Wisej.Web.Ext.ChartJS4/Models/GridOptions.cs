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
	/// Grid line options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class GridOptions : OptionsBase
	{
		/// <summary>
		/// If true, draw grid lines.
		/// </summary>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Display grid lines.")]
		public bool Display { get; set; } = true;

		/// <summary>
		/// Grid line color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Grid line color.")]
		public object? Color { get; set; }

		/// <summary>
		/// Stroke width of grid lines.
		/// </summary>
		[JsonPropertyName("lineWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(1)]
		[Description("Grid line width.")]
		public int LineWidth { get; set; } = 1;

		/// <summary>
		/// If true, draw border at the edge between the axis and the chart area.
		/// </summary>
		[JsonPropertyName("drawBorder")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Draw border at edge.")]
		public bool DrawBorder { get; set; } = true;

		/// <summary>
		/// If true, gridlines are circular (on radar chart only).
		/// </summary>
		[JsonPropertyName("circular")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Circular grid lines (radar only).")]
		public bool Circular { get; set; }

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
		public bool ShouldSerializeDisplay() => Display != true;

		/// <summary>
		/// Resets the Display property to its default value.
		/// </summary>
		public void ResetDisplay() => Display = true;

		/// <summary>
		/// Determines whether the Color property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeColor() => Color != null;

		/// <summary>
		/// Resets the Color property to its default value.
		/// </summary>
		public void ResetColor() => Color = null;

		/// <summary>
		/// Determines whether the LineWidth property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeLineWidth() => LineWidth != 1;

		/// <summary>
		/// Resets the LineWidth property to its default value.
		/// </summary>
		public void ResetLineWidth() => LineWidth = 1;

		/// <summary>
		/// Determines whether the DrawBorder property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeDrawBorder() => DrawBorder != true;

		/// <summary>
		/// Resets the DrawBorder property to its default value.
		/// </summary>
		public void ResetDrawBorder() => DrawBorder = true;

		/// <summary>
		/// Determines whether the Circular property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeCircular() => Circular != false;

		/// <summary>
		/// Resets the Circular property to its default value.
		/// </summary>
		public void ResetCircular() => Circular = false;

	}
}
