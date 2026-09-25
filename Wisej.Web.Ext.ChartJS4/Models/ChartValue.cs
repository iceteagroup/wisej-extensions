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
using Wisej.Core;

namespace Wisej.Web.Ext.ChartJS4.Models
{
	/// <summary>
	/// Provides special Chart.js data point sentinel values for use in dataset <c>Data</c> arrays.
	/// </summary>
	/// <remarks>
	/// JSON has no representation for <c>NaN</c>, so the sentinel values are serialized as special strings that the
	/// client-side script converts back to the corresponding JavaScript value before passing the data to Chart.js.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// chart.DataSets.Add(new LineDataSet
	/// {
	///     Label = "Readings",
	///     Data = new object[] { 10, 12, ChartValue.NaN, 15, 11 }
	/// });
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public static class ChartValue
	{
		/// <summary>
		/// Represents a skipped (NaN) data point. When used in a dataset's <c>Data</c> array,
		/// it serializes to JavaScript <c>NaN</c>, causing Chart.js to mark the point as skipped
		/// (<c>ctx.p0.skip</c> / <c>ctx.p1.skip</c> = <c>true</c>).
		/// This enables per-segment styling via the <c>segment</c> dataset option.
		/// </summary>
		/// <remarks>
		/// The value is written to JSON as the sentinel string <c>"__NaN__"</c> and converted to JavaScript <c>NaN</c>
		/// on the client. With <c>spanGaps</c> enabled the line is drawn across the skipped point, and the segments
		/// adjacent to it can be styled through a <c>segment</c> scriptable option.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.WidgetFunctions = new[] { new ChartJS4.WidgetFunction
		///     { Name = "skippedDash", Source = "return ctx.p0.skip || ctx.p1.skip ? [6, 6] : undefined;" } };
		/// var dataSet = new LineDataSet { Data = new object[] { 3, 5, ChartValue.NaN, 4, 6 }, SpanGaps = true };
		/// dataSet.ExtensionData = new Dictionary<string, object>
		/// {
		///     ["segment"] = new { borderDash = "(ctx)=>skippedDash" }
		/// };
		/// ]]></code>
		/// </example>
		public static readonly ChartNaN NaN = new ChartNaN();
	}

	/// <summary>
	/// Sentinel struct that serializes to JavaScript <c>NaN</c>.
	/// Use <see cref="ChartValue.NaN"/> instead of instantiating this directly.
	/// </summary>
	/// <remarks>
	/// Instances are written to JSON as the sentinel string <c>"__NaN__"</c>, which the client-side startup
	/// script replaces with JavaScript <c>NaN</c> in the dataset data.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// object[] data = { 1, 2, ChartValue.NaN, 4 };
	/// bool isSkipped = data[2] is ChartNaN; // true
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public readonly struct ChartNaN
	{
		/// <summary>Internal sentinel string recognized by the client-side startup script.</summary>
		internal const string Sentinel = "__NaN__";
	}
}
