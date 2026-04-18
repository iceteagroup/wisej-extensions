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
	[ApiCategory("ChartJS4")]
	public static class ChartValue
	{
		/// <summary>
		/// Represents a skipped (NaN) data point. When used in a dataset's <c>Data</c> array,
		/// it serializes to JavaScript <c>NaN</c>, causing Chart.js to mark the point as skipped
		/// (<c>ctx.p0.skip</c> / <c>ctx.p1.skip</c> = <c>true</c>).
		/// This enables per-segment styling via the <c>segment</c> dataset option.
		/// </summary>
		public static readonly ChartNaN NaN = new ChartNaN();
	}

	/// <summary>
	/// Sentinel struct that serializes to JavaScript <c>NaN</c>.
	/// Use <see cref="ChartValue.NaN"/> instead of instantiating this directly.
	/// </summary>
	[ApiCategory("ChartJS4")]
	public readonly struct ChartNaN
	{
		/// <summary>Internal sentinel string recognized by the client-side startup script.</summary>
		internal const string Sentinel = "__NaN__";
	}
}
