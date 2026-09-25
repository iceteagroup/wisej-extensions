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

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Type of scale being employed.
	/// </summary>
	/// <remarks>
	/// Used by the <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Type"/> property of the axes.
	/// The x axes default to <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Category"/> and the y axes to <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Linear"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// // plot values spanning several orders of magnitude.
	/// this.chartJS31.Options.Scales.yAxes[0].Type = ScaleType.Logarithmic;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS3")]
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
	/// Specifies the time units used by the time scale.
	/// </summary>
	/// <remarks>
	/// Used by the <see cref="P:Wisej.Web.Ext.ChartJS3.ScaleTime.Unit"/> and <see cref="P:Wisej.Web.Ext.ChartJS3.ScaleTime.Round"/> properties.
	/// The format shown for each member is the default Moment.js format used for the axis tick marks,
	/// which can be changed using <see cref="P:Wisej.Web.Ext.ChartJS3.ScaleTime.DisplayFormats"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var xAxis = this.chartJS31.Options.Scales.xAxes[0];
	/// xAxis.Type = ScaleType.Time;
	/// xAxis.Time.Unit = TimeScaleTimeUnit.quarter;
	/// ]]></code>
	/// </example>
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
		/// Minutes 'h:mm a'
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
		/// Weeks 'll'
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
	/// Options used when the axis is a time scale.
	/// </summary>
	/// <remarks>
	/// An instance of this class is exposed by the <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Time"/> property of each axis
	/// in <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.xAxes"/> and <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.yAxes"/>
	/// and maps to the <c>time</c> configuration of the Chart.js 3 axis. The options are serialized only
	/// when the axis <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Type"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Time"/>.
	/// The dates are parsed and formatted using the Moment.js library loaded by the control.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var xAxis = this.chartJS31.Options.Scales.xAxes[0];
	/// xAxis.Type = ScaleType.Time;
	/// xAxis.Time.Unit = TimeScaleTimeUnit.day;
	/// xAxis.Time.TooltipFormat = "MMM D, YYYY";
	/// ]]></code>
	/// </example>
	public class ScaleTime : OptionsBase
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		/// <remarks>
		/// Creates a set of time scale options that is not attached to an owner.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var time = new ScaleTime();
		/// time.Unit = TimeScaleTimeUnit.month;
		/// this.chartJS31.Options.Scales.xAxes[0].Type = ScaleType.Time;
		/// this.chartJS31.Options.Scales.xAxes[0].Time = time;
		/// ]]></code>
		/// </example>
		public ScaleTime()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.ScaleTime"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> instance (usually the axis options) that owns this set of options.</param>
		/// <example>
		/// <code><![CDATA[
		/// var xAxis = this.chartJS31.Options.Scales.xAxes[0];
		/// var time = new ScaleTime(xAxis);
		/// time.Unit = TimeScaleTimeUnit.week;
		/// xAxis.Type = ScaleType.Time;
		/// xAxis.Time = time;
		/// ]]></code>
		/// </example>
		public ScaleTime(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Returns or sets the unit to which the dates are rounded (to the start of the unit).
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS3.TimeScaleTimeUnit"/> values, or null (default) to disable rounding.
		/// </value>
		/// <remarks>
		/// Maps to the <c>time.round</c> option of Chart.js.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // round all the timestamps to the start of the hour.
		/// var xAxis = this.chartJS31.Options.Scales.xAxes[0];
		/// xAxis.Type = ScaleType.Time;
		/// xAxis.Time.Round = TimeScaleTimeUnit.hour;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the time unit used for the ticks of the scale.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS3.TimeScaleTimeUnit"/> values, or null (default)
		/// to let Chart.js determine the most appropriate unit from the data.
		/// </value>
		/// <remarks>
		/// Maps to the <c>time.unit</c> option of Chart.js.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var xAxis = this.chartJS31.Options.Scales.xAxes[0];
		/// xAxis.Type = ScaleType.Time;
		/// xAxis.Time.Unit = TimeScaleTimeUnit.month;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the number of units between grid lines.
		/// </summary>
		/// <value>
		/// The default is 1.
		/// </value>
		/// <remarks>
		/// The value is serialized as <c>time.unitStepSize</c>, which is the Chart.js 2 name of the option;
		/// Chart.js 3 uses <c>time.stepSize</c> and ignores this value.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // a tick every 15 minutes.
		/// var time = this.chartJS31.Options.Scales.xAxes[0].Time;
		/// time.Unit = TimeScaleTimeUnit.minute;
		/// time.UnitStepSize = 15;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the Moment.js format string used to display the dates in the tooltips.
		/// </summary>
		/// <value>
		/// A Moment.js format string. The default is an empty string, which uses the Chart.js default.
		/// </value>
		/// <remarks>
		/// See <see href="https://momentjs.com/docs/#/displaying/format/"/> for the format tokens.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var xAxis = this.chartJS31.Options.Scales.xAxes[0];
		/// xAxis.Type = ScaleType.Time;
		/// xAxis.Time.TooltipFormat = "ddd, MMM D YYYY h:mm a";
		/// ]]></code>
		/// </example>
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

		/// <summary>
		/// Returns or sets how the different time units are formatted on the axis ticks.
		/// </summary>
		/// <value>
		/// An object whose property names are the time units (i.e. <c>day</c>, <c>month</c>) and values
		/// are Moment.js format strings; null (default) uses the Chart.js defaults.
		/// </value>
		/// <remarks>
		/// The object is serialized as-is to the <c>time.displayFormats</c> option of Chart.js;
		/// an anonymous type is the simplest way to specify it.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var time = this.chartJS31.Options.Scales.xAxes[0].Time;
		/// time.DisplayFormats = new
		/// {
		/// 	day = "DD/MM",
		/// 	month = "MMM YY"
		/// };
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Sets how different time units are displayed.")]
		public object DisplayFormats
		{
			get
			{
				return this._displayFormats;
			}
			set
			{
				if (this._displayFormats != value)
				{
					this._displayFormats = value;
					Update();
				}
			}
		}
		private object _displayFormats;

		/// <summary>
		/// Returns or sets a custom format to be used by Moment.js to parse the dates.
		/// </summary>
		/// <value>
		/// A Moment.js format string. The default is an empty string, which lets Moment.js detect the format.
		/// </value>
		/// <remarks>
		/// Use it when the data values are strings in a format that Moment.js cannot recognize automatically.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var xAxis = this.chartJS31.Options.Scales.xAxes[0];
		/// xAxis.Type = ScaleType.Time;
		/// xAxis.Time.Parser = "DD/MM/YYYY";
		/// this.chartJS31.Labels = new[] { "01/03/2024", "02/03/2024", "03/03/2024" };
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		[Description("A custom format to be used by Moment.js to parse the date.")]
		public string Parser
		{
			get
			{
				return this._parser;
			}
			set
			{
				if (this._parser != value)
				{
					this._parser = value;
					Update();
				}
			}
		}
		private string _parser = "";
	}
}
