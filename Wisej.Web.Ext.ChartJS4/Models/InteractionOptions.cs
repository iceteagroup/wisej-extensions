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
	/// Interaction options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class InteractionOptions : OptionsBase
	{
		/// <summary>
		/// Sets which elements appear in the interaction.
		/// </summary>
		[JsonPropertyName("mode")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Sets which elements appear in the interaction.")]
		[DefaultValue(null)]
		public string? Mode { get; set; } = null;

		/// <summary>
		/// If true, the interaction mode only applies when the mouse position intersects an item on the chart.
		/// </summary>
		[JsonPropertyName("intersect")]
		[JsonIgnore(Condition = JsonIgnoreCondition.Never)]
		[DefaultValue(true)]
		[Description("If true, the interaction mode only applies when the mouse position intersects an item on the chart.")]
		public bool Intersect { get; set; } = true;

		/// <summary>
		/// Can be 'x', 'y', 'xy', or 'r' to define which directions are used in calculating distances.
		/// </summary>
		[JsonPropertyName("axis")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Axis for interaction calculations.")]
		public string? Axis { get; set; }

		/// <summary>
		/// If true, include invisible points.
		/// </summary>
		[JsonPropertyName("includeInvisible")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Include invisible points in interactions.")]
		public bool IncludeInvisible { get; set; }

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
		/// Determines whether the Axis property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeAxis() => Axis != null;

		/// <summary>
		/// Resets the Axis property to its default value.
		/// </summary>
		public void ResetAxis() => Axis = null;

		/// <summary>
		/// Determines whether the IncludeInvisible property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeIncludeInvisible() => IncludeInvisible != false;

		/// <summary>
		/// Resets the IncludeInvisible property to its default value.
		/// </summary>
		public void ResetIncludeInvisible() => IncludeInvisible = false;

		/// <summary>
		/// Determines whether the ExtensionData property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeExtensionData() => ExtensionData != null && ExtensionData.Count > 0;

		/// <summary>
		/// Resets the ExtensionData property to its default value.
		/// </summary>
		public void ResetExtensionData() => ExtensionData = null;
	}
}
