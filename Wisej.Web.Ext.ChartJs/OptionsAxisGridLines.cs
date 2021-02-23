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
	public class OptionsAxisGridLines : OptionsBase
	{
		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsAxisGridLines()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsAxisGridlines"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		public OptionsAxisGridLines(OptionsBase owner)
		{
			this.Owner = owner;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Length and spacing of dashes on grid lines. 
		/// See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/setLineDash.
		/// </summary>
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
		/// Offset for line dashes.
		/// See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineDashOffset.
		/// </summary>
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
		/// If true, grid lines are circular (on radar chart only).
		/// </summary>
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
		/// Shows or hides the grid lines for the specified axis.
		/// </summary>
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
		/// If true, draw border at the edge between the axis and the chart area.
		/// </summary>
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
		/// Length in pixels that the grid lines will draw into the axis area.
		/// </summary>
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

		/// <summary>
		/// Length and spacing of dashes of the grid line for the first index (index 0). 
		/// See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/setLineDash.
		/// </summary>
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
		/// Offset for line dashes of the grid line for the first index (index 0). 
		/// See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineDashOffset.
		/// </summary>
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
		/// Stroke color of the grid line for the first index (index 0).
		/// </summary>
		[DefaultValue(null)]
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
		/// Stroke width of the grid line for the first index (index 0).
		/// </summary>
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
