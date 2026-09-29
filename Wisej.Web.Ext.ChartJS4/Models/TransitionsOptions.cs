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
	/// Transitions options (Chart.js <c>options.transitions</c>).
	/// </summary>
	/// <remarks>
	/// Transitions configure the animations used for specific update modes: <c>active</c> (hover), <c>resize</c>,
	/// <c>show</c>, <c>hide</c>, <c>reset</c>, <c>none</c> or any custom mode. Each mode maps to an object with
	/// <c>animation</c> and/or <c>animations</c> members. Since the modes are open-ended, they are supplied through
	/// <see cref="ExtensionData"/>. An instance is obtained from <see cref="ChartOptions.Transitions"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// chart.ChartOptions.Transitions.ExtensionData = new Dictionary<string, object>
	/// {
	///     ["show"] = new { animations = new { x = new { from = 0 }, y = new { from = 0 } } },
	///     ["hide"] = new { animations = new { x = new { to = 0 }, y = new { to = 0 } } }
	/// };
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class TransitionsOptions : OptionsBase
	{
		/// <summary>
		/// Returns or sets additional custom properties that can be serialized to JSON.
		/// </summary>
		/// <value>
		/// A dictionary whose keys are transition mode names (e.g. <c>"active"</c>, <c>"resize"</c>, <c>"show"</c>, <c>"hide"</c>)
		/// and whose values are transition configurations. The default is <c>null</c>.
		/// </value>
		/// <remarks>
		/// Marked with <see cref="JsonExtensionDataAttribute"/>: every entry is written as a property of the Chart.js
		/// <c>transitions</c> object. Changes to the dictionary do not refresh the chart automatically.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var transitions = chart.ChartOptions.Transitions;
		/// transitions.ExtensionData = new Dictionary<string, object>
		/// {
		///     ["active"] = new { animation = new { duration = 0 } }
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
		/// Determines whether the <see cref="ExtensionData"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="ExtensionData"/> contains at least one entry; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (transitions.ShouldSerializeExtensionData())
		///     transitions.ResetExtensionData();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeExtensionData() => ExtensionData != null && ExtensionData.Count > 0;

		/// <summary>
		/// Resets the <see cref="ExtensionData"/> property to its default value of <c>null</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Transitions.ResetExtensionData();
		/// ]]></code>
		/// </example>
		public void ResetExtensionData() => ExtensionData = null;
	}
}
