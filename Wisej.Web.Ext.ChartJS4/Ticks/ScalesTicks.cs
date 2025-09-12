using System;
using System.ComponentModel;
using System.Drawing;
using static Wisej.Web.Ext.ChartJS4.ChartJS4;

namespace Wisej.Web.Ext.ChartJS4
{
	/// <summary>
	/// Represents the options for the ticks element of the axes.
	/// </summary>
	/// <remarks>
	/// See: https://www.chartjs.org/docs/latest/axes/styling.html#tick-configuration
	/// </remarks>
	public class ScalesTicks : OptionsBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ScalesTicks()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionsScalesTicks"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public ScalesTicks(OptionsBase owner)
		{
			this.Owner = owner;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Color of label backdrops.
		/// </summary>
		public Color BackdropColor
		{
			get
			{
				return this._backdropColor;
			}
			set
			{
				if (this._backdropColor != value) 
				{
					this._backdropColor = value;
					Update();
				}
			}
		}
		private Color _backdropColor = Color.FromArgb(191, 255, 255, 255);

		private void ResetBackdropColor()
		{
			this._backdropColor = Color.FromArgb(191, 255, 255, 255);
		}

		private bool ShouldSerializeBackdropColor()
		{
			return this._backdropColor != Color.FromArgb(191, 255, 255, 255);
		}

		/// <summary>
		/// Padding of label backdrop.
		/// </summary>
		[DefaultValue(2)]
		[Description("Padding of label backdrop.")]
		public int BackdropPadding
		{
			get
			{
				return this._backdropPadding;
			}
			set
			{
				if (this._backdropPadding != value)
				{
					this._backdropPadding = value;
					Update();
				}
			}
		}
		private int _backdropPadding = 2;

		/// <summary>
		/// Returns the string representation of the tick value as it should be 
		/// displayed on the chart using a <see cref="WidgetFunction"/>.
		/// </summary>
		/// <remarks>
		/// If a <see cref="WidgetFunction"/>, "tickCallback", is defined, 
		/// then the value of this property would be "()=>tickCallback".
		/// </remarks>
		[DefaultValue(null)]
		[Description("Returns the string representation of the tick value as it should be displayed on the chart using a WidgetFunction.")]
		public string Callback
		{
			get
			{
				return this._callback;
			}
			set
			{
				if (this._callback != value)
				{
					this._callback = value;
					Update();
				}
			}
		}
		private string _callback;

		/// <summary>
		/// If true, show tick labels.
		/// </summary>
		[DefaultValue(true)]
		[Description("If true, show tick labels.")]
		public bool Display
		{
			get
			{
				return this._display;
			}
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
		/// Padding between the tick label and the axis. Only applicable to horizontal scales.
		/// </summary>
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
		/// Font of the tick labels.
		/// </summary>
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

		private bool ShouldSerializeFont()
		{
			return this._font != null;
		}

		/// <summary>
		/// Tick labels color.
		/// </summary>
		[Description("Tick labels color.")]
		public Color Color
		{
			get
			{
				var chart = this.Chart;
				if (this._color.IsEmpty && chart != null)
					return chart.ForeColor;

				return this._color;
			}
			set
			{
				if (this._color != value)
				{
					this._color = value;
					Update();
				}
			}
		}
		private Color _color;

		private bool ShouldSerializeColor()
		{
			return this._color != Color.Empty;
		}

		/// <summary>
		///	Min Rotation value of the tick.
		/// </summary>
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
        ///	Max Rotation value of the tick.
        /// </summary>
        [DefaultValue(50)]
        [Description("Max Rotation value the tick")]
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
		/// If True, automatically calculates how many labels can be shown and hides labels accordingly.
		/// Labels will be rotated up to <see cref="MaxRotation"/> before skipping any.
		/// Turn AutoSkip off to show all labels no matter what.
		/// </summary>
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

		/// <summary>
		/// Gets or sets the step size for the ticks.
		/// </summary>
		/// <remarks>
		/// This property determines the interval between tick marks on the axis.
		/// </remarks>
		[Description("Gets or sets the step size for the ticks.")]
		[DefaultValue(1)]
		public int StepSize
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
		private int _stepSize = 1;

		/// <summary>
		/// TODO: Julie
		/// </summary>
		[DefaultValue("")]
		[Description("TODO: Julie")]
		public string Source
		{
			get { return this._source; }
			set
			{
				if (this._source != value)
				{
					this._source = value;
					Update();
				}
			}
		}
		private string _source = string.Empty;

		/// <summary>
		/// TODO: Julie
		/// </summary>
		[DefaultValue("center")]
		[Description("TODO: Julie")]
		public string Align
		{
			get
			{
				return this._align;
			}
			set
			{
				if(this._align != value)
				{
					this._align = value;
					Update();
				}
			}
		}
		private string _align = "center";

		#endregion

	}
}
