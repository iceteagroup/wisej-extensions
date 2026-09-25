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
	/// Represents the options of the Chart.js decimation plugin (<c>options.plugins.decimation</c>), used to downsample large line chart datasets for faster rendering.
	/// </summary>
	/// <remarks>
	/// According to Chart.js, decimation only applies to line charts with a linear or time x axis, datasets whose <c>indexAxis</c> is <c>"x"</c>, and data that does not need parsing (<c>parsing: false</c>).
	/// An instance is available through <see cref="PluginsOptions.Decimation"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var decimation = chart.ChartOptions.Plugins.Decimation;
	/// decimation.Enabled = true;
	/// decimation.Algorithm = "lttb";
	/// decimation.Samples = 500;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class DecimationOptions : OptionsBase
	{
		private bool _enabled;
		private string? _algorithm;
		private int _samples;

		/// <summary>
		/// Returns or sets whether the decimation plugin is enabled (Chart.js option <c>enabled</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to enable decimation; otherwise <c>false</c>. The default is <c>false</c>.
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var decimation = chart.ChartOptions.Plugins.Decimation;
		/// decimation.Enabled = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("enabled")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Enable decimation.")]
		public bool Enabled
		{
			get => _enabled;
			set => SetProperty(ref _enabled, value);
		}

		/// <summary>
		/// Returns or sets the decimation algorithm (Chart.js option <c>algorithm</c>).
		/// </summary>
		/// <value>
		/// <c>"min-max"</c> or <c>"lttb"</c>, or <c>null</c> (default) to use the Chart.js default (<c>"min-max"</c>).
		/// </value>
		/// <remarks>
		/// <c>"min-max"</c> preserves peaks by keeping up to 4 points per pixel; <c>"lttb"</c> (Largest Triangle Three Bucket) reduces the data to the number of points specified by <see cref="Samples"/>.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var decimation = chart.ChartOptions.Plugins.Decimation;
		/// decimation.Enabled = true;
		/// decimation.Algorithm = "min-max";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("algorithm")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Decimation algorithm.")]
		public string? Algorithm
		{
			get => _algorithm;
			set => SetProperty(ref _algorithm, value);
		}

		/// <summary>
		/// Returns or sets the number of samples to keep after decimation when <see cref="Algorithm"/> is <c>"lttb"</c> (Chart.js option <c>samples</c>).
		/// </summary>
		/// <value>
		/// The number of points to keep. The default is <c>0</c>, which is not serialized and lets Chart.js use the width of the chart area in pixels.
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var decimation = chart.ChartOptions.Plugins.Decimation;
		/// decimation.Enabled = true;
		/// decimation.Algorithm = "lttb";
		/// decimation.Samples = 250;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("samples")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Number of samples.")]
		[DefaultValue(0)]
		public int Samples
		{
			get => _samples;
			set => SetProperty(ref _samples, value);
		}

		/// <summary>
		/// Returns or sets a dictionary of additional Chart.js options that are not exposed as typed properties of the decimation options, such as <c>threshold</c>.
		/// </summary>
		/// <value>
		/// A <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/> of option names and values, or <c>null</c> (default).
		/// </value>
		/// <remarks>
		/// The dictionary is marked with <c>[JsonExtensionData]</c>: each entry is serialized as an additional top-level property of this options object, using the key as the JSON property name. Use it to set any Chart.js option not covered by the typed API.
		/// This property is hidden from the property grid and is not persisted by the designer. Assigning the property does not refresh the chart automatically; the new values are sent with the next chart update.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var decimation = chart.ChartOptions.Plugins.Decimation;
		/// decimation.ExtensionData = new System.Collections.Generic.Dictionary<string, object>
		/// {
		///     ["threshold"] = 1000
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
		/// Returns whether the <see cref="Enabled"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Enabled"/> is <c>true</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (decimation.ShouldSerializeEnabled())
		///     decimation.ResetEnabled();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeEnabled() => Enabled != false;

		/// <summary>
		/// Resets the <see cref="Enabled"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Enabled"/> to <c>false</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var decimation = chart.ChartOptions.Plugins.Decimation;
		/// decimation.ResetEnabled();
		/// ]]></code>
		/// </example>
		public void ResetEnabled() => Enabled = false;

		/// <summary>
		/// Returns whether the <see cref="Algorithm"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Algorithm"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (decimation.ShouldSerializeAlgorithm())
		///     decimation.ResetAlgorithm();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeAlgorithm() => Algorithm != null;

		/// <summary>
		/// Resets the <see cref="Algorithm"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Algorithm"/> to <c>null</c> (Chart.js default).
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var decimation = chart.ChartOptions.Plugins.Decimation;
		/// decimation.ResetAlgorithm();
		/// ]]></code>
		/// </example>
		public void ResetAlgorithm() => Algorithm = null;

		/// <summary>
		/// Returns whether the <see cref="Samples"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Samples"/> is not <c>0</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (decimation.ShouldSerializeSamples())
		///     decimation.ResetSamples();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeSamples() => Samples != 0;

		/// <summary>
		/// Resets the <see cref="Samples"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Samples"/> to <c>0</c> (Chart.js default).
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var decimation = chart.ChartOptions.Plugins.Decimation;
		/// decimation.ResetSamples();
		/// ]]></code>
		/// </example>
		public void ResetSamples() => Samples = 0;

	}
}
