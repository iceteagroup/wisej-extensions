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
using System.Linq;
using System.Runtime.CompilerServices;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// FullCalendar is a drag-n-drop widget for displaying events on a full-sized calendar based on
	/// the open-source fullcalendar.io. See <see href="http://fullcalendar.io/"/>.
	/// </summary>
	/// <remarks>
	/// The FullCalendar JavaScript component is developed by Adam Shaw and released under the MIT license: <see href="http://fullcalendar.io/license/"/>.
	/// </remarks>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(FullCalendar))]
	[DefaultEvent("EventClick")]
	[Description("FullCalendar is a drag-n-drop widget for displaying events on a full-sized calendar based on the open-source fullcalendar.io. See <see href=\"http://fullcalendar.io\"/>")]
	[ApiCategory("FullCalendar")]
	public class FullCalendar : Widget, IWisejDataStore, IWisejControl
	{
		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> control.
		/// </summary>
		public FullCalendar()
		{
			// the control will issue a data load when it's created - we don't want to
			// call more refetchEvents while the server component is created and populated.
			this._inDataRead = true;
		}

		#region Events

		/// <summary>
		/// Triggered when the user clicks a day in the calendar.
		/// </summary>
		[Description("Triggered when the user clicks a day in the calendar.")]
		public event DayClickEventHandler DayClick
		{
			add { base.Events.AddHandler(nameof(DayClick), value); }
			remove { base.Events.RemoveHandler(nameof(DayClick), value); }
		}

		/// <summary>
		/// Triggered when the user double clicks a day in the calendar.
		/// </summary>
		[Description("Triggered when the user double clicks a day in the calendar.")]
		public event DayClickEventHandler DayDoubleClick
		{
			add { base.Events.AddHandler(nameof(DayDoubleClick), value); }
			remove { base.Events.RemoveHandler(nameof(DayDoubleClick), value); }
		}

		/// <summary>
		/// Triggered when the user drops an object on the calendar.
		/// </summary>
		[Description("Triggered when the user drops an object on the calendar.")]
		public event ItemDropEventHandler ItemDrop
		{
			add { base.Events.AddHandler(nameof(ItemDrop), value); }
			remove { base.Events.RemoveHandler(nameof(ItemDrop), value); }
		}

		/// <summary>
		/// Triggered when the user clicks an event.
		/// </summary>
		[Description("Triggered when the user clicks an event.")]
		public event EventClickEventHandler EventClick
		{
			add { base.Events.AddHandler(nameof(EventClick), value); }
			remove { base.Events.RemoveHandler(nameof(EventClick), value); }
		}

		/// <summary>
		/// Triggered when the user double clicks an event.
		/// </summary>
		[Description("Triggered when the user double clicks an event.")]
		public event EventClickEventHandler EventDoubleClick
		{
			add { base.Events.AddHandler(nameof(EventDoubleClick), value); }
			remove { base.Events.RemoveHandler(nameof(EventDoubleClick), value); }
		}

		/// <summary>
		/// Triggered when <see cref="P:Wisej.Web.Ext.FullCalendar.VirtualMode"/> is true
		/// and the control needs to populate the events on a certain date, week or month.
		/// </summary>
		/// <remarks>
		/// The application should manage and cache the events that are returned in response to this event.
		/// </remarks>
		[Description("Triggered when VirtualMode is true and the control needs to populate the events on a certain date, week or month.")]
		public event VirtualEventsNeededEventHandler VirtualEventsNeeded
		{
			add { base.Events.AddHandler(nameof(VirtualEventsNeeded), value); }
			remove { base.Events.RemoveHandler(nameof(VirtualEventsNeeded), value); }
		}

		/// <summary>
		/// Triggered when <see cref="P:Wisej.Web.Ext.FullCalendar.VirtualMode"/> is true
		/// and the control needs to retrieve a specific virtual event instance.
		/// </summary>
		[Description("Triggered when VirtualMode is true and the control needs to retrieve a specific event by index or ID.")]
		public event RetrieveVirtualEventEventHandler RetrieveVirtualEvent
		{
			add { base.Events.AddHandler(nameof(RetrieveVirtualEvent), value); }
			remove { base.Events.RemoveHandler(nameof(RetrieveVirtualEvent), value); }
		}

		/// <summary>
		/// Triggered when the user changed (dragged or resized) an <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> object.
		/// </summary>
		[Description("Triggered when the user changed (dragged or resized) an Event object.")]
		public event EventValueEventHandler EventChanged
		{
			add { base.Events.AddHandler(nameof(EventChanged), value); }
			remove { base.Events.RemoveHandler(nameof(EventChanged), value); }
		}

		/// <summary>
		/// Triggered when the current date used to display the calendar view is changed.
		/// </summary>
		[Description("Triggered when the current date used to display the calendar view is changed.")]
		public event EventHandler CurrentDateChanged
		{
			add { base.Events.AddHandler(nameof(CurrentDateChanged), value); }
			remove { base.Events.RemoveHandler(nameof(CurrentDateChanged), value); }
		}

		/// <summary>
		/// Triggered when the a <see cref="Resource"/> object changes.
		/// </summary>
		[Description("Triggered when the a Resource object changes.")]
		public event ResourceEventHandler ResourceChanged
		{
			add { base.Events.AddHandler(nameof(ResourceChanged), value); }
			remove { base.Events.RemoveHandler(nameof(ResourceChanged), value); }
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.DayClick"/> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnEventClick(EventClickEventArgs e)
		{
			((EventClickEventHandler)base.Events[nameof(EventClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.DayClick"/> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnDayClick(DayClickEventArgs e)
		{
			((DayClickEventHandler)base.Events[nameof(DayClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.DayDoubleClick"/> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnDayDoubleClick(DayClickEventArgs e)
		{
			((DayClickEventHandler)base.Events[nameof(DayDoubleClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.ItemDrop"/> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnItemDrop(ItemDropEventArgs e)
		{
			((ItemDropEventHandler)base.Events[nameof(ItemDrop)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.EventDoubleClick"/> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnEventDoubleClick(EventClickEventArgs e)
		{
			((EventClickEventHandler)base.Events[nameof(EventDoubleClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.FullCalendar.VirtualEventsNeeded" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.Ext.FullCalendar.EventsNeededEventArgs" /> that contains the event data. </param>
		protected virtual void OnVirtualEventsNeeded(VirtualEventsNeededEventArgs e)
		{
			((VirtualEventsNeededEventHandler)base.Events[nameof(VirtualEventsNeeded)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.FullCalendar.RetrieveVirtualEvent" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.Ext.FullCalendar.RetrieveVirtualEventEventArgs" /> that contains the event data. </param>
		protected virtual void OnRetrieveVirtualEvent(RetrieveVirtualEventEventArgs e)
		{
			((RetrieveVirtualEventEventHandler)base.Events[nameof(RetrieveVirtualEvent)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.FullCalendar.EventChanged" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.Ext.EventValueEventArgs" /> that contains the event data. </param>
		protected virtual void OnEventChanged(EventValueEventArgs e)
		{
			((EventValueEventHandler)base.Events[nameof(EventChanged)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.FullCalendar.ResourceChanged" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.Ext.ResourceEventArgs" /> that contains the event data. </param>
		protected virtual void OnResourceChanged(ResourceEventArgs e)
		{
			((ResourceEventHandler)base.Events[nameof(ResourceChanged)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.FullCalendar.CurrentDateChanged" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> instance that contains the event data. </param>
		protected virtual void OnCurrentDateChanged(EventArgs e)
		{
			((EventHandler)base.Events[nameof(CurrentDateChanged)])?.Invoke(this, e);
		}
		#endregion

		#region Properties

		/// <summary>
		/// Determines which view the calendar uses to display the events.
		/// </summary>		
		[DesignerActionList]
		[DefaultValue(ViewType.Month)]
		[Description("Determines which view the calendar uses to display the events.")]
		public ViewType View
		{
			get { return this._view; }
			set
			{
				if (this._view != value)
				{
					switch (value)
					{
						case ViewType.TimelineDay:
						case ViewType.TimelineMonth:
						case ViewType.TimelineWeek:
						case ViewType.TimelineYear:
							if (String.IsNullOrEmpty(this.SchedulerLicenseKey))
								throw new Exception("SchedulerLicenseKey is empty.");
							break;
					}

					this._view = value;

					IWisejControl me = this;
					if (me.DesignMode)
					{
						Update();
					}
					else if (!me.IsNew)
					{
						Call("exec", "changeView", value);
					}
				}
			}
		}
		private ViewType _view = ViewType.Month;

		/// <summary>
		/// License key for the scheduler plug-in.<br/>
		/// See <see href="https://fullcalendar.io/scheduler"/>.
		/// </summary>
		/// <remarks>
		/// Use "GPL-My-Project-Is-Open-Source" for GPL projects, or "CC-Attribution-NonCommercial-NoDerivatives"
		/// for non commercial projects.
		/// </remarks>
		[DefaultValue("")]
		[Description("License key for the scheduler plug-in.")]
		public string SchedulerLicenseKey
		{
			get { return this._schedulerLicenseKey; }
			set
			{
				if (this._schedulerLicenseKey != value)
				{
					this._schedulerLicenseKey = value;
					Update();
				}
			}
		}
		private string _schedulerLicenseKey = "";

		/// <summary>
		/// Determines the theme system used by the calendar.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The ThemeSystem property can be one of these values:
		/// </para>
		/// <list type="bullet">
		///		<item>
		///			<term>Standard</term>
		///			<description>Renders the built-in look &amp; feel.</description>
		///		</item>
		///		<item>
		///			<term>Bootstrap3</term>
		///			<description>Supports Bootstrap 3 themes.The Bootstrap CSS file must be loaded separately in its own link tag.</description>
		///		</item>
		///		<item>
		///			<term>jQueryUI</term>
		///			<description>Supports jQuery UI themes. The jQuery UI CSS file must be loaded separately loaded in its own link tag.</description>
		///		</item>
		/// </list>
		/// </remarks>
		[DesignerActionList]
		[DefaultValue(ThemeSystem.Standard)]
		[Description("Determines the theme system used by the calendar.")]
		public ThemeSystem ThemeSystem
		{
			get { return this._themeSystem; }
			set
			{
				if (this._themeSystem != value)
				{
					this._themeSystem = value;

					IWisejControl me = this;
					if (me.DesignMode)
					{
						Update();
					}
					else if (!me.IsNew)
					{
						Call("exec", "option", "themeSystem", TranslateThemeSystem(value));
					}
				}
			}
		}
		private ThemeSystem _themeSystem = ThemeSystem.Standard;

		/// <summary>
		/// Returns or sets the value that is used by <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> as today's date.
		/// </summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing today's date. The default value is the current system date.</returns>
		[DesignerActionList]
		[Description("Returns or sets the value that is used by FullCalendar as today's date.")]
		public DateTime TodayDate
		{
			get
			{
				if (this._todayDateSet)
					return this._todayDate;

				return DateTime.Now;
			}

			set
			{
				if (!this._todayDateSet || this._todayDate != value)
				{
					this._todayDate = value;
					this._todayDateSet = true;

					Update();
				}
			}
		}
		private bool _todayDateSet = false;
		private DateTime _todayDate = DateTime.Now;

		private bool ShouldSerializeTodayDate()
		{
			return this._todayDateSet;
		}

		private void ResetTodayDate()
		{
			this._todayDateSet = false;
			this._todayDate = DateTime.Now;

			Update();
		}

		/// <summary>
		/// Returns or sets the current value that is used by the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> to display the current view.
		/// </summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the currently displayed date/time. The default is the current system time.</returns>
		[DesignerActionList]
		[Description("Returns or sets the current value that is used by the FullCalendar to display the current view.")]
		public DateTime CurrentDate
		{
			get
			{
				return this._currentDate;
			}

			set
			{
				if (!this._currentDateSet || this._currentDate != value)
				{
					this._currentDate = value.Date;
					this._currentDateSet = true;

					if (this.DesignMode)
						Update();
					else
						GotoDate(value);

					OnCurrentDateChanged(EventArgs.Empty);
				}
			}
		}
		private bool _currentDateSet = false;
		private DateTime _currentDate = DateTime.Now;

		private bool ShouldSerializeCurrentDate()
		{
			return this._currentDateSet;
		}

		private void ResetCurrentDate()
		{
			this._currentDateSet = false;
			this._currentDate = DateTime.Now;
		}

		/// <summary>
		/// Determines the starting time that will be displayed, even when the scrollbars have been scrolled all the way up.
		/// </summary>
		[Description("Determines the starting time that will be displayed, even when the scrollbars have been scrolled all the way up.")]
		public TimeSpan MinTime
		{
			get { return this._minTime; }
			set
			{
				if (this._minTime != value)
				{
					this._minTime = value;
					Update();
				}
			}
		}
		private TimeSpan _minTime = new TimeSpan(0, 0, 0);

		private bool ShouldSerializeMinTime()
		{
			return this._minTime < DefaultMinTime;
		}

		private void ResetMinTime()
		{
			this.MinTime = DefaultMinTime;
		}

		private static readonly TimeSpan DefaultMinTime = new TimeSpan(0, 0, 0);

		/// <summary>
		/// Determines the end time (exclusively) that will be displayed, even when the scrollbars have been scrolled all the way down.
		/// </summary>
		[Description("Determines the end time (exclusively) that will be displayed, even when the scrollbars have been scrolled all the way down.")]
		public TimeSpan MaxTime
		{
			get { return this._maxTime; }
			set
			{
				if (this._maxTime != value)
				{
					this._maxTime = value;
					Update();
				}
			}
		}
		private TimeSpan _maxTime = DefaultMaxTime;

		private bool ShouldSerializeMaxTime()
		{
			return this._maxTime < DefaultMaxTime;
		}

		private void ResetMaxTime()
		{
			this.MaxTime = DefaultMaxTime;
		}

		private static readonly TimeSpan DefaultMaxTime = new TimeSpan(24, 0, 0);

		/// <summary>
		/// Determines how far down the scroll pane is initially scrolled down.
		/// </summary>
		[Description("Determines how far down the scroll pane is initially scrolled down.")]
		public TimeSpan ScrollTime
		{
			get { return this._scrollTime; }
			set
			{
				if (this._scrollTime != value)
				{
					this._scrollTime = value;
					Update();
				}
			}
		}
		private TimeSpan _scrollTime = DefaultScrollTime;

		private bool ShouldSerializeScrollTime()
		{
			return this._scrollTime != DefaultScrollTime;
		}

		private void ResetScrollTime()
		{
			this.ScrollTime = DefaultScrollTime;
		}

		private static readonly TimeSpan DefaultScrollTime = new TimeSpan(6, 0, 0);

		/// <summary>
		/// Determines how often the time-axis is labeled with text displaying the date/time of slots..
		/// </summary>
		/// <remarks>
		/// If not specified, this value is automatically computed from slotDuration.
		/// With slotDuration's default value of 30 minutes, this value will be 1 hour.
		/// </remarks>
		[Description("Determines how often the time-axis is labeled with text displaying the date/time of slots.")]
		public TimeSpan SlotLabelInterval
		{
			get { return this._slotLabelInterval; }
			set
			{
				if (this._slotLabelInterval != value)
				{
					this._slotLabelInterval = value;
					Update();
				}
			}
		}
		private TimeSpan _slotLabelInterval = DefaultSlotLabelInterval;

		private bool ShouldSerializeSlotLabelInterval()
		{
			return this._slotLabelInterval != DefaultSlotLabelInterval;
		}

		private void ResetSlotLabelInterval()
		{
			this.SlotLabelInterval = DefaultSlotLabelInterval;
		}

		private static readonly TimeSpan DefaultSlotLabelInterval = new TimeSpan(1, 0, 0);

		/// <summary>
		/// Determines the frequency for displaying time slots.
		/// </summary>
		[Description("Determines the frequency for displaying time slots.")]
		public TimeSpan SlotDuration
		{
			get { return this._slotDuration; }
			set
			{
				if (this._slotDuration != value)
				{
					this._slotDuration = value;
					Update();
				}
			}
		}
		private TimeSpan _slotDuration = DefaultSlotDuration;

		private bool ShouldSerializeSlotDuration()
		{
			return this._slotDuration != DefaultSlotDuration;
		}

		private void ResetSlotDuration()
		{
			this.SlotDuration = DefaultSlotDuration;
		}

		private static readonly TimeSpan DefaultSlotDuration = new TimeSpan(0, 30, 0);

		/// <summary>
		/// Determines the next-day threshold time.
		/// </summary>
		/// <remarks>
		/// <para>
		/// When an event's end time spans into another day, this is the minimum time 
		/// it must be in order for it to render as if it were on that day.
		/// </para>
		/// <para>
		/// Only affects timed events that appear on whole-days.
		/// Whole-day cells occur in month view, basicDay, basicWeek and the all-day slots in the agenda views.
		/// </para>
		/// </remarks>
		[Description("Determines the next-day threshold time.")]
		public TimeSpan NextDayThreshold
		{
			get { return this._nextDayThreshold; }
			set
			{
				if (this._nextDayThreshold != value)
				{
					this._nextDayThreshold = value;
					Update();
				}
			}
		}
		private TimeSpan _nextDayThreshold = new TimeSpan(9, 0, 0);

		private bool ShouldSerializeNextDayThreshold()
		{
			return this._nextDayThreshold < DefaultNextDayThreshold;
		}

		private void ResetNextDayThreshold()
		{
			this.NextDayThreshold = DefaultNextDayThreshold;
		}
		private static readonly TimeSpan DefaultNextDayThreshold = new TimeSpan(9, 0, 0);

		/// <summary>
		/// Determines the time-text that will be displayed on the vertical axis of the agenda views
		/// using momentjs format patterns: <see href="http://momentjs.com/docs/#/displaying/format/"/>.
		/// </summary>
		[Description("Determines the time-text that will be displayed on the vertical axis of the agenda views.")]
		public string SlotLabelFormat
		{
			get { return this._slotLabelFormat ?? "Default"; }
			set
			{
				if (value == string.Empty)
					value = null;

				if (this._slotLabelFormat != value)
				{
					this._slotLabelFormat = value;
					Update();
				}
			}
		}
		private string _slotLabelFormat = null;

		private bool ShouldSerializeSlotLabelFormat()
		{
			return this._slotLabelFormat != null;
		}

		private void ResetSlotLabelFormat()
		{
			this.SlotLabelFormat = null;
		}

		/// <summary>
		/// Determines the formatting of the column headers in the different views
		/// using momentjs format patterns: <see href="http://momentjs.com/docs/#/displaying/format/"/>.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Determines the formatting of the column headers in the different views.")]
		public ColumnHeaderFormats HeaderFormats
		{
			get
			{
				return this._columnHeaderFormat
					= this._columnHeaderFormat
						?? new ColumnHeaderFormats(this);
			}
		}
		private ColumnHeaderFormats _columnHeaderFormat;

		/// <summary>
		/// Returns or sets the first day of the week as displayed in the FullCalendar.
		/// </summary>
		/// <returns>One of the <see cref="T:Wisej.Web.Day" /> values. The default is <see cref="F:Wisej.Web.Day.Default" />.</returns>
		[Localizable(true)]
		[DefaultValue(Day.Default)]
		[Description("Returns or sets the first day of the week as displayed in the calendar.")]
		public Day FirstDayOfWeek
		{
			get
			{
				return this._firstDayOfWeek;
			}
			set
			{
				if (this._firstDayOfWeek != value)
				{
					this._firstDayOfWeek = value;

					Update();
				}
			}
		}
		private Day _firstDayOfWeek = Day.Default;

		/// <summary>
		/// Emphasizes certain time slots on the calendar. By default, Monday-Friday, 9am-5pm.
		/// </summary>
		[Localizable(true)]
		[DefaultValue(null)]
		[Description("Returns or sets the business hours to emphasizes on the calendar.")]
		public BusinessHours[] BusinessHours
		{
			get { return this._businessHours; }
			set
			{
				if (this._businessHours != value)
				{
					this._businessHours = value;

					Update();
				}
			}
		}
		private BusinessHours[] _businessHours;

		/// <summary>
		/// Limits the number of events displayed on a day.
		/// </summary>
		/// <remarks>
		/// When there are too many events, a link that looks like "+2 more" is displayed.
		/// The exact action that happens when the user clicks the link is determined by <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.EventLimitClick" />.
		/// </remarks>
		[DefaultValue(0)]
		[Description("Limits the number of events displayed on a day.")]
		public int EventLimit
		{
			get { return this._eventLimit; }
			set
			{
				if (value < 0)
					throw new ArgumentException("EventLimit can be 0 (unlimited) or a positive number: " + value);

				if (this._eventLimit != value)
				{
					this._eventLimit = value;
					Update();
				}
			}
		}
		private int _eventLimit = 0;

		/// <summary>
		/// Returns or sets whether you have provided your own data-management operations for the <see cref="T:Wisej.Web.Ext.FullCalendar" /> control.
		/// </summary>
		/// <returns>true if the <see cref="T:Wisej.Web.Ext.FullCalendar" /> uses data-management operations that you provide; otherwise, false. The default is false.</returns>
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Description("Returns or sets whether you have provided your own data-management to the FullCalendar control.")]
		public bool VirtualMode
		{
			get
			{
				return this._virtualMode;
			}
			set
			{
				if (this._virtualMode != value)
				{
					this._virtualMode = value;

					this._events?.Clear();
					ClientRefetchEvents();
				}
			}
		}
		private bool _virtualMode;

		/// <summary>
		/// Returns or sets the number of <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> objects contained in the list when in virtual mode.
		/// </summary>
		/// <returns>The number of <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> objects contained in the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> when in virtual mode.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.VirtualSize" /> is set to a value less than 0.</exception>
		[DefaultValue(0)]
		[Description("Returns or sets the number of Event objects contained in the list when in virtual mode.")]
		public int VirtualSize
		{
			get
			{
				return this._virtualSize;
			}
			set
			{
				if (this._virtualSize != value)
				{
					if (value < 0)
						throw new ArgumentException("VirtualSize cannot be less than 0.");

					this._virtualSize = value;

					if (this.VirtualMode)
						ClientRefetchEvents();
				}
			}
		}
		private int _virtualSize = 0;

		// Returns the number of events in the calendar.
		private int EventCount
		{
			get
			{
				return
					this.VirtualMode
						? this.VirtualSize
						: this._events?.Count ?? 0;
			}
		}

		/// <summary>
		/// Determines whether the events on the calendar can be modified.
		/// </summary>
		[DefaultValue(true)]
		[Description("Determines whether the events on the calendar can be modified.")]
		public bool Editable
		{
			get { return this._editable; }
			set
			{
				if (this._editable != value)
				{
					this._editable = value;
					Update();
				}
			}
		}
		private bool _editable = true;

		/// <summary>
		/// Returns or sets the message to display in one of the list views when there are no events to display.
		/// </summary>
		[Localizable(true)]
		[Description("Returns or sets the message to display in one of the list views when there are no events to display.")]
		public string NoEventsMessage
		{
			get { return this._noEventsMessage ?? this.DefaultNoEventsMessage; }
			set
			{
				if (value == "" || value == this.DefaultNoEventsMessage)
					value = null;

				if (value != this._noEventsMessage)
				{
					this._noEventsMessage = value;
					Update();
				}
			}
		}
		private string _noEventsMessage = null;

		private bool ShouldSerializeNoEventsMessage()
		{
			return this._noEventsMessage != null;
		}

		private void ResetNoEventsMessage()
		{
			this.NoEventsMessage = null;
		}

		/// <summary>
		/// Returns the default message to display in one if the list views when there are no events.
		/// </summary>
		[Browsable(false)]
		protected virtual string DefaultNoEventsMessage
		{
			get { return "No events to display"; }
		}

		/// <summary>
		/// Returns or sets the text titling the "all-day" slot at the top of the calendar.
		/// </summary>
		[Localizable(true)]
		[Description("Returns or sets the text titling the all-day slot at the top of the calendar.")]
		public string AllDayText
		{
			get { return this._allDayText ?? this.DefaultAllDayText; }
			set
			{
				if (value == "" || value == this.DefaultAllDayText)
					value = null;

				if (value != this._allDayText)
				{
					this._allDayText = value;
					Update();
				}
			}
		}
		private string _allDayText = null;

		private bool ShouldSerializeAllDayText()
		{
			return this._allDayText != null;
		}

		private void ResetAllDayText()
		{
			this._allDayText = null;
		}

		/// <summary>
		/// Returns the default The text titling the "all-day" slot at the top of the calendar.
		/// </summary>
		[Browsable(false)]
		protected virtual string DefaultAllDayText
		{
			get { return "All Day"; }
		}

		/// <summary>
		/// Determines the time-text that will be displayed on each event
		/// using momentjs format patterns: <see href="http://momentjs.com/docs/#/displaying/format/"/>.
		/// </summary>
		/// <remarks>
		/// Sets time format to display on the events.
		/// <code lang="cs">
		///		this.TimeFormat = "h:mm"; // shows 5:00
		///		this.TimeFormat = "h(:mm)t"; // shows 5p
		/// </code>
		/// </remarks>
		[DefaultValue(null)]
		[Description("Determines the time-text that will be displayed on each event.")]
		public string TimeFormat
		{
			get { return this._timeFormat; }
			set
			{
				if (this._timeFormat != value)
				{
					this._timeFormat = value;
					Update();
				}
			}
		}
		private string _timeFormat;

		/// <summary>
		/// Determines whether or not to display a marker indicating the current time.
		/// </summary>
		[DefaultValue(true)]
		[Description("Determines whether or not to display a marker indicating the current time.")]
		public bool ShowCurrentTime
		{
			get { return this._showCurrentTime; }
			set
			{
				if (this._showCurrentTime != value)
				{
					this._showCurrentTime = value;
					Update();
				}
			}
		}
		private bool _showCurrentTime = true;

		/// <summary>
		/// Determines if the "all-day" slot is displayed at the top of the calendar.
		/// </summary>
		[DefaultValue(true)]
		public bool AllDaySlot
		{
			get { return this._allDaySlot; }
			set
			{
				if (this._allDaySlot != value)
				{
					this._allDaySlot = value;
					Update();
				}
			}
		}
		private bool _allDaySlot = true;


		/// <summary>
		/// Determines if timed events in agenda view should visually overlap.
		/// </summary>
		[DefaultValue(true)]
		public bool SlotEventOverlap
		{
			get { return this._slotEventOverlap; }
			set
			{
				if (this._slotEventOverlap != value)
				{
					this._slotEventOverlap = value;
					Update();
				}
			}
		}
		private bool _slotEventOverlap = true;

		/// <summary>
		/// Sets the background color for all events in the calendar.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[Description("Sets the background color for all events in the calendar.")]
		public Color EventBackgroundColor
		{
			get
			{
				return this._eventBackgroundColor;
			}
			set
			{
				if (this._eventBackgroundColor != value)
				{
					this._eventBackgroundColor = value;
					Update();
				}
			}
		}
		private Color _eventBackgroundColor = Color.Empty;


		/// <summary>
		/// Sets the border color for all events on the calendar.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[Description("Sets the border color for all events on the calendar.")]
		public Color EventBorderColor
		{
			get
			{
				return this._eventBorderColor;
			}
			set
			{
				if (this._eventBorderColor != value)
				{
					this._eventBorderColor = value;
					Update();
				}
			}
		}
		private Color _eventBorderColor = Color.Empty;

		/// <summary>
		/// Sets the border color for all events on the calendar.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[Description("Sets the border color for all events on the calendar.")]
		public Color EventTextColor
		{
			get
			{
				return this._eventTextColor;
			}
			set
			{
				if (this._eventTextColor != value)
				{
					this._eventTextColor = value;
					Update();
				}
			}
		}
		private Color _eventTextColor = Color.Empty;

		/// <summary>
		/// Returns the collection of <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> managed by this <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> control.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new EventCollection Events
		{
			get
			{
				if (this._events == null)
					this._events = new EventCollection(this);

				return this._events;
			}
		}
		private EventCollection _events;

		/// <summary>
		/// Returns or sets the scheduler resources.
		/// Requires the <see cref="SchedulerLicenseKey"/> to be set to a valid license
		/// or to a GPL or CC license.
		/// </summary>
		[DefaultValue(null)]
		[Description("Returns or sets the scheduler resources.")]
		public Resource[] Resources
		{
			get { return this._resources; }
			set
			{
				if (this._resources != value)
				{
					if (this._resources != null)
					{
						foreach (var r in this._resources)
						{
							r.Owner = null;
						}
					}

					this._resources = value;

					if (this._resources != null)
					{
						foreach (var r in this._resources)
						{
							r.Owner = this;
						}
					}

					Update();
				}
			}
		}
		private Resource[] _resources;

		/// <summary>
		/// Returns or sets the text that will appear above the list of resources.
		/// Requires the <see cref="SchedulerLicenseKey"/> to be set to a valid license
		/// or to a GPL or CC license.
		/// </summary>
		[DefaultValue("Resources")]
		[Description("Returns or sets the text that will appear above the list of resources.")]
		public string ResourceLabelText
		{
			get { return this._resourceLabelText; }
			set
			{
				if (this._resourceLabelText != value)
				{
					this._resourceLabelText = value;
					Update();
				}
			}
		}
		private string _resourceLabelText = "Resources";

		/// <summary>
		/// Determines the width of the area that contains the list of resources.
		/// Requires the <see cref="SchedulerLicenseKey"/> to be set to a valid license
		/// or to a GPL or CC license.
		/// </summary>
		[DefaultValue("30%")]
		[Description("Determines the width of the area that contains the list of resources.")]
		public string ResourceAreaWidth
		{
			get { return this._resourceAreaWidth; }
			set
			{
				if (this._resourceAreaWidth != value)
				{
					this._resourceAreaWidth = value;
					Update();
				}
			}
		}
		private string _resourceAreaWidth = "30%";

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

		#endregion

		#region Methods

		/// <summary>
		/// Moves the calendar one step back (either by a month, week, or day).
		/// </summary>
		/// <remarks>
		/// If the calendar is in month view, will move the calendar back one month.
		/// If the calendar is in basicWeek or agendaWeek, will move the calendar back one week.
		///	If the calendar is in basicDay or agendaDay, will move the calendar back one day.
		/// </remarks>
		public void Previous()
		{
			Call("exec", "prev");
		}

		/// <summary>
		/// Moves the calendar one step forward (either by a month, week, or day).
		/// </summary>
		/// <remarks>
		/// If the calendar is in month view, will move the calendar forward one month.
		///	If the calendar is in basicWeek or agendaWeek, will move the calendar forward one week.
		///	If the calendar is in basicDay or agendaDay, will move the calendar forward one day.
		/// </remarks>
		public void Next()
		{
			Call("exec", "next");
		}

		/// <summary>
		/// Moves the calendar back one year.
		/// </summary>
		public void PreviousYear()
		{
			Call("exec", "prevYear");
		}

		/// <summary>
		/// Moves the calendar forward one year.
		/// </summary>
		public void NextYear()
		{
			Call("exec", "nextYear");
		}

		/// <summary>
		/// Moves the calendar to the current date.
		/// </summary>
		public void Today()
		{
			Call("exec", "today");
		}

		/// <summary>
		/// Moves the calendar to an arbitrary date.
		/// </summary>
		/// <param name="dateTime">The date to set the calendar view to.</param>
		public void GotoDate(DateTime dateTime)
		{
			Call("exec", "gotoDate", dateTime);
		}

		// Requests the specified virtual event. 
		internal Event OnRetrieveVirtualEvent(int index)
		{
			var args = new RetrieveVirtualEventEventArgs(index);
			OnRetrieveVirtualEvent(args);
			return args.Event;
		}

		// Requests the specified virtual event. 
		internal Event OnRetrieveVirtualEvent(string id)
		{
			var args = new RetrieveVirtualEventEventArgs(id);
			OnRetrieveVirtualEvent(args);
			return args.Event;
		}

		internal void OnEventRemoved(Event ev)
		{
			ClientRefetchEvents();
		}

		internal void OnEventAdded(Event ev)
		{
			ClientRefetchEvents();
		}

		internal void OnEventChanged(Event ev, DateTime oldStart, DateTime oldEnd)
		{
			OnEventChanged(new EventValueEventArgs(ev, oldStart, oldEnd));
			ClientRefetchEvents();
		}

		internal void OnResourceChanged(Resource resource)
		{
			OnResourceChanged(new ResourceEventArgs(resource));
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns the theme appearance key for this control.
		/// </summary>
		string IWisejControl.AppearanceKey
		{
			get { return this.AppearanceKey ?? "fullcalendar"; }
		}

		private bool _inDataRead;
		private bool _refetchScheduled;

		// Issues a refetchEvent call on the client after the request cycle is completed.
		private void ClientRefetchEvents()
		{

			if (this._refetchScheduled || this._inDataRead)
				return;

			if (this.IsDisposed || this.Disposing || !this.Created)
				return;

			this._refetchScheduled = true;

			Application.Post(() =>
			{

				Call("refetchEvents");
				this._refetchScheduled = false;
			});
		}


		// Processes the "eventResize" event from the web client.
		private void ProcessResizeWebEvent(WidgetEventArgs e)
		{
			var data = e.Data;
			string id = data.id;
			bool allDay = data.allDay ?? false;
			DateTime end = data.end ?? DateTime.MinValue;
			DateTime start = data.start ?? DateTime.MinValue;

			var ev = this.Events[id];
			if (ev != null)
			{
				var oldStart = ev.Start;
				var oldEnd = ev.End;
				ev.StartInternal = start;
				ev.EndInternal = end;
				ev.AllDayInternal = allDay;

				OnEventChanged(new EventValueEventArgs(ev, oldStart, oldEnd));
			}
		}

		// Processes the "eventDrop" event from the web client.
		private void ProcessDropWebEvent(WidgetEventArgs e)
		{
			var data = e.Data;
			string id = data.id as string;
			bool allDay = data.allDay ?? false;
			DateTime end = data.end ?? DateTime.MinValue;
			DateTime start = data.start ?? DateTime.MinValue;
			string resourceId = data.resourceId;

			var ev = this.Events[id];
			if (ev != null)
			{
				var oldStart = ev.Start;
				var oldEnd = ev.End;
				ev.StartInternal = start;
				ev.EndInternal = end;
				ev.AllDayInternal = allDay;
				ev.ResourceIdInternal = resourceId;

				OnEventChanged(new EventValueEventArgs(ev, oldStart, oldEnd));
			}
		}

		// Process "drop" events from Wisej converted to "itemDrop" carrying the date/time of the drop location.

		private void ProcessItemDropWebEvent(WidgetEventArgs e)
		{
			dynamic data = e.Data;
			DateTime day = data.date ?? DateTime.MinValue;
			if (day > DateTime.MinValue)
			{
				int x = data.x ?? 0;
				int y = data.y ?? 0;
				Control target = data.target;
				string resourceId = data.resourceId;
				var location = PointToClient(new Point(x, y));

				OnItemDrop(new ItemDropEventArgs(target, day, location, resourceId));
			}
		}

		// Processes the "currentDateChanged" event - fired when the calendar changes the
		// date that it's currently viewing.
		private void ProcessCurrentDateChangedWebEvent(WidgetEventArgs e)
		{
			DateTime date = e.Data;
			this._currentDate = date;
			OnCurrentDateChanged(EventArgs.Empty);
		}

		// Handles clicks on event items.
		private void ProcessEventClickWebEvent(WidgetEventArgs e)
		{
			dynamic data = e.Data;
			var id = data.id ?? "";
			if (!String.IsNullOrEmpty(id))
			{
				var ev = this.Events[id];
				if (ev != null)
				{
					// verify x and y are integers.
					int x = Convert.ToInt32(data.x);
					int y = Convert.ToInt32(data.y);
					var location = PointToClient(new Point(x, y));
					MouseButtons button = GetMouseButton(data.button ?? 0);

					if (e.Type == "eventClick")
						OnEventClick(new EventClickEventArgs(ev, button, 1, location));
					else if (e.Type == "eventDblClick")
						OnEventDoubleClick(new EventClickEventArgs(ev, button, 2, location));
				}
			}
		}

		// Handles clicks on a day in the calendar.
		private void ProcessDayClickWebEvent(WidgetEventArgs e)
		{
			dynamic data = e.Data;
			DateTime day = data.date ?? DateTime.MinValue;
			if (day > DateTime.MinValue)
			{
				int x = data.x ?? 0;
				int y = data.y ?? 0;
				var location = PointToClient(new Point(x, y));
				MouseButtons button = GetMouseButton(data.button ?? 0);

				if (e.Type == "dayClick")
					OnDayClick(new DayClickEventArgs(day, button, 1, location));
				else if (e.Type == "dayDblClick")
					OnDayDoubleClick(new DayClickEventArgs(day, button, 2, location));
			}
		}

		private static MouseButtons GetMouseButton(int button)
		{
			switch (button)
			{
				case 0: return MouseButtons.Left;
				case 1: return MouseButtons.Middle;
				case 2: return MouseButtons.Right;
				default:
					return MouseButtons.None;
			}
		}

		/// <summary>
		/// Handles events fired by the widget.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			switch (e.Type)
			{
				case "eventClick":
				case "eventDblClick":
					ProcessEventClickWebEvent(e);
					break;

				case "dayClick":
				case "dayDblClick":
					ProcessDayClickWebEvent(e);
					break;

				case "eventDrop":
					ProcessDropWebEvent(e);
					break;

				case "itemDrop":
					ProcessItemDropWebEvent(e);
					break;

				case "eventResize":
					ProcessResizeWebEvent(e);
					break;

				case "currentDateChanged":
					ProcessCurrentDateChangedWebEvent(e);
					break;

				default:
					base.OnWidgetEvent(e);
					break;
			}
		}

		/// <summary>
		/// Overridden, not used.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override dynamic Options
		{
			get { return null; }
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
					base.Packages.AddRange(new Package[] {
						new Package() {
							Name = "jquery.js",
							Source = GetResourceURL("Wisej.Web.Ext.FullCalendar.JavaScript.jquery-3.1.1.js")
						},
						new Package() {
							Name = "moment.js",
							Source = GetResourceURL("Wisej.Web.Ext.FullCalendar.JavaScript.moment-with-locales-2.17.1.js")
						},
						new Package() {
							Name = "fullcalendar.js",
							Source = GetResourceURL("Wisej.Web.Ext.FullCalendar.JavaScript.fullcalendar-3.9.0.js")
						},
						new Package() {
							Name = "fullcalendar.css",
							Source = GetResourceURL("Wisej.Web.Ext.FullCalendar.JavaScript.fullcalendar-3.9.0.css")
						}
					});

					if (!String.IsNullOrEmpty(this.SchedulerLicenseKey))
					{
						base.Packages.AddRange(new Package[] {
							new Package() {
								Name = "scheduler.js",
								Source = GetResourceURL("Wisej.Web.Ext.FullCalendar.JavaScript.scheduler-1.9.4.js")
							},
							new Package() {
								Name = "scheduler.css",
								Source = GetResourceURL("Wisej.Web.Ext.FullCalendar.JavaScript.scheduler-1.9.4.css")
							},
						});
					}
				}

				return base.Packages;
			}
		}

		private string BuildInitScript()
		{

			IWisejControl me = this;
			dynamic options = new DynamicObject();
			string script = GetResourceString("Wisej.Web.Ext.FullCalendar.JavaScript.startup.js");

			options.editable = this.Editable;
			options.eventBackgroundColor = this.EventBackgroundColor;
			options.eventBorderColor = this.EventBorderColor;
			options.eventTextColor = this.EventTextColor;
			options.now = this.TodayDate;
			options.defaultDate = this.CurrentDate;
			options.nowIndicator = this.ShowCurrentTime;
			options.noEventsMessage = this.NoEventsMessage;
			options.allDayText = this.AllDayText;
			options.allDaySlot = this.AllDaySlot;
			options.slotEventOverlap = this.SlotEventOverlap;
			options.minTime = this.MinTime.ToString();
			options.maxTime = this.MaxTime.ToString();
			options.nextDayThreshold = this.NextDayThreshold.ToString();
			options.slotLabelInterval = this.SlotLabelInterval.ToString();
			options.slotDuration = this.SlotDuration.ToString();
			options.scrollTime = this.ScrollTime.ToString();
			options.defaultView = this.View;
			options.themeSystem = TranslateThemeSystem(this.ThemeSystem);
			options.businessHours = this.BusinessHours;
			options.timeFormat = this.TimeFormat;

			if (this.ShouldSerializeSlotLabelFormat())
				options.slotLabelFormat = this.SlotLabelFormat;

			this.HeaderFormats.Render(options);

			options.firstDay = Math.Max(0, (int)this.FirstDayOfWeek);
			options.isRTL = this.RightToLeft == RightToLeft.Yes;
			options.eventLimit = this.EventLimit == 0 ? (object)false : (object)this.EventLimit;

			// scheduler properties.
			if (!String.IsNullOrEmpty(this.SchedulerLicenseKey))
			{
				options.resources = this.Resources;
				options.resourceLabelText = this.ResourceLabelText;
				options.resourceAreaWidth = this.ResourceAreaWidth;
				options.schedulerLicenseKey = this.SchedulerLicenseKey;
			}

			script = script.Replace("$options", options.ToString());
			return script;
		}

		private string TranslateThemeSystem(ThemeSystem name)
		{
			switch (name)
			{
				case ThemeSystem.JQueryUI: return "jquery-ui";
				case ThemeSystem.Bootstrap3: return "bootstrap3";
				case ThemeSystem.Standard: return "standard";

				default: return "standard";
			}
		}

		#endregion

		#region IWisejDataStore

		/// <summary>
		/// Returns the number of available records.
		/// </summary>
		/// <returns>The total number of rows.</returns>
		int IWisejDataStore.OnDataCount()
		{
			this._inDataRead = true;
			try
			{
				return OnWebDataCount();
			}
			finally
			{
				this._inDataRead = false;

			}
		}

		/// <summary>
		/// Returns a collection of records.
		/// </summary>
		/// <param name="data">Request data: first, last, sortIndex, sortDirection.</param>
		/// <returns>A collection of records.</returns>
		object IWisejDataStore.OnDataRead(dynamic data)
		{
			DateTime end = data.end;
			DateTime start = data.start;

			this._inDataRead = true;
			try
			{
				return OnWebDataRead(start, end);
			}
			finally
			{
				this._inDataRead = false;
			}
		}

		/// <summary>
		/// Returns the number of available data rows.
		/// </summary>
		/// <returns></returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual int OnWebDataCount()
		{
			return this.EventCount;
		}

		/// <summary>
		/// Returns the data requested by the client.
		/// </summary>
		/// <param name="start">The first <see cref="T:System.DateTime"/> date of the requested range.</param>
		/// <param name="end">The last <see cref="T:System.DateTime"/> date of the requested range, including <paramref name="end"/>.</param>
		/// <returns></returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual object OnWebDataRead(DateTime start, DateTime end)
		{

			object events = null;

			lock (this.Events)
			{
				// virtual mode? fire the eventsNeeded event.
				if (this.VirtualMode)
				{
					VirtualEventsNeededEventArgs args = new VirtualEventsNeededEventArgs(start, end);
					OnVirtualEventsNeeded(args);
					if (args.Events != null)
					{
						var list = args.Events.Where(o =>
							(o.Start >= start && o.Start <= end) || (o.End >= start && o.End <= end)
						);

						events = list;
					}
				}
				else
				{
					var list = this.Events.Where(o =>
						(o.Start >= start && o.Start <= end) || (o.End >= start && o.End <= end)
					);

					events = list;
				}
			}

			return events;
		}

		#endregion
	}
}
