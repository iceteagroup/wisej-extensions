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
	/// <remarks>
	/// Represents a Chart.js animation configuration (<c>duration</c>, <c>easing</c>, <c>delay</c>, <c>loop</c>).
	/// An instance is typically assigned to <see cref="ChartOptions.Animation"/> to configure the global animation.
	/// Only values that differ from their defaults are sent to the client, and additional Chart.js animation
	/// properties (e.g. <c>from</c>, <c>to</c>, <c>type</c>) can be supplied through <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// chart.ChartOptions.Animation = new AnimationsOptions
	/// {
	///     Duration = 2000,
	///     Easing = "easeOutBounce",
	///     Delay = 250
	/// };
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class AnimationsOptions : OptionsBase
	{
		private int _duration = 1000;
		private string? _easing;
		private int _delay;
		private bool _loop;

		/// <summary>
		/// Returns or sets the number of milliseconds an animation takes.
		/// </summary>
		/// <value>
		/// An <see cref="int"/> in milliseconds. The default is <c>1000</c>. Maps to the Chart.js <c>duration</c> option.
		/// </value>
		/// <remarks>
		/// Set it to <c>0</c> to disable the animation.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var animation = new AnimationsOptions { Duration = 500 };
		/// chart.ChartOptions.Animation = animation;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("duration")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(1000)]
		[Description("Animation duration in milliseconds.")]
		public int Duration
		{
			get => _duration;
			set => SetProperty(ref _duration, value);
		}

		/// <summary>
		/// Returns or sets the easing function to use. Available options: 'linear', 'easeInQuad', 'easeOutQuad', etc.
		/// </summary>
		/// <value>
		/// The name of a Chart.js easing function. The default is <c>null</c> (Chart.js default <c>"easeOutQuart"</c>).
		/// Maps to the Chart.js <c>easing</c> option.
		/// </value>
		/// <remarks>
		/// Accepted values: <c>"linear"</c>, and the <c>easeIn</c>/<c>easeOut</c>/<c>easeInOut</c> variants of
		/// <c>Quad</c>, <c>Cubic</c>, <c>Quart</c>, <c>Quint</c>, <c>Sine</c>, <c>Expo</c>, <c>Circ</c>, <c>Elastic</c>,
		/// <c>Back</c> and <c>Bounce</c> (e.g. <c>"easeInOutCubic"</c>, <c>"easeOutBounce"</c>).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var animation = new AnimationsOptions { Easing = "easeInOutCubic" };
		/// chart.ChartOptions.Animation = animation;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("easing")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Easing function for animations.")]
		[DefaultValue(null)]
		public string? Easing
		{
			get => _easing;
			set => SetProperty(ref _easing, value);
		}

		/// <summary>
		/// Returns or sets the delay, in milliseconds, before starting the animation.
		/// </summary>
		/// <value>
		/// An <see cref="int"/> in milliseconds. The default is <c>0</c>. Maps to the Chart.js <c>delay</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var animation = new AnimationsOptions { Delay = 300, Duration = 800 };
		/// chart.ChartOptions.Animation = animation;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("delay")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Delay before animation starts in milliseconds.")]
		public int Delay
		{
			get => _delay;
			set => SetProperty(ref _delay, value);
		}

		/// <summary>
		/// Returns or sets whether the animation loops endlessly.
		/// </summary>
		/// <value>
		/// <c>true</c> to repeat the animation indefinitely; otherwise <c>false</c>. The default is <c>false</c>.
		/// Maps to the Chart.js <c>loop</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var animation = new AnimationsOptions { Duration = 1500, Loop = true };
		/// chart.ChartOptions.Animation = animation;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("loop")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Loop the animation.")]
		public bool Loop
		{
			get => _loop;
			set => SetProperty(ref _loop, value);
		}

		/// <summary>
		/// Returns or sets additional custom properties that can be serialized to JSON.
		/// </summary>
		/// <value>
		/// A dictionary of Chart.js animation option names and values, or <c>null</c>. The default is <c>null</c>.
		/// </value>
		/// <remarks>
		/// Marked with <see cref="JsonExtensionDataAttribute"/>: every entry is written as an additional property of the
		/// animation JSON object. Use it for Chart.js animation options not exposed as properties, such as
		/// <c>type</c>, <c>from</c>, <c>to</c> or <c>properties</c>. Changes to the dictionary do not refresh the chart automatically.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var animation = new AnimationsOptions { Duration = 1000 };
		/// animation.ExtensionData = new Dictionary<string, object>
		/// {
		///     ["type"] = "number",
		///     ["from"] = 0
		/// };
		/// ]]></code>
		/// </example>
		[JsonExtensionData]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <summary>
		/// Determines whether the <see cref="Duration"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Duration"/> is not <c>1000</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (animation.ShouldSerializeDuration())
		///     animation.ResetDuration();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDuration() => Duration != 1000;

		/// <summary>
		/// Resets the <see cref="Duration"/> property to its default value of <c>1000</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// animation.ResetDuration();
		/// ]]></code>
		/// </example>
		public void ResetDuration() => Duration = 1000;

		/// <summary>
		/// Determines whether the <see cref="Easing"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Easing"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (animation.ShouldSerializeEasing())
		///     animation.ResetEasing();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeEasing() => Easing != null;

		/// <summary>
		/// Resets the <see cref="Easing"/> property to its default value of <c>null</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// animation.ResetEasing();
		/// ]]></code>
		/// </example>
		public void ResetEasing() => Easing = null;

		/// <summary>
		/// Determines whether the <see cref="Delay"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Delay"/> is not <c>0</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (animation.ShouldSerializeDelay())
		///     animation.ResetDelay();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDelay() => Delay != 0;

		/// <summary>
		/// Resets the <see cref="Delay"/> property to its default value of <c>0</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// animation.ResetDelay();
		/// ]]></code>
		/// </example>
		public void ResetDelay() => Delay = 0;

		/// <summary>
		/// Determines whether the <see cref="Loop"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Loop"/> is not <c>false</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (animation.ShouldSerializeLoop())
		///     animation.ResetLoop();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeLoop() => Loop != false;

		/// <summary>
		/// Resets the <see cref="Loop"/> property to its default value of <c>false</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// animation.ResetLoop();
		/// ]]></code>
		/// </example>
		public void ResetLoop() => Loop = false;

		/// <summary>
		/// Determines whether the <see cref="ExtensionData"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="ExtensionData"/> contains at least one entry; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (animation.ShouldSerializeExtensionData())
		///     animation.ResetExtensionData();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeExtensionData() => ExtensionData != null && ExtensionData.Count > 0;

		/// <summary>
		/// Resets the <see cref="ExtensionData"/> property to its default value of <c>null</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// animation.ResetExtensionData();
		/// ]]></code>
		/// </example>
		public void ResetExtensionData() => ExtensionData = null;
	}
}
