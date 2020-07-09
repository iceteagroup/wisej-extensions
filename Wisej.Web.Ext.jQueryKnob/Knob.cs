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
	/// Nice, downward compatible, touchable, jQuery dial from https://github.com/aterrien/jQuery-Knob.
	/// </summary>
	[ToolboxBitmap(typeof(Knob))]
	[DefaultEvent("ValueChanged")]
	public class Knob : Widget
	{
		/// <summary>
		/// Constructs a new <see cref="T: Wisej.Web.Ext.jQueryKnob.Knob"/> control.
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
		/// Returns or sets the DisplayPrevious property.
		/// When it's set to true, the widget briefly shows the previous
		/// value when the user is dragging the knob.
		/// </summary>
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
		/// Shows or hides the input field.
		/// </summary>
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
		/// Returns or sets the value.
		/// </summary>
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
		/// Returns or sets the skin used to render the jQueryKnob.
		/// </summary>
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
		/// Returns or sets the size of the cursor. Valid only when Type = KnobType.Cursor.
		/// </summary>
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
		/// Returns or sets the read only property.
		/// When set to true, the value of the knob cannot be changed by the user.
		/// </summary>
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
		/// Returns or sets the thickness of the gage.
		/// </summary>
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
		/// Overridden to create our initialization script.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
			set { }
		}

		/// <summary>
		/// Returns or sets the color of 
		/// </summary>
		[DefaultValue(typeof(Color), "SkyBlue")]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}

		/// <summary>
		/// Overridden to return our list of script resources.
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

			script = script.Replace("$options", options.ToString());

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
