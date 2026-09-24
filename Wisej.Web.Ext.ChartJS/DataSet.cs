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
using System.Globalization;
using System.Reflection;

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Represents the data used by the <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control to plot the chart.
	/// See <see href="http://www.chartjs.org/docs/#line-chart-data-structure"/> for additional information regarding the data structure of ChartJS.
	/// </summary>
	/// <remarks>
	/// This is the base class for the chart-specific data sets: <see cref="T:Wisej.Web.Ext.ChartJS.LineDataSet"/>,
	/// <see cref="T:Wisej.Web.Ext.ChartJS.BarDataSet"/>, <see cref="T:Wisej.Web.Ext.ChartJS.HorizontalBarDataSet"/>,
	/// <see cref="T:Wisej.Web.Ext.ChartJS.BubbleDataSet"/>, <see cref="T:Wisej.Web.Ext.ChartJS.DoughnutDataSet"/>,
	/// <see cref="T:Wisej.Web.Ext.ChartJS.PieDataSet"/>, <see cref="T:Wisej.Web.Ext.ChartJS.PolarAreaDataSet"/>
	/// and <see cref="T:Wisej.Web.Ext.ChartJS.RadarDataSet"/>.
	/// <para>
	/// Use <see cref="M:Wisej.Web.Ext.ChartJS.DataSetCollection.Add(System.String)"/> on <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.DataSets"/>
	/// to create a data set that matches the current <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/>, then cast it
	/// to the specialized type to access the chart-specific properties. The properties are sent to the client as the
	/// Chart.js dataset configuration.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Bar;
	/// this.chartJS1.Labels = new[] { "Q1", "Q2", "Q3", "Q4" };
	///
	/// var sales = this.chartJS1.DataSets.Add("Sales");
	/// sales.Data = new object[] { 120, 150, 90, 180 };
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS")]
	[TypeConverter(typeof(DataSet.Converter))]
	public class DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/>.
		/// </summary>
		/// <remarks>
		/// The new data set has the <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Label"/> set to "Data Set" and the
		/// <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Type"/> set to <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Line"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new DataSet
		/// {
		/// 	Label = "Visitors",
		/// 	Data = new object[] { 10, 20, 15 }
		/// };
		/// this.chartJS1.DataSets.Add(dataSet);
		/// ]]></code>
		/// </example>
		public DataSet()
		{
			this.Label = "Data Set";
			this.Type = ChartType.Line;
		}

		/// <summary>
		/// Returns or sets the label for the data set which appears in the legend and tooltips.
		/// </summary>
		/// <value>
		/// The text of the label. The constructor initializes it to "Data Set"; data sets created with
		/// <see cref="M:Wisej.Web.Ext.ChartJS.DataSetCollection.Add(System.String)"/> use the specified name.
		/// </value>
		/// <remarks>
		/// The label is also used by the <see cref="T:Wisej.Web.Ext.ChartJS.DataSetCollection"/> string indexer to find a data set.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = this.chartJS1.DataSets.Add("Revenue");
		///
		/// // rename the data set, the legend shows the new label.
		/// dataSet.Label = "Revenue (USD)";
		/// this.chartJS1.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		[Description("The label for the dataset which appears in the legend and tooltips.")]
		public string Label
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets whether the data set is hidden.
		/// </summary>
		/// <value>
		/// True to hide the data set; otherwise false. The default is false.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hidden" property. A hidden data set is not plotted but its entry
		/// is still shown (crossed out) in the legend.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// private void checkBoxShowCosts_CheckedChanged(object sender, EventArgs e)
		/// {
		/// 	var costs = this.chartJS1.DataSets["Costs"];
		/// 	if (costs != null)
		/// 	{
		/// 		costs.Hidden = !this.checkBoxShowCosts.Checked;
		/// 		this.chartJS1.Update();
		/// 	}
		/// }
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("Hides the dataset.")]
		public bool Hidden
		{
			get;
			set;
		}

		/// <summary>
		/// Returns the type of chart that plots this type of <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/>.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS.ChartType"/> values. The value is set by the constructor of the
		/// specialized data set class and cannot be changed from outside the class.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// foreach (var dataSet in this.chartJS1.DataSets)
		/// {
		/// 	if (dataSet.Type == ChartType.Line)
		/// 		((LineDataSet)dataSet).Fill = true;
		/// }
		/// this.chartJS1.Update();
		/// ]]></code>
		/// </example>
		[Description("Returns the type of chart that plots this data set.")]
		public ChartType Type
		{
			get;
			protected set;
		}

		/// <summary>
		/// Returns or sets the data to plot.
		/// </summary>
		/// <value>
		/// An array of values, one for each entry in <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Labels"/>. The default is null.
		/// </value>
		/// <remarks>
		/// The array is serialized to the client as the Chart.js dataset "data" property. It usually contains numbers but may
		/// also contain objects, for example objects with x and y properties for scatter charts or x, y and r for bubble charts.
		/// Call <see cref="M:Wisej.Web.Ext.ChartJS.ChartJS.UpdateData(System.Int32)"/> after changing the data to animate
		/// the chart to the new values.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var temperatures = this.chartJS1.DataSets.Add("Temperature");
		/// temperatures.Data = new object[] { 12.5, 14.1, 17.8, 21.3, 19.0 };
		///
		/// // later: replace the values and animate the transition.
		/// temperatures.Data = new object[] { 13.0, 15.2, 18.4, 22.0, 20.1 };
		/// this.chartJS1.UpdateData(500);
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public object[] Data
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the formatted representation of the data to plot, displayed when <see cref="P:Wisej.Web.Ext.ChartJS.OptionsDataLabel.Display"/> is true.
		/// </summary>
		/// <value>
		/// An array of strings matching the items in <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Data"/> by position, or null.
		/// </value>
		/// <remarks>
		/// When set, the client uses the formatted string at the same index of the data point for the data labels
		/// and the tooltips. When the array is null or doesn't contain an entry for a data point, the raw value is shown.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var sales = this.chartJS1.DataSets.Add("Sales");
		/// sales.Data = new object[] { 1200.5, 980.25, 1430 };
		/// sales.Formatted = new[] { "$1,200.50", "$980.25", "$1,430.00" };
		///
		/// this.chartJS1.Options.DataLabel.Display = true;
		/// ]]></code>
		/// </example>
		public string[] Formatted
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the fill color of the data set. What it fills is up to the chart type.
		/// </summary>
		/// <value>
		/// The fill <see cref="T:System.Drawing.Color"/>. The default is <see cref="F:System.Drawing.Color.Empty"/>, which uses the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "backgroundColor" property. Some specialized data sets (bar, pie, doughnut and polar area)
		/// hide this property with an array version that allows a different color for each data point.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// DataSet dataSet = this.chartJS1.DataSets.Add("Orders");
		/// dataSet.BackgroundColor = Color.FromArgb(80, Color.SteelBlue);
		/// dataSet.BorderColor = Color.SteelBlue;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[Description("The fill color of the data set. What it fills is up to the chart type.")]
		public Color BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border color of the data set. What it colors is up to the chart type.
		/// </summary>
		/// <value>
		/// The border <see cref="T:System.Drawing.Color"/>. The default is <see cref="F:System.Drawing.Color.Empty"/>, which uses the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "borderColor" property. Some specialized data sets hide this property with
		/// their own version, for example <see cref="P:Wisej.Web.Ext.ChartJS.LineDataSet.BorderColor"/> or the array version in
		/// <see cref="P:Wisej.Web.Ext.ChartJS.BarDataSet.BorderColor"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = this.chartJS1.DataSets.Add("Signups");
		/// dataSet.BorderColor = Color.DarkOrange;
		/// dataSet.BorderWidth = 2;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[Description("The border color of the data set. What it fills is up to the chart type.")]
		public Color BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the width of the border in pixels.
		/// </summary>
		/// <value>
		/// The width of the border in pixels. The default is 0.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "borderWidth" property. For line charts it is the width of the line.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var trend = (LineDataSet)this.chartJS1.DataSets.Add("Trend");
		/// trend.BorderColor = Color.Green;
		/// trend.BorderWidth = 3;
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
		[Description("The width of the border in pixels.")]
		public int BorderWidth
		{
			get;
			set;
		}

		/// <summary>
		/// Binds the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to the specified y axis.
		/// </summary>
		/// <value>
		/// The <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Id"/> of the y axis, or null to use the first y axis. The default is null.
		/// </value>
		/// <remarks>
		/// Use this property with multi-axes charts: the value must match the <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Id"/>
		/// of one of the axes in <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScales.yAxes"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var yAxes = this.chartJS1.Options.Scales.yAxes;
		/// yAxes[0].Id = "left-axis";
		///
		/// var revenue = this.chartJS1.DataSets.Add("Revenue");
		/// revenue.yAxisID = "left-axis";
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Binds the dataset to the specified y axis.")]
		public string yAxisID
		{
			get;
			set;
		}

		/// <summary>
		/// Binds the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to the specified x axis.
		/// </summary>
		/// <value>
		/// The <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Id"/> of the x axis, or null to use the first x axis. The default is null.
		/// </value>
		/// <remarks>
		/// Use this property with multi-axes charts: the value must match the <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Id"/>
		/// of one of the axes in <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScales.xAxes"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.Scales.xAxes[0].Id = "time-axis";
		///
		/// var events = this.chartJS1.DataSets.Add("Events");
		/// events.xAxisID = "time-axis";
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Binds the dataset to the specified x axis.")]
		public string xAxisID
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the drawing order of the data set. Also affects the order for stacking, tooltip, and legend.
		/// </summary>
		/// <value>
		/// An integer that defines the order of the data set. The default is 0.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "order" property. Data sets with a lower value are drawn on top of data sets with a higher value.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var actual = this.chartJS1.DataSets.Add("Actual");
		/// var target = this.chartJS1.DataSets.Add("Target");
		///
		/// // draw "Actual" above "Target".
		/// actual.Order = 1;
		/// target.Order = 2;
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
		[Description("The drawing order of the dataset. Also affects order for stacking, tooltip, and legend.")]
		public int Order
		{
			get;
			set;
		}

		// Initializes this data set copying the value from another data set.
		internal virtual void CopyFrom(DataSet source)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			Type targetType = GetType();
			Type sourceType = source.GetType();

			var sourceProperties = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
			var targetProperties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
			foreach (PropertyInfo sourceProperty  in sourceProperties)
			{
				PropertyInfo targetProperty = Array.Find(targetProperties, (p) => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);
				if (targetProperty == null)
					continue;

				try
				{
					if (targetProperty.GetSetMethod() != null && sourceProperty.GetSetMethod() != null)
					{
						targetProperty.SetValue(this, sourceProperty.GetValue(source));
					}
				}
				catch { }
			}
		}

		#region Converter

		internal class Converter : System.ComponentModel.TypeConverter
		{
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					DataSet dataSet = (DataSet)value;
					if (dataSet != null && !String.IsNullOrEmpty(dataSet.Label))
						return dataSet.Label;
				}

				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		#endregion
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Line"/> chart.
	/// </summary>
	/// <remarks>
	/// <see cref="M:Wisej.Web.Ext.ChartJS.DataSetCollection.Add(System.String)"/> creates a <see cref="T:Wisej.Web.Ext.ChartJS.LineDataSet"/>
	/// when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Line"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Line;
	/// this.chartJS1.Labels = new[] { "Mon", "Tue", "Wed", "Thu", "Fri" };
	///
	/// var visits = (LineDataSet)this.chartJS1.DataSets.Add("Visits");
	/// visits.Data = new object[] { 320, 410, 380, 450, 500 };
	/// visits.BorderColor = Color.RoyalBlue;
	/// visits.LineTension = 0;
	/// ]]></code>
	/// </example>
	public class LineDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.LineDataSet"/>.
		/// </summary>
		/// <remarks>
		/// The new data set is not filled, shows the line, doesn't span gaps, isn't stepped, uses
		/// <see cref="F:Wisej.Web.Ext.ChartJS.PointStyle.Circle"/> points with a radius and hover radius of 5 pixels, and
		/// has a <see cref="P:Wisej.Web.Ext.ChartJS.LineDataSet.LineTension"/> of 0.4.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var forecast = new LineDataSet
		/// {
		/// 	Label = "Forecast",
		/// 	BorderDash = new[] { 5, 5 },
		/// 	Data = new object[] { 100, 110, 125 }
		/// };
		/// this.chartJS1.DataSets.Add(forecast);
		/// ]]></code>
		/// </example>
		public LineDataSet() : base()
		{
			this.Fill = false;
			this.ShowLine = true;
			this.SpanGaps = false;
			this.PointRadius = new int[] { 5 };
			this.PointHoverRadius = new int[] { 5 };
			this.Type = ChartType.Line;
			this.SteppedLine = SteppedLine.False;
			this.PointStyle = new PointStyle[] { Wisej.Web.Ext.ChartJS.PointStyle.Circle };
		}

		/// <summary>
		/// Returns or sets the color of the line.
		/// </summary>
		/// <value>
		/// The <see cref="T:System.Drawing.Color"/> of the line. The default is <see cref="F:System.Drawing.Color.Empty"/>, which uses the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.BorderColor"/> and maps to the Chart.js dataset "borderColor" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var profit = (LineDataSet)this.chartJS1.DataSets.Add("Profit");
		/// profit.BorderColor = Color.SeaGreen;
		/// profit.BorderWidth = 2;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[Description("The color of the line.")]
		public new Color BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the length and spacing of dashes. See <see href="https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/setLineDash"/>.
		/// </summary>
		/// <value>
		/// An array of numbers that alternately specify the lengths of the dashes and of the gaps in pixels, or null for a solid line.
		/// The default is null.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var budget = (LineDataSet)this.chartJS1.DataSets.Add("Budget");
		///
		/// // 10px dash followed by a 5px gap.
		/// budget.BorderDash = new[] { 10, 5 };
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Length and spacing of dashes.")]
		public int[] BorderDash
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets whether to fill the area under the line.
		/// </summary>
		/// <value>
		/// True to fill the area under the line using <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.BackgroundColor"/>; otherwise false.
		/// The default is false.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var usage = (LineDataSet)this.chartJS1.DataSets.Add("CPU Usage");
		/// usage.Fill = true;
		/// usage.BackgroundColor = Color.FromArgb(60, Color.OrangeRed);
		/// usage.BorderColor = Color.OrangeRed;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("If true, fill the area under the line.")]
		public bool Fill
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets whether the line between the points is drawn.
		/// </summary>
		/// <value>
		/// False to draw only the points; otherwise true. The default is true.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// // show only the points, like a scatter plot.
		/// var samples = (LineDataSet)this.chartJS1.DataSets.Add("Samples");
		/// samples.ShowLine = false;
		/// samples.PointRadius = new[] { 4 };
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("If false lines between points are not drawn.")]
		public bool ShowLine
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets whether lines are drawn between points with no or null data.
		/// </summary>
		/// <value>
		/// True to draw lines across points with no or null data; false to break the line at those points. The default is false.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var readings = (LineDataSet)this.chartJS1.DataSets.Add("Readings");
		/// readings.Data = new object[] { 5, 7, null, 6, 8 };
		///
		/// // connect 7 and 6 across the missing value.
		/// readings.SpanGaps = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("If true, lines will be drawn between points with no or null data. If false, points with NaN data will create a break in the line.")]
		public bool SpanGaps
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the style of the stepped line.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS.SteppedLine"/> values. The default is <see cref="F:Wisej.Web.Ext.ChartJS.SteppedLine.False"/>.
		/// </value>
		/// <remarks>
		/// When set to a value other than <see cref="F:Wisej.Web.Ext.ChartJS.SteppedLine.False"/>, the line is drawn as steps
		/// and <see cref="P:Wisej.Web.Ext.ChartJS.LineDataSet.LineTension"/> is ignored.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var price = (LineDataSet)this.chartJS1.DataSets.Add("Price");
		/// price.Data = new object[] { 9.99, 9.99, 12.49, 12.49, 11.99 };
		/// price.SteppedLine = SteppedLine.After;
		/// ]]></code>
		/// </example>
		[DefaultValue(SteppedLine.False)]
		[Description("Show a stepped line rather than a curve.")]
		public SteppedLine SteppedLine
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the style of the points on the line. One entry is the default for all points, otherwise each point can define a style.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:Wisej.Web.Ext.ChartJS.PointStyle"/> values. The default is a single <see cref="F:Wisej.Web.Ext.ChartJS.PointStyle.Circle"/>.
		/// </value>
		/// <remarks>
		/// When the array contains a single entry, it's sent to Chart.js as a single value applied to all the points.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (LineDataSet)this.chartJS1.DataSets.Add("Milestones");
		/// ds.Data = new object[] { 1, 3, 2 };
		///
		/// // a different style for each point.
		/// ds.PointStyle = new[] { PointStyle.Circle, PointStyle.Star, PointStyle.Triangle };
		/// ]]></code>
		/// </example>
		[DefaultValue(new PointStyle[] { Wisej.Web.Ext.ChartJS.PointStyle.Circle })]
		[MergableProperty(false)]
		[Description("The style of a point on the line. One entry is the default for all points, otherwise each point can define a style.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public PointStyle[] PointStyle
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the radius of the point shape. If set to 0, nothing is rendered. One entry is the default radius for all points, otherwise each point can define a radius.
		/// </summary>
		/// <value>
		/// An array of radius values in pixels. The default is a single entry with the value 5.
		/// </value>
		/// <remarks>
		/// When the array contains a single entry, it's sent to Chart.js as a single value applied to all the points.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = (LineDataSet)this.chartJS1.DataSets.Add("Throughput");
		///
		/// // hide the points and show only the line.
		/// line.PointRadius = new[] { 0 };
		/// ]]></code>
		/// </example>
		[DefaultValue(new int[] { 5 })]
		[MergableProperty(false)]
		[Description("The radius of the point shape. If set to 0, nothing is rendered. One entry is the default radius for all points, otherwise each point can define a radius.")]
		public int[] PointRadius
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the radius of the point when hovered. One entry is the default hover radius for all points, otherwise each point can define a hover radius.
		/// </summary>
		/// <value>
		/// An array of radius values in pixels. The default is a single entry with the value 5.
		/// </value>
		/// <remarks>
		/// When the array contains a single entry, it's sent to Chart.js as a single value applied to all the points.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (LineDataSet)this.chartJS1.DataSets.Add("Latency");
		/// ds.PointRadius = new[] { 3 };
		/// ds.PointHoverRadius = new[] { 8 };
		/// ]]></code>
		/// </example>
		[DefaultValue(new int[] { 5 })]
		[MergableProperty(false)]
		[Description("The radius of the point when hovered")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public int[] PointHoverRadius
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the fill colors of the points in the data set. One entry is the default color for all the points, otherwise
		/// each point can be defined as a different color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// When the array contains a single color, it's sent to Chart.js as a single value applied to all the points.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (LineDataSet)this.chartJS1.DataSets.Add("Score");
		/// ds.Data = new object[] { 72, 45, 88 };
		///
		/// // highlight the low score in red.
		/// ds.PointBackgroundColor = new[] { Color.Green, Color.Red, Color.Green };
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the points for the data set. One entry is the default color for all the points, otherwise each point can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] PointBackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border colors of the points in the data set. One entry is the default color for all the points, otherwise
		/// each point can be defined as a different color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// When the array contains a single color, it's sent to Chart.js as a single value applied to all the points.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (LineDataSet)this.chartJS1.DataSets.Add("Orders");
		/// ds.PointBackgroundColor = new[] { Color.White };
		/// ds.PointBorderColor = new[] { Color.Navy };
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the points for the data set. One entry is the default color for all the points, otherwise each point can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] PointBorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the Bezier curve tension of the line. Set to 0 to draw straight lines. This option is ignored if monotone cubic interpolation is used.
		/// </summary>
		/// <value>
		/// The tension of the curve. The default is 0.4.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (LineDataSet)this.chartJS1.DataSets.Add("Balance");
		///
		/// // straight segments between the points.
		/// ds.LineTension = 0;
		/// ]]></code>
		/// </example>
		[DefaultValue(0.4)]
		[MergableProperty(false)]
		[Description("Bezier curve tension of the line. Set to 0 to draw straight lines. This option is ignored if monotone cubic interpolation is used.")]
		public double LineTension 
		{ 
			get; 
			set; 
		} = 0.4;
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bar"/> chart.
	/// </summary>
	/// <remarks>
	/// <see cref="M:Wisej.Web.Ext.ChartJS.DataSetCollection.Add(System.String)"/> creates a <see cref="T:Wisej.Web.Ext.ChartJS.BarDataSet"/>
	/// when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bar"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Bar;
	/// this.chartJS1.Labels = new[] { "North", "South", "East", "West" };
	///
	/// var units = (BarDataSet)this.chartJS1.DataSets.Add("Units Sold");
	/// units.Data = new object[] { 340, 210, 480, 150 };
	/// units.BackgroundColor = new[] { Color.CornflowerBlue };
	/// ]]></code>
	/// </example>
	public class BarDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.BarDataSet"/>.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Type"/> to <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bar"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var returns = new BarDataSet
		/// {
		/// 	Label = "Returns",
		/// 	Data = new object[] { 12, 8, 15, 4 }
		/// };
		/// this.chartJS1.DataSets.Add(returns);
		/// ]]></code>
		/// </example>
		public BarDataSet() : base()
		{
			this.Type = ChartType.Bar;
		}

		/// <summary>
		/// Returns or sets the fill colors of the data set. One entry is the default color for all the bars, otherwise
		/// each bar can define a different color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.BackgroundColor"/>. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the bars.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (BarDataSet)this.chartJS1.DataSets.Add("Stock");
		/// ds.Data = new object[] { 50, 5, 30 };
		/// ds.BackgroundColor = new[] { Color.LightGreen, Color.Salmon, Color.LightGreen };
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the bars, otherwise each bar can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border colors of the data set. One entry is the default color for all the bars, otherwise
		/// each bar can define a different border color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.BorderColor"/>. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the bars. Set <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.BorderWidth"/>
		/// to a value greater than 0 to show the border.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (BarDataSet)this.chartJS1.DataSets.Add("Hours");
		/// ds.BackgroundColor = new[] { Color.FromArgb(100, Color.Purple) };
		/// ds.BorderColor = new[] { Color.Purple };
		/// ds.BorderWidth = 1;
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the bars, otherwise each bar can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the hover colors of the data set. One entry is the default color for all the bars, otherwise
		/// each bar can define a different hover background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hoverBackgroundColor" property. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the bars.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (BarDataSet)this.chartJS1.DataSets.Add("Downloads");
		/// ds.BackgroundColor = new[] { Color.SkyBlue };
		/// ds.HoverBackgroundColor = new[] { Color.DodgerBlue };
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.HorizontalBar"/> chart.
	/// </summary>
	/// <remarks>
	/// Inherits all the properties of <see cref="T:Wisej.Web.Ext.ChartJS.BarDataSet"/>. The bars are drawn horizontally.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.HorizontalBar;
	/// this.chartJS1.Labels = new[] { "Chrome", "Firefox", "Safari", "Edge" };
	///
	/// var share = (HorizontalBarDataSet)this.chartJS1.DataSets.Add("Browser Share");
	/// share.Data = new object[] { 64, 8, 19, 5 };
	/// share.BackgroundColor = new[] { Color.Teal };
	/// ]]></code>
	/// </example>
	public class HorizontalBarDataSet : BarDataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.HorizontalBarDataSet"/>.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Type"/> to <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.HorizontalBar"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.ChartType = ChartType.HorizontalBar;
		/// this.chartJS1.DataSets.Add(new HorizontalBarDataSet
		/// {
		/// 	Label = "Tasks",
		/// 	Data = new object[] { 7, 3, 12 }
		/// });
		/// ]]></code>
		/// </example>
		public HorizontalBarDataSet() : base()
		{
			this.Type = ChartType.HorizontalBar;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bubble"/> chart.
	/// </summary>
	/// <remarks>
	/// Each item in <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Data"/> should be an object with the x, y and r
	/// (bubble radius in pixels) values.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Bubble;
	///
	/// var cities = new BubbleDataSet { Label = "Cities" };
	/// cities.Data = new object[]
	/// {
	/// 	new { x = 10, y = 20, r = 15 },
	/// 	new { x = 25, y = 12, r = 8 }
	/// };
	/// this.chartJS1.DataSets.Add(cities);
	/// ]]></code>
	/// </example>
	public class BubbleDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.BubbleDataSet"/>.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Type"/> to <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bubble"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = new BubbleDataSet();
		/// ds.Label = "Projects";
		/// ds.BackgroundColor = Color.FromArgb(120, Color.Gold);
		/// ds.Data = new object[] { new { x = 3, y = 7, r = 10 } };
		/// this.chartJS1.DataSets.Add(ds);
		/// ]]></code>
		/// </example>
		public BubbleDataSet() : base()
		{
			this.Type = ChartType.Bubble;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Doughnut"/> chart.
	/// </summary>
	/// <remarks>
	/// <see cref="M:Wisej.Web.Ext.ChartJS.DataSetCollection.Add(System.String)"/> creates a <see cref="T:Wisej.Web.Ext.ChartJS.DoughnutDataSet"/>
	/// when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Doughnut"/>.
	/// Each value in <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Data"/> is a segment, usually with its own color.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Doughnut;
	/// this.chartJS1.Labels = new[] { "Desktop", "Mobile", "Tablet" };
	///
	/// var ds = (DoughnutDataSet)this.chartJS1.DataSets.Add("Devices");
	/// ds.Data = new object[] { 55, 35, 10 };
	/// ds.BackgroundColor = new[] { Color.Tomato, Color.Gold, Color.MediumSeaGreen };
	/// ]]></code>
	/// </example>
	public class DoughnutDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.DoughnutDataSet"/>.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Type"/> to <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Doughnut"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.ChartType = ChartType.Doughnut;
		/// this.chartJS1.DataSets.Add(new DoughnutDataSet
		/// {
		/// 	Label = "Devices",
		/// 	Data = new object[] { 55, 35, 10 }
		/// });
		/// ]]></code>
		/// </example>
		public DoughnutDataSet() : base()
		{
			this.Type = ChartType.Doughnut;
		}

		/// <summary>
		/// Returns or sets the fill colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, usually one for each value in <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Data"/>,
		/// or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.BackgroundColor"/>. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the slices.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (DoughnutDataSet)this.chartJS1.DataSets.Add("Tickets");
		/// ds.Data = new object[] { 30, 12, 5 };
		/// ds.BackgroundColor = new[] { Color.SteelBlue, Color.Orange, Color.Crimson };
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different border color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.BorderColor"/>. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the slices.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (DoughnutDataSet)this.chartJS1.DataSets.Add("Storage");
		///
		/// // white separators between the slices.
		/// ds.BorderColor = new[] { Color.White };
		/// ds.BorderWidth = 2;
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the hover colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different hover background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hoverBackgroundColor" property. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the slices.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (DoughnutDataSet)this.chartJS1.DataSets.Add("Status");
		/// ds.BackgroundColor = new[] { Color.LightGreen, Color.LightCoral };
		/// ds.HoverBackgroundColor = new[] { Color.Green, Color.Red };
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Pie"/> chart.
	/// </summary>
	/// <remarks>
	/// <see cref="M:Wisej.Web.Ext.ChartJS.DataSetCollection.Add(System.String)"/> creates a <see cref="T:Wisej.Web.Ext.ChartJS.PieDataSet"/>
	/// when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Pie"/>.
	/// Each value in <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Data"/> is a segment, usually with its own color.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Pie;
	/// this.chartJS1.Labels = new[] { "Rent", "Food", "Other" };
	///
	/// var ds = (PieDataSet)this.chartJS1.DataSets.Add("Expenses");
	/// ds.Data = new object[] { 1200, 450, 300 };
	/// ds.BackgroundColor = new[] { Color.Tomato, Color.Gold, Color.MediumSeaGreen };
	/// ]]></code>
	/// </example>
	public class PieDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.PieDataSet"/>.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Type"/> to <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Pie"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.ChartType = ChartType.Pie;
		/// this.chartJS1.DataSets.Add(new PieDataSet
		/// {
		/// 	Label = "Expenses",
		/// 	Data = new object[] { 1200, 450, 300 }
		/// });
		/// ]]></code>
		/// </example>
		public PieDataSet() : base()
		{
			this.Type = ChartType.Pie;
		}

		/// <summary>
		/// Returns or sets the fill colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, usually one for each value in <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Data"/>,
		/// or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.BackgroundColor"/>. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the slices.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (PieDataSet)this.chartJS1.DataSets.Add("Market Share");
		/// ds.Data = new object[] { 45, 30, 25 };
		/// ds.BackgroundColor = new[] { Color.MediumPurple, Color.Goldenrod, Color.CadetBlue };
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different border color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.BorderColor"/>. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the slices.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (PieDataSet)this.chartJS1.DataSets.Add("Votes");
		/// ds.BorderColor = new[] { Color.Black };
		/// ds.BorderWidth = 1;
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the hover colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different hover background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hoverBackgroundColor" property. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the slices.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (PieDataSet)this.chartJS1.DataSets["Votes"];
		/// if (ds != null)
		/// {
		/// 	ds.HoverBackgroundColor = new[] { Color.DarkSlateGray };
		/// 	this.chartJS1.Update();
		/// }
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.PolarArea"/> chart.
	/// </summary>
	/// <remarks>
	/// <see cref="M:Wisej.Web.Ext.ChartJS.DataSetCollection.Add(System.String)"/> creates a <see cref="T:Wisej.Web.Ext.ChartJS.PolarAreaDataSet"/>
	/// when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.PolarArea"/>.
	/// Each value in <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Data"/> is a segment, usually with its own color.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.PolarArea;
	/// this.chartJS1.Labels = new[] { "Speed", "Power", "Range" };
	///
	/// var ds = (PolarAreaDataSet)this.chartJS1.DataSets.Add("Ratings");
	/// ds.Data = new object[] { 8, 6, 9 };
	/// ds.BackgroundColor = new[] { Color.Tomato, Color.Gold, Color.MediumSeaGreen };
	/// ]]></code>
	/// </example>
	public class PolarAreaDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.PolarAreaDataSet"/>.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Type"/> to <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.PolarArea"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.ChartType = ChartType.PolarArea;
		/// this.chartJS1.DataSets.Add(new PolarAreaDataSet
		/// {
		/// 	Label = "Ratings",
		/// 	Data = new object[] { 8, 6, 9 }
		/// });
		/// ]]></code>
		/// </example>
		public PolarAreaDataSet() : base()
		{
			this.Type = ChartType.PolarArea;
		}

		/// <summary>
		/// Returns or sets the fill colors of the data set. One entry is the default color for all the segments, otherwise
		/// each segment can define a background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, usually one for each value in <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Data"/>,
		/// or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.BackgroundColor"/>. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the segments.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (PolarAreaDataSet)this.chartJS1.DataSets.Add("Skills");
		/// ds.Data = new object[] { 7, 9, 5, 6 };
		/// ds.BackgroundColor = new[]
		/// {
		/// 	Color.FromArgb(150, Color.Red), Color.FromArgb(150, Color.Blue),
		/// 	Color.FromArgb(150, Color.Green), Color.FromArgb(150, Color.Orange)
		/// };
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border colors of the data set. One entry is the default color for all the segments, otherwise
		/// each segment can define a different border color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.BorderColor"/>. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the segments.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (PolarAreaDataSet)this.chartJS1.DataSets.Add("Wind");
		/// ds.BorderColor = new[] { Color.DimGray };
		/// ds.BorderWidth = 1;
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the hover colors of the data set. One entry is the default color for all the segments, otherwise
		/// each segment can define a different hover background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hoverBackgroundColor" property. When the array contains a single color,
		/// it's sent to Chart.js as a single value applied to all the segments.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (PolarAreaDataSet)this.chartJS1.DataSets.Add("Load");
		/// ds.HoverBackgroundColor = new[] { Color.FromArgb(220, Color.Orange) };
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Radar"/> chart.
	/// </summary>
	/// <remarks>
	/// <see cref="M:Wisej.Web.Ext.ChartJS.DataSetCollection.Add(System.String)"/> creates a <see cref="T:Wisej.Web.Ext.ChartJS.RadarDataSet"/>
	/// when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Radar"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Radar;
	/// this.chartJS1.Labels = new[] { "Speed", "Comfort", "Safety", "Price", "Design" };
	///
	/// var carA = (RadarDataSet)this.chartJS1.DataSets.Add("Car A");
	/// carA.Data = new object[] { 8, 6, 9, 5, 7 };
	/// carA.BorderColor = Color.DarkCyan;
	/// ]]></code>
	/// </example>
	public class RadarDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.RadarDataSet"/>.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Type"/> to <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Radar"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.ChartType = ChartType.Radar;
		/// this.chartJS1.DataSets.Add(new RadarDataSet
		/// {
		/// 	Label = "Team B",
		/// 	Data = new object[] { 5, 7, 6, 8, 4 }
		/// });
		/// ]]></code>
		/// </example>
		public RadarDataSet() : base()
		{
			this.Type = ChartType.Radar;
		}

		/// <summary>
		/// Returns or sets the Bezier curve tension of the line. Set to 0 to draw straight lines.
		/// </summary>
		/// <value>
		/// The tension of the curve. The default is 0.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var ds = (RadarDataSet)this.chartJS1.DataSets.Add("Profile");
		///
		/// // slightly rounded edges.
		/// ds.LineTension = 0.2;
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
		[MergableProperty(false)]
		[Description("Bezier curve tension of the line. Set to 0 to draw straight lines.")]
		public double LineTension
		{
			get;
			set;
		}
	}
}
