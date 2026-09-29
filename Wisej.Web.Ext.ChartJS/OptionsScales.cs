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

namespace Wisej.Web.Ext.ChartJS
{
    /// <summary>
    /// Represents the options for the axes (scales) of the chart.
    /// </summary>
    /// <remarks>
    /// Maps to the Chart.js <c>options.scales</c> configuration. The axes are exposed as arrays
    /// (<see cref="P:Wisej.Web.Ext.ChartJS.OptionsScales.xAxes"/> and <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScales.yAxes"/>)
    /// to support charts with multiple axes. An instance is available through the
    /// <see cref="P:Wisej.Web.Ext.ChartJS.Options.Scales"/> property of the chart options.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// var scales = this.chartJS1.Options.Scales;
    /// scales.xAxes[0].ScaleLabel.LabelString = "Month";
    /// scales.yAxes[0].ScaleLabel.LabelString = "Revenue";
    /// scales.yAxes[0].Ticks.BeginAtZero = true;
    /// ]]></code>
    /// </example>
    [ApiCategory("ChartJS")]
    public class OptionsScales : OptionsBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// Creates a set of scale options that is not attached to any chart yet.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var scales = new OptionsScales();
        /// scales.yAxes[0].Ticks.Max = 100;
        /// this.chartJS1.Options.Scales = scales;
        /// ]]></code>
        /// </example>
        public OptionsScales()
        {
        }

        /// <summary>
        /// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsScales"/> set.
        /// </summary>
        /// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> set of options that owns this instance.</param>
        /// <example>
        /// <code><![CDATA[
        /// var options = (LineOptions)this.chartJS1.Options;
        /// var scales = new OptionsScales(options);
        /// scales.xAxes[0].Display = false;
        /// options.Scales = scales;
        /// ]]></code>
        /// </example>
        public OptionsScales(OptionsBase owner)
        {
            this.Owner = owner;
        }

        /// <summary>
        /// Options for the x-axes.
        /// </summary>
        /// <value>
        /// An array of <see cref="T:Wisej.Web.Ext.ChartJS.OptionScalesAxesX"/> objects, one for each x-axis.
        /// When not set, the getter creates an array containing a single default x-axis.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>options.scales.xAxes</c> array. To use more than one x-axis,
        /// assign a new array and give each axis a unique <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Id"/>.
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">An axis in the array already belongs to another set of options.</exception>
        /// <example>
        /// <code><![CDATA[
        /// var xAxis = this.chartJS1.Options.Scales.xAxes[0];
        /// xAxis.Position = HeaderPosition.Top;
        /// xAxis.GridLines.Display = false;
        /// xAxis.ScaleLabel.LabelString = "Quarter";
        /// ]]></code>
        /// </example>
        [MergableProperty(false)]
        [TypeConverter(typeof(ArrayConverter))]
        [Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public OptionScalesAxesX[] xAxes
        {
            get
            {
                if (this._xAxes == null)
                {
                    this.xAxes = new OptionScalesAxesX[1]
                    {
                        (OptionScalesAxesX)this.DefaultOptionScalesAxesX.Clone()
                    };
                }

                return this._xAxes;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                this._xAxes = value;

                if (this._xAxes != null)
                {
                    foreach (var x in this._xAxes)
                        x.Owner = this;
                }
            }
        }

        private OptionScalesAxesX[] _xAxes;

        private OptionScalesAxesX DefaultOptionScalesAxesX
        {
            get
            {
                if (this._defaultOptionScalesAxesX == null)
                    this._defaultOptionScalesAxesX = new OptionScalesAxesX(this);

                return this._defaultOptionScalesAxesX;
            }
        }

        private OptionScalesAxesX _defaultOptionScalesAxesX;

        private bool ShouldSerializexAxes()
        {
            return this._xAxes != null && this._xAxes.Length > 0 && !Object.Equals(this._xAxes[0], DefaultOptionScalesAxesX);
        }

        /// <summary>
        /// Options for the y-axes.
        /// </summary>
        /// <value>
        /// An array of <see cref="T:Wisej.Web.Ext.ChartJS.OptionScalesAxesY"/> objects, one for each y-axis.
        /// When not set, the getter creates an array containing a single default y-axis.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>options.scales.yAxes</c> array. To use more than one y-axis,
        /// assign a new array and give each axis a unique <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Id"/>.
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">An axis in the array already belongs to another set of options.</exception>
        /// <example>
        /// <code><![CDATA[
        /// var options = (LineOptions)this.chartJS1.Options;
        /// options.Scales.yAxes = new OptionScalesAxesY[]
        /// {
        ///     new OptionScalesAxesY { Id = "sales", Position = HeaderPosition.Left },
        ///     new OptionScalesAxesY { Id = "percent", Position = HeaderPosition.Right }
        /// };
        /// options.Scales.yAxes[1].Ticks.Max = 100;
        /// ]]></code>
        /// </example>
        [TypeConverter(typeof(ArrayConverter))]
        [Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public OptionScalesAxesY[] yAxes
        {
            get
            {
                if (this._yAxes == null)
                {
                    this.yAxes = new OptionScalesAxesY[1]
                    {
                        (OptionScalesAxesY)this.DefaultOptionScalesAxesY.Clone()
                    };
                }

                return this._yAxes;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                this._yAxes = value;

                if (this._yAxes != null)
                {
                    foreach (var x in this._yAxes)
                        x.Owner = this;
                }
            }
        }

        private OptionScalesAxesY[] _yAxes;

        private OptionScalesAxesY DefaultOptionScalesAxesY
        {
            get
            {
                if (this._defaultOptionScalesAxesY == null)
                    this._defaultOptionScalesAxesY = new OptionScalesAxesY(this);

                return this._defaultOptionScalesAxesY;
            }
        }

        private OptionScalesAxesY _defaultOptionScalesAxesY;

        private bool ShouldSerializeyAxes()
        {
            return this._yAxes != null && this._yAxes.Length > 0 && !Object.Equals(this._yAxes[0], DefaultOptionScalesAxesY);
        }
    }

    /// <summary>
    /// Represents the options for the scale axes.
    /// </summary>
    /// <remarks>
    /// This is the base class of <see cref="T:Wisej.Web.Ext.ChartJS.OptionScalesAxesX"/> and
    /// <see cref="T:Wisej.Web.Ext.ChartJS.OptionScalesAxesY"/> and maps to a single entry in the
    /// Chart.js <c>scales.xAxes</c> or <c>scales.yAxes</c> arrays.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// foreach (OptionScalesAxes axis in this.chartJS1.Options.Scales.yAxes)
    /// {
    ///     axis.GridLines.Display = false;
    ///     axis.Ticks.FontColor = Color.Gray;
    /// }
    /// ]]></code>
    /// </example>
    public abstract class OptionScalesAxes : OptionsBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <example>
        /// <code><![CDATA[
        /// // OptionScalesAxes is abstract; create one of the derived axis classes.
        /// OptionScalesAxes axis = new OptionScalesAxesY();
        /// axis.Stacked = true;
        /// this.chartJS1.Options.Scales.yAxes = new[] { (OptionScalesAxesY)axis };
        /// ]]></code>
        /// </example>
        public OptionScalesAxes()
        {
        }

        /// <summary>
        /// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionScalesAxes"/> set.
        /// </summary>
        /// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> set of options that owns this instance.</param>
        /// <remarks>
        /// When the owner belongs to a chart of type <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Pie"/>,
        /// <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Radar"/>, <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Doughnut"/>
        /// or <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.PolarArea"/>, <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Display"/>
        /// is initialized to false.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var scales = this.chartJS1.Options.Scales;
        /// OptionScalesAxes axis = new OptionScalesAxesX(scales);
        /// axis.Display = true;
        /// ]]></code>
        /// </example>
        public OptionScalesAxes(OptionsBase owner)
        {
            this.Owner = owner;

            var chart = this.Chart;
            if (chart != null)
            {
                switch (chart.ChartType)
                {
                    case ChartType.Pie:
                    case ChartType.Radar:
                    case ChartType.Doughnut:
                    case ChartType.PolarArea:
                        this.Display = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Specifies the configuration of the axis' grid lines.
        /// </summary>
        /// <value>
        /// An <see cref="T:Wisej.Web.Ext.ChartJS.OptionsAxisGridLines"/> object with the grid line options.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>gridLines</c> option of the axis.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var gridLines = this.chartJS1.Options.Scales.yAxes[0].GridLines;
        /// gridLines.DrawBorder = false;
        /// gridLines.BorderDash = new[] { 4, 4 };
        /// gridLines.Color = new[] { Color.LightGray };
        /// this.chartJS1.Update();
        /// ]]></code>
        /// </example>
        public OptionsAxisGridLines GridLines
        {
            get { return this._gridLines; }
            set
            {
                if (!this._gridLines.Equals(value))
                    this._gridLines = value;
            }
        }

        private OptionsAxisGridLines _gridLines = new OptionsAxisGridLines();

        /// <summary>
        /// If true, show the scale including grid lines, ticks, and labels.
        /// </summary>
        /// <value>
        /// Default is true. It is initialized to false for pie, radar, doughnut and polar area charts.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// // hide the x-axis completely.
        /// var options = (BarOptions)this.chartJS1.Options;
        /// options.Scales.xAxes[0].Display = false;
        /// ]]></code>
        /// </example>
        [DefaultValue(true)]
        [Description("If true, show the scale including grid lines, ticks, and labels.")]
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
        /// Used to identify the scale options for multi-axes charts.
        /// </summary>
        /// <value>
        /// Default is an empty string.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>id</c> option of the axis. Data sets are bound to an axis using
        /// the axis id. Changing this property doesn't update the chart automatically; call
        /// <see cref="M:Wisej.Web.Ext.ChartJS.OptionsBase.Update"/> to refresh it.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var scales = this.chartJS1.Options.Scales;
        /// scales.yAxes = new[] { new OptionScalesAxesY(), new OptionScalesAxesY() };
        /// scales.yAxes[0].Id = "left-axis";
        /// scales.yAxes[1].Id = "right-axis";
        /// scales.yAxes[1].Position = HeaderPosition.Right;
        /// ]]></code>
        /// </example>
        [DefaultValue("")]
        [Description("Used to identify the scale options for multi-axes charts")]
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        private string _id = "";

        /// <summary>
        /// Options for the chart ticks on the axes.
        /// </summary>
        /// <value>
        /// An <see cref="T:Wisej.Web.Ext.ChartJS.OptionsScalesTicks"/> object, created on first access.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>ticks</c> option of the axis.
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The value already belongs to another set of options.</exception>
        /// <example>
        /// <code><![CDATA[
        /// var ticks = ((LineOptions)this.chartJS1.Options).Scales.yAxes[0].Ticks;
        /// ticks.Min = 0;
        /// ticks.Max = 200;
        /// ticks.StepSize = 25;
        /// ]]></code>
        /// </example>
        [Description("Options for the chart ticks on the axes.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public OptionsScalesTicks Ticks
        {
            get
            {
                if (this._ticks == null)
                    this._ticks = new OptionsScalesTicks(this);

                return this._ticks;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                value.Owner = this;
                this._ticks = value;
            }
        }

        private OptionsScalesTicks _ticks;

        /// <summary>
        /// Options for the title (label) displayed along the axis.
        /// </summary>
        /// <value>
        /// An <see cref="T:Wisej.Web.Ext.ChartJS.OptionsScaleTitle"/> object, created on first access.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>scaleLabel</c> option of the axis. The label is shown only when
        /// <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScaleTitle.LabelString"/> is not empty.
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The value already belongs to another set of options.</exception>
        /// <example>
        /// <code><![CDATA[
        /// var label = this.chartJS1.Options.Scales.xAxes[0].ScaleLabel;
        /// label.LabelString = "Day of the week";
        /// label.FontColor = Color.DarkBlue;
        /// ]]></code>
        /// </example>
        [Description("Options for the title on the axes.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public OptionsScaleTitle ScaleLabel
        {
            get
            {
                if (this._scaleLabel == null)
                    this._scaleLabel = new OptionsScaleTitle(this);

                return this._scaleLabel;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                value.Owner = this;
                this._scaleLabel = value;
            }
        }

        private OptionsScaleTitle _scaleLabel;

        /// <summary>
        /// Type of scale being employed.
        /// </summary>
        /// <value>
        /// One of the <see cref="T:Wisej.Web.Ext.ChartJS.ScaleType"/> values. Default is <see cref="F:Wisej.Web.Ext.ChartJS.ScaleType.Linear"/>.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>type</c> option of the axis. When the type is
        /// <see cref="F:Wisej.Web.Ext.ChartJS.ScaleType.Time"/>, the <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Time"/> options are also used.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// // use a logarithmic y-axis.
        /// var yAxis = ((LineOptions)this.chartJS1.Options).Scales.yAxes[0];
        /// yAxis.Type = ScaleType.Logarithmic;
        /// ]]></code>
        /// </example>
        [DefaultValue(ScaleType.Linear)]
        [Description("Type of scale being employed")]
        public ScaleType Type
        {
            get { return this._type; }
            set
            {
                if (this._type != value)
                {
                    this._type = value;
                    Update();
                }
            }
        }

        private ScaleType _type = ScaleType.Linear;

        /// <summary>
        /// When true, the bars are stacked.
        /// </summary>
        /// <value>
        /// Default is false.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>stacked</c> option of the axis. To create a stacked bar chart,
        /// set this property to true on both the x-axis and the y-axis.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var scales = ((BarOptions)this.chartJS1.Options).Scales;
        /// scales.xAxes[0].Stacked = true;
        /// scales.yAxes[0].Stacked = true;
        /// ]]></code>
        /// </example>
        [DefaultValue(false)]
        [Description("When true, the bars are stacked.")]
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

        /// <summary>
        /// Options for the time scale type - ignored if not a time scale type.
        /// </summary>
        /// <value>
        /// A <see cref="T:Wisej.Web.Ext.ChartJS.ScaleTime"/> object, created on first access.
        /// </value>
        /// <remarks>
        /// These options are serialized only when <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Type"/>
        /// is set to <see cref="F:Wisej.Web.Ext.ChartJS.ScaleType.Time"/>.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var xAxis = ((LineOptions)this.chartJS1.Options).Scales.xAxes[0];
        /// xAxis.Type = ScaleType.Time;
        /// xAxis.Time.Unit = TimeScaleTimeUnit.month;
        /// xAxis.Time.TooltipFormat = "MMM YYYY";
        /// ]]></code>
        /// </example>
        [Description("Options for the time scale type - ignored if not a time scale type")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ScaleTime Time
        {
            get
            {
                if (this._time == null)
                    this._time = new ScaleTime(this);

                return this._time;
            }
            set
            {
                if (this._time != value)
                {
                    this._time = value;
                    Update();
                }
            }
        }

        private ScaleTime _time = null;

        private bool ShouldSerializeTime()
        {
            return this.Type == ScaleType.Time && this._time != null;
        }

        /// <summary>
        /// Percent (0-1) of the available width each bar should be within the category width.
        /// </summary>
        /// <value>
        /// Default is 0.9. A value of 1.0 makes the bars take the whole category width.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>barPercentage</c> option of the axis and applies to bar charts.
        /// Changing this property doesn't update the chart automatically; call
        /// <see cref="M:Wisej.Web.Ext.ChartJS.OptionsBase.Update"/> to refresh it.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var xAxis = ((BarOptions)this.chartJS1.Options).Scales.xAxes[0];
        /// xAxis.BarPercentage = 1.0F;
        /// xAxis.CategoryPercentage = 1.0F;
        /// this.chartJS1.Update();
        /// ]]></code>
        /// </example>
        [DefaultValue(0.9F)]
        [Description("Percent (0-1) of the available width each bar should be within the category width.")]
        public float BarPercentage
        {
            get { return this._barPercentage; }
            set
            {
                if (this._barPercentage != value)
                {
                    this._barPercentage = value;
                }
            }
        }

        private float _barPercentage = 0.9F;

        /// <summary>
        /// Percent (0-1) of the available width each category should be within the sample width.
        /// </summary>
        /// <value>
        /// Default is 0.8.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>categoryPercentage</c> option of the axis and applies to bar charts.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// // leave more space between the groups of bars.
        /// var xAxis = ((BarOptions)this.chartJS1.Options).Scales.xAxes[0];
        /// xAxis.CategoryPercentage = 0.5F;
        /// ]]></code>
        /// </example>
        [DefaultValue(0.8F)]
        [Description("Percent(0 - 1) of the available width each category should be within the sample width.")]
        public float CategoryPercentage
        {
            get { return this._categoryPercentage; }
            set
            {
                if (this._categoryPercentage != value)
                {
                    this._categoryPercentage = value;
                    Update();
                }
            }
        }

        private float _categoryPercentage = 0.8F;
    }

    /// <summary>
    /// Represents the options for the scale X axes.
    /// </summary>
    /// <remarks>
    /// The default <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxesX.Type"/> of an x-axis is
    /// <see cref="F:Wisej.Web.Ext.ChartJS.ScaleType.Category"/>.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// var xAxis = new OptionScalesAxesX();
    /// xAxis.Position = HeaderPosition.Top;
    /// xAxis.ScaleLabel.LabelString = "Product";
    /// this.chartJS1.Options.Scales.xAxes = new[] { xAxis };
    /// ]]></code>
    /// </example>
    public class OptionScalesAxesX : OptionScalesAxes
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// Initializes <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxesX.Type"/> to <see cref="F:Wisej.Web.Ext.ChartJS.ScaleType.Category"/>.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var xAxis = new OptionScalesAxesX { Id = "x-time", Type = ScaleType.Time };
        /// this.chartJS1.Options.Scales.xAxes = new[] { xAxis };
        /// ]]></code>
        /// </example>
        public OptionScalesAxesX()
        {
            this.Type = ScaleType.Category;
        }

        /// <summary>
        /// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionScalesAxesX"/> set.
        /// </summary>
        /// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> set of options that owns this instance.</param>
        /// <remarks>
        /// Initializes <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxesX.Type"/> to <see cref="F:Wisej.Web.Ext.ChartJS.ScaleType.Category"/>.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var scales = this.chartJS1.Options.Scales;
        /// var xAxis = new OptionScalesAxesX(scales);
        /// xAxis.GridLines.Display = false;
        /// ]]></code>
        /// </example>
        public OptionScalesAxesX(OptionsBase owner) : base(owner)
        {
            this.Type = ScaleType.Category;
        }

        /// <summary>
        /// Position of the X axes scale.
        /// </summary>
        /// <value>
        /// One of the <see cref="T:Wisej.Web.HeaderPosition"/> values. Default is <see cref="F:Wisej.Web.HeaderPosition.Bottom"/>.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>position</c> option of the axis. Use Top or Bottom for an x-axis.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var xAxis = ((LineOptions)this.chartJS1.Options).Scales.xAxes[0];
        /// xAxis.Position = HeaderPosition.Top;
        /// ]]></code>
        /// </example>
        [DefaultValue(HeaderPosition.Bottom)]
        [Description("Position of the x axes scale.")]
        public HeaderPosition Position
        {
            get { return this._position; }
            set
            {
                if (this._position != value)
                {
                    this._position = value;
                    Update();
                }
            }
        }

        private HeaderPosition _position = HeaderPosition.Bottom;

        /// <summary>
        /// Type of scale being employed.
        /// </summary>
        /// <value>
        /// One of the <see cref="T:Wisej.Web.Ext.ChartJS.ScaleType"/> values. Default is <see cref="F:Wisej.Web.Ext.ChartJS.ScaleType.Category"/>.
        /// </value>
        /// <remarks>
        /// With <see cref="F:Wisej.Web.Ext.ChartJS.ScaleType.Category"/> the axis labels are taken from
        /// <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Labels"/>.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var xAxis = ((ScatterOptions)this.chartJS1.Options).Scales.xAxes[0];
        /// xAxis.Type = ScaleType.Linear;
        /// ]]></code>
        /// </example>
        [DefaultValue(ScaleType.Category)]
        [Description("Type of scale being employed")]
        public new ScaleType Type
        {
            get { return base.Type; }
            set { base.Type = value; }
        }

        /// <summary>
        /// Rotation value of the X Axis labels, in degrees.
        /// </summary>
        /// <value>
        /// Default is 0.
        /// </value>
        /// <remarks>
        /// Setting a value greater than 0 sets both <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScalesTicks.MinRotation"/>
        /// and <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScalesTicks.MaxRotation"/> of <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Ticks"/>
        /// to the value and turns off <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScalesTicks.AutoSkip"/>, so that all
        /// labels are rotated by the same angle. Setting it to 0 turns <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScalesTicks.AutoSkip"/>
        /// back on but leaves the rotation values unchanged.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// // rotate the category labels by 45 degrees.
        /// var xAxis = ((BarOptions)this.chartJS1.Options).Scales.xAxes[0];
        /// xAxis.LabelRotation = 45;
        /// ]]></code>
        /// </example>
        [Description("Rotation value of the X Axis")]
        public int LabelRotation
        {
            get { return this._labelRotation; }
            set
            {
                if (value != this._labelRotation)
                {
                    this.Ticks.MaxRotation = value > 0 ? value : this.Ticks.MaxRotation;
                    this.Ticks.MinRotation = value > 0 ? value : this.Ticks.MinRotation;
                    this.Ticks.AutoSkip = value == 0 ? true : false;

                    this._labelRotation = value;
                    Update();
                }
            }
        }

        private int _labelRotation;
    }

    /// <summary>
    /// Represents the options for the scale Y axes.
    /// </summary>
    /// <remarks>
    /// The default <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Type"/> of a y-axis is
    /// <see cref="F:Wisej.Web.Ext.ChartJS.ScaleType.Linear"/>.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// var yAxis = new OptionScalesAxesY { Position = HeaderPosition.Right };
    /// yAxis.Ticks.BeginAtZero = true;
    /// this.chartJS1.Options.Scales.yAxes = new[] { yAxis };
    /// ]]></code>
    /// </example>
    public class OptionScalesAxesY : OptionScalesAxes
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <example>
        /// <code><![CDATA[
        /// var yAxis = new OptionScalesAxesY();
        /// yAxis.ScaleLabel.LabelString = "Temperature (°C)";
        /// this.chartJS1.Options.Scales.yAxes = new[] { yAxis };
        /// ]]></code>
        /// </example>
        public OptionScalesAxesY()
        {
        }

        /// <summary>
        /// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionScalesAxesY"/> set.
        /// </summary>
        /// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> set of options that owns this instance.</param>
        /// <example>
        /// <code><![CDATA[
        /// var scales = this.chartJS1.Options.Scales;
        /// var yAxis = new OptionScalesAxesY(scales);
        /// yAxis.Ticks.Mirror = true;
        /// ]]></code>
        /// </example>
        public OptionScalesAxesY(OptionsBase owner) : base(owner)
        {
        }

        /// <summary>
        /// Position of the Y axes scale.
        /// </summary>
        /// <value>
        /// One of the <see cref="T:Wisej.Web.HeaderPosition"/> values. Default is <see cref="F:Wisej.Web.HeaderPosition.Left"/>.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>position</c> option of the axis. Use Left or Right for a y-axis.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var yAxis = ((LineOptions)this.chartJS1.Options).Scales.yAxes[0];
        /// yAxis.Position = HeaderPosition.Right;
        /// ]]></code>
        /// </example>
        [DefaultValue(HeaderPosition.Left)]
        [Description("Position of the Y axes scale.")]
        public HeaderPosition Position
        {
            get { return this._position; }
            set
            {
                if (this._position != value)
                {
                    this._position = value;
                    Update();
                }
            }
        }

        private HeaderPosition _position = HeaderPosition.Left;

        /// <summary>
        /// Rotation value of the Y Axis labels, in degrees.
        /// </summary>
        /// <value>
        /// Default is 0.
        /// </value>
        /// <remarks>
        /// Setting a value greater than 0 sets both <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScalesTicks.MinRotation"/>
        /// and <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScalesTicks.MaxRotation"/> of <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Ticks"/>
        /// to the value and turns off <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScalesTicks.AutoSkip"/>, so that all
        /// labels are rotated by the same angle. Setting it to 0 turns <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScalesTicks.AutoSkip"/>
        /// back on but leaves the rotation values unchanged.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var yAxis = ((BarOptions)this.chartJS1.Options).Scales.yAxes[0];
        /// yAxis.LabelRotation = 30;
        /// ]]></code>
        /// </example>
        [Description("Rotation value of the X Axis")]
        public int LabelRotation
        {
            get { return this._labelRotation; }
            set
            {
                if (value != this._labelRotation)
                {
                    this.Ticks.MaxRotation = value > 0 ? value : this.Ticks.MaxRotation;
                    this.Ticks.MinRotation = value > 0 ? value : this.Ticks.MinRotation;
                    this.Ticks.AutoSkip = value == 0 ? true : false;

                    this._labelRotation = value;
                    Update();
                }
            }
        }

        private int _labelRotation;
    }

	/// <summary>
	/// Represents the options for the ticks element of the axes.
	/// </summary>
	/// <remarks>
	/// Maps to the Chart.js <c>ticks</c> option of an axis. Use the
	/// <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.Ticks"/> property to access it.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var ticks = ((LineOptions)this.chartJS1.Options).Scales.yAxes[0].Ticks;
	/// ticks.BeginAtZero = true;
	/// ticks.FontColor = Color.SteelBlue;
	/// ]]></code>
	/// </example>
	public class OptionsScalesTicks : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new OptionsScalesTicks { Min = 0, Max = 10, StepSize = 1 };
		/// this.chartJS1.Options.Scales.yAxes[0].Ticks = ticks;
		/// ]]></code>
		/// </example>
		public OptionsScalesTicks()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsScalesTicks"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> set of options that owns this instance.</param>
		/// <example>
		/// <code><![CDATA[
		/// var yAxis = this.chartJS1.Options.Scales.yAxes[0];
		/// var ticks = new OptionsScalesTicks(yAxis);
		/// ticks.Reverse = true;
		/// ]]></code>
		/// </example>
		public OptionsScalesTicks(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// User defined maximum number for the scale, overrides maximum value from data.
		/// </summary>
		/// <value>
		/// Default is null, the maximum is calculated from the data.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// // fixed 0-100 range for a percentage axis.
		/// var ticks = ((LineOptions)this.chartJS1.Options).Scales.yAxes[0].Ticks;
		/// ticks.Min = 0;
		/// ticks.Max = 100;
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("User defined maximum number for the scale, overrides maximum value from data.")]
		public int? Max
		{
			get { return this._max; }
			set
			{
				if (this._max != value)
				{
					this._max = value;
					Update();
				}
			}
		}
		private int? _max;

		/// <summary>
		/// User defined minimum number for the scale, overrides minimum value from data.
		/// </summary>
		/// <value>
		/// Default is null, the minimum is calculated from the data.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = ((BarOptions)this.chartJS1.Options).Scales.yAxes[0].Ticks;
		/// ticks.BeginAtZero = false;
		/// ticks.Min = -50;
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("User defined minimum number for the scale, overrides minimum value from data.")]
		public int? Min
		{
			get { return this._min; }
			set
			{
				if (this._min != value)
				{
					this._min = value;
					Update();
				}
			}
		}
		private int? _min;

		/// <summary>
		/// User defined fixed step size for the scale.
		/// </summary>
		/// <value>
		/// Default is null.
		/// </value>
		/// <remarks>
		/// If set, the scale ticks will be enumerated by multiple of stepSize, having one tick per increment.
		/// If not set, the ticks are labeled automatically using the nice numbers algorithm.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = ((LineOptions)this.chartJS1.Options).Scales.yAxes[0].Ticks;
		/// ticks.StepSize = 5;
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("User defined fixed step size for the scale.")]
		public int? StepSize
		{
			get { return this._stepSize; }
			set
			{
				if (this._stepSize != value)
				{
					this._stepSize = value;
					Update();
				}
			}
		}
		private int? _stepSize;


		/// <summary>
		/// Padding between the tick label and the axis. Only applicable to horizontal scales.
		/// </summary>
		/// <value>
		/// The padding in pixels. Default is 10.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = ((LineOptions)this.chartJS1.Options).Scales.xAxes[0].Ticks;
		/// ticks.Padding = 20;
		/// ]]></code>
		/// </example>
		[DefaultValue(10)]
		[Description("Padding between the tick label and the axis. Only applicable to horizontal scales.")]
		public int Padding
		{
			get { return this._padding; }
			set
			{
				if (this._padding != value)
				{
					this._padding = value;
					Update();
				}
			}
		}
		private int _padding = 10;

		/// <summary>
		/// Flips tick labels around axis, displaying the labels inside the chart instead of outside. Only applicable to vertical scales.
		/// </summary>
		/// <value>
		/// Default is false.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// // draw the y-axis labels inside the chart area.
		/// var ticks = ((LineOptions)this.chartJS1.Options).Scales.yAxes[0].Ticks;
		/// ticks.Mirror = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("Flips tick labels around axis, displaying the labels inside the chart instead of outside. Only applicable to vertical scales.")]
		public bool Mirror
		{
			get { return this._mirror; }
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
		/// Reverses order of tick labels.
		/// </summary>
		/// <value>
		/// Default is false.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// // show the highest values at the bottom of the y-axis.
		/// var ticks = ((BarOptions)this.chartJS1.Options).Scales.yAxes[0].Ticks;
		/// ticks.Reverse = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("Reverses order of tick labels.")]
		public bool Reverse
		{
			get { return this._reverse; }
			set
			{
				if (this._reverse != value)
				{
					this._reverse = value;
					Update();
				}
			}
		}
		private bool _reverse = false;

		/// <summary>
		/// Font of the tick labels.
		/// </summary>
		/// <value>
		/// Default is null. When not set, the getter returns the <see cref="P:Wisej.Web.Control.Font"/> of the chart.
		/// </value>
		/// <remarks>
		/// The font is converted on the client to the Chart.js <c>fontSize</c>, <c>fontFamily</c> and <c>fontStyle</c> tick options.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = ((LineOptions)this.chartJS1.Options).Scales.xAxes[0].Ticks;
		/// ticks.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Font of the tick labels.")]
		public Font Font
		{
			get
			{
				var chart = this.Chart;
				if (this._font == null && chart != null)
					return chart.Font;

				return this._font;
			}
			set
			{
				if (this._font != value)
				{
					this._font = value;
					Update();
				}
			}
		}
		private Font _font;

		/// <summary>
		/// Tick labels color.
		/// </summary>
		/// <value>
		/// Default is <see cref="F:System.Drawing.Color.Empty"/>. When not set, the getter returns the <see cref="P:Wisej.Web.Control.ForeColor"/> of the chart.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var scales = ((LineOptions)this.chartJS1.Options).Scales;
		/// scales.xAxes[0].Ticks.FontColor = Color.DimGray;
		/// scales.yAxes[0].Ticks.FontColor = Color.DimGray;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[Description("Tick labels color.")]
		public Color FontColor
		{
			get
			{
				var chart = this.Chart;
				if (this._fontColor.IsEmpty && chart != null)
					return chart.ForeColor;

				return this._fontColor;
			}
			set
			{
				if (this._fontColor != value)
				{
					this._fontColor = value;
					Update();
				}
			}
		}
		private Color _fontColor;

		/// <summary>
		/// If true, scale will include 0 if it is not already included.
		/// </summary>
		/// <value>
		/// Default is true.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// // let the y-axis start near the lowest value in the data.
		/// var ticks = ((LineOptions)this.chartJS1.Options).Scales.yAxes[0].Ticks;
		/// ticks.BeginAtZero = false;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("If true, scale will include 0 if it is not already included.")]
		public bool BeginAtZero
		{
			get { return this._beginAtZero; }
			set
			{
				if (this._beginAtZero != value)
				{
					this._beginAtZero = value;
					Update();
				}
			}
		}
		private bool _beginAtZero = true;

		/// <summary>
		/// Minimum rotation of the tick labels, in degrees.
		/// </summary>
		/// <value>
		/// Default is 0.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js <c>minRotation</c> tick option. Changing this property doesn't update
		/// the chart automatically; call <see cref="M:Wisej.Web.Ext.ChartJS.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = ((BarOptions)this.chartJS1.Options).Scales.xAxes[0].Ticks;
		/// ticks.MinRotation = 30;
		/// ticks.MaxRotation = 90;
		/// this.chartJS1.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
		[Description("Min Rotation value of the tick")]
		public int MinRotation
		{
			get { return this._minRotation; }
			set
			{
				if (this._minRotation != value)
				{
					this._minRotation = value;
				}
			}
		}
		private int _minRotation = 0;

        /// <summary>
        /// Maximum rotation of the tick labels, in degrees.
        /// </summary>
        /// <value>
        /// Default is 50.
        /// </value>
        /// <remarks>
        /// Maps to the Chart.js <c>maxRotation</c> tick option. When the labels don't fit, they are
        /// rotated up to this angle before <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScalesTicks.AutoSkip"/> skips any.
        /// Changing this property doesn't update the chart automatically; call
        /// <see cref="M:Wisej.Web.Ext.ChartJS.OptionsBase.Update"/> to refresh it.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// // never rotate the x-axis labels.
        /// var ticks = ((LineOptions)this.chartJS1.Options).Scales.xAxes[0].Ticks;
        /// ticks.MaxRotation = 0;
        /// this.chartJS1.Update();
        /// ]]></code>
        /// </example>
        [DefaultValue(50)]
        [Description("Max Rotation value of the tick")]
        public int MaxRotation
        {
            get { return this._maxRotation; }
            set
            {
                if (this._maxRotation != value)
                {
                    this._maxRotation = value;
                }
            }
        }

        private int _maxRotation = 50;

		/// <summary>
		/// If true, automatically calculates how many labels can be shown and hides labels accordingly.
		/// Labels will be rotated up to <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScalesTicks.MaxRotation"/> before skipping any.
		/// Turn AutoSkip off to show all labels no matter what.
		/// </summary>
		/// <value>
		/// Default is true.
		/// </value>
		/// <remarks>
		/// Changing this property doesn't update the chart automatically; call
		/// <see cref="M:Wisej.Web.Ext.ChartJS.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // show every label on the x-axis.
		/// var ticks = ((BarOptions)this.chartJS1.Options).Scales.xAxes[0].Ticks;
		/// ticks.AutoSkip = false;
		/// this.chartJS1.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("Automatically calculates how many labels can be shown and hides labels accordingly")]
		public bool AutoSkip
		{
			get
			{
				return this._autoSkip;
			}

			set
			{
				if (value != this._autoSkip)
				{
					this._autoSkip = value;
				}
			}
		}
		private bool _autoSkip = true;
	}

	/// <summary>
	/// Represents the options for the scale title (the label displayed along an axis).
	/// </summary>
	/// <remarks>
	/// Maps to the Chart.js <c>scaleLabel</c> option of an axis. Use the
	/// <see cref="P:Wisej.Web.Ext.ChartJS.OptionScalesAxes.ScaleLabel"/> property to access it.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var title = ((LineOptions)this.chartJS1.Options).Scales.yAxes[0].ScaleLabel;
	/// title.LabelString = "Visitors";
	/// title.Font = new Font("Arial", 12F);
	/// ]]></code>
	/// </example>
	public class OptionsScaleTitle : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var title = new OptionsScaleTitle { LabelString = "Hours" };
		/// this.chartJS1.Options.Scales.xAxes[0].ScaleLabel = title;
		/// ]]></code>
		/// </example>
		public OptionsScaleTitle()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsScaleTitle"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> set of options that owns this instance.</param>
		/// <example>
		/// <code><![CDATA[
		/// var yAxis = this.chartJS1.Options.Scales.yAxes[0];
		/// var title = new OptionsScaleTitle(yAxis);
		/// title.LabelString = "Amount";
		/// ]]></code>
		/// </example>
		public OptionsScaleTitle(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Font of the scale title.
		/// </summary>
		/// <value>
		/// Default is null. When not set, the getter returns the <see cref="P:Wisej.Web.Control.Font"/> of the chart.
		/// </value>
		/// <remarks>
		/// The font is converted on the client to the Chart.js <c>fontSize</c>, <c>fontFamily</c> and <c>fontStyle</c> scale label options.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = ((BarOptions)this.chartJS1.Options).Scales.xAxes[0].ScaleLabel;
		/// title.LabelString = "Region";
		/// title.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Scale label font.")]
		public Font Font
		{
			get
			{
				var chart = this.Chart;
				if (this._font == null && chart != null)
					return chart.Font;

				return this._font;
			}
			set
			{
				if (this._font != value)
				{
					this._font = value;
					Update();
				}
			}
		}
		private Font _font;

		/// <summary>
		/// Shows or hides the scale title.
		/// </summary>
		/// <value>
		/// Default is true. The getter returns false when <see cref="P:Wisej.Web.Ext.ChartJS.OptionsScaleTitle.LabelString"/> is empty,
		/// regardless of the value that was set.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var title = ((LineOptions)this.chartJS1.Options).Scales.yAxes[0].ScaleLabel;
		/// title.LabelString = "Sales";
		/// // temporarily hide the axis title.
		/// title.Display = false;
		/// ]]></code>
		/// </example>
		[Description("Show the scale label block.")]
		public bool Display
		{
			get { return this._display && !String.IsNullOrEmpty(this._labelString); }
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

		private bool ShouldSerializeDisplay()
		{
			return !this._display;
		}

		private void ResetDisplay()
		{
			this.Display = true;
		}

		/// <summary>
		/// Text of the scale title.
		/// </summary>
		/// <value>
		/// Default is an empty string. Setting it to null stores an empty string.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js <c>labelString</c> option. The title is displayed only when this text is not empty.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var scales = ((LineOptions)this.chartJS1.Options).Scales;
		/// scales.xAxes[0].ScaleLabel.LabelString = "Date";
		/// scales.yAxes[0].ScaleLabel.LabelString = "Price (USD)";
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		[Description("Label text.")]
		public string LabelString
		{
			get
			{
				return this._labelString;
			}
			set
			{
				value = value ?? string.Empty;

				if (this._labelString != value)
				{
					this._labelString = value;
					Update();
				}
			}
		}
		private string _labelString = string.Empty;

		/// <summary>
		/// Color of the scale title.
		/// </summary>
		/// <value>
		/// Default is <see cref="F:System.Drawing.Color.Empty"/>. When not set, the getter returns the <see cref="P:Wisej.Web.Control.ForeColor"/> of the chart.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var title = ((BarOptions)this.chartJS1.Options).Scales.yAxes[0].ScaleLabel;
		/// title.LabelString = "Units";
		/// title.FontColor = Color.Firebrick;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[Description("Scale label font color.")]
		public Color FontColor
		{
			get
			{
				var chart = this.Chart;
				if (this._fontColor.IsEmpty && chart != null)
					return chart.ForeColor;

				return this._fontColor;
			}
			set
			{
				if (this._fontColor != value)
				{
					this._fontColor = value;
					Update();
				}
			}
		}
		private Color _fontColor = Color.Empty;
	}
}
