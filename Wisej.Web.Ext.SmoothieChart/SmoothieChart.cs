///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Drawing;
using System.ComponentModel;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.SmoothieChart
{
	/// <summary>
	/// Represents a real-time streaming line chart based on the SmoothieChart JavaScript library
	/// <see href="http://smoothiecharts.org/"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The chart polls the server every <see cref="DataFrequency"/> milliseconds and fires the <see cref="DataNeeded"/>
	/// event once for each line in <see cref="TimeSeries"/>. The handler provides the value of the new point, which is
	/// appended to the line and scrolled from right to left, delayed by <see cref="UpdateDelay"/> milliseconds to
	/// produce a smooth animation.
	/// </para>
	/// <para>
	/// The <see cref="Control.BackColor"/> is used for the chart background and the <see cref="Control.ForeColor"/>
	/// for the labels.
	/// </para>
	/// </remarks>
	/// <example>
	/// Streaming the CPU usage and the memory usage in two lines:
	/// <code><![CDATA[
	/// var chart = new SmoothieChart
	/// {
	///     Dock = DockStyle.Fill,
	///     MinValue = 0,
	///     MaxValue = 100,
	///     DataFrequency = 1000,
	///     UpdateDelay = 2000
	/// };
	/// chart.TimeSeries.Add(new TimeSeries { LineColor = Color.Lime, LineWidth = 2 });
	/// chart.TimeSeries.Add(new TimeSeries { LineColor = Color.Orange, FillColor = Color.FromArgb(60, Color.Orange) });
	/// 
	/// chart.DataNeeded += (s, e) =>
	/// {
	///     e.Value = e.LineIndex == 0 ? GetCpuUsage() : GetMemoryUsage();
	/// };
	/// 
	/// this.Controls.Add(chart);
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(SmoothieChart))]
	[DefaultEvent("DataNeeded")]
	[Description("SmoothieChart is a JavaScript charting library for streaming data.")]
	[ApiCategory("SmoothieChart")]
	public class SmoothieChart : Control
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SmoothieChart"/> control.
		/// </summary>
		/// <remarks>
		/// The new chart has a black background, white labels and white grid lines, and it's not focusable.
		/// </remarks>
		public SmoothieChart()
		{
			this.TabStop = false;
			base.SetStyle(ControlStyles.Selectable, false);

			this.BackColor = Color.Black;
			this.ForeColor = Color.White;
			this.GridLineColor = Color.White;
		}

		#region Events

		/// <summary>
		/// Fired when the chart needs a data point.
		/// </summary>
		public event DataNeededEventHandler DataNeeded;

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.SmoothieChart.SmoothieChart.DataNeeded"/> event.
		/// </summary>
		/// <param name="e">Provides data for the <see cref="E:Wisej.Web.Ext.SmoothieChart.SmoothieChart.DataNeeded" /> event.</param>
		protected virtual void OnDataNeeded(DataNeededEventArgs e)
		{
			if (this.DataNeeded != null)
				DataNeeded(this, e);
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoSizeChanged
		{
			add { base.AutoSizeChanged += value; }
			remove { base.AutoSizeChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add { base.BackgroundImageChanged += value; }
			remove { base.BackgroundImageChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add { base.BackgroundImageLayoutChanged += value; }
			remove { base.BackgroundImageLayoutChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add { base.ImeModeChanged += value; }
			remove { base.ImeModeChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add { base.TextChanged += value; }
			remove { base.TextChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged
		{
			add { base.TabIndexChanged += value; }
			remove { base.TabIndexChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add { base.TabStopChanged += value; }
			remove { base.TabStopChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Enter
		{
			add { base.Enter += value; }
			remove { base.Enter -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Leave
		{
			add { base.Leave += value; }
			remove { base.Leave -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add { base.RightToLeftChanged += value; }
			remove { base.RightToLeftChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add { base.KeyDown += value; }
			remove { base.KeyDown -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add { base.KeyPress += value; }
			remove { base.KeyPress -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add { base.KeyUp += value; }
			remove { base.KeyUp -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CausesValidationChanged
		{
			add { base.CausesValidationChanged += value; }
			remove { base.CausesValidationChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Validated
		{
			add { base.Validated += value; }
			remove { base.Validated -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event CancelEventHandler Validating
		{
			add { base.Validating += value; }
			remove { base.Validating -= value; }
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the delay in milliseconds between the time of a data point and the moment it's displayed in the chart.
		/// </summary>
		/// <remarks>
		/// The chart draws the lines this many milliseconds in the past, so that the points received from the server
		/// are already available when they scroll into view. Use a value larger than <see cref="DataFrequency"/> plus the
		/// round-trip time to the server, otherwise the right end of the lines appears to jump. The default is 1000.
		/// </remarks>
		/// <example>
		/// Requesting a new point every 2 seconds and keeping the animation smooth:
		/// <code><![CDATA[
		/// this.smoothieChart1.DataFrequency = 2000;
		/// this.smoothieChart1.UpdateDelay = 3000;
		/// ]]></code>
		/// </example>
		[DefaultValue(1000)]
		[SRCategory("CatBehavior")]
		[Description("UpdateDelay in milliseconds. It is the time lag between the data received from the server and the screen update.")]
		public int UpdateDelay
		{
			get { return this._updateDelay; }
			set
			{
				if (this._updateDelay != value)
				{
					this._updateDelay = value;

					Update();
				}
			}
		}
		private int _updateDelay = 1000;

		/// <summary>
		/// Returns or sets the interval in milliseconds between the data requests sent to the server.
		/// </summary>
		/// <remarks>
		/// Each request fires the <see cref="DataNeeded"/> event once for every line in <see cref="TimeSeries"/>.
		/// A request is skipped if the previous one hasn't completed yet. Set to 0 to stop requesting data.
		/// The default is 500. See also <see cref="UpdateDelay"/>.
		/// </remarks>
		[DefaultValue(500)]
		[SRCategory("CatBehavior")]
		[Description("DataFrequency in milliseconds. It is the interval between data requests.")]
		public int DataFrequency
		{
			get { return this._dataFrequency; }
			set
			{
				if (this._dataFrequency != value)
				{
					this._dataFrequency = value;

					Update();
				}
			}
		}
		private int _dataFrequency = 500;

		/// <summary>
		/// Returns or sets the scroll speed expressed in milliseconds per pixel.
		/// </summary>
		/// <remarks>
		/// Each horizontal pixel represents this many milliseconds: higher values scroll more slowly and show a longer
		/// time span in the same width. The default is 20 (a 500 pixels wide chart shows 10 seconds).
		/// </remarks>
		[DefaultValue(20)]
		[SRCategory("CatBehavior")]
		[Description("ScrollSpeed in milliseconds/pixel.")]
		public int ScrollSpeed
		{
			get { return this._scrollSpeed; }
			set
			{
				if (this._scrollSpeed != value)
				{
					this._scrollSpeed = value;

					Update();
				}
			}
		}
		private int _scrollSpeed = 20;

		/// <summary>
		/// Returns or sets the background color of the chart.
		/// </summary>
		/// <remarks>
		/// The default is <see cref="Color.Black"/>.
		/// </remarks>
		[SRCategory("CatAppearance")]
		[SRDescription("ControlBackColorDescr")]
		[DefaultValue(typeof(Color), "Black")]
		public override Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		/// <summary>
		/// Returns or sets the color of the labels displayed in the chart.
		/// </summary>
		/// <remarks>
		/// The default is <see cref="Color.White"/>.
		/// </remarks>
		[SRCategory("CatAppearance")]
		[SRDescription("ControlBackColorDescr")]
		[DefaultValue(typeof(Color), "White")]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}

		/// <summary>
		/// Returns or sets the font size in pixels used to display the labels.
		/// </summary>
		[SRCategory("CatAppearance")]
		[Description("The font size used to display the labels.")]
		[DefaultValue(10)]
		public int FontSize
		{
			get { return this._fontSize; }
			set
			{
				if (this._fontSize != value)
				{
					this._fontSize = value;
					Update();
				}
			}
		}
		private int _fontSize = 10;

		/// <summary>
		/// Returns or sets the color of the grid lines.
		/// </summary>
		[DefaultValue(typeof(Color), "White")]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the color of the grid lines.")]
		public Color GridLineColor
		{
			get { return this._gridLineColor; }
			set
			{
				if (this._gridLineColor != value)
				{
					this._gridLineColor = value;
					Update();
				}
			}
		}
		private Color _gridLineColor = Color.White;

		/// <summary>
		/// Returns or sets the width of the grid lines in pixels.
		/// </summary>
		[DefaultValue(1)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the size of the grid lines.")]
		public int GidLineSize
		{
			get { return this._gridLineSize; }
			set
			{
				if (this._gridLineSize != value)
				{
					this._gridLineSize = value;
					Update();
				}
			}
		}
		private int _gridLineSize = 1;

		/// <summary>
		/// Returns or sets whether the control displays the minimum and maximum labels.
		/// </summary>
		[DefaultValue(true)]
		[DesignerActionList]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets whether the control displays the minimum and maximum labels.")]
		public bool ShowMinMaxLabels
		{
			get { return this._showMinMaxLabels; }
			set
			{
				if (this._showMinMaxLabels != value)
				{
					this._showMinMaxLabels = value;
					Update();
				}
			}
		}
		private bool _showMinMaxLabels = true;

		/// <summary>
		/// Returns or sets whether the control displays the time stamp labels.
		/// </summary>
		[DefaultValue(true)]
		[DesignerActionList]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets whether the control displays the time stamp labels.")]
		public bool ShowTimeStamps
		{
			get { return this._showTimeStamps; }
			set
			{
				if (this._showTimeStamps != value)
				{
					this._showTimeStamps = value;
					Update();
				}
			}
		}
		private bool _showTimeStamps = true;

		/// <summary>
		/// Returns or sets the border style for the control.
		/// </summary>
		/// <returns>One of the <see cref="T:Wisej.Web.BorderStyle" /> values. The default is BorderStyle.None.</returns>
		[DesignerActionList]
		[DefaultValue(BorderStyle.None)]
		[SRCategory("CatAppearance")]
		[SRDescription("PanelBorderStyleDescr")]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				return this._borderStyle;
			}
			set
			{
				if (this._borderStyle != value)
				{
					this._borderStyle = value;

					Refresh();
					OnStyleChanged(EventArgs.Empty);
				}
			}
		}
		private BorderStyle _borderStyle = BorderStyle.None;

		/// <summary>
		/// Returns or sets the number of vertical sections marked out by horizontal grid lines.
		/// </summary>
		[DefaultValue(2)]
		[DesignerActionList]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the number of vertical sections marked out by horizontal grid lines.")]
		public int VerticalSections
		{
			get { return this._verticalSections; }
			set
			{
				if (this._verticalSections != value)
				{
					this._verticalSections = value;
					Update();
				}
			}
		}
		private int _verticalSections = 2;

		/// <summary>
		/// Returns or sets the distance between the vertical grid lines in milliseconds/line.
		/// </summary>
		/// <remarks>
		/// The vertical grid lines scroll with the data; with the default value of 1000 there is a line every second.
		/// The distance in pixels is this value divided by <see cref="ScrollSpeed"/>.
		/// </remarks>
		[DefaultValue(1000)]
		[DesignerActionList]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the distance between the vertical grid lines in milliseconds/line.")]
		public int TimeLineSpacing
		{
			get { return this._timeLineSpacing; }
			set
			{
				if (this._timeLineSpacing != value)
				{
					this._timeLineSpacing = value;
					Update();
				}
			}
		}
		private int _timeLineSpacing = 1000;	

		/// <summary>
		/// Returns or sets the minimum value. Leave null to let the chart dynamically adjust the minimum value.
		/// </summary>
		/// <remarks>
		/// When null (default), the bottom of the chart follows the smallest visible value.
		/// Set both <see cref="MinValue"/> and <see cref="MaxValue"/> to use a fixed range.
		/// </remarks>
		/// <example>
		/// Using a fixed 0-100 range for percentage values:
		/// <code><![CDATA[
		/// this.smoothieChart1.MinValue = 0;
		/// this.smoothieChart1.MaxValue = 100;
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the minimum value. Leave null to let the chart dynamically adjust the minimum value.")]
		public int? MinValue
		{
			get { return this._minValue; }
			set
			{
				if (this._minValue != value)
				{
					this._minValue = value;
					Update();
				}
			}
		}
		private int? _minValue = null;

		/// <summary>
		/// Returns or sets the maximum value. Leave null to let the chart dynamically adjust the maximum value.
		/// </summary>
		/// <remarks>
		/// When null (default), the top of the chart follows the largest visible value. See <see cref="MinValue"/>.
		/// </remarks>
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the maximum value. Leave null to let the chart dynamically adjust the maximum value.")]
		public int? MaxValue
		{
			get { return this._maxValue; }
			set
			{
				if (this._maxValue != value)
				{
					this._maxValue = value;
					Update();
				}
			}
		}
		private int? _maxValue = null;

		/// <summary>
		/// Returns the collection of <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> lines to display in the chart.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The <see cref="DataNeeded"/> event is fired once for each line, the index of the line in this collection is
		/// passed in <see cref="DataNeededEventArgs.LineIndex"/>.
		/// </para>
		/// <para>
		/// Changing the collection sends all the lines to the client again, where they are recreated:
		/// the data points already plotted are cleared.
		/// </para>
		/// </remarks>
		/// <example>
		/// Adding two lines to the chart:
		/// <code><![CDATA[
		/// this.smoothieChart1.TimeSeries.Add(new TimeSeries { LineColor = Color.Red, LineWidth = 2 });
		/// this.smoothieChart1.TimeSeries.Add(new TimeSeries { LineColor = Color.DodgerBlue });
		/// ]]></code>
		/// </example>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TimeSeriesCollection TimeSeries
		{
			get
			{
				return
				  this._timeSeries =
					this._timeSeries ?? new TimeSeriesCollection(this);
			}
		}
		private TimeSeriesCollection _timeSeries;

		/// <summary>
		/// Returns or sets the type of interpolation used to connect the points of all the time series.
		/// </summary>
		[DefaultValue(InterpolationType.Bezier)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the type of interpolation to use when drawing this time series.")]
		public InterpolationType Interpolation
		{
			get { return this._interpolation; }
			set
			{
				if (this._interpolation != value)
				{
					this._interpolation = value;
					Update();
				}
			}
		}
		private InterpolationType _interpolation = InterpolationType.Bezier;

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string BackgroundImageSource
		{
			get { return base.BackgroundImageSource; }
			set { base.BackgroundImageSource = value; }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <returns>An <see cref="T:Wisej.Web.ImageLayout" />.</returns>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
			set { base.BackgroundImageLayout = value; }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Enabled
		{
			get { return base.Enabled; }
			set { base.Enabled = value; }
		}

		/// <summary>
		/// Returns or sets whether the user can give the focus to this control using the TAB key and
		/// the <see cref="P:Wisej.Base.ControlBase.Focusable"/> property is set to true.
		/// </summary>
		/// <returns>True if the control can receive the focus using the TAB key. The default is false.</returns>
		/// <remarks>
		/// The chart cannot receive the focus: assigning this property has no effect.
		/// </remarks>
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool TabStop
		{
			get { return base.TabStop; }
			set { }
		}

		/// <summary>
		/// Returns or sets whether the control can gain the focus.
		/// </summary>
		/// <returns>true if the control is focusable; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// The chart cannot receive the focus: assigning this property has no effect.
		/// </remarks>
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Focusable
		{
			get { return base.Focusable; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int TabIndex
		{
			get { return base.TabIndex; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AllowDrag
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AllowDrop
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override RightToLeft RightToLeft
		{
			get { return RightToLeft.No; }
			set { }
		}
			

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string AccessibleName
		{
			get { return string.Empty; }
			set { }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string AccessibleDescription
		{
			get { return string.Empty; }
			set { }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Font Font { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Starts, or resumes, the scrolling animation of the chart.
		/// </summary>
		/// <remarks>
		/// The chart starts automatically when it's first displayed. This method doesn't affect the data requests,
		/// which are controlled by <see cref="DataFrequency"/>.
		/// </remarks>
		/// <example>
		/// Resuming the chart:
		/// <code><![CDATA[
		/// private void buttonResume_Click(object sender, EventArgs e)
		/// {
		///     this.smoothieChart1.Start();
		/// }
		/// ]]></code>
		/// </example>
		public void Start()
		{
			Call("start");
		}

		/// <summary>
		/// Stops the scrolling animation of the chart, freezing the current view.
		/// </summary>
		/// <remarks>
		/// The chart keeps requesting data every <see cref="DataFrequency"/> milliseconds and firing <see cref="DataNeeded"/>;
		/// set <see cref="DataFrequency"/> to 0 to stop the requests as well.
		/// </remarks>
		/// <example>
		/// Pausing the chart and the data requests:
		/// <code><![CDATA[
		/// private void buttonPause_Click(object sender, EventArgs e)
		/// {
		///     this.smoothieChart1.Stop();
		///     this.smoothieChart1.DataFrequency = 0;
		/// }
		/// ]]></code>
		/// </example>
		public void Stop()
		{
			Call("stop");
		}

		/// <summary>
		/// Returns the data requested by the widget.
		/// </summary>
		/// <returns>An array with one element for each <see cref="TimeSeries"/> line, each with the <c>time</c> and <c>value</c> of the new point.</returns>
		/// <remarks>
		/// This method is called by the client widget every <see cref="DataFrequency"/> milliseconds; it fires the
		/// <see cref="DataNeeded"/> event once for each line and collects the returned values. Applications don't
		/// normally need to call it directly.
		/// </remarks>
		/// <example>
		/// Collecting the next data points on the server, i.e. in a unit test:
		/// <code><![CDATA[
		/// var points = (object[])this.smoothieChart1.GetData();
		/// System.Diagnostics.Debug.WriteLine($"{points.Length} points generated.");
		/// ]]></code>
		/// </example>
		[WebMethod]
		public object GetData()
		{
			dynamic[] data = null;
			data = new dynamic[this.TimeSeries.Count];

			var args = new DataNeededEventArgs(0, DateTime.Now);
			for (int i = 0; i < this.TimeSeries.Count; i++)
			{
				args.LineIndex = i;
				OnDataNeeded(args);
				data[i] = new { time = args.TimeStamp, value = args.Value };
			}
			return data;
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);
			IWisejComponent me = this;

			// change the widget class name.
			config.className = "wisej.web.ext.SmoothieChart";

			// render our new properties.
			config.fontSize = this.FontSize;
			config.borderStyle = this.BorderStyle;
			config.updateDelay = this.UpdateDelay;
			config.dataFrequency = this.DataFrequency;
			config.scrollSpeed = this.ScrollSpeed;
			config.verticalSections = this.VerticalSections;
			config.timeLineSpacing = this.TimeLineSpacing;
			config.showMinMaxLabels = this.ShowMinMaxLabels;
			config.showTimeStamps = this.ShowTimeStamps;
			config.interpolation = this.Interpolation;
			config.gridLineColor = this.GridLineColor;
			config.gridLineSize = this.GidLineSize;
			config.minValue = this.MinValue.HasValue ? (object)this.MinValue.Value : null;
			config.maxValue = this.MaxValue.HasValue ? (object)this.MaxValue.Value : null;			

			if (me.IsNew || this.TimeSeries.IsDirty)
				config.timeSeries = this.TimeSeries.Render();

			// register the web methods in the control.
			// wisej does it automatically only for top level controls: Page, Form, Desktop.
			config.webMethods = new[] { "GetData" };

		}

		#endregion
	}
}