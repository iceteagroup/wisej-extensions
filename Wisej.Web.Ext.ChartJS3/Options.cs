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

using System;
using System.ComponentModel;
using System.Globalization;

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Base class for all the option classes.
	/// </summary>
	/// <remarks>
	/// Every option set belongs either directly to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control or to a parent option set.
	/// Changing a value property of an option set calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>, which updates the owner
	/// <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control. An option set instance cannot be assigned to two different owners.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// // all option classes derive from OptionsBase.
	/// OptionsBase title = this.chartJS31.Options.Plugins.Title;
	/// OptionsBase legend = this.chartJS31.Options.Plugins.Legend;
	///
	/// // force a refresh of the chart.
	/// title.Update();
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS3")]
	[TypeConverter(typeof(OptionsBase.Converter))]
	[Editor("Wisej.Web.Ext.ChartJS3.Design.OptionsEditor", 
			"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class OptionsBase : ICloneable
	{
		#region Properties

		// the owner Options instance.
		internal OptionsBase Owner
		{
			get { return this._owner; }
			set
			{
				if (this._owner != value)
				{
					// cannot assign a set of options to two different ChartJS controls.
					if (this._owner != null)
						throw new InvalidOperationException("The " + value.GetType().Name + " instance belongs to another ChartJS control.");

					this._owner = value;
				}
			}
		}
		private OptionsBase _owner;

		// the owner ChartJS control.
		internal ChartJS3 Chart
		{
			get
			{
				if (this._chart != null)
					return this._chart;

				if (this.Owner != null)
					return this.Owner.Chart;

				return null;
			}
			set
			{
				if (this._chart != value)
				{
					// cannot assign a set of options to two different ChartJS controls.
					if (this._chart != null)
						throw new InvalidOperationException("The " + value.GetType().Name + " instance belongs to another ChartJS control.");

					this._chart = value;
				}
			}
		}
		private ChartJS3 _chart;

		#endregion

		#region Methods

		/// <summary>
		/// Updates the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control using this set of options.
		/// </summary>
		/// <remarks>
		/// The call is ignored when this option set is not attached to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control, either
		/// directly or through its owner option set. It is called automatically when a value property of the option set changes, but not when
		/// a nested option set (for example <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/> or <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/>) is replaced.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = this.chartJS31.Options;
		/// options.Plugins.Title = new OptionsTitle { Text = "Monthly Sales" };
		///
		/// // assigning a nested option set doesn't refresh the chart.
		/// options.Update();
		/// ]]></code>
		/// </example>
		public void Update()
		{
			var chart = this.Chart;
			if (chart != null)
				chart.Update();
		}

		// Clones this instance and all its children.
		internal OptionsBase Clone()
		{
			OptionsBase options = (OptionsBase)Activator.CreateInstance(this.GetType());
			options.CopyFrom(this);
			options._chart = this._chart;

			return options;
		}

		// Initializes this option set copying the value from another option set.
		internal virtual void CopyFrom(OptionsBase source)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			// copy only the shared base properties.
			var targetProperties = TypeDescriptor.GetProperties(this);
			var sourceProperties = TypeDescriptor.GetProperties(source);
			foreach (PropertyDescriptor pSource in sourceProperties)
			{
				var pTarget = targetProperties[pSource.Name];
				if (pTarget == null || pTarget.PropertyType != pSource.PropertyType)
					continue;

				try
				{
					// go deep for nested options.
					if (pSource.PropertyType.IsSubclassOf(typeof(OptionsBase)))
					{
						// create a new instance of the child options member.
						OptionsBase options = (OptionsBase)Activator.CreateInstance(pSource.PropertyType, this);
						options.CopyFrom((OptionsBase)pSource.GetValue(source));
						pTarget.SetValue(this, options);
					}
					else
					{
						if (pSource.ShouldSerializeValue(source))
							pTarget.SetValue(this, pSource.GetValue(source));
					}
				}
				catch { }
			}
		}

		/// <summary>
		/// Compares two Options instances.
		/// </summary>
		/// <param name="obj">The object to compare with this option set.</param>
		/// <returns>true if <paramref name="obj"/> is of the same type as this instance and all its properties have equal values; otherwise, false.</returns>
		/// <remarks>
		/// Nested option sets are compared using the same rule.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip1 = this.chartJS31.Options.Plugins.Tooltip;
		/// var tooltip2 = this.chartJS32.Options.Plugins.Tooltip;
		///
		/// if (!tooltip1.Equals(tooltip2))
		/// 	tooltip2.Enabled = tooltip1.Enabled;
		/// ]]></code>
		/// </example>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			var type = obj.GetType();
			if (type != this.GetType())
				return false;

			var properties = TypeDescriptor.GetProperties(this);
			foreach (PropertyDescriptor p in properties)
			{
				if (!Object.Equals(p.GetValue(this), p.GetValue(obj)))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		/// <remarks>
		/// The hash code is calculated from the JSON serialization of the option set, therefore it changes when the property values change.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = this.chartJS31.Options.Plugins.Legend;
		/// int hash = legend.GetHashCode();
		/// legend.Position = HeaderPosition.Bottom;
		///
		/// // the hash code is different now.
		/// bool changed = hash != legend.GetHashCode();
		/// ]]></code>
		/// </example>
		public override int GetHashCode()
		{
			return this.ToJSON().GetHashCode();
		}

		#endregion

		#region Converter

		internal class Converter : System.ComponentModel.ExpandableObjectConverter
		{
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
					return "(...)";

				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		#endregion

		#region ICloneable

		object ICloneable.Clone()
		{
			return Clone();
		}

		#endregion

	}

	/// <summary>
	/// Represents the base options for the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> widget.
	/// Different <see cref="T:Wisej.Web.Ext.ChartJS3.ChartType"/> values extend this class with type specific options.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property returns the instance of the class that matches the current
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/>: <see cref="T:Wisej.Web.Ext.ChartJS3.LineOptions"/>, <see cref="T:Wisej.Web.Ext.ChartJS3.BarOptions"/> (also used for
	/// <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.HorizontalBar"/>), <see cref="T:Wisej.Web.Ext.ChartJS3.PieOptions"/>, <see cref="T:Wisej.Web.Ext.ChartJS3.PolarAreaOptions"/>, <see cref="T:Wisej.Web.Ext.ChartJS3.DoughnutOptions"/>,
	/// <see cref="T:Wisej.Web.Ext.ChartJS3.RadarOptions"/>, <see cref="T:Wisej.Web.Ext.ChartJS3.BubbleOptions"/> or <see cref="T:Wisej.Web.Ext.ChartJS3.ScatterOptions"/>.
	/// Cast it to the specific class to access the type specific options.
	/// In Chart.js 3 the title, legend, tooltip and data labels options are grouped under <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// Options options = this.chartJS31.Options;
	/// options.Plugins.Title.Text = "Revenue";
	/// options.Plugins.Legend.Display = false;
	///
	/// if (options is LineOptions)
	/// 	((LineOptions)options).ShowLines = true;
	/// ]]></code>
	/// </example>
	public abstract class Options : OptionsBase
	{

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Used by derived classes to create an option set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // derived classes call the default constructor.
		/// Options options = new LineOptions();
		/// options.Plugins.Title.Text = "Temperatures";
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public Options()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.Options"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it are copied into the new instance.
		/// Nested option sets are copied into new instances, other properties are copied only when they have a non-default value.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // derived classes pass the arguments to the base constructor.
		/// var options = new BarOptions(this.chartJS31, this.chartJS31.Options);
		/// options.IndexAxis = "y";
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public Options(ChartJS3 chart, Options defaults)
		{
			this.Chart = chart;

			if (defaults != null)
				CopyFrom(defaults);
		}

		#region Properties

		/// <summary>
		/// Options for the chart scales.
		/// </summary>
		/// <value>An <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsScales"/> instance. It is created automatically the first time the property is read.</value>
		/// <remarks>
		/// Serialized as the <c>scales</c> option of the chart. The axes defined in <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.xAxes"/> and
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.yAxes"/> are converted on the client into the Chart.js 3 <c>scales</c> object, using the keys
		/// <c>x0</c>, <c>x1</c>, ... and <c>y0</c>, <c>y1</c>, ...
		/// Assigning a new instance doesn't update the chart, call <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsScales"/> instance assigned already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var yAxis = this.chartJS31.Options.Scales.yAxes[0];
		/// yAxis.Ticks.BeginAtZero = true;
		/// yAxis.SuggestedMax = 100;
		/// yAxis.Title.Display = true;
		/// yAxis.Title.Text = "Percent";
		/// ]]></code>
		/// </example>
		[Description("Options for the chart scales.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsScales Scales
		{
			get
			{
				if (this._scales == null)
					this._scales = new OptionsScales(this);

				return this._scales;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._scales = value;
			}
		}
		private OptionsScales _scales;

		/// <summary>
		/// Returns or sets the index axis of the chart.
		/// </summary>
		/// <value>The name of the index axis: "x" or "y". The default is "x".</value>
		/// <remarks>
		/// Serialized as the Chart.js 3 <c>indexAxis</c> option: the axis used as the base of the data points.
		/// Set it to "y" to draw horizontal bars in a bar chart. The value is not validated.
		/// Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // draw the bars horizontally.
		/// this.chartJS31.ChartType = ChartType.Bar;
		/// this.chartJS31.Options.IndexAxis = "y";
		/// ]]></code>
		/// </example>
		[DefaultValue("x")]
		[Description("Gets or sets the indent axis of the chart.")]
		public string IndexAxis
		{
			get { return this._indexAxis; }
			set
			{
				if (this._indexAxis != value)
				{
					this._indexAxis = value;
					Update();
				}
			}
		}
		private string _indexAxis = "x";

		/// <summary>
		/// Options for the chart plugins.
		/// </summary>
		/// <value>An <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsPlugins"/> instance. It is created automatically the first time the property is read.</value>
		/// <remarks>
		/// Serialized as the Chart.js 3 <c>plugins</c> option, which contains the title, legend, tooltip and data labels options.
		/// Assigning a new instance doesn't update the chart, call <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsPlugins"/> instance assigned already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var plugins = this.chartJS31.Options.Plugins;
		/// plugins.Title.Text = "Visitors";
		/// plugins.Legend.Position = HeaderPosition.Bottom;
		/// plugins.Tooltip.Enabled = false;
		/// plugins.DataLabels.Display = true;
		/// ]]></code>
		/// </example>
		[Description("Options for the chart plugins.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsPlugins Plugins
		{
			get
			{
				if (this._plugins == null)
					this._plugins = new OptionsPlugins(this);

				return this._plugins;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._plugins = value;
			}
		}
		private OptionsPlugins _plugins;

		#endregion
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Line"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Line"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Line;
	///
	/// var options = (LineOptions)this.chartJS31.Options;
	/// options.ShowLines = true;
	/// options.Plugins.Title.Text = "Temperatures";
	/// ]]></code>
	/// </example>
	public class LineOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS3.LineOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property of a line chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new LineOptions();
		/// options.Plugins.Title.Text = "Measurements";
		///
		/// this.chartJS31.ChartType = ChartType.Line;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public LineOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.LineOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/>, <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> or <see cref="P:Wisej.Web.Ext.ChartJS3.Options.IndexAxis"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> changes.
		/// Nested option sets are copied into new instances, other properties are copied only when they have a non-default value.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Line"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // create a new set of line options preserving the current shared options.
		/// var options = new LineOptions(this.chartJS31, this.chartJS31.Options);
		/// options.ShowLines = false;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public LineOptions(ChartJS3 chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// Returns or sets whether the lines between points are drawn. If false, the lines between points are not drawn.
		/// </summary>
		/// <value>true to draw the lines between the points; otherwise, false. The default is true.</value>
		/// <remarks>
		/// Serialized as the top level <c>showLines</c> option of the chart. Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // show only the points of the line chart.
		/// var options = (LineOptions)this.chartJS31.Options;
		/// options.ShowLines = false;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("If false, the lines between points are not drawn.")]
		public bool ShowLines
		{
			get { return this._showLines; }
			set
			{
				if (this._showLines != value)
				{
					this._showLines = value;
					Update();
				}
			}
		}
		private bool _showLines = true;

		/// <summary>
		/// Returns or sets whether the lines stack on top of each other along the y axis.
		/// </summary>
		/// <value>true to stack the lines; otherwise, false. The default is false.</value>
		/// <remarks>
		/// Serialized as the top level <c>stacked</c> option of the chart. In Chart.js 3 stacking is configured on the axes,
		/// use <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Stacked"/> on the axes in <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> to stack the lines.
		/// Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = (LineOptions)this.chartJS31.Options;
		/// options.Stacked = true;
		/// options.Scales.yAxes[0].Stacked = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("If true, lines stack on top of each other along the y axis.")]
		public bool Stacked
		{
			get { return this._stacked; }
			set
			{
				if (this._stacked != value)
				{
					this._stacked = value;
					Update();
				}
			}
		}
		private bool _stacked = false;
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bar"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is
	/// <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bar"/> or <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.HorizontalBar"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Bar;
	///
	/// var options = (BarOptions)this.chartJS31.Options;
	/// options.IndexAxis = "y";
	/// options.Plugins.Legend.Display = false;
	/// ]]></code>
	/// </example>
	public class BarOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS3.BarOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property of a bar chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new BarOptions();
		/// options.Plugins.Title.Text = "Orders by Region";
		///
		/// this.chartJS31.ChartType = ChartType.Bar;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public BarOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.BarOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/>, <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> or <see cref="P:Wisej.Web.Ext.ChartJS3.Options.IndexAxis"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> changes.
		/// Nested option sets are copied into new instances, other properties are copied only when they have a non-default value.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bar"/> or <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.HorizontalBar"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new BarOptions(this.chartJS31, this.chartJS31.Options);
		/// options.Stacked = true;
		/// options.Scales.xAxes[0].Stacked = true;
		/// options.Scales.yAxes[0].Stacked = true;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public BarOptions(ChartJS3 chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// Returns or sets whether the bars stack on top of each other along the y axis.
		/// </summary>
		/// <value>true to stack the bars; otherwise, false. The default is false.</value>
		/// <remarks>
		/// Serialized as the top level <c>stacked</c> option of the chart. In Chart.js 3 stacking is configured on the axes,
		/// use <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Stacked"/> on the axes in <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> to stack the bars.
		/// Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = (BarOptions)this.chartJS31.Options;
		/// options.Stacked = true;
		/// options.Scales.xAxes[0].Stacked = true;
		/// options.Scales.yAxes[0].Stacked = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("If true, lines stack on top of each other along the y axis.")]
		public bool Stacked
		{
			get { return this._stacked; }
			set
			{
				if (this._stacked != value)
				{
					this._stacked = value;
					Update();
				}
			}
		}
		private bool _stacked = false;
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Pie"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Pie"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Pie;
	///
	/// var options = (PieOptions)this.chartJS31.Options;
	/// options.Plugins.Legend.Position = HeaderPosition.Left;
	/// options.Plugins.Title.Text = "Market Share";
	/// ]]></code>
	/// </example>
	public class PieOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS3.PieOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property of a pie chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new PieOptions();
		/// options.Plugins.Legend.Display = false;
		///
		/// this.chartJS31.ChartType = ChartType.Pie;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public PieOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.PieOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/>, <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> or <see cref="P:Wisej.Web.Ext.ChartJS3.Options.IndexAxis"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> changes.
		/// Nested option sets are copied into new instances, other properties are copied only when they have a non-default value.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Pie"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new PieOptions(this.chartJS31, this.chartJS31.Options);
		/// options.Plugins.Tooltip.Enabled = false;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public PieOptions(ChartJS3 chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// Returns or sets the size of the inner part of the chart that is cut out.
		/// </summary>
		/// <value>The size of the cut out area. The default is 0.</value>
		/// <remarks>
		/// Serialized as the Chart.js 3 <c>cutout</c> option. Chart.js 3 interprets a numeric <c>cutout</c> as a size in pixels
		/// (a percentage requires a string such as "50%", which this property can't express), so the value is the radius in pixels of the hole in the middle of the chart.
		/// A value of 0 draws a filled pie. Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // turn the pie into a ring.
		/// var options = (PieOptions)this.chartJS31.Options;
		/// options.Cutout = 60;
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
		[Description("This equates what percentage of the inner part should be cut out.")]
		public int Cutout
		{
			get { return this._cutOut; }
			set
			{
				if (this._cutOut != value)
				{
					this._cutOut = value;
					Update();
				}
			}
		}
		private int _cutOut = 0;
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.PolarArea"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.PolarArea"/>.
	/// This class doesn't add any option to the shared options defined in <see cref="T:Wisej.Web.Ext.ChartJS3.Options"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.PolarArea;
	///
	/// var options = (PolarAreaOptions)this.chartJS31.Options;
	/// options.Plugins.Title.Text = "Skills";
	/// options.Plugins.Legend.Position = HeaderPosition.Right;
	/// ]]></code>
	/// </example>
	public class PolarAreaOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS3.PolarAreaOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property of a polar area chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new PolarAreaOptions();
		/// options.Plugins.Title.Text = "Wind Directions";
		///
		/// this.chartJS31.ChartType = ChartType.PolarArea;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public PolarAreaOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.PolarAreaOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/>, <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> or <see cref="P:Wisej.Web.Ext.ChartJS3.Options.IndexAxis"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> changes.
		/// Nested option sets are copied into new instances, other properties are copied only when they have a non-default value.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.PolarArea"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new PolarAreaOptions(this.chartJS31, this.chartJS31.Options);
		/// options.Plugins.Legend.Display = false;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public PolarAreaOptions(ChartJS3 chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Doughnut"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Doughnut"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Doughnut;
	///
	/// var options = (DoughnutOptions)this.chartJS31.Options;
	/// options.Cutout = 80;
	/// options.Plugins.Title.Text = "Budget";
	/// ]]></code>
	/// </example>
	public class DoughnutOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS3.DoughnutOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property of a doughnut chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new DoughnutOptions();
		/// options.Cutout = 70;
		///
		/// this.chartJS31.ChartType = ChartType.Doughnut;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public DoughnutOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.DoughnutOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/>, <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> or <see cref="P:Wisej.Web.Ext.ChartJS3.Options.IndexAxis"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> changes.
		/// Nested option sets are copied into new instances, other properties are copied only when they have a non-default value.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Doughnut"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new DoughnutOptions(this.chartJS31, this.chartJS31.Options);
		/// options.Cutout = 40;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public DoughnutOptions(ChartJS3 chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// Returns or sets the size of the inner part of the chart that is cut out.
		/// </summary>
		/// <value>The size of the cut out area. The default is 50.</value>
		/// <remarks>
		/// Serialized as the Chart.js 3 <c>cutout</c> option. Chart.js 3 interprets a numeric <c>cutout</c> as a size in pixels
		/// (a percentage requires a string such as "50%", which this property can't express), so the value is the radius in pixels of the hole in the middle of the chart.
		/// A value of 0 draws a filled pie. Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // draw a thin ring.
		/// var options = (DoughnutOptions)this.chartJS31.Options;
		/// options.Cutout = 120;
		/// ]]></code>
		/// </example>
		[DefaultValue(50)]
		[Description("This equates what percentage of the inner part should be cut out.")]
		public int Cutout
		{
			get { return this._cutOut; }
			set
			{
				if (this._cutOut != value)
				{
					this._cutOut = value;
					Update();
				}
			}
		}
		private int _cutOut = 50;
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Radar"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Radar"/>.
	/// This class doesn't add any option to the shared options defined in <see cref="T:Wisej.Web.Ext.ChartJS3.Options"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Radar;
	///
	/// var options = (RadarOptions)this.chartJS31.Options;
	/// options.Plugins.Title.Text = "Player Stats";
	/// options.Plugins.Legend.Position = HeaderPosition.Bottom;
	/// ]]></code>
	/// </example>
	public class RadarOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS3.RadarOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property of a radar chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new RadarOptions();
		/// options.Plugins.Tooltip.Enabled = false;
		///
		/// this.chartJS31.ChartType = ChartType.Radar;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public RadarOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.RadarOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/>, <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> or <see cref="P:Wisej.Web.Ext.ChartJS3.Options.IndexAxis"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> changes.
		/// Nested option sets are copied into new instances, other properties are copied only when they have a non-default value.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Radar"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new RadarOptions(this.chartJS31, this.chartJS31.Options);
		/// options.Plugins.Title.Text = "Product Comparison";
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public RadarOptions(ChartJS3 chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bubble"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bubble"/>.
	/// This class doesn't add any option to the shared options defined in <see cref="T:Wisej.Web.Ext.ChartJS3.Options"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Bubble;
	///
	/// var options = (BubbleOptions)this.chartJS31.Options;
	/// options.Plugins.Title.Text = "Population vs. GDP";
	/// options.Scales.xAxes[0].Title.Display = true;
	/// options.Scales.xAxes[0].Title.Text = "GDP";
	/// ]]></code>
	/// </example>
	public class BubbleOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS3.BubbleOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property of a bubble chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new BubbleOptions();
		/// options.Plugins.Legend.Display = false;
		///
		/// this.chartJS31.ChartType = ChartType.Bubble;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public BubbleOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.BubbleOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/>, <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> or <see cref="P:Wisej.Web.Ext.ChartJS3.Options.IndexAxis"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> changes.
		/// Nested option sets are copied into new instances, other properties are copied only when they have a non-default value.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bubble"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new BubbleOptions(this.chartJS31, this.chartJS31.Options);
		/// options.Plugins.DataLabels.Display = true;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public BubbleOptions(ChartJS3 chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Scatter"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Scatter"/>.
	/// This class doesn't add any option to the shared options defined in <see cref="T:Wisej.Web.Ext.ChartJS3.Options"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Scatter;
	///
	/// var options = (ScatterOptions)this.chartJS31.Options;
	/// options.Plugins.Title.Text = "Height vs. Weight";
	/// options.Scales.yAxes[0].Ticks.BeginAtZero = true;
	/// ]]></code>
	/// </example>
	public class ScatterOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS3.ScatterOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> property of a scatter chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new ScatterOptions();
		/// options.Plugins.Title.Text = "Samples";
		///
		/// this.chartJS31.ChartType = ChartType.Scatter;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public ScatterOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.ScatterOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/>, <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> or <see cref="P:Wisej.Web.Ext.ChartJS3.Options.IndexAxis"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> changes.
		/// Nested option sets are copied into new instances, other properties are copied only when they have a non-default value.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Scatter"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new ScatterOptions(this.chartJS31, this.chartJS31.Options);
		/// options.Plugins.Legend.Position = HeaderPosition.Top;
		/// this.chartJS31.Options = options;
		/// ]]></code>
		/// </example>
		public ScatterOptions(ChartJS3 chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}
}
