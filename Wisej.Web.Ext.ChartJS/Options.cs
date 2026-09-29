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
using System.Globalization;

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Base class for all the option classes.
	/// </summary>
	/// <remarks>
	/// Every option set belongs either directly to a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control or to a parent option set.
	/// Changing any property of an option set calls <see cref="M:Wisej.Web.Ext.ChartJS.OptionsBase.Update"/>, which updates the owner
	/// <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control. An option set instance cannot be assigned to two different owners.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// // all option classes derive from OptionsBase.
	/// OptionsBase title = this.chartJS1.Options.Title;
	/// OptionsBase legend = this.chartJS1.Options.Legend;
	///
	/// // force a refresh of the chart.
	/// title.Update();
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS")]
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
		internal ChartJS Chart
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
		private ChartJS _chart;

		#endregion

		#region Methods

		/// <summary>
		/// Updates the <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control using this set of options.
		/// </summary>
		/// <remarks>
		/// The call is ignored when this option set is not attached to a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control, either
		/// directly or through its owner option set. It is called automatically when a property of the option set changes.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = this.chartJS1.Options;
		/// options.Title.Text = "Monthly Sales";
		///
		/// // refresh the chart with the current options.
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
		/// var title1 = this.chartJS1.Options.Title;
		/// var title2 = this.chartJS2.Options.Title;
		///
		/// if (!title1.Equals(title2))
		/// 	title2.Text = title1.Text;
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
		/// int hash = this.chartJS1.Options.Title.GetHashCode();
		/// this.chartJS1.Options.Title.Text = "Changed";
		///
		/// // the hash code is different now.
		/// bool changed = hash != this.chartJS1.Options.Title.GetHashCode();
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
	/// Represents the base options for the <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> widget.
	/// Different <see cref="T:Wisej.Web.Ext.ChartJS.ChartType"/> values extend this class with type specific options.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property returns the instance of the class that matches the current
	/// <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/>: <see cref="T:Wisej.Web.Ext.ChartJS.LineOptions"/>, <see cref="T:Wisej.Web.Ext.ChartJS.BarOptions"/> (also used for
	/// <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.HorizontalBar"/>), <see cref="T:Wisej.Web.Ext.ChartJS.PieOptions"/>, <see cref="T:Wisej.Web.Ext.ChartJS.PolarAreaOptions"/>, <see cref="T:Wisej.Web.Ext.ChartJS.DoughnutOptions"/>,
	/// <see cref="T:Wisej.Web.Ext.ChartJS.RadarOptions"/>, <see cref="T:Wisej.Web.Ext.ChartJS.BubbleOptions"/> or <see cref="T:Wisej.Web.Ext.ChartJS.ScatterOptions"/>.
	/// Cast it to the specific class to access the type specific options.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// Options options = this.chartJS1.Options;
	/// options.Title.Text = "Revenue";
	/// options.Legend.Display = false;
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
		/// Used by derived classes to create an option set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // derived classes call the default constructor.
		/// Options options = new LineOptions();
		/// options.Title.Text = "Temperatures";
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public Options()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.Options"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it are copied into the new instance.
		/// Nested option sets are copied into new instances, other properties are copied only when they have a non-default value.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // derived classes pass the arguments to the base constructor.
		/// var options = new BarOptions(this.chartJS1, this.chartJS1.Options);
		/// options.Stacked = true;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public Options(ChartJS chart, Options defaults)
		{
			this.Chart = chart;

			if (defaults != null)
				CopyFrom(defaults);
		}

		#region Properties

		/// <summary>
		/// Options for the chart title.
		/// </summary>
		/// <value>An <see cref="T:Wisej.Web.Ext.ChartJS.OptionsTitle"/> instance. It is created automatically the first time the property is read.</value>
		/// <remarks>
		/// Serialized as the <c>title</c> option of the chart.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsTitle"/> instance assigned already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.Title.Display = true;
		/// this.chartJS1.Options.Title.Text = "Quarterly Results";
		/// this.chartJS1.Options.Title.Position = HeaderPosition.Bottom;
		/// ]]></code>
		/// </example>
		[Description("Options for the chart title.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsTitle Title
		{
			get
			{
				if (this._title == null)
					this._title = new OptionsTitle(this);

				return this._title;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._title = value;
			}
		}
		private OptionsTitle _title;

		/// <summary>
		/// Options for the chart tooltips.
		/// </summary>
		/// <value>An <see cref="T:Wisej.Web.Ext.ChartJS.OptionsTooltips"/> instance. It is created automatically the first time the property is read.</value>
		/// <remarks>
		/// Serialized as the <c>tooltips</c> option of the chart.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsTooltips"/> instance assigned already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// // hide the tooltips when the user hovers the data points.
		/// this.chartJS1.Options.Tooltips.Enabled = false;
		/// ]]></code>
		/// </example>
		[Description("Options for the chart tooltips.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsTooltips Tooltips
		{
			get
			{
				if (this._tooltips == null)
					this._tooltips = new OptionsTooltips(this);

				return this._tooltips;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._tooltips = value;
			}
		}
		private OptionsTooltips _tooltips;

		/// <summary>
		/// Options for the chart legend.
		/// </summary>
		/// <value>An <see cref="T:Wisej.Web.Ext.ChartJS.OptionsLegend"/> instance. It is created automatically the first time the property is read.</value>
		/// <remarks>
		/// Serialized as the <c>legend</c> option of the chart.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsLegend"/> instance assigned already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.Legend.Display = true;
		/// this.chartJS1.Options.Legend.Position = HeaderPosition.Right;
		/// this.chartJS1.Options.Legend.Labels.UsePointStyle = true;
		/// ]]></code>
		/// </example>
		[Description("Options for the chart legend.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsLegend Legend
		{
			get
			{
				if (this._legend == null)
					this._legend = new OptionsLegend(this);

				return this._legend;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._legend = value;
			}
		}
		private OptionsLegend _legend;

		/// <summary>
		/// Options for the chart scales.
		/// </summary>
		/// <value>An <see cref="T:Wisej.Web.Ext.ChartJS.OptionsScales"/> instance. It is created automatically the first time the property is read.</value>
		/// <remarks>
		/// Serialized as the <c>scales</c> option of the chart.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsScales"/> instance assigned already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var yAxis = this.chartJS1.Options.Scales.yAxes[0];
		/// yAxis.Ticks.Min = 0;
		/// yAxis.Ticks.Max = 100;
		/// yAxis.GridLines.Display = false;
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
		/// Options for the data label.
		/// </summary>
		/// <value>An <see cref="T:Wisej.Web.Ext.ChartJS.OptionsDataLabel"/> instance. It is created automatically the first time the property is read.</value>
		/// <remarks>
		/// Serialized as the <c>dataLabel</c> option of the chart.
		/// The data labels are rendered by the chartjs-plugin-datalabels plugin.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsDataLabel"/> instance assigned already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabel = this.chartJS1.Options.DataLabel;
		/// dataLabel.Display = true;
		/// dataLabel.Anchor = DataLabelAnchor.End;
		/// dataLabel.Align = DataLabelAlign.Top;
		/// ]]></code>
		/// </example>
		[Description("Options for the data label.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsDataLabel DataLabel
		{
			get
			{
				if (this._dataLabel == null)
					this._dataLabel = new OptionsDataLabel(this);

				return this._dataLabel;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._dataLabel = value;
			}
		}
		private OptionsDataLabel _dataLabel;

		#endregion
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Line"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Line"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Line;
	///
	/// var options = (LineOptions)this.chartJS1.Options;
	/// options.ShowLines = true;
	/// options.Title.Text = "Visitors per Day";
	/// ]]></code>
	/// </example>
	public class LineOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS.LineOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property of a line chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new LineOptions();
		/// options.ShowLines = false;
		/// options.Title.Text = "Measurements";
		///
		/// this.chartJS1.ChartType = ChartType.Line;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public LineOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.LineOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS.Options.Title"/>, <see cref="P:Wisej.Web.Ext.ChartJS.Options.Legend"/> or <see cref="P:Wisej.Web.Ext.ChartJS.Options.Tooltips"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> changes.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Line"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // create a new set of line options preserving the current shared options.
		/// var options = new LineOptions(this.chartJS1, this.chartJS1.Options);
		/// options.ShowLines = false;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public LineOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// Returns or sets whether the lines between points are drawn. If false, the lines between points are not drawn.
		/// </summary>
		/// <value>true to draw the lines between the points; otherwise, false. The default is true.</value>
		/// <remarks>
		/// Serialized as the Chart.js <c>showLines</c> option. Set it to false to display only the data points.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // show only the points of the line chart.
		/// var options = (LineOptions)this.chartJS1.Options;
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
		/// Serialized as the top level <c>stacked</c> option of the chart. The stacking of each axis can also be set
		/// using <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Stacked"/> on the axes in <see cref="P:Wisej.Web.Ext.ChartJS.Options.Scales"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = (LineOptions)this.chartJS1.Options;
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
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bar"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is
	/// <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bar"/> or <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.HorizontalBar"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.HorizontalBar;
	///
	/// var options = (BarOptions)this.chartJS1.Options;
	/// options.Stacked = false;
	/// options.Legend.Display = false;
	/// ]]></code>
	/// </example>
	public class BarOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS.BarOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property of a bar chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new BarOptions();
		/// options.Title.Text = "Orders by Region";
		///
		/// this.chartJS1.ChartType = ChartType.Bar;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public BarOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.BarOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS.Options.Title"/>, <see cref="P:Wisej.Web.Ext.ChartJS.Options.Legend"/> or <see cref="P:Wisej.Web.Ext.ChartJS.Options.Tooltips"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> changes.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bar"/> or <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.HorizontalBar"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new BarOptions(this.chartJS1, this.chartJS1.Options);
		/// options.Stacked = true;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public BarOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// Returns or sets whether the bars stack on top of each other along the y axis.
		/// </summary>
		/// <value>true to stack the bars; otherwise, false. The default is false.</value>
		/// <remarks>
		/// Serialized as the top level <c>stacked</c> option of the chart. The stacking of each axis can also be set
		/// using <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Stacked"/> on the axes in <see cref="P:Wisej.Web.Ext.ChartJS.Options.Scales"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = (BarOptions)this.chartJS1.Options;
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
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Pie"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Pie"/>.
	/// This class doesn't add any option to the shared options defined in <see cref="T:Wisej.Web.Ext.ChartJS.Options"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Pie;
	///
	/// var options = (PieOptions)this.chartJS1.Options;
	/// options.Legend.Position = HeaderPosition.Left;
	/// options.Title.Text = "Market Share";
	/// ]]></code>
	/// </example>
	public class PieOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS.PieOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property of a pie chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new PieOptions();
		/// options.Legend.Display = false;
		///
		/// this.chartJS1.ChartType = ChartType.Pie;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public PieOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.PieOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS.Options.Title"/>, <see cref="P:Wisej.Web.Ext.ChartJS.Options.Legend"/> or <see cref="P:Wisej.Web.Ext.ChartJS.Options.Tooltips"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> changes.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Pie"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new PieOptions(this.chartJS1, this.chartJS1.Options);
		/// options.Tooltips.Enabled = false;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public PieOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.PolarArea"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.PolarArea"/>.
	/// This class doesn't add any option to the shared options defined in <see cref="T:Wisej.Web.Ext.ChartJS.Options"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.PolarArea;
	///
	/// var options = (PolarAreaOptions)this.chartJS1.Options;
	/// options.Title.Text = "Skills";
	/// options.Legend.Position = HeaderPosition.Right;
	/// ]]></code>
	/// </example>
	public class PolarAreaOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS.PolarAreaOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property of a polar area chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new PolarAreaOptions();
		/// options.Title.Text = "Wind Directions";
		///
		/// this.chartJS1.ChartType = ChartType.PolarArea;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public PolarAreaOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.PolarAreaOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS.Options.Title"/>, <see cref="P:Wisej.Web.Ext.ChartJS.Options.Legend"/> or <see cref="P:Wisej.Web.Ext.ChartJS.Options.Tooltips"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> changes.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.PolarArea"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new PolarAreaOptions(this.chartJS1, this.chartJS1.Options);
		/// options.Legend.Display = false;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public PolarAreaOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Doughnut"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Doughnut"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Doughnut;
	///
	/// var options = (DoughnutOptions)this.chartJS1.Options;
	/// options.CutoutPercentage = 70;
	/// options.Title.Text = "Budget";
	/// ]]></code>
	/// </example>
	public class DoughnutOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS.DoughnutOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property of a doughnut chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new DoughnutOptions();
		/// options.CutoutPercentage = 60;
		///
		/// this.chartJS1.ChartType = ChartType.Doughnut;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public DoughnutOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.DoughnutOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS.Options.Title"/>, <see cref="P:Wisej.Web.Ext.ChartJS.Options.Legend"/> or <see cref="P:Wisej.Web.Ext.ChartJS.Options.Tooltips"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> changes.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Doughnut"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new DoughnutOptions(this.chartJS1, this.chartJS1.Options);
		/// options.CutoutPercentage = 40;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public DoughnutOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// Returns or sets the percentage of the chart that is cut out of the middle.
		/// </summary>
		/// <value>The percentage of the inner part of the chart that is cut out. The default is 50.</value>
		/// <remarks>
		/// Serialized as the Chart.js <c>cutoutPercentage</c> option. A value of 0 draws a filled pie,
		/// higher values make the ring thinner.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // draw a thin ring.
		/// var options = (DoughnutOptions)this.chartJS1.Options;
		/// options.CutoutPercentage = 85;
		/// ]]></code>
		/// </example>
		[DefaultValue(50)]
		[Description("This equates what percentage of the inner part should be cut out")]
		public int CutoutPercentage
		{
			get { return this._cutOutPercentage; }
			set
			{
				if (this._cutOutPercentage != value)
				{
					this._cutOutPercentage = value;
					Update();
				}
			}
		}
		private int _cutOutPercentage = 50;
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Radar"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Radar"/>.
	/// This class doesn't add any option to the shared options defined in <see cref="T:Wisej.Web.Ext.ChartJS.Options"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Radar;
	///
	/// var options = (RadarOptions)this.chartJS1.Options;
	/// options.Title.Text = "Player Stats";
	/// options.Legend.Position = HeaderPosition.Bottom;
	/// ]]></code>
	/// </example>
	public class RadarOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS.RadarOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property of a radar chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new RadarOptions();
		/// options.Tooltips.Enabled = false;
		///
		/// this.chartJS1.ChartType = ChartType.Radar;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public RadarOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.RadarOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS.Options.Title"/>, <see cref="P:Wisej.Web.Ext.ChartJS.Options.Legend"/> or <see cref="P:Wisej.Web.Ext.ChartJS.Options.Tooltips"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> changes.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Radar"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new RadarOptions(this.chartJS1, this.chartJS1.Options);
		/// options.Title.Text = "Comparison";
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public RadarOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bubble"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bubble"/>.
	/// This class doesn't add any option to the shared options defined in <see cref="T:Wisej.Web.Ext.ChartJS.Options"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Bubble;
	///
	/// var options = (BubbleOptions)this.chartJS1.Options;
	/// options.Title.Text = "Population vs. GDP";
	/// options.Scales.xAxes[0].Ticks.Min = 0;
	/// ]]></code>
	/// </example>
	public class BubbleOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS.BubbleOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property of a bubble chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new BubbleOptions();
		/// options.Legend.Display = false;
		///
		/// this.chartJS1.ChartType = ChartType.Bubble;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public BubbleOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.BubbleOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS.Options.Title"/>, <see cref="P:Wisej.Web.Ext.ChartJS.Options.Legend"/> or <see cref="P:Wisej.Web.Ext.ChartJS.Options.Tooltips"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> changes.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bubble"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new BubbleOptions(this.chartJS1, this.chartJS1.Options);
		/// options.Title.Display = false;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public BubbleOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Scatter"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property returns an instance of this class when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Scatter"/>.
	/// This class doesn't add any option to the shared options defined in <see cref="T:Wisej.Web.Ext.ChartJS.Options"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Scatter;
	///
	/// var options = (ScatterOptions)this.chartJS1.Options;
	/// options.Title.Text = "Height vs. Weight";
	/// options.Scales.yAxes[0].Ticks.Min = 0;
	/// ]]></code>
	/// </example>
	public class ScatterOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone <see cref="T:Wisej.Web.Ext.ChartJS.ScatterOptions"/> set that is not yet attached to a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
		/// Assign it to the <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/> property of a scatter chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new ScatterOptions();
		/// options.Title.Text = "Samples";
		///
		/// this.chartJS1.ChartType = ChartType.Scatter;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public ScatterOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.ScatterOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from. Can be null.</param>
		/// <remarks>
		/// When <paramref name="defaults"/> is not null, the properties shared with it (for example
		/// <see cref="P:Wisej.Web.Ext.ChartJS.Options.Title"/>, <see cref="P:Wisej.Web.Ext.ChartJS.Options.Legend"/> or <see cref="P:Wisej.Web.Ext.ChartJS.Options.Tooltips"/>) are copied into the new instance,
		/// which is how the chart preserves the common settings when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> changes.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control creates this instance automatically when <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Scatter"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new ScatterOptions(this.chartJS1, this.chartJS1.Options);
		/// options.Legend.Display = false;
		/// this.chartJS1.Options = options;
		/// ]]></code>
		/// </example>
		public ScatterOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}
}
