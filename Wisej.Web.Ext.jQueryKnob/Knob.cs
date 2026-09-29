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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.jQueryKnob
{
	/// <summary>
	/// Represents a touchable dial control based on jQuery Knob (<see href="https://github.com/aterrien/jQuery-Knob"/>).
	/// </summary>
	/// <remarks>
	/// The user changes the <see cref="Value"/> by dragging the dial, scrolling the mouse wheel or typing in the
	/// input field displayed in the center (see <see cref="ShowInput"/>). The dial is always round and sized to the smaller
	/// of <see cref="Control.Width"/> and <see cref="Control.Height"/>; the arc is drawn using the
	/// <see cref="ForeColor"/> over the <see cref="Control.BackColor"/>.
	/// </remarks>
	/// <example>
	/// Creating a volume knob that spans 270 degrees:
	/// <code><![CDATA[
	/// var knob = new Knob
	/// {
	///     Size = new Size(150, 150),
	///     MinValue = 0,
	///     MaxValue = 11,
	///     Value = 5,
	///     AngleArc = 270,
	///     AngleOffset = 225,
	///     LineCapStyle = LineCapType.Round,
	///     ForeColor = Color.OrangeRed
	/// };
	/// knob.ValueChanged += (s, e) => SetVolume(knob.Value);
	/// this.Controls.Add(knob);
	/// ]]></code>
	/// </example>
	[ToolboxBitmap(typeof(Knob))]
	[DefaultEvent("ValueChanged")]
	[ApiCategory("jQueryKnob")]
	public class Knob : Widget
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Knob"/> control.
		/// </summary>
		public Knob()
		{
			this.ForeColor = Color.SkyBlue;
		}

		#region Events

		/// <summary>
		/// Fired when the user changes the value of the knob.
		/// </summary>
		public event EventHandler ValueChanged
		{
			add { base.AddHandler(nameof(ValueChanged), value); }
			remove { base.RemoveHandler(nameof(ValueChanged), value); }
		}

		/// <summary>
		/// Fires the ValueChanged event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnValueChanged(EventArgs e)
		{
			((EventHandler)base.Events[nameof(ValueChanged)])?.Invoke(this,e);			
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets whether the previous value is displayed while the user drags the knob.
		/// </summary>
		/// <remarks>
		/// When set to true, the arc of the previous value is drawn with a lighter color while the user is changing the value.
		/// </remarks>
		[DesignerActionList]
		[DefaultValue(false)]
		public bool DisplayPrevious
		{
			get { return this._displayPrevious; }
			set
			{
				if (this._displayPrevious != value)
				{
					this._displayPrevious = value;
					Update();
				}
			}
		}
		private bool _displayPrevious = false;

		/// <summary>
		/// Returns or sets whether the input field showing the value is displayed in the center of the dial.
		/// </summary>
		/// <remarks>
		/// The user can also type a new value in the input field, unless <see cref="ReadOnly"/> is true.
		/// The field uses the <see cref="Control.Font"/> of the control.
		/// </remarks>
		[DefaultValue(true)]
		[DesignerActionList]
		public bool ShowInput
		{
			get { return this._showInput; }
			set
			{
				if (this._showInput != value)
				{
					this._showInput = value;
					Update();
				}
			}
		}
		private bool _showInput = true;

		/// <summary>
		/// Returns or sets the current value of the knob.
		/// </summary>
		/// <remarks>
		/// The value should be between <see cref="MinValue"/> and <see cref="MaxValue"/>; it is not validated on the server.
		/// When the user changes the value, this property is updated and <see cref="ValueChanged"/> is fired
		/// continuously while the knob is dragged. Setting this property from code also causes the client to fire
		/// <see cref="ValueChanged"/> back to the server.
		/// </remarks>
		/// <example>
		/// Setting the value and reading it back when the user changes it:
		/// <code><![CDATA[
		/// this.knob1.Value = 42;
		///
		/// private void knob1_ValueChanged(object sender, EventArgs e)
		/// {
		///     this.labelTemperature.Text = this.knob1.Value + " °C";
		/// }
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
		[DesignerActionList]
		public int Value
		{
			get { return this._value; }
			set
			{
				if (this._value != value)
				{
					this._value = value;

					Update();
				}
			}
		}
		private int _value = 0;


		/// <summary>
		/// Returns or sets the minimum value.
		/// </summary>
		/// <remarks>
		/// The default is 0.
		/// </remarks>
		[DefaultValue(0)]
		[DesignerActionList]
		public int MinValue
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
		private int _minValue = 0;

		/// <summary>
		/// Returns or sets the maximum value.
		/// </summary>
		/// <remarks>
		/// The default is 100.
		/// </remarks>
		[DefaultValue(100)]
		[DesignerActionList]
		public int MaxValue
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
		private int _maxValue = 100;

		/// <summary>
		/// Returns or sets the style of the ends of the gauge arc.
		/// </summary>
		/// <remarks>
		/// <see cref="LineCapType.Butt"/> draws square ends; <see cref="LineCapType.Round"/> draws rounded ends.
		/// </remarks>
		[DesignerActionList]
		[DefaultValue(LineCapType.Butt)]
		public LineCapType LineCapStyle
		{
			get { return this._lineCapStyle; }

			set
			{
				if (this._lineCapStyle != value)
				{
					this._lineCapStyle = value;
					Update();
				}
			}
		}
		private LineCapType _lineCapStyle = LineCapType.Butt;

		/// <summary>
		/// Returns or sets the type of knob.
		/// </summary>
		/// <remarks>
		/// <see cref="jQueryKnob.KnobType.Gauge"/> fills the arc from the start angle to the value;
		/// <see cref="jQueryKnob.KnobType.Cursor"/> draws only a cursor at the value position, sized using <see cref="CursorSize"/>.
		/// </remarks>
		[DesignerActionList]
		[DefaultValue(KnobType.Gauge)]
		public KnobType KnobType
		{
			get { return this._knobType; }

			set
			{
				if (this._knobType != value)
				{
					this._knobType = value;
					Update();
				}
			}
		}
		private KnobType _knobType = KnobType.Gauge;

		/// <summary>
		/// Returns or sets the size of the cursor.
		/// </summary>
		/// <remarks>
		/// Applies only when <see cref="KnobType"/> is <see cref="jQueryKnob.KnobType.Cursor"/>. The cursor arc extends
		/// <c>CursorSize / 100</c> radians on each side of the value position. The default is 30.
		/// </remarks>
		/// <example>
		/// Displaying a narrow cursor instead of a filled gauge:
		/// <code><![CDATA[
		/// this.knob1.KnobType = KnobType.Cursor;
		/// this.knob1.CursorSize = 10;
		/// ]]></code>
		/// </example>
		[DefaultValue(30)]
		public int CursorSize
		{
			get { return this._cursorSize; }
			set
			{

				if (this._cursorSize != value)
				{
					this._cursorSize = value;
					Update();
				}
			}
		}
		private int _cursorSize = 30;

		/// <summary>
		/// Returns or sets whether the knob is read-only.
		/// </summary>
		/// <remarks>
		/// When set to true, the value of the knob cannot be changed by the user; it can still be changed
		/// by setting the <see cref="Value"/> property.
		/// </remarks>
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get { return this._readOnly; }
			set
			{
				if (this._readOnly != value)
				{
					this._readOnly = value;
					Update();
				}
			}
		}
		private bool _readOnly = false;

		/// <summary>
		/// Returns or sets the step size.
		/// </summary>
		/// <remarks>
		/// The value changes by multiples of this amount when the user drags the knob or uses the mouse wheel. The default is 1.
		/// </remarks>
		[DefaultValue(1)]
		public int Step
		{
			get { return this._step; }
			set
			{
				if (this._step != value)
				{
					this._step = value;
					Update();
				}
			}
		}
		private int _step = 1;

		/// <summary>
		/// Returns or sets the thickness of the gauge as a percentage of the radius of the dial.
		/// </summary>
		/// <remarks>
		/// The value is clamped between 1 and 100. A value of 100 fills the whole dial. The default is 24.
		/// </remarks>
		/// <example>
		/// Drawing a thin ring:
		/// <code><![CDATA[
		/// this.knob1.Thickness = 10; // 10% of the radius.
		/// ]]></code>
		/// </example>
		[DefaultValue(24)]
		public int Thickness
		{
			get { return this._thickness; }
			set
			{
				value = Math.Max(value, 1);
				value = Math.Min(value, 100);

				if (this._thickness != value)
				{
					this._thickness = value;
					Update();
				}
			}
		}
		private int _thickness = 24;

		/// <summary>
		/// Returns or sets the arc size in degrees.
		/// </summary>
		/// <remarks>
		/// The value is clamped between 0 and 360. The range from <see cref="MinValue"/> to <see cref="MaxValue"/>
		/// is mapped to this arc, starting at <see cref="AngleOffset"/>. The default is 360 (full circle).
		/// </remarks>
		/// <example>
		/// Drawing a half-circle gauge opened at the bottom:
		/// <code><![CDATA[
		/// this.knob1.AngleArc = 180;
		/// this.knob1.AngleOffset = 270; // start from the left (9 o'clock).
		/// ]]></code>
		/// </example>
		[DefaultValue(10)]
		public int AngleArc
		{
			get { return this._angleArc; }
			set
			{
				value = Math.Max(value, 0);
				value = Math.Min(value, 360);

				if (this._angleArc != value)
				{
					this._angleArc = value;
					Update();
				}
			}
		}
		private int _angleArc = 360;

		/// <summary>
		/// Returns or sets the starting angle in degrees.
		/// </summary>
		/// <remarks>
		/// The angle is measured clockwise from the top of the dial (12 o'clock) and is clamped between 0 and 360.
		/// The default is 0.
		/// </remarks>
		[DefaultValue(10)]
		public int AngleOffset
		{
			get { return this._angleOffset; }
			set
			{
				value = Math.Max(value, 0);
				value = Math.Min(value, 360);

				if (this._angleOffset != value)
				{
					this._angleOffset = value;
					Update();
				}
			}
		}
		private int _angleOffset = 0;

		/// <summary>
		/// Returns the initialization script that creates the knob on the client.
		/// </summary>
		/// <remarks>
		/// The script is built from the embedded <c>startup.js</c> resource and the current property values. The setter is ignored.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
			set { }
		}

		/// <summary>
		/// Returns or sets the color of the gauge arc and of the text in the input field.
		/// </summary>
		/// <remarks>
		/// The default is <see cref="Color.SkyBlue"/>. The unfilled part of the dial uses the <see cref="Control.BackColor"/>.
		/// </remarks>
		[DefaultValue(typeof(Color), "SkyBlue")]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}

		/// <summary>
		/// Returns the list of packages (jQuery and jQuery Knob) loaded on the client before the knob is created.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Package> Packages
		{
			// disable inlining or we lose the calling assembly in GetResourceString().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (base.Packages.Count == 0)
				{
					// initialize the loader with the required libraries.
					base.Packages.Add(new Package()
					{
						Name = "jquery.js",
						Source = GetResourceURL("Wisej.Web.Ext.jQueryKnob.JavaScript.jquery-3.1.1.js")
					});
					base.Packages.Add(new Package()
					{
						Name = "jqueryknob.js",
						Source = GetResourceURL("Wisej.Web.Ext.jQueryKnob.JavaScript.jquery.knob.js")
					});
				}

				return base.Packages;
			}
		}

		// disable inlining or we lose the calling assembly in GetResourceString().
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string BuildInitScript()
		{
			IWisejControl me = this;
			dynamic options = new DynamicObject();
			string script = GetResourceString("Wisej.Web.Ext.jQueryKnob.JavaScript.startup.js");

			options.min = this.MinValue;
			options.max = this.MaxValue;
			options.value = this.Value;
			options.step = this.Step;
			options.thickness = this.Thickness / 100d;
			options.angleArc = this._angleArc;
			options.angleOffset = this.AngleOffset;
			options.lineCap = this.LineCapStyle;
			options.displayInput = this.ShowInput;
			options.displayPrevious = this.DisplayPrevious;
			options.readOnly = this.ReadOnly;
			options.font = this.Font;
			options.fgColor = this.ForeColor;
			options.bgColor = this.BackColor;
			options.height = options.width = Math.Min(this.Width, this.Height);
			options.cursor =
				this.KnobType == KnobType.Gauge
					? 0
					: this.CursorSize;

			script = script.Replace("$options", options.ToJSON(WisejSerializerOptions.CamelCase));

			return script;
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Handles events fired by the widget.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			switch (e.Type)
			{
				case "valueChanged":
					this._value = e.Data;
					OnValueChanged(EventArgs.Empty);
					break;

				default:
					base.OnWidgetEvent(e);
					break;
			}
		}

		#endregion
	}
}
