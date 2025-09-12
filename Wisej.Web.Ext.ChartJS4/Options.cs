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
using System.Drawing;
using System.Globalization;
using Wisej.Core;

namespace Wisej.Web.Ext.ChartJS4
{
	/// <summary>
	/// Base class for all the option classes.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(OptionsBase.Converter))]
	[Editor("Wisej.Web.Ext.ChartJS4.Design.OptionsEditor",
			"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[WisejSerializerOptions(WisejSerializerOptions.IgnoreNulls)]
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
		internal ChartJS4 Chart
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
		private ChartJS4 _chart;

		/// <summary>
		/// Gets or sets the type of chart.
		/// </summary>
		[Browsable(true)]
		[DefaultValue("")]
		[Description("Gets or sets The type of chart.")]
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (this._type != value)
				{
					this._type = value;

					Update();
				}
			}
		}
		private string _type = string.Empty;

		private void ResetType()
		{
			this._type = string.Empty;
		}

		/// <summary>
		/// Gets or sets the chart data.
		/// </summary>
		[Description("Gets or sets the chart elements.")]
		[Editor(
			"Wisej.Design.CodeEditor, Wisej.Framework.Design, Version=3.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
			"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public dynamic Elements
		{
			get
			{
				return this._elements;
			}
			set
			{
				if (this._elements != value)
				{
					this._elements = value;
					Update();
				}
			}
		}
		private dynamic _elements;

		private bool ShouldSerializeElements()
		{
			return this._elements != null;
		}

		private void ResetElements()
		{
			this._elements = null;
		}


		/// <summary>
		/// Gets or sets the background color of the points in the chart.
		/// </summary>
		[Description("Gets or sets the background color of the points in the chart.")]
		[Editor("Wisej.Web.Ext.ChartJS4.Design.ChartColorUIEditor", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[TypeConverter(typeof(ChartColorConverter))]
		[DefaultValue(null)]
		public object PointBackgroundColor
		{
			get
			{
				return this._pointBackgroundColor;
			}
			set
			{
				if (this._pointBackgroundColor != value)
				{
					this._pointBackgroundColor = value;
					Update();
				}
			}
		}
		private object _pointBackgroundColor = null;

		private bool ShouldSerializePointBackgroundColor()
		{
			return this._pointBackgroundColor is Color color && color != Color.Empty ||
				   this._pointBackgroundColor is string str && str != string.Empty || this._pointBackgroundColor is not null;
		}

		public object HoverBackgroundColor
		{
			get
			{
				return this._hoverBackgroundColor;
			}
			set
			{
				if(this._hoverBackgroundColor != value)
				{
					this._hoverBackgroundColor = value;
					Update();
				}
			}
		}
		private object _hoverBackgroundColor;

		/// <summary>  
		/// Gets or sets the radius of the chart points.  
		/// </summary>
		[Description("Gets or sets the radius of the chart points.")]
		[DefaultValue(0)]
		public int Radius
		{
			get { return this._radius; }
			set
			{
				if (this._radius != value)
				{
					this._radius = value;
					Update();
				}
			}
		}
		private int _radius = 0;


		/// <summary>
		/// Gets or sets custom options for the chart in JSON format.  
		/// </summary>
		/// <summary>
		/// Custom options for plugins.
		/// </summary>
		[Editor(
			"Wisej.Design.CodeEditor, Wisej.Framework.Design, Version=3.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
			"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Description("Gets or sets custom options for the chart in JSON format.")]
		[DefaultValue("")]
		public string CustomOptions
		{
			get { return this._customOptions; }
			set
			{
				if (this._customOptions != value)
				{
					this._customOptions = value;
					Update();
				}
			}
		}
		private string _customOptions = string.Empty;

		/// <summary>
		/// TODO:Julie - Add description
		/// </summary>
		[DefaultValue(true)]
		[Description("")]
		public dynamic Animation
		{
			get
			{
				return this._animation;
			}
			set
			{
				if (this._animation != value)
				{
					this._animation = value;
					Update();
				}
			}
		}
		private dynamic _animation;

		/// <summary>
		/// TODO:Julie - Add description
		/// </summary>
		[Description("TODO:Julie - Add description")]
		public dynamic Animations
		{
			get
			{
				return this._animations;
			}
			set
			{
				if (this._animations != value)
				{
					this._animations = value;
					Update();
				}
			}
		}
		private dynamic _animations;

		/// <summary>
		/// If true, the chart will be parsed.
		/// </summary>
		[DefaultValue(true)]
		[Description("If true, the chart will be parsed")]
		public bool Parsing
		{
			get
			{
				return this._parsing;
			}
			set
			{
				if (this._parsing != value)
				{
					this._parsing = value;
					Update();
				}
			}
		}
		private bool _parsing = true;

		#endregion

		#region Methods

		/// <summary>
		/// Updates the <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> control using
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

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
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
	/// Represents the base options for the <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> widget.
	/// Different <see cref="T:Wisej.Web.Ext.ChartJS4.ChartType"/> extend this class with type specific options.
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
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.Options"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public Options(ChartJS4 chart, Options defaults)
		{
			this.Chart = chart;

			if (defaults != null)
				CopyFrom(defaults);
		}

		#region Properties

		/// <summary>
		/// Options for the chart layout
		/// </summary>
		public OptionsLayout Layout
		{
			get
			{
				if (this._layout == null)
					this._layout = new OptionsLayout(this);

				return this._layout;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._layout = value;
			}
		}
		private OptionsLayout _layout;

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

		public OptionsInteraction Interaction
		{
			get
			{
				if (this._interaction == null)
					this._interaction = new OptionsInteraction();
				return this._interaction;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				this._interaction = value;
			}
		}
		private OptionsInteraction _interaction;

		/// <summary>
		/// Gets or sets the index axis of the chart.
		/// </summary>
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
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS4.ChartType.Line"/> chart.
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
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.LineOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public LineOptions(ChartJS4 chart, Options defaults)
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
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS4.ChartType.Bar"/> chart.
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
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.LineOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public BarOptions(ChartJS4 chart, Options defaults)
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
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS4.ChartType.Pie"/> chart.
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
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.PieOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public PieOptions(ChartJS4 chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// This equates what percentage of the inner part should be cut out.
		/// </summary>
		[DefaultValue("0")]
		[Description("This equates what percentage of the inner part should be cut out")]
		public string Cutout
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
		private string _cutOut = "0";
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS4.ChartType.PolarArea"/> chart.
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
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.PolarAreaOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public PolarAreaOptions(ChartJS4 chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS4.ChartType.Doughnut"/> chart.
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
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.DoughnutOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public DoughnutOptions(ChartJS4 chart, Options defaults)
			: base(chart, defaults)
		{
		}

		/// <summary>
		/// This equates what percentage of the inner part should be cut out.
		/// </summary>
		[DefaultValue(50)]
		[Description("This equates what percentage of the inner part should be cut out")]
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
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS4.ChartType.Radar"/> chart.
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
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.RadarOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public RadarOptions(ChartJS4 chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS4.ChartType.Bubble"/> chart.
	/// </summary>
	[WisejSerializerOptions(WisejSerializerOptions.IgnoreNulls)]
	public class BubbleOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public BubbleOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.BubbleOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public BubbleOptions(ChartJS4 chart, Options defaults)
			: base(chart, defaults)
		{
		}


	}

	public class CustomOptions : Options
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CustomOptions()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.CustomOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public CustomOptions(ChartJS4 chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}

	/// <summary>
	/// Options for the <see cref="F:Wisej.Web.Ext.ChartJS4.ChartType.Scatter"/> chart.
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
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.ScatterOptions"/> set.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		/// <param name="defaults">Default options to copy from.</param>
		public ScatterOptions(ChartJS4 chart, Options defaults)
			: base(chart, defaults)
		{
		}
	}
}
