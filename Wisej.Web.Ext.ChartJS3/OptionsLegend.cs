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

using System;
using System.ComponentModel;
using System.Drawing;

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Represents the options for the chart legend.
	/// </summary>
	/// <remarks>
	/// An instance of this class is exposed by the <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsPlugins.Legend"/> property
	/// and maps to the <c>plugins.legend</c> configuration of Chart.js 3.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var legend = this.chartJS31.Options.Plugins.Legend;
	/// legend.Position = HeaderPosition.Bottom;
	/// legend.Labels.UsePointStyle = true;
	/// legend.Labels.Padding = 20;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS3")]
	public class OptionsLegend : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a set of legend options that is not attached to an owner.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = new OptionsLegend();
		/// legend.Position = HeaderPosition.Right;
		/// this.chartJS31.Options.Plugins.Legend = legend;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsLegend()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsLegend"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> instance (usually the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsPlugins"/>) that owns this set of options.</param>
		/// <example>
		/// <code><![CDATA[
		/// var plugins = this.chartJS31.Options.Plugins;
		/// var legend = new OptionsLegend(plugins);
		/// legend.Display = false;
		/// plugins.Legend = legend;
		/// ]]></code>
		/// </example>
		public OptionsLegend(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Returns or sets the position of the legend.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.HeaderPosition"/> values. The default is <see cref="F:Wisej.Web.HeaderPosition.Top"/>.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // show the legend to the right of a pie chart.
		/// this.chartJS31.ChartType = ChartType.Pie;
		/// this.chartJS31.Options.Plugins.Legend.Position = HeaderPosition.Right;
		/// ]]></code>
		/// </example>
		[DefaultValue(HeaderPosition.Top)]
		[Description("Position of the title.")]
		public HeaderPosition Position
		{
			get { return this._position; }
			set
			{
				if (this._position != value)
				{
					this._position = value;
					Update();
				}
			}
		}
		private HeaderPosition _position = HeaderPosition.Top;

		/// <summary>
		/// Returns or sets whether the legend is displayed.
		/// </summary>
		/// <value>
		/// The default is true.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // a chart with a single data set doesn't need a legend.
		/// this.chartJS31.Options.Plugins.Legend.Display = this.chartJS31.DataSets.Count > 1;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("Show the title block.")]
		public bool Display
		{
			get { return this._display; }
			set
			{
				if (this._display != value)
				{
					this._display = value;
					Update();
				}
			}
		}
		private bool _display = true;

		/// <summary>
		/// Returns or sets the options for the labels in the legend.
		/// </summary>
		/// <value>
		/// An <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsLegendLabels"/> instance; it's created automatically on first access.
		/// </value>
		/// <remarks>
		/// Maps to the <c>plugins.legend.labels</c> option of Chart.js 3.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsLegendLabels"/> instance already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var labels = this.chartJS31.Options.Plugins.Legend.Labels;
		/// labels.BoxWidth = 20;
		/// labels.Color = Color.DimGray;
		/// labels.Font = new Font("Segoe UI", 10F);
		/// ]]></code>
		/// </example>
		[Description("Options for the labels in the legend.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsLegendLabels Labels
		{
			get
			{
				if (this._labels == null)
					this._labels = new OptionsLegendLabels(this);

				return this._labels;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._labels = value;
			}
		}
		private OptionsLegendLabels _labels;

	}

	/// <summary>
	/// Represents the options for the labels in the chart legend.
	/// </summary>
	/// <remarks>
	/// An instance of this class is exposed by the <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsLegend.Labels"/> property
	/// and maps to the <c>plugins.legend.labels</c> configuration of Chart.js 3.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var labels = this.chartJS31.Options.Plugins.Legend.Labels;
	/// labels.UsePointStyle = true;
	/// labels.Padding = 15;
	/// labels.Color = Color.Navy;
	/// ]]></code>
	/// </example>
	public class OptionsLegendLabels : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a set of legend label options that is not attached to an owner.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var labels = new OptionsLegendLabels();
		/// labels.BoxWidth = 12;
		/// this.chartJS31.Options.Plugins.Legend.Labels = labels;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsLegendLabels()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsLegendLabels"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> instance (usually the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsLegend"/>) that owns this set of options.</param>
		/// <example>
		/// <code><![CDATA[
		/// var legend = this.chartJS31.Options.Plugins.Legend;
		/// var labels = new OptionsLegendLabels(legend);
		/// labels.UsePointStyle = true;
		/// legend.Labels = labels;
		/// ]]></code>
		/// </example>
		public OptionsLegendLabels(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Returns or sets the font of the legend labels.
		/// </summary>
		/// <value>
		/// When not set (null), returns the <see cref="P:Wisej.Web.Control.Font"/> of the chart control.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Plugins.Legend.Labels.Font = new Font("Arial", 12F, FontStyle.Italic);
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Font of the title.")]
		public Font Font
		{
			get
			{
				var chart = this.Chart;
				if (this._font == null && chart != null)
					return chart.Font;

				return this._font;
			}
			set
			{
				if (this._font != value)
				{
					this._font = value;
					Update();
				}
			}
		}
		private Font _font;

		
		/// <summary>
		/// Returns or sets the padding between the legend labels (rows of colored boxes).
		/// </summary>
		/// <value>
		/// The padding in pixels. The default is 10.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = this.chartJS31.Options.Plugins.Legend;
		/// legend.Position = HeaderPosition.Left;
		/// legend.Labels.Padding = 25;
		/// ]]></code>
		/// </example>
		[DefaultValue(10)]
		[Description("Padding between labels (rows of colored boxes.)")]
		public int Padding
		{
			get { return this._padding; }
			set
			{
				if (this._padding != value)
				{
					this._padding = value;
					Update();
				}
			}
		}
		private int _padding = 10;
		
		/// <summary>
		/// Returns or sets whether the legend uses the point style of each data set instead of a rectangle to identify it.
		/// </summary>
		/// <value>
		/// The default is false.
		/// </value>
		/// <remarks>
		/// The symbol is taken from the point style of the data set (for example <see cref="P:Wisej.Web.Ext.ChartJS3.LineDataSet.PointStyle"/>)
		/// and its size is based on the font size.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = (LineDataSet)this.chartJS31.DataSets[0];
		/// dataSet.PointStyle = new[] { PointStyle.Triangle };
		/// this.chartJS31.Options.Plugins.Legend.Labels.UsePointStyle = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("Point style in the legend instead of a rectangle to identify each dataset.")]
		public bool UsePointStyle 
		{
			get
			{
				return this._usePointStyle;
			}
			set
			{
				if (this._usePointStyle != value)
				{
					this._usePointStyle = value;
					Update();
				}
			} 
		}
		private bool _usePointStyle;

		/// <summary>
		/// Returns or sets the width of the colored box.
		/// </summary>
		/// <value>
		/// The width in pixels. The default is 40.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // small square boxes.
		/// this.chartJS31.Options.Plugins.Legend.Labels.BoxWidth = 12;
		/// ]]></code>
		/// </example>
		[DefaultValue(40)]
		[Description("Width of colored box. Default 40")]
		public int BoxWidth
		{
			get
			{
				return this._boxWidth;
			}
			set
			{
				if (this._boxWidth != value)
				{
					this._boxWidth = value;
					Update();
				}
			}
		}
		private int _boxWidth = 40;

		/// <summary>
		/// Returns or sets the color of the legend label text.
		/// </summary>
		/// <value>
		/// The default is <see cref="F:System.Drawing.Color.Empty"/>, which uses the Chart.js default color.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.BackColor = Color.FromArgb(33, 33, 33);
		/// this.chartJS31.Options.Plugins.Legend.Labels.Color = Color.WhiteSmoke;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[Description("Color of the label")]
		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				if (this._color != value)
				{
					this._color = value;
					Update();
				}
			}
		}
		private Color _color;
	}
}
