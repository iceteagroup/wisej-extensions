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
	/// Represents the options of the Chart.js filler plugin (<c>options.plugins.filler</c>), which fills the area of line and radar datasets that have the <c>fill</c> option set.
	/// </summary>
	/// <remarks>
	/// An instance is available through <see cref="PluginsOptions.Filler"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var filler = chart.ChartOptions.Plugins.Filler;
	/// filler.DrawTime = "beforeDatasetsDraw";
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class FillerOptions : OptionsBase
	{
		private bool _propagate = true;
		private string? _drawTime;

		/// <summary>
		/// Returns or sets whether the fill propagates to the next visible dataset when the target dataset of a fill is hidden (Chart.js option <c>propagate</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to fill up to the next visible target; <c>false</c> to disable the fill when its target is hidden. The default is <c>true</c>.
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var filler = chart.ChartOptions.Plugins.Filler;
		/// bool propagate = filler.Propagate;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("propagate")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Propagate fill to visible datasets.")]
		public bool Propagate
		{
			get => _propagate;
			set => SetProperty(ref _propagate, value);
		}

		/// <summary>
		/// Returns or sets when the fill areas are drawn relative to the datasets (Chart.js option <c>drawTime</c>).
		/// </summary>
		/// <value>
		/// <c>"beforeDraw"</c>, <c>"beforeDatasetDraw"</c> or <c>"beforeDatasetsDraw"</c>, or <c>null</c> (default) to use the Chart.js default (<c>"beforeDatasetDraw"</c>).
		/// </value>
		/// <remarks>
		/// <c>"beforeDraw"</c> draws the fill before anything else (behind the grid), <c>"beforeDatasetsDraw"</c> draws all fills before all datasets, <c>"beforeDatasetDraw"</c> draws each fill right before its dataset.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var filler = chart.ChartOptions.Plugins.Filler;
		/// filler.DrawTime = "beforeDatasetsDraw";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("drawTime")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("When to draw the fill.")]
		public string? DrawTime
		{
			get => _drawTime;
			set => SetProperty(ref _drawTime, value);
		}

		/// <summary>
		/// Returns or sets a dictionary of additional Chart.js options that are not exposed as typed properties of the filler options.
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
		/// var filler = chart.ChartOptions.Plugins.Filler;
		/// filler.ExtensionData = new System.Collections.Generic.Dictionary<string, object>
		/// {
		///     ["drawTime"] = "beforeDraw"
		/// };
		/// ]]></code>
		/// </example>
		[JsonExtensionData]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <summary>
		/// Returns whether the <see cref="Propagate"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Propagate"/> is <c>false</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (filler.ShouldSerializePropagate())
		///     filler.ResetPropagate();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePropagate() => Propagate != true;

		/// <summary>
		/// Resets the <see cref="Propagate"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Propagate"/> to <c>true</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var filler = chart.ChartOptions.Plugins.Filler;
		/// filler.ResetPropagate();
		/// ]]></code>
		/// </example>
		public void ResetPropagate() => Propagate = true;

		/// <summary>
		/// Returns whether the <see cref="DrawTime"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="DrawTime"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (filler.ShouldSerializeDrawTime())
		///     filler.ResetDrawTime();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDrawTime() => DrawTime != null;

		/// <summary>
		/// Resets the <see cref="DrawTime"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="DrawTime"/> to <c>null</c> (Chart.js default).
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var filler = chart.ChartOptions.Plugins.Filler;
		/// filler.ResetDrawTime();
		/// ]]></code>
		/// </example>
		public void ResetDrawTime() => DrawTime = null;

	}
}
