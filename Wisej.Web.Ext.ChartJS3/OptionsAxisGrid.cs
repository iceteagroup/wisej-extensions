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

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Specifies the grid line configuration for each axis.
	/// </summary>
	/// <remarks>
	/// An instance of this class is exposed by the <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Grid"/> property of each axis
	/// in <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.xAxes"/> and <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.yAxes"/>
	/// and maps to the <c>grid</c> configuration of the Chart.js 3 axis.
	/// The property setters call <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, but the default grid instance
	/// of an axis is not attached to the chart: call <c>Update()</c> on the control to apply the changes.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var grid = this.chartJS31.Options.Scales.yAxes[0].Grid;
	/// grid.Color = new[] { Color.LightGray };
	/// grid.BorderDash = new[] { 4, 2 };
	/// grid.DrawTicks = false;
	/// this.chartJS31.Update();
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS3")]
	public class OptionsAxisGrid : OptionsBase
	{
		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a set of grid line options that is not attached to an owner.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = new OptionsAxisGrid();
		/// grid.Display = false;
		/// this.chartJS31.Options.Scales.xAxes[0].Grid = grid;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsAxisGrid()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsAxisGrid"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> instance (usually the axis options) that owns this set of options.</param>
		/// <remarks>
		/// When the owner is attached to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control, changing the
		/// properties of this instance refreshes the chart automatically.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = this.chartJS31.Options.Scales.yAxes[0];
		/// var grid = new OptionsAxisGrid(axis);
		/// grid.LineWidth = 2;
		/// axis.Grid = grid;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsAxisGrid(OptionsBase owner)
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
		/// <remarks>
		/// Maps to the <c>grid.borderDash</c> option of Chart.js.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // 5px dash followed by a 3px gap.
		/// this.chartJS31.Options.Scales.yAxes[0].Grid.BorderDash = new[] { 5, 3 };
		/// this.chartJS31.Update();
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
		/// Only has a visible effect when <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsAxisGrid.BorderDash"/> is set.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = this.chartJS31.Options.Scales.xAxes[0].Grid;
		/// grid.BorderDash = new[] { 6, 4 };
		/// grid.BorderDashOffset = 2F;
		/// this.chartJS31.Update();
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
		/// Returns or sets whether the grid lines are circular (on radar charts only).
		/// </summary>
		/// <value>
		/// true to draw circular grid lines; false (default) to draw polygonal grid lines.
		/// </value>
		/// <remarks>
		/// Applies only to the radial scale used by <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Radar"/> charts.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.ChartType = ChartType.Radar;
		/// this.chartJS31.Options.Scales.yAxes[0].Grid.Circular = true;
		/// this.chartJS31.Update();
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
		/// When the array contains more than one color, the first color applies to the
		/// first grid line, the second to the second grid line and so on.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // highlight the first grid line and use light gray for the others.
		/// this.chartJS31.Options.Scales.yAxes[0].Grid.Color = new[]
		/// {
		/// 	Color.Red, Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray
		/// };
		/// this.chartJS31.Update();
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
		/// Returns or sets whether the grid lines are displayed for the axis.
		/// </summary>
		/// <value>
		/// true (default) to show the grid lines; false to hide them.
		/// </value>
		/// <remarks>
		/// Hiding the grid lines doesn't hide the axis ticks and labels; use
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Display"/> to hide the whole axis.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // remove the vertical grid lines.
		/// this.chartJS31.Options.Scales.xAxes[0].Grid.Display = false;
		/// this.chartJS31.Update();
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
		/// Returns or sets whether to draw a border at the edge between the axis and the chart area.
		/// </summary>
		/// <value>
		/// The default is true.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = this.chartJS31.Options.Scales.yAxes[0].Grid;
		/// grid.DrawBorder = false;
		/// this.chartJS31.Update();
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
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // draw the grid only for the first y axis.
		/// var yAxes = this.chartJS31.Options.Scales.yAxes;
		/// for (var i = 1; i < yAxes.Length; i++)
		/// 	yAxes[i].Grid.DrawOnChartArea = false;
		///
		/// this.chartJS31.Update();
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
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Scales.xAxes[0].Grid.DrawTicks = false;
		/// this.chartJS31.Update();
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
		/// Returns or sets the stroke width of the grid lines.
		/// </summary>
		/// <value>
		/// The width in pixels. The default is 1.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = this.chartJS31.Options.Scales.yAxes[0].Grid;
		/// grid.LineWidth = 2;
		/// grid.Color = new[] { Color.Gainsboro };
		/// this.chartJS31.Update();
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
		/// Returns or sets whether the grid lines are shifted to be between labels.
		/// </summary>
		/// <value>
		/// The default is false.
		/// </value>
		/// <remarks>
		/// The value is serialized as <c>grid.offsetGridLines</c>, which is the Chart.js 2 name of the option;
		/// Chart.js 3 uses <c>grid.offset</c> and ignores this value.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.ChartType = ChartType.Bar;
		/// this.chartJS31.Options.Scales.xAxes[0].Grid.OffsetGridLines = true;
		/// this.chartJS31.Update();
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
		/// Returns or sets the length in pixels that the grid lines draw into the axis area.
		/// </summary>
		/// <value>
		/// The length in pixels. The default is 10.
		/// </value>
		/// <remarks>
		/// The value is serialized as <c>grid.tickMarkLength</c>, which is the Chart.js 2 name of the option;
		/// Chart.js 3 uses <c>grid.tickLength</c> and ignores this value.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Scales.xAxes[0].Grid.TickMarkLength = 5;
		/// this.chartJS31.Update();
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
		/// Returns or sets the z-index of the grid line layer. Values &lt;= 0 are drawn under the data sets, &gt; 0 on top.
		/// </summary>
		/// <value>
		/// The default is 0.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // draw the grid lines on top of the bars.
		/// this.chartJS31.ChartType = ChartType.Bar;
		/// this.chartJS31.Options.Scales.yAxes[0].Grid.Z = 1;
		/// this.chartJS31.Update();
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
		/// An array of alternating dash and gap lengths in pixels; the default is null.
		/// </value>
		/// <remarks>
		/// The zero line options were removed in Chart.js 3: the value is serialized as <c>grid.zeroLineBorderDash</c>
		/// and ignored by Chart.js 3.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Scales.yAxes[0].Grid.ZeroLineBorderDash = new[] { 2, 2 };
		/// this.chartJS31.Update();
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
		/// The zero line options were removed in Chart.js 3: the value is serialized as <c>grid.zeroLineBorderDashOffset</c>
		/// and ignored by Chart.js 3.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = this.chartJS31.Options.Scales.yAxes[0].Grid;
		/// grid.ZeroLineBorderDash = new[] { 4, 4 };
		/// grid.ZeroLineBorderDashOffset = 2F;
		/// this.chartJS31.Update();
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
		/// The default is <see cref="F:System.Drawing.Color.Empty"/>.
		/// </value>
		/// <remarks>
		/// The zero line options were removed in Chart.js 3: the value is serialized as <c>grid.zeroLineColor</c>
		/// and ignored by Chart.js 3.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Scales.yAxes[0].Grid.ZeroLineColor = Color.Black;
		/// this.chartJS31.Update();
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
		/// <remarks>
		/// The zero line options were removed in Chart.js 3: the value is serialized as <c>grid.zeroLineWidth</c>
		/// and ignored by Chart.js 3.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which refreshes the chart only
		/// when this instance is attached to a chart. The default grid instance created by the axis is not attached,
		/// so call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control after changing it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = this.chartJS31.Options.Scales.yAxes[0].Grid;
		/// grid.ZeroLineWidth = 2;
		/// grid.ZeroLineColor = Color.DimGray;
		/// this.chartJS31.Update();
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
