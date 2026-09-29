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
using System.Drawing;

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Represents the options for the chart legend.
	/// </summary>
	/// <remarks>
	/// An instance of this class is exposed by <see cref="P:Wisej.Web.Ext.ChartJS.Options.Legend"/>
	/// and maps to the <c>legend</c> configuration of Chart.js.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var legend = this.chartJS1.Options.Legend;
	/// legend.Display = true;
	/// legend.Position = HeaderPosition.Bottom;
	/// legend.Labels.UsePointStyle = true;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS")]
	public class OptionsLegend : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a set of legend options that is not yet attached to an owner.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = new OptionsLegend();
		/// legend.Position = HeaderPosition.Bottom;
		/// this.chartJS1.Options.Legend = legend;
		/// ]]></code>
		/// </example>
		public OptionsLegend()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsLegend"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> instance (usually the chart <see cref="T:Wisej.Web.Ext.ChartJS.Options"/>) that owns this set of options.</param>
		/// <exception cref="T:System.InvalidOperationException">Thrown when the owner is already assigned to a different instance.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var options = this.chartJS1.Options;
		/// var legend = new OptionsLegend(options);
		/// legend.Display = false;
		/// options.Legend = legend;
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
		/// Either <see cref="F:Wisej.Web.HeaderPosition.Top"/> (default) or <see cref="F:Wisej.Web.HeaderPosition.Bottom"/>.
		/// </value>
		/// <exception cref="T:System.ArgumentException">
		/// The value is <see cref="F:Wisej.Web.HeaderPosition.Left"/> or <see cref="F:Wisej.Web.HeaderPosition.Right"/>.
		/// </exception>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.Legend.Position = HeaderPosition.Bottom;
		/// ]]></code>
		/// </example>
		[DefaultValue(HeaderPosition.Top)]
		[Description("Position of the title.")]
		public HeaderPosition Position
		{
			get { return this._position; }
			set
			{
				if (value == HeaderPosition.Left || value == HeaderPosition.Right)
					throw new ArgumentException("The legend position can only be Top or Bottom.");

				if (this._position != value)
				{
					this._position = value;
					Update();
				}
			}
		}
		private HeaderPosition _position = HeaderPosition.Top;

		/// <summary>
		/// Shows or hides the legend block.
		/// </summary>
		/// <value>
		/// true (default) to show the legend; false to hide it.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// // a single data set doesn't need a legend.
		/// this.chartJS1.Options.Legend.Display = this.chartJS1.DataSets.Count > 1;
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
		/// An <see cref="T:Wisej.Web.Ext.ChartJS.OptionsLegendLabels"/> instance, created automatically on first access.
		/// </value>
		/// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The assigned instance already belongs to a different owner.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var labels = this.chartJS1.Options.Legend.Labels;
		/// labels.BoxWidth = 20;
		/// labels.Padding = 15;
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
	/// An instance of this class is exposed by <see cref="P:Wisej.Web.Ext.ChartJS.OptionsLegend.Labels"/>
	/// and maps to the <c>legend.labels</c> configuration of Chart.js.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var labels = this.chartJS1.Options.Legend.Labels;
	/// labels.UsePointStyle = true;
	/// labels.Padding = 20;
	/// ]]></code>
	/// </example>
	public class OptionsLegendLabels : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a set of legend label options that is not yet attached to an owner.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var labels = new OptionsLegendLabels();
		/// labels.BoxWidth = 12;
		/// this.chartJS1.Options.Legend.Labels = labels;
		/// ]]></code>
		/// </example>
		public OptionsLegendLabels()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsLegendLabels"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> instance (usually the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsLegend"/>) that owns this set of options.</param>
		/// <exception cref="T:System.InvalidOperationException">Thrown when the owner is already assigned to a different instance.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var legend = this.chartJS1.Options.Legend;
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
		/// When not set (null), returns the <b>Font</b> of the owner <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
		/// </value>
		/// <remarks>
		/// The font is converted on the client to the Chart.js <c>fontSize</c>, <c>fontFamily</c> and <c>fontStyle</c> options.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.Legend.Labels.Font = new Font("Arial", 11F, FontStyle.Bold);
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
		/// Returns or sets the padding between labels (rows of colored boxes).
		/// </summary>
		/// <value>
		/// The padding in pixels. The default is 10.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.Legend.Labels.Padding = 20;
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
		/// Returns or sets whether the legend uses the point style of each dataset instead of a rectangle to identify it.
		/// </summary>
		/// <value>
		/// The default is false.
		/// </value>
		/// <remarks>
		/// When true, the size of the legend marker is based on the font size instead of <see cref="P:Wisej.Web.Ext.ChartJS.OptionsLegendLabels.BoxWidth"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.ChartType = ChartType.Line;
		/// this.chartJS1.Options.Legend.Labels.UsePointStyle = true;
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
		/// <example>
		/// <code><![CDATA[
		/// // small square markers.
		/// this.chartJS1.Options.Legend.Labels.BoxWidth = 12;
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
		/// Returns or sets the color of the legend labels.
		/// </summary>
		/// <value>
		/// The default is <see cref="F:System.Drawing.Color.Empty"/>.
		/// </value>
		/// <remarks>
		/// The value is sent to the client as the <c>legend.labels.color</c> option.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.Legend.Labels.Color = Color.DarkSlateGray;
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
