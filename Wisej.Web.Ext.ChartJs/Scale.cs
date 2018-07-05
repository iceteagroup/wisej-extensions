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

using System.ComponentModel;

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Type of scale being employed.
	/// </summary>
	public enum ScaleType
	{
		/// <summary>
		/// The linear scale can be used to display numerical data. It can be placed on either the x or y axis. The scatter chart type automatically configures a line chart to use one of these scales for the x axis.
		/// </summary>
		Linear,

		/// <summary>
		/// The time scale is used to display times and dates. It can be placed on the x axis. When building its ticks, it will automatically calculate the most comfortable unit base on the size of the scale.
		/// </summary>
		Time,

		/// <summary>
		/// Labels are drawn in from the labels array included in the chart data.
		/// </summary>
		Category,

		/// <summary>
		/// The logarithmic scale is used to display logarithmic data of course. It can be placed on either the x or y axis.
		/// </summary>
		Logarithmic,

		/// <summary>
		/// The radial linear scale is used specifically for the radar chart type.
		/// </summary>
		RadialLinear
	}

	/// <summary>
	/// Configure how different time units are formatted into strings for the axis tick marks.
	/// </summary>
	public enum TimeScaleTimeUnit
	{
		/// <summary>
		/// Milliseconds 'SSS [ms]'
		/// </summary>
		millisecond,
		/// <summary>
		/// Seconds 'h:mm:ss a'
		/// </summary>
		second,
		/// <summary>
		/// Minutes 'h:mmm:ss a'
		/// </summary>
		minute,
		/// <summary>
		/// Hours 'MMM D, hA'
		/// </summary>
		hour,
		/// <summary>
		/// Days 'll'
		/// </summary>
		day,
		/// <summary>
		/// Weeks ;ll'
		/// </summary>
		week,
		/// <summary>
		/// Months 'MMM YYYY'
		/// </summary>
		month,
		/// <summary>
		/// Quarters '[Q]Q - YYYY'
		/// </summary>
		quarter,
		/// <summary>
		/// Years 'YYYY'
		/// </summary>
		year
	}

	/// <summary>
	/// Options when using a time scale
	/// </summary>
	public class ScaleTime : OptionsBase
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public ScaleTime()
		{
		}

		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		public ScaleTime(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Rounds the dates to the start of this unit.
		/// </summary>
		[DefaultValue(null)]
		[Description("Rounds the dates to the start of this unit.")]
		public TimeScaleTimeUnit? Round
		{
			get { return this._round; }
			set
			{
				if (this._round != value)
				{
					this._round = value;
					Update();
				}
			}

		}
		private TimeScaleTimeUnit? _round = null;

		/// <summary>
		/// Force the unit to be a certain type.
		/// </summary>
		[DefaultValue(null)]
		[Description("Force the unit to be a certain type.")]
		public TimeScaleTimeUnit? Unit
		{
			get { return this._unit; }
			set
			{
				if (this._unit != value)
				{
					this._unit = value;
					Update();
				}
			}
		}
		private TimeScaleTimeUnit? _unit = null;

		/// <summary>
		/// The number of units between grid lines.
		/// </summary>
		[DefaultValue(1)]
		[Description("The number of units between grid lines.")]
		public int UnitStepSize
		{
			get { return this._unitStepSize; }
			set
			{
				if (this._unitStepSize != value)
				{
					this._unitStepSize = value;
					Update();
				}
			}
		}
		private int _unitStepSize = 1;

		/// <summary>
		/// The moment js format string to use for the tooltip
		/// </summary>
		[DefaultValue("")]
		[Description("The moment js format string to use for the tooltip.")]
		public string TooltipFormat
		{
			get { return this._tooltipFormat; }
			set
			{
				if (this._tooltipFormat != value)
				{
					this._tooltipFormat = value;
					Update();
				}
			}
		}
		private string _tooltipFormat = "";
	}
}
