
using System.ComponentModel;

namespace Wisej.Web.Ext.ChartJS4.Ticks
{

	/// <summary>
	/// Common tick options for cartesian scales.
	/// </summary>
	/// <remarks>
	/// https://www.chartjs.org/docs/latest/axes/cartesian/
	/// </remarks>
	public class ScalesTicksCartesian : ScalesTicks
	{
		/// <summary>
		/// The tick alignment along the axis.
		/// </summary>
		[DefaultValue(ScalesTicksCartesianAlign.Center)]
		[Description("The tick alignment along the axis.")]
		public ScalesTicksCartesianAlign Align
		{
			get
			{
				return this._align;
			}
			set
			{
				if (this._align != value) 
				{
					this._align = value;
					Update();
				}
			}
		}
		private ScalesTicksCartesianAlign _align = ScalesTicksCartesianAlign.Center;

		/// <summary>
		/// The tick alignment perpendicular to the axis.
		/// </summary>
		[DefaultValue(ScalesTicksCartesianCrossAlign.Near)]
		[Description("The tick alignment perpendicular to the axis.")]
		public ScalesTicksCartesianCrossAlign CrossAlign
		{
			get
			{
				return this._crossAlign;
			}
			set
			{
				if (this._crossAlign != value) 
				{
					this._crossAlign = value;
					Update();
				}
			}
		}
		private ScalesTicksCartesianCrossAlign _crossAlign = ScalesTicksCartesianCrossAlign.Near;

		/// <summary>
		/// Padding between the ticks on the horizontal axis when autoSkip is enabled.
		/// </summary>
		[DefaultValue(3)]
		[Description("Padding between the ticks on the horizontal axis when autoSkip is enabled.")]
		private int AutoSkipPadding
		{
			get
			{
				return this._autoSkipPadding;
			}
			set
			{
				if (this._autoSkipPadding != value)
				{
					this._autoSkipPadding = value;
					Update();
				}
			}
		}
		private int _autoSkipPadding = 3;

		/// <summary>
		/// Should the defined min and max values be presented as ticks even if they are not "nice".
		/// </summary>
		[DefaultValue(true)]
		[Description("Should the defined min and max values be presented as ticks even if they are not \"nice\".")]
		public bool IncludeBounds
		{
			get
			{
				return this._includeBounds;
			}
			set
			{
				if (this._includeBounds != value)
				{
					this._includeBounds = value;
					Update();
				}
			}
		}
		private bool _includeBounds = true;

		/// <summary>
		/// Distance in pixels to offset the label from the centre point of the tick (in the x-direction for the x-axis, and the y-direction for the y-axis).
		/// </summary>
		/// <remarks>
		/// Note: this can cause labels at the edges to be cropped by the edge of the canvas.
		/// </remarks>
		[DefaultValue(0)]
		[Description("Distance in pixels to offset the label from the centre point of the tick.")]
		public int LabelOffset
		{
			get
			{
				return this._labelOffset;
			}
			set
			{
				if (this._labelOffset != value)
				{
					this._labelOffset = value;
					Update();
				}
			}
		}
		private int _labelOffset = 0;

		/// <summary>
		/// Flips tick labels around axis, displaying the labels inside the chart instead of outside.
		/// </summary>
		/// <remarks>
		/// Note: Only applicable to vertical scales.
		/// </remarks>
		public bool Mirror
		{
			get
			{
				return this._mirror;
			}
			set
			{
				if (this._mirror != value)
				{
					this._mirror = value;
					Update();
				}
			}
		}
		private bool _mirror = false;

		/// <summary>
		/// Maximum number of ticks and gridlines to show.
		/// </summary>
		[DefaultValue(11)]
		[Description("Maximum number of ticks and gridlines to show.")]
		public int MaxTicksLimit
		{
			get
			{
				return this._maxTicksLimit;
			}
			set
			{
				if (this._maxTicksLimit != value)
				{
					this._maxTicksLimit = value;
					Update();
				}
			}
		}
		private int _maxTicksLimit = 11;
	}

	/// <summary>
	/// tick alignment along the axis.
	/// </summary>
	[ApiCategory("ChartJS4")]
	public enum ScalesTicksCartesianAlign
	{
		/// <summary>
		/// Near the 
		/// </summary>
		Start,

		/// <summary>
		/// 
		/// </summary>
		Center,

		/// <summary>
		/// 
		/// </summary>
		End,

		/// <summary>
		/// 
		/// </summary>
		Inner
	}

	/// <summary>
	/// Tick alignment perpendicular to the axis.
	/// </summary>
	[ApiCategory("ChartJS4")]
	public enum ScalesTicksCartesianCrossAlign
	{
		/// <summary>
		/// Near the axis.
		/// </summary>
		Near,

		/// <summary>
		/// Middle fof the axis.
		/// </summary>
		Center,

		/// <summary>
		/// Away from the axis.
		/// </summary>
		Far
	}
}
