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
using System.Web;
using Wisej.Base;
using Wisej.Core;
using MicrosoftCharts = System.Windows.Forms.DataVisualization.Charting;

namespace Wisej.Web.Ext.WebCharts
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Chart))]
	public class Chart: Control, ISupportInitialize, IWisejHandler
	{
		// the chart engine.
		private ChartRenderer chart = new ChartRenderer();

		#region Properties

		/// <summary>
		/// Returns a read-only <see cref="T:System.Web.UI.DataVisualization.Charting.ChartAreaCollection" /> object that is used to store <see cref="T:System.Web.UI.DataVisualization.Charting.ChartArea" /> objects.
		/// </summary>
		/// <returns>A <see cref="T:System.Web.UI.DataVisualization.Charting.ChartAreaCollection" /> object.</returns>
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
		/// Returns a <see cref="T:System.Web.UI.DataVisualization.Charting.SeriesCollection" /> object.
		/// </summary>
		/// <returns>A <see cref="T:System.Web.UI.DataVisualization.Charting.SeriesCollection" /> object, which contains <see cref="T:System.Web.UI.DataVisualization.Charting.Series" /> objects.</returns>
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
		public void BeginInit()
		{
			this.chart.BeginInit();
		}

		/// <summary>
		/// Signals to the System.Windows.Forms.DataVisualization.Charting.Chart object
		/// that initialization is complete.
		/// </summary>
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
