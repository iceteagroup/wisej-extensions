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

namespace Wisej.Web.Ext.ChartJS4
{
	/// <summary>
	/// Provides constant values for Chart.js point styles.
	/// </summary>
	/// <remarks>
	/// Use these constants with properties that map to the Chart.js <c>pointStyle</c> option, such as
	/// <see cref="Models.LineDataSet.PointStyle"/>. Chart.js also accepts an image or canvas object for this option.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// chart.DataSets.Add(new LineDataSet
	/// {
	///     Label = "Targets",
	///     Data = new object[] { 4, 7, 5 },
	///     PointStyle = PointStyle.Star,
	///     PointRadius = 8
	/// });
	/// ]]></code>
	/// </example>
	public static class PointStyle
	{
		/// <summary>Circle point (<c>"circle"</c>). This is the Chart.js default.</summary>
		public const string Circle = "circle";
		/// <summary>Triangle point (<c>"triangle"</c>).</summary>
		public const string Triangle = "triangle";
		/// <summary>Rectangle point (<c>"rect"</c>).</summary>
		public const string Rect = "rect";
		/// <summary>Rectangle rotated by 45 degrees, i.e. a diamond (<c>"rectRot"</c>).</summary>
		public const string RectRot = "rectRot";
		/// <summary>Rectangle with rounded corners (<c>"rectRounded"</c>).</summary>
		public const string RectRounded = "rectRounded";
		/// <summary>Cross (plus sign) point (<c>"cross"</c>).</summary>
		public const string Cross = "cross";
		/// <summary>Cross rotated by 45 degrees, i.e. an X (<c>"crossRot"</c>).</summary>
		public const string CrossRot = "crossRot";
		/// <summary>Star point (<c>"star"</c>).</summary>
		public const string Star = "star";
		/// <summary>Horizontal line point (<c>"line"</c>).</summary>
		public const string Line = "line";
		/// <summary>Dash point, a short line starting at the point center (<c>"dash"</c>).</summary>
		public const string Dash = "dash";
		/// <summary>Intended to disable point rendering (<c>"false"</c>).</summary>
		/// <remarks>
		/// This constant is the string <c>"false"</c>. Chart.js disables points only for the boolean value <c>false</c>
		/// and may draw unrecognized strings with its default shape, so assign the boolean <c>false</c> to object-typed
		/// point style properties (e.g. <c>dataSet.PointStyle = false;</c>) to reliably hide the points.
		/// </remarks>
		public const string False = "false";
	}
}
