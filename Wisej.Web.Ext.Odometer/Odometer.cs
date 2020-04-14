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

namespace Wisej.Web.Ext.Odometer
{
	/// <summary>
	/// Represents an odometer widget to display smoothly transitioning numbers. 
	/// 
	/// See: http://github.hubspot.com/odometer/
	/// 
	/// </summary>
	public class Odometer : Widget
	{
		/// <summary>
		/// Creates a new instance of the <see cref="T:Wisej.Web.Ext.Odometer"/> control.
		/// </summary>
		public Odometer()
		{
			base.TabStop = false;
			base.SetStyle(ControlStyles.Selectable, false);
		}

		#region Non Applicable Properties and Events

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
		public new event EventHandler FontChanged
		{
			add { base.FontChanged += value; }
			remove { base.FontChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add { base.BackColorChanged += value; }
			remove { base.BackColorChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add { base.ForeColorChanged += value; }
			remove { base.ForeColorChanged -= value; }
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
		/// Fired when a key is pressed when the control has focus.
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
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
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
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
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
		[DefaultValue(0)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		/// <returns>A <see cref="T:System.Drawing.Font" />.</returns>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
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
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AnchorStyles ResizableEdges
		{
			get { return AnchorStyles.None; }
			set { }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AccessibleRole AccessibleRole
		{
			get { return AccessibleRole.Default; }
			set { }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		/// <exclude/>
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
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string AccessibleDescription
		{
			get { return string.Empty; }
			set { }
		}

		#endregion

		/// <summary>
		/// Fired when the odometer reached the final value.
		/// </summary>
		public EventHandler OdometerDone;

		/// <summary>
		/// The default <see cref="T:System.Drawing.Size" /> of the control.
		/// </summary>
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 40);
			}
		}

		/// <summary>
		/// Returns or sets the value to display in the odometer.
		/// </summary>
		[DesignerActionList]
		[DefaultValue(0)]
		[Description("Gets or sets the value to display in the odometer.")]
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
		private int _value;

		/// <summary>
		/// Returns or sets the font size of the odometer in pixels.
		/// </summary>
		[DesignerActionList]
		[DefaultValue(13)]
		[Description("Gets or sets the font size of the odometer in pixels.")]
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
		private int _fontSize = 13;

		/// <summary>
		/// Returns or sets the duration of the animation to the final value, in milliseconds.
		/// </summary>
		[DesignerActionList]
		[DefaultValue(750)]
		[Description("Gets or sets the duration of the animation to the final value, in milliseconds.")]
		public int Duration
		{
			get { return this._duration; }
			set
			{
				if (this._duration != value)
				{
					this._duration = value;
					Update();
				}
			}
		}
		private int _duration = 750;

		/// <summary>
		/// Returns or sets the name of skin used to render the odometer.
		/// </summary>
		[DesignerActionList]
		[DefaultValue(OdometerSkin.Default)]
		public OdometerSkin Skin
		{
			get { return this._skin; }
			set {

				if (this._skin != value)
				{
					this._skin = value;
					Update();
				}
			}
		}
		private OdometerSkin _skin = OdometerSkin.Default;

		/// <summary>
		/// Overridden to create our initialization script.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			// disable inlining or we lose the calling assembly in GetResourceString().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get { return GetResourceString("Wisej.Web.Ext.Odometer.JavaScript.startup.js"); }
			set { }
		}

		/// <summary>
		/// Overridden.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override dynamic Options
		{
			get
			{
				dynamic options = new DynamicObject();

				options.theme = TranslateSkinName(this.Skin);
				options.value = this.Value;
				options.duration = this.Duration;
				options.fontSize = this.FontSize;
				return options;
			}
			set { }
		}

		private string TranslateSkinName(OdometerSkin skin)
		{
			switch (skin)
			{
				case OdometerSkin.Default:
					return "default";
				case OdometerSkin.Car:
					return "car";
				case OdometerSkin.Digital:
					return "digital";
				case OdometerSkin.Minimal:
					return "minimal";
				case OdometerSkin.Plaza:
					return "plaza";
				case OdometerSkin.SlotMachine:
					return "slot-machine";
				case OdometerSkin.TrainStation:
					return "train-station";
				default:
					return skin.ToString().ToLower();
			}
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
						Name = "odometer",
						Source = GetResourceURL("Wisej.Web.Ext.Odometer.JavaScript.odometer.js")
					});

					foreach (OdometerSkin skin in Enum.GetValues(typeof(OdometerSkin)))
					{
						base.Packages.Add(new Package()
						{
							Name = $"odometer-theme-{skin}",
							Source = GetResourceURL($"Wisej.Web.Ext.Odometer.Resources.odometer-theme-{TranslateSkinName(skin)}.css")
						});
					}
				}

				return base.Packages;
			}
		}

		/// <summary>
		/// Handles events from the widget.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			if (e.Type == "odometerDone" && this.OdometerDone != null)
				OdometerDone(this, EventArgs.Empty);


			base.OnWidgetEvent(e);
		}
	}
}
