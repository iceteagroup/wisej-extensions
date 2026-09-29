///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Drawing;
using Wisej.Base;

namespace Wisej.Web.Ext.SmoothieChart
{
	/// <summary>
	/// Represents a line on the <see cref="T:Wisej.Web.Ext.SmoothieChart.SmoothieChart"/> control.
	/// </summary>
	/// <remarks>
	/// Add instances to the <see cref="SmoothieChart.TimeSeries"/> collection; the values of the line are provided by
	/// the <see cref="SmoothieChart.DataNeeded"/> event.
	/// </remarks>
	/// <example>
	/// Adding a filled line to the chart:
	/// <code><![CDATA[
	/// this.smoothieChart1.TimeSeries.Add(new TimeSeries
	/// {
	///     LineColor = Color.Lime,
	///     FillColor = Color.FromArgb(80, Color.Lime),
	///     LineWidth = 2
	/// });
	/// ]]></code>
	/// </example>
	[ApiCategory("SmoothieChart")]
	public class TimeSeries
	{
		#region Properties

		internal SmoothieChart Owner
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the line color.
		/// </summary>
		[DefaultValue(typeof(Color), "Green")]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the line color.")]
		public Color LineColor
		{
			get { return this._lineColor; }
			set
			{
				if (this._lineColor != value)
				{
					this._lineColor = value;
					Update();
				}
			}
		}
		private Color _lineColor = Color.Green;

		/// <summary>
		/// Returns or sets the color used to fill the area below the line.
		/// </summary>
		/// <remarks>
		/// The default is <see cref="Color.Transparent"/> (no fill). Use a semi-transparent color to keep the grid
		/// and the other lines visible.
		/// </remarks>
		[DefaultValue(typeof(Color), "Transparent")]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the fill color.")]
		public Color FillColor
		{
			get { return this._fillColor; }
			set
			{
				if (this._fillColor != value)
				{
					this._fillColor = value;
					Update();
				}
			}
		}
		private Color _fillColor = Color.Transparent;

		/// <summary>
		/// Returns or sets the line width in pixels.
		/// </summary>
		[DefaultValue(1)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the line width in pixels.")]
		public int LineWidth
		{
			get { return this._lineWidth; }
			set
			{
				if (this._lineWidth != value)
				{
					this._lineWidth = value;

					Update();
				}
			}
		}
		private int _lineWidth = 1;

		#endregion

		#region Methods

		private void Update()
		{
			this.Owner?.Update();
		}

		#endregion
	}
}
