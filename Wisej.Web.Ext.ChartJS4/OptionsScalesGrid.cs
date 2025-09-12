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

namespace Wisej.Web.Ext.ChartJS4
{
	/// <summary>
	/// Specifies the grid line configuration for each axis.
	/// </summary>
	/// <remarks>
	/// See: https://www.chartjs.org/docs/latest/axes/styling.html#grid-line-configuration.
	/// </remarks>
	[ApiCategory("ChartJS4")]
	public class OptionsScalesGrid : OptionsBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsScalesGrid()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionsAxisGridlines"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public OptionsScalesGrid(OptionsBase owner)
		{
			this.Owner = owner;

			// offset is true by default for bar charts.
			if (owner.Chart?.ChartType == ChartType.Bar)
				this.Offset = true;
		}

		#endregion

		#region Properties

		/// <summary>
		/// If true, grid lines are circular (on radar and polar area charts only).
		/// </summary>
		[DefaultValue(false)]
		[Description("If true, gridlines are circular (on radar and polar area charts only).")]
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
		/// The colors of the grid lines.
		/// </summary>
		/// <remarks>
		/// If specified as an array, the first color applies to the 
		/// first grid line, the second to the second grid line and so on.
		/// </remarks>
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
		/// If false, do not display grid lines for this axis.
		/// </summary>
		[DefaultValue(true)]
		[Description("If false, do not display grid lines for this axis.")]
		public bool Display
		{
			get
			{
				return this._display;
			}
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
		/// If true, draw lines on the chart area inside the axis lines.
		/// </summary>
		/// <remarks>
		/// This is useful when there are multiple axes and 
		/// you need to control which grid lines are drawn.
		/// </remarks>
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
		/// If true, draw lines beside the ticks in the axis area beside the chart.
		/// </summary>
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
		/// Stroke width of grid lines.
		/// </summary>
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
		/// If true, grid lines will be shifted to be between labels.
		/// </summary>
		[DefaultValue(false)]
		[Description("If true, grid lines will be shifted to be between labels.")]
		public bool Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if (this._offset != value)
				{
					this._offset = value;
					Update();
				}
			}
		}
		private bool _offset = false;

		/// <summary>
		/// Length and spacing of the tick mark line. If not set, defaults to the grid line borderDash value.
		/// </summary>
		[DefaultValue(null)]
		[Description("Length and spacing of the tick mark line. If not set, defaults to the grid line borderDash value.")]
		public int[] TickBorderDash
		{
			get
			{
				return this._tickBorderDash;
			}
			set
			{
				this._tickBorderDash = value;
				Update();
			}
		}
		private int[] _tickBorderDash;

		/// <summary>
		/// Offset for the line dash of the tick mark. If unset, defaults to the grid line borderDashOffset value
		/// </summary>
		[DefaultValue(null)]
		[Description("Offset for the line dash of the tick mark. If unset, defaults to the grid line borderDashOffset value")]
		public float? TickBorderDashOffset
		{
			get
			{
				return this._tickBorderDashOffset;
			}
			set
			{
				if (this._tickBorderDashOffset != value)
				{
					this._tickBorderDashOffset = value;
					Update();
				}
			}
		}
		private float? _tickBorderDashOffset;

		/// <summary>
		/// Color of the tick line. If unset, defaults to the grid line color.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[Description("Color of the tick line. If unset, defaults to the grid line color.")]
		public Color TickColor
		{
			get
			{
				return this._tickColor;
			}
			set
			{
				if (this._tickColor != value)
				{
					this._tickColor = value;
					Update();
				}
			}
		}
		private Color _tickColor;

		/// <summary>
		/// Length in pixels that the grid lines will draw into the axis area.
		/// </summary>
		[DefaultValue(8)]
		[Description("Length in pixels that the grid lines will draw into the axis area.")]
		public int TickLength
		{
			get
			{
				return this._tickLength;
			}
			set
			{
				if (this._tickLength != value)
				{
					this._tickLength = value;
					Update();
				}
			}
		}
		private int _tickLength = 8;

		/// <summary>
		/// Width of the tick mark in pixels. If unset, defaults to the grid line width.
		/// </summary>
		[DefaultValue(null)]
		[Description("Width of the tick mark in pixels. If unset, defaults to the grid line width.")]
		public int? TickWidth
		{
			get
			{
				return this._tickWidth;
			}
			set
			{
				if (this._tickWidth != value)
				{
					this._tickWidth = value;
					Update();
				}
			}
		}
		private int? _tickWidth;

		/// <summary>
		/// z-index of gridline layer. Values &lt;= 0 are drawn under datasets, > 0 on top.
		/// </summary>
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

		#endregion
	}
}
