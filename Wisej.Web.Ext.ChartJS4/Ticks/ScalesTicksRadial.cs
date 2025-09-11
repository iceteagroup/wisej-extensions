

using System.ComponentModel;

namespace Wisej.Web.Ext.ChartJS4.Ticks
{
	/// <summary>
	/// Common tick options for radial scales.
	/// </summary>
	/// <remarks>
	/// See: https://www.chartjs.org/docs/latest/axes/radial/linear.html#linear-radial-axis-specific-tick-options.
	/// </remarks>
	public class ScalesTicksRadial : ScalesTicks
	{
		/// <summary>
		/// The number of ticks to generate. If specified, this overrides the automatic generation.
		/// </summary>
		[DefaultValue(null)]
		[Description("The number of ticks to generate. If specified, this overrides the automatic generation.")]
		public int? Count
		{
			get
			{
				return this._count;
			}
			set
			{
				if (this._count != value) 
				{ 
					this._count = value;
					Update();
				}
			}
		}
		private int? _count = null;

		/// <summary>
		/// If defined and stepSize is not specified, the step size will be rounded to this many decimal places.
		/// </summary>
		[DefaultValue(null)]
		[Description("If defined and stepSize is not specified, the step size will be rounded to this many decimal places.")]
		public int? Precision
		{
			get
			{
				return this._precision;
			}
			set
			{
				if (this._precision != value) 
				{
					this._precision = value;
					Update();
				}
			}
		}
		private int? _precision = null;

		/// <summary>
		/// User defined fixed step size for the scale.
		/// </summary>
		[DefaultValue(null)]
		[Description("User defined fixed step size for the scale.")]
		public int? StepSize
		{
			get
			{
				return this._stepSize;
			}
			set
			{
				if (this._stepSize != value)
				{
					this._stepSize = value;
					Update();
				}
			}
		}
		private int? _stepSize = null;
	}
}
