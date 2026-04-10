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
	/// Animation options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class AnimationsOptions : OptionsBase
	{
		/// <summary>
		/// The number of milliseconds an animation takes.
		/// </summary>
		[JsonPropertyName("duration")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(1000)]
		[Description("Animation duration in milliseconds.")]
		public int Duration { get; set; } = 1000;

		/// <summary>
		/// Easing function to use. Available options: 'linear', 'easeInQuad', 'easeOutQuad', etc.
		/// </summary>
		[JsonPropertyName("easing")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Easing function for animations.")]
		[DefaultValue(null)]
		public string? Easing { get; set; }

		/// <summary>
		/// Delay before starting the animation.
		/// </summary>
		[JsonPropertyName("delay")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Delay before animation starts in milliseconds.")]
		public int Delay { get; set; }

		/// <summary>
		/// If true, the chart will animate in with a rotation animation.
		/// </summary>
		[JsonPropertyName("loop")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Loop the animation.")]
		public bool Loop { get; set; }

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
		/// Determines whether the Duration property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeDuration() => Duration != 1000;

		/// <summary>
		/// Resets the Duration property to its default value.
		/// </summary>
		public void ResetDuration() => Duration = 1000;

		/// <summary>
		/// Determines whether the Easing property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeEasing() => Easing != null;

		/// <summary>
		/// Resets the Easing property to its default value.
		/// </summary>
		public void ResetEasing() => Easing = null;

		/// <summary>
		/// Determines whether the Delay property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeDelay() => Delay != 0;

		/// <summary>
		/// Resets the Delay property to its default value.
		/// </summary>
		public void ResetDelay() => Delay = 0;

		/// <summary>
		/// Determines whether the Loop property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeLoop() => Loop != false;

		/// <summary>
		/// Resets the Loop property to its default value.
		/// </summary>
		public void ResetLoop() => Loop = false;

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
