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
	/// Tick options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class TickOptions : OptionsBase
	{
		/// <summary>
		/// If true, show tick labels.
		/// </summary>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Display tick labels.")]
		public bool Display { get; set; } = true;

		/// <summary>
		/// Color of tick labels.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Tick label color.")]
		public object? Color { get; set; }

		/// <summary>
		/// Font configuration for ticks.
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
		/// Maximum rotation for tick labels when rotating to condense labels.
		/// </summary>
		[JsonPropertyName("maxRotation")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(50)]
		[Description("Maximum rotation angle.")]
		public int MaxRotation { get; set; } = 50;

		/// <summary>
		/// Minimum rotation for tick labels.
		/// </summary>
		[JsonPropertyName("minRotation")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Minimum rotation angle.")]
		public int MinRotation { get; set; }

		/// <summary>
		/// Flips tick labels around axis, displaying the labels inside the chart instead of outside.
		/// </summary>
		[JsonPropertyName("mirror")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Mirror tick labels.")]
		public bool Mirror { get; set; }

		/// <summary>
		/// Alignment of tick labels: 'start', 'center', 'end', 'inner'.
		/// </summary>
		[JsonPropertyName("align")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Alignment of tick labels.")]
		public string? Align { get; set; }

		/// <summary>
		/// Padding between the tick label and the axis.
		/// </summary>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Padding between label and axis.")]
		[DefaultValue(3)]
		public int Padding { get; set; } = 3;

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
		/// Determines whether the Align property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeAlign() => Align != null;

		/// <summary>
		/// Resets the Align property to its default value.
		/// </summary>
		public void ResetAlign() => Align = null;

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
		/// Determines whether the Font property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeFont() => Font != null && !Font.IsDefault;

		/// <summary>
		/// Resets the Font property to its default value.
		/// </summary>
		public void ResetFont() => Font = null;

		/// <summary>
		/// Determines whether the MaxRotation property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeMaxRotation() => MaxRotation != 50;

		/// <summary>
		/// Resets the MaxRotation property to its default value.
		/// </summary>
		public void ResetMaxRotation() => MaxRotation = 50;

		/// <summary>
		/// Determines whether the MinRotation property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeMinRotation() => MinRotation != 0;

		/// <summary>
		/// Resets the MinRotation property to its default value.
		/// </summary>
		public void ResetMinRotation() => MinRotation = 0;

		/// <summary>
		/// Determines whether the Mirror property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeMirror() => Mirror != false;

		/// <summary>
		/// Resets the Mirror property to its default value.
		/// </summary>
		public void ResetMirror() => Mirror = false;

		/// <summary>
		/// Determines whether the Padding property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializePadding() => Padding != 3;

		/// <summary>
		/// Resets the Padding property to its default value.
		/// </summary>
		public void ResetPadding() => Padding = 3;

	}
}
