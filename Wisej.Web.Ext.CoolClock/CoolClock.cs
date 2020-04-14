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

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.CoolClock
{
	/// <summary>
	/// CoolClock is a customizable javascript analog clock from http://randomibis.com/coolclock.
	/// </summary>
	[ToolboxBitmap(typeof(CoolClock))]
	public class CoolClock : Widget
	{
		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.CoolClock.CoolClock"/> control.
		/// </summary>
		public CoolClock()
		{
			// change the appearance key, in case we want to theme this component.
			this.AppearanceKey = "coolclock";
		}

		#region Properties

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool TabStop
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Focusable
		{
			get { return false; }
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
			set { base.TabIndex = value; }
		}

		/// <summary>
		/// Returns or sets the tick delay.
		/// </summary>
		[DefaultValue(1000)]
		public int TickDelay
		{
			get { return this._tickDelay; }
			set
			{

				if (this._tickDelay != value)
				{
					this._tickDelay = value;
					Update();
				}
			}
		}
		private int _tickDelay = 1000;

		/// <summary>
		/// Returns or sets the long tick delay.
		/// </summary>
		[DefaultValue(15000)]
		public int LongTickDelay
		{
			get { return this._longTickDelay; }
			set
			{
				if (this._longTickDelay != value)
				{

					this._longTickDelay = value;
					Update();
				}
			}
		}
		private int _longTickDelay = 15000;

		/// <summary>
		/// Shows or hides the second hand.
		/// </summary>
		[DefaultValue(true)]
		[DesignerActionList]
		public bool ShowSecondHand
		{
			get { return this._showSecondHand; }
			set
			{
				if (this._showSecondHand != value)
				{
					this._showSecondHand = value;
					Update();
				}
			}
		}
		private bool _showSecondHand = true;

		/// <summary>
		/// Returns or sets the skin used to render the CoolClock.
		/// </summary>
		[DesignerActionList]
		[DefaultValue(CoolClockSkin.ChunkySwiss)]
		public CoolClockSkin Skin
		{
			get { return this._skin; }

			set
			{
				if (this._skin != value)
				{
					this._skin = value;
					Update();
				}
			}
		}
		private CoolClockSkin _skin = CoolClockSkin.ChunkySwiss;

		/// <summary>
		/// Returns or sets the type of clock to render.
		/// </summary>
		[DefaultValue(CoolClockType.Standard)]
		public CoolClockType ClockType
		{
			get { return this._clockType; }

			set
			{
				if (this._clockType != value)
				{
					this._clockType = value;
					Update();
				}
			}
		}
		private CoolClockType _clockType = CoolClockType.Standard;

		/// <summary>
		/// Shows the digital clock.
		/// </summary>
		[DefaultValue(false)]
		public bool ShowDigital
		{
			get { return this._showDigital; }
			set
			{
				if (this._showDigital != value)
				{
					this._showDigital = value;
					Update();
				}
			}
		}
		private bool _showDigital = false;

		/// <summary>
		/// Returns or sets the GMT offset.
		/// </summary>
		[DefaultValue(0)]
		public int GmtOffset
		{
			get { return this._gmtOffset; }
			set
			{
				if (this._gmtOffset != value)
				{
					this._gmtOffset = value;
					Update();
				}
			}
		}
		private int _gmtOffset = 0;

		/// <summary>
		/// Overridden to create our initialization script.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			// disable inlining or we lose the calling assembly in GetResourceString().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get { return GetResourceString("Wisej.Web.Ext.CoolClock.JavaScript.startup.js"); }
			set { }
		}

		/// <summary>
		/// Overridden.
		/// </summary>
		[Browsable(false)]
		[WisejSerializerOptions(WisejSerializerOptions.None)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override dynamic Options
		{
			get
			{
				dynamic options = new DynamicObject();

				options.skinId = this.Skin;
				options.tickDelay = this.TickDelay;
				options.longTickDelay = this.LongTickDelay;
				options.showSecondHand = this.ShowSecondHand;
				options.showDigital = this.ShowDigital;
				options.gmtOffset = this.GmtOffset;
				options.logClock = this.ClockType == CoolClockType.Logarithmic;
				options.logClockRev = this.ClockType == CoolClockType.LogarithmicReversed;
				options.displayRadius =
					this.Width < this.Height
						? this.Width / 2
						: this.Height / 2;

				return options;
			}
			set { }
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
						Name = "coolclock.js",
						Source = GetResourceURL("Wisej.Web.Ext.CoolClock.JavaScript.coolclock.js")
					});
				}

				return base.Packages;
			}
		}

		#endregion
	}
}
