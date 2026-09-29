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
	/// Represents an analog clock control based on the CoolClock JavaScript library
	/// (<see href="http://randomibis.com/coolclock"/>), rendered on an HTML canvas.
	/// </summary>
	/// <remarks>
	/// The clock face is always round; its radius is half of the smaller of <see cref="Control.Width"/>
	/// and <see cref="Control.Height"/> and it is centered horizontally. The time displayed is the browser's time,
	/// optionally shifted using <see cref="GmtOffset"/>.
	/// </remarks>
	/// <example>
	/// Adding a clock showing the time in Tokyo to a page:
	/// <code><![CDATA[
	/// var clock = new CoolClock
	/// {
	///     Size = new Size(200, 200),
	///     Skin = CoolClockSkin.Classic,
	///     ShowDigital = true,
	///     GmtOffset = 9
	/// };
	/// this.Controls.Add(clock);
	/// ]]></code>
	/// </example>
	[ToolboxBitmap(typeof(CoolClock))]
	[ApiCategory("CoolClock")]
	public class CoolClock : Widget
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CoolClock"/> control.
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
		/// Returns or sets the interval in milliseconds between redraws of the clock when <see cref="ShowSecondHand"/> is <c>true</c>.
		/// </summary>
		/// <remarks>
		/// When <see cref="ShowSecondHand"/> is <c>false</c>, <see cref="LongTickDelay"/> is used instead.
		/// The default is 1000 (one second).
		/// </remarks>
		/// <example>
		/// Setting the redraw intervals for both modes:
		/// <code><![CDATA[
		/// this.coolClock1.TickDelay = 1000;       // redraw every second with the second hand.
		/// this.coolClock1.LongTickDelay = 30000;  // redraw every 30 seconds without it.
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the interval in milliseconds between redraws of the clock when <see cref="ShowSecondHand"/> is <c>false</c>.
		/// </summary>
		/// <remarks>
		/// When <see cref="ShowSecondHand"/> is <c>true</c>, <see cref="TickDelay"/> is used instead.
		/// The default is 15000 (15 seconds).
		/// </remarks>
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
		/// Returns or sets whether the clock displays the second hand.
		/// </summary>
		/// <remarks>
		/// This property also determines whether the clock is redrawn every <see cref="TickDelay"/>
		/// or every <see cref="LongTickDelay"/> milliseconds.
		/// </remarks>
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
		/// Returns or sets the skin used to render the clock.
		/// </summary>
		/// <remarks>
		/// See <see cref="CoolClockSkin"/> for the available skins. The default is <see cref="CoolClockSkin.ChunkySwiss"/>.
		/// </remarks>
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
		/// <remarks>
		/// <see cref="CoolClockType.Logarithmic"/> and <see cref="CoolClockType.LogarithmicReversed"/> draw the
		/// hands and tick marks on a logarithmic scale instead of the standard linear dial.
		/// </remarks>
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
		/// Returns or sets whether the time is also displayed in digital format inside the clock face.
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
		/// Returns or sets the offset from GMT, in hours, of the time displayed by the clock.
		/// </summary>
		/// <remarks>
		/// When the value is not 0, the clock shows the UTC time plus the specified number of hours.
		/// When the value is 0 (the default), the clock shows the local time of the browser, not the GMT time.
		/// </remarks>
		/// <example>
		/// Displaying clocks for different time zones:
		/// <code><![CDATA[
		/// this.clockNewYork.GmtOffset = -5;
		/// this.clockBerlin.GmtOffset = 1;
		/// this.clockLocal.GmtOffset = 0; // browser's local time.
		/// ]]></code>
		/// </example>
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
		/// Returns the initialization script that creates the clock on the client.
		/// </summary>
		/// <remarks>
		/// The script is loaded from the embedded resource <c>startup.js</c> and cannot be changed; the setter is ignored.
		/// </remarks>
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
		/// Returns the options object sent to the client-side CoolClock instance.
		/// </summary>
		/// <remarks>
		/// The options are built from the <see cref="Skin"/>, <see cref="TickDelay"/>, <see cref="LongTickDelay"/>,
		/// <see cref="ShowSecondHand"/>, <see cref="ShowDigital"/>, <see cref="GmtOffset"/> and <see cref="ClockType"/>
		/// properties and the current size of the control. The setter is ignored; use the individual properties instead.
		/// </remarks>
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
		/// Returns the list of packages (the <c>coolclock.js</c> library) loaded on the client before the clock is created.
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
