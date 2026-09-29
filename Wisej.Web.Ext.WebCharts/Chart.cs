///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using Wisej.Base;
using Wisej.Core;
using MicrosoftCharts = System.Windows.Forms.DataVisualization.Charting;

namespace Wisej.Web.Ext.WebCharts
{
	/// <summary>
	/// Represents a chart control that renders a <see cref="T:System.Windows.Forms.DataVisualization.Charting.Chart"/>
	/// on the server and displays it in the browser as an image.
	/// </summary>
	/// <remarks>
	/// The chart is rendered as a PNG image with the current size of the control every time the browser requests it.
	/// The image is not refreshed automatically when the <see cref="ChartAreas"/> or <see cref="Series"/> change:
	/// call <see cref="Control.Update"/> after changing the chart to send a new image URL to the browser.
	/// </remarks>
	/// <example>
	/// Populating a column chart from code:
	/// <code><![CDATA[
	/// using Charting = System.Windows.Forms.DataVisualization.Charting;
	///
	/// private void Page1_Load(object sender, EventArgs e)
	/// {
	///     this.chart1.ChartAreas.Add(new Charting.ChartArea("Default"));
	///
	///     var sales = new Charting.Series("Sales")
	///     {
	///         ChartType = Charting.SeriesChartType.Column,
	///         ChartArea = "Default"
	///     };
	///     sales.Points.AddXY("Q1", 120);
	///     sales.Points.AddXY("Q2", 145);
	///     sales.Points.AddXY("Q3", 98);
	///     this.chart1.Series.Add(sales);
	///
	///     this.chart1.Update();
	/// }
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Chart))]
	public class Chart: Control, ISupportInitialize, IWisejHandler
	{
		// the chart engine.
		private ChartRenderer chart = new ChartRenderer();

		#region Properties

		/// <summary>
		/// Returns a read-only <see cref="T:System.Windows.Forms.DataVisualization.Charting.ChartAreaCollection" /> object that is used to store <see cref="T:System.Windows.Forms.DataVisualization.Charting.ChartArea" /> objects.
		/// </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataVisualization.Charting.ChartAreaCollection" /> object.</returns>
		/// <remarks>
		/// Each <see cref="T:System.Windows.Forms.DataVisualization.Charting.Series"/> is drawn in the chart area named by its
		/// ChartArea property. Call <see cref="Control.Update"/> after changing the collection at runtime to refresh the image in the browser.
		/// </remarks>
		/// <example>
		/// Adding a chart area with a custom Y axis range:
		/// <code><![CDATA[
		/// var area = new System.Windows.Forms.DataVisualization.Charting.ChartArea("Revenue");
		/// area.AxisY.Minimum = 0;
		/// area.AxisY.Maximum = 500;
		/// this.chart1.ChartAreas.Add(area);
		/// this.chart1.Update();
		/// ]]></code>
		/// </example>
		[Bindable(true)]
		[SRCategory("CategoryAttributeChart")]
		[SRDescription("DescriptionAttributeChartAreas")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MicrosoftCharts.ChartAreaCollection ChartAreas
		{
			get
			{
				return this.chart.ChartAreas;
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.Windows.Forms.DataVisualization.Charting.SeriesCollection" /> object.
		/// </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataVisualization.Charting.SeriesCollection" /> object, which contains <see cref="T:System.Windows.Forms.DataVisualization.Charting.Series" /> objects.</returns>
		/// <remarks>
		/// Changes to the series or to their data points are not sent to the browser automatically:
		/// call <see cref="Control.Update"/> to render and download a new image of the chart.
		/// </remarks>
		/// <example>
		/// Replacing the data points of an existing series:
		/// <code><![CDATA[
		/// var series = this.chart1.Series["Sales"];
		/// series.Points.Clear();
		/// foreach (var row in GetMonthlySales())
		/// {
		///     series.Points.AddXY(row.Month, row.Total);
		/// }
		/// this.chart1.Update();
		/// ]]></code>
		/// </example>
		[SRCategory("CategoryAttributeChart")]
		[SRDescription("DescriptionAttributeChart_Series")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MicrosoftCharts.SeriesCollection Series
		{
			get
			{
				return this.chart.Series;
			}
		}

		#endregion

		protected override void OnCreateControl()
		{
			// when in design mode, attach the paint handler
			// to render the chart on the design surface.
			// at runtime the chart is returned in the http response stream.

			IWisejControl me = this;
			if (me.DesignMode)
			{
				this.Paint += Chart_Paint;
			}

			base.OnCreateControl();
		}

		/// <summary>
		/// Signals to the object that initialization is starting.
		/// </summary>
		/// <remarks>
		/// Called by the designer-generated code. Use it together with <see cref="EndInit"/> to
		/// suspend the chart's internal processing while applying several changes.
		/// </remarks>
		/// <example>
		/// Batching the initialization of a chart:
		/// <code><![CDATA[
		/// this.chart1.BeginInit();
		/// this.chart1.Series.Add("Sales");
		/// this.chart1.Series["Sales"].Points.AddY(42);
		/// this.chart1.EndInit();
		/// ]]></code>
		/// </example>
		public void BeginInit()
		{
			this.chart.BeginInit();
		}

		/// <summary>
		/// Signals to the <see cref="T:System.Windows.Forms.DataVisualization.Charting.Chart"/> object
		/// that initialization is complete.
		/// </summary>
		/// <example>
		/// Completing a batched initialization started with <see cref="BeginInit"/>:
		/// <code><![CDATA[
		/// this.chart1.BeginInit();
		/// this.chart1.ChartAreas.Add("Default");
		/// this.chart1.EndInit();
		/// ]]></code>
		/// </example>
		public void EndInit()
		{
			this.chart.EndInit();
		}

		private void Chart_Paint(object sender, PaintEventArgs e)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				chart.Width = this.Width;
				chart.Height = this.Height;
				chart.SaveImage(stream, MicrosoftCharts.ChartImageFormat.Png);
				using (Image image = Image.FromStream(stream))
				{
					e.Graphics.DrawImageUnscaled(image, 0, 0);
				}
			}
		}

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);
			IWisejControl me = this;

			if (!me.DesignMode)
			{
				config.backgroundImages = new
				{
					layout = ImageLayout.Zoom,
					image = this.GetPostbackURL() + "&v=" + DateTime.Now.Ticks
				};
			}
		}

		/// <summary>
		/// Dont compress the output. Images are already compressed.
		/// </summary>
		bool IWisejHandler.Compress { get { return false; } }

		/// <summary>
		/// Process the http request.
		/// </summary>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext"/>.</param>
		void IWisejHandler.ProcessRequest(HttpContext context)
		{
			chart.Width = this.Width;
			chart.Height = this.Height;
			context.Response.ContentType = "image/png";
			chart.SaveImage(context.Response.OutputStream, MicrosoftCharts.ChartImageFormat.Png);
		}
	}
}
