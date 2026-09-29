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
using System.Drawing;

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Specifies the grid line configuration for each axis.
	/// </summary>
	/// <remarks>
	/// An instance of this class is exposed by the <b>GridLines</b> property of each axis
	/// in <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScales.xAxes"/> and <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScales.yAxes"/>
	/// and maps to the <c>gridLines</c> configuration of the Chart.js axis.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var gridLines = this.chartJS1.Options.Scales.yAxes[0].GridLines;
	/// gridLines.Color = new[] { Color.LightGray };
	/// gridLines.BorderDash = new[] { 4, 2 };
	/// gridLines.DrawTicks = false;
	/// this.chartJS1.Update();
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS")]
	public class OptionsAxisGridLines : OptionsBase
	{
		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a set of grid line options that is not yet attached to an owner.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var gridLines = new OptionsAxisGridLines();
		/// gridLines.Display = false;
		/// this.chartJS1.Options.Scales.xAxes[0].GridLines = gridLines;
		/// this.chartJS1.Update();
		/// ]]></code>
		/// </example>
		public OptionsAxisGridLines()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsAxisGridLines"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> instance (usually the axis options) that owns this set of options.</param>
		/// <exception cref="T:System.InvalidOperationException">Thrown when the owner is already assigned to a different instance.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var axis = this.chartJS1.Options.Scales.yAxes[0];
		/// var gridLines = new OptionsAxisGridLines(axis);
		/// gridLines.LineWidth = 2;
		/// axis.GridLines = gridLines;
		/// ]]></code>
		/// </example>
		public OptionsAxisGridLines(OptionsBase owner)
		{
			this.Owner = owner;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the length and spacing of dashes on grid lines.
		/// See <see href="https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/setLineDash"/>.
		/// </summary>
		/// <value>
		/// An array of alternating dash and gap lengths in pixels; null (default) draws solid lines.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// // 5px dash followed by a 3px gap.
		/// this.chartJS1.Options.Scales.yAxes[0].GridLines.BorderDash = new[] { 5, 3 };
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Length and spacing of dashes on grid lines.")]
		public int[] BorderDash
		{
			get
			{
				return this._borderDash;
			}
			set
			{
				this._borderDash = value;
				Update();
			}
		}
		private int[] _borderDash;

		/// <summary>
		/// Returns or sets the offset for line dashes.
		/// See <see href="https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineDashOffset"/>.
		/// </summary>
		/// <value>
		/// The dash offset in pixels. The default is 0.
		/// </value>
		/// <remarks>
		/// Only has a visible effect when <see cref="P:Wisej.Web.Ext.ChartJS.OptionsAxisGridLines.BorderDash"/> is set.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var gridLines = this.chartJS1.Options.Scales.xAxes[0].GridLines;
		/// gridLines.BorderDash = new[] { 6, 4 };
		/// gridLines.BorderDashOffset = 2F;
		/// ]]></code>
		/// </example>
		[DefaultValue(0F)]
		[Description("Offset for line dashes.")]
		public float BorderDashOffset
		{
			get
			{
				return this._borderDashOffset;
			}
			set
			{
				if (this._borderDashOffset != value)
				{
					this._borderDashOffset = value;
					Update();
				}
			}
		}
		private float _borderDashOffset = 0F;

		/// <summary>
		/// Returns or sets whether grid lines are circular (on radar chart only).
		/// </summary>
		/// <value>
		/// true to draw circular grid lines; false (default) to draw straight polygon segments.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.ChartType = ChartType.Radar;
		/// this.chartJS1.Options.Scales.yAxes[0].GridLines.Circular = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("If true, gridlines are circular (on radar chart only).")]
		public bool Circular
		{
			get { return this._circular; }
			set
			{
				if (this._circular != value)
				{
					this._circular = value;
					Update();
				}
			}
		}
		private bool _circular = false;

		/// <summary>
		/// Returns or sets the colors of the grid lines.
		/// </summary>
		/// <value>
		/// An array of colors; null (default) uses the Chart.js default color.
		/// </value>
		/// <remarks>
		/// If specified as an array, the first color applies to the
		/// first grid line, the second to the second grid line and so on.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // alternate two colors on the first grid lines.
		/// this.chartJS1.Options.Scales.yAxes[0].GridLines.Color = new[]
		/// {
		/// 	Color.Gray, Color.LightGray, Color.Gray, Color.LightGray
		/// };
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("The colors of the grid lines.")]
		public Color[] Color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
				Update();
			}
		}
		private Color[] _color;

		/// <summary>
		/// Shows or hides the grid lines for the specified axis.
		/// </summary>
		/// <value>
		/// true (default) to show the grid lines; false to hide them.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// // hide the vertical grid lines, keep the horizontal ones.
		/// this.chartJS1.Options.Scales.xAxes[0].GridLines.Display = false;
		/// this.chartJS1.Options.Scales.yAxes[0].GridLines.Display = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("Shows or hides the grid lines for the specified axis.")]
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
		/// Returns or sets whether to draw the border at the edge between the axis and the chart area.
		/// </summary>
		/// <value>
		/// The default is true.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.Scales.yAxes[0].GridLines.DrawBorder = false;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("If true, draw border at the edge between the axis and the chart area.")]
		public bool DrawBorder
		{
			get
			{
				return this._drawBorder;
			}
			set
			{
				if (this._drawBorder != value)
				{
					this._drawBorder = value;

					Update();
				}
			}
		}
		private bool _drawBorder = true;

		/// <summary>
		/// Returns or sets whether to draw lines on the chart area inside the axis lines.
		/// </summary>
		/// <value>
		/// The default is true.
		/// </value>
		/// <remarks>
		/// This is useful when there are multiple axes and
		/// you need to control which grid lines are drawn.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // draw the grid only for the first y axis.
		/// var yAxes = this.chartJS1.Options.Scales.yAxes;
		/// for (var i = 1; i < yAxes.Length; i++)
		/// 	yAxes[i].GridLines.DrawOnChartArea = false;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("If true, draw lines on the chart area inside the axis lines.")]
		public bool DrawOnChartArea
		{
			get
			{
				return this._drawOnChartArea;
			}
			set
			{
				if (this._drawOnChartArea != value)
				{
					this._drawOnChartArea = value;
					Update();
				}
			}
		}
		private bool _drawOnChartArea = true;

		/// <summary>
		/// Returns or sets whether to draw lines beside the ticks in the axis area beside the chart.
		/// </summary>
		/// <value>
		/// The default is true.
		/// </value>
		/// <remarks>
		/// The length of the tick lines is set by <see cref="P:Wisej.Web.Ext.ChartJS.OptionsAxisGridLines.TickMarkLength"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.Scales.xAxes[0].GridLines.DrawTicks = false;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("If true, draw lines beside the ticks in the axis area beside the chart.")]
		public bool DrawTicks
		{
			get
			{
				return this._drawTicks;
			}
			set
			{
				if (this._drawTicks != value)
				{
					this._drawTicks = value;
					Update();
				}
			}
		}
		private bool _drawTicks = true;

		/// <summary>
		/// Returns or sets the stroke width of grid lines.
		/// </summary>
		/// <value>
		/// The width in pixels. The default is 1.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.Scales.yAxes[0].GridLines.LineWidth = 2;
		/// ]]></code>
		/// </example>
		[DefaultValue(1)]
		[Description("Stroke width of grid lines.")]
		public int LineWidth
		{
			get
			{
				return this._lineWidth;
			}
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

		/// <summary>
		/// Returns or sets whether grid lines will be shifted to be between labels.
		/// </summary>
		/// <value>
		/// The default is false.
		/// </value>
		/// <remarks>
		/// Chart.js sets this option to true by default on the category axis of bar charts.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.ChartType = ChartType.Bar;
		/// this.chartJS1.Options.Scales.xAxes[0].GridLines.OffsetGridLines = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("If true, grid lines will be shifted to be between labels.")]
		public bool OffsetGridLines
		{
			get
			{
				return this._offsetGridLines;
			}
			set
			{
				if (this._offsetGridLines != value)
				{
					this._offsetGridLines = value;
					Update();
				}
			}
		}
		private bool _offsetGridLines = false;

		/// <summary>
		/// Returns or sets the length in pixels that the grid lines will draw into the axis area.
		/// </summary>
		/// <value>
		/// The length in pixels. The default is 10.
		/// </value>
		/// <remarks>
		/// Only applies when <see cref="P:Wisej.Web.Ext.ChartJS.OptionsAxisGridLines.DrawTicks"/> is true.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var gridLines = this.chartJS1.Options.Scales.xAxes[0].GridLines;
		/// gridLines.DrawTicks = true;
		/// gridLines.TickMarkLength = 5;
		/// ]]></code>
		/// </example>
		[DefaultValue(10)]
		[Description("Length in pixels that the grid lines will draw into the axis area.")]
		public int TickMarkLength
		{
			get
			{
				return this._tickMarkLength;
			}
			set
			{
				if (this._tickMarkLength != value)
				{
					this._tickMarkLength = value;
					Update();
				}
			}
		}
		private int _tickMarkLength = 10;

		/// <summary>
		/// Returns or sets the z-index of the grid line layer. Values &lt;= 0 are drawn under datasets, &gt; 0 on top.
		/// </summary>
		/// <value>
		/// The default is 0.
		/// </value>
		/// <remarks>
		/// The <c>z</c> grid line option was introduced in Chart.js 2.8; the Chart.js 2.7.2 library
		/// bundled with this extension ignores it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // draw the grid lines on top of the datasets.
		/// this.chartJS1.Options.Scales.yAxes[0].GridLines.Z = 1;
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
		[Description("z-index of gridline layer. Values <= 0 are drawn under datasets, > 0 on top.")]
		public int Z
		{
			get
			{
				return this._z;
			}
			set
			{
				if (this._z != value)
				{
					this._z = value;
					Update();
				}
			}
		}
		private int _z = 0;

		/// <summary>
		/// Returns or sets the length and spacing of dashes of the grid line for the first index (index 0).
		/// See <see href="https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/setLineDash"/>.
		/// </summary>
		/// <value>
		/// An array of alternating dash and gap lengths in pixels; null (default) draws a solid line.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var gridLines = this.chartJS1.Options.Scales.yAxes[0].GridLines;
		/// gridLines.ZeroLineColor = Color.Black;
		/// gridLines.ZeroLineBorderDash = new[] { 8, 4 };
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Length and spacing of dashes of the grid line for the first index (index 0).")]
		public int[] ZeroLineBorderDash
		{
			get
			{
				return this._zeroLineBorderDash;
			}
			set
			{
				this._zeroLineBorderDash = value;
				Update();
			}
		}
		private int[] _zeroLineBorderDash;

		/// <summary>
		/// Returns or sets the offset for line dashes of the grid line for the first index (index 0).
		/// See <see href="https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineDashOffset"/>.
		/// </summary>
		/// <value>
		/// The dash offset in pixels. The default is 0.
		/// </value>
		/// <remarks>
		/// Only has a visible effect when <see cref="P:Wisej.Web.Ext.ChartJS.OptionsAxisGridLines.ZeroLineBorderDash"/> is set.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var gridLines = this.chartJS1.Options.Scales.yAxes[0].GridLines;
		/// gridLines.ZeroLineBorderDash = new[] { 4, 4 };
		/// gridLines.ZeroLineBorderDashOffset = 4F;
		/// ]]></code>
		/// </example>
		[DefaultValue(0F)]
		[Description("Offset for line dashes of the grid line for the first index (index 0).")]
		public float ZeroLineBorderDashOffset
		{
			get
			{
				return this._zeroLineBorderDashOffset;
			}
			set
			{
				if (this._zeroLineBorderDashOffset != value)
				{
					this._zeroLineBorderDashOffset = value;
					Update();
				}
			}
		}
		private float _zeroLineBorderDashOffset = 0F;

		/// <summary>
		/// Returns or sets the stroke color of the grid line for the first index (index 0).
		/// </summary>
		/// <value>
		/// The default is <see cref="F:System.Drawing.Color.Empty"/>, which uses the Chart.js default color.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// // highlight the zero line of a chart with negative values.
		/// this.chartJS1.Options.Scales.yAxes[0].GridLines.ZeroLineColor = Color.Red;
		/// this.chartJS1.Options.Scales.yAxes[0].GridLines.ZeroLineWidth = 2;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[Description("Stroke color of the grid line for the first index (index 0).")]
		public Color ZeroLineColor
		{
			get
			{
				return this._zeroLineColor;
			}
			set
			{
				if (this._zeroLineColor != value)
				{
					this._zeroLineColor = value;
					Update();
				}
			}
		}
		private Color _zeroLineColor;

		/// <summary>
		/// Returns or sets the stroke width of the grid line for the first index (index 0).
		/// </summary>
		/// <value>
		/// The width in pixels. The default is 1.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.Scales.yAxes[0].GridLines.ZeroLineWidth = 3;
		/// ]]></code>
		/// </example>
		[DefaultValue(1)]
		[Description("Stroke width of the grid line for the first index (index 0).")]
		public int ZeroLineWidth
		{
			get
			{
				return this._zeroLineWidth;
			}
			set
			{
				if (this._zeroLineWidth != value)
				{
					this._zeroLineWidth = value;
					Update();
				}
			}
		}
		private int _zeroLineWidth = 1;

		#endregion
	}
}
