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
using System.Drawing.Design;
using System.Globalization;

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Base class for all the option classes.
	/// </summary>
	[TypeConverter(typeof(OptionsBase.Converter))]
	[Editor(typeof(Design.OptionsEditor), typeof(UITypeEditor))]
	public abstract class OptionsBase
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
		/// Updates the <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control using
		/// this set of options.
		/// </summary>
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
		/// <param name="obj"></param>
		/// <returns></returns>
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

	}

	/// <summary>
	/// Represents the base options for the <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> widget.
	/// Different <see cref="T:Wisej.Web.Ext.ChartJS.ChartType"/> extend this class with type specific options.
	/// </summary>
	public abstract class Options : OptionsBase
	{

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Options()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.Options"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
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
	public class LineOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public LineOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.LineOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public LineOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// If false, the lines between points are not drawn.
		/// </summary>
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
		/// If true, lines stack on top of each other along the y axis.
		/// </summary>
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
	public class BarOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public BarOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.LineOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public BarOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// If true, lines stack on top of each other along the y axis.
		/// </summary>
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
	public class PieOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PieOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.PieOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public PieOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.PolarArea"/> chart.
	/// </summary>
	public class PolarAreaOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PolarAreaOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.PolarAreaOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public PolarAreaOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Doughnut"/> chart.
	/// </summary>
	public class DoughnutOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public DoughnutOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.DoughnutOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public DoughnutOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// This equates what percentage of the inner part should be cut out.
		/// </summary>
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
	public class RadarOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public RadarOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.RadarOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public RadarOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bubble"/> chart.
	/// </summary>
	public class BubbleOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public BubbleOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.BubbleOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public BubbleOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Scatter"/> chart.
	/// </summary>
	public class ScatterOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ScatterOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.ScatterOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public ScatterOptions(ChartJS chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

}
