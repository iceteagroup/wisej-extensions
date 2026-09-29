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
	/// <para>
	/// The control uses FullCalendar 3.9.0 (and the optional scheduler plug-in 1.9.4, see <see cref="SchedulerLicenseKey"/>).
	/// The calendar doesn't show its own header toolbar: navigate using <see cref="Previous"/>, <see cref="Next"/>,
	/// <see cref="Today"/>, <see cref="GotoDate"/> or <see cref="CurrentDate"/> and switch views using <see cref="View"/>.
	/// </para>
	/// <para>
	/// The events are loaded on demand: every time the visible date range changes, the client requests the events in that range
	/// from the <see cref="Events"/> collection or, when <see cref="VirtualMode"/> is true, through the <see cref="VirtualEventsNeeded"/> event.
	/// Changing most of the properties recreates the calendar on the client.
	/// </para>
	/// </remarks>
	/// <example>
	/// Creating a calendar in week view with a couple of events:
	/// <code><![CDATA[
	/// var calendar = new FullCalendar();
	/// calendar.Dock = DockStyle.Fill;
	/// calendar.View = ViewType.AgendaWeek;
	/// calendar.Events.Add(new Event(DateTime.Today.AddHours(9), TimeSpan.FromHours(1)) { Title = "Stand-up" });
	/// calendar.Events.Add(DateTime.Today.AddDays(1)).Title = "Company holiday";
	/// calendar.EventClick += (s, e) => AlertBox.Show(e.Event.Title);
	/// this.Controls.Add(calendar);
	/// ]]></code>
	/// </example>
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
		/// Triggered when the mouse enters the event control.
		/// </summary>
		public event EventMouseEnterHandler EventMouseEnter
		{
			add { base.Events.AddHandler(nameof(EventMouseEnter), value); }
			remove { base.Events.RemoveHandler(nameof(EventMouseEnter), value); }
		}

		/// <summary>
		/// Triggered when the mouse leaves the event control.
		/// </summary>
		public event EventMouseLeaveHandler EventMouseLeave
		{
			add { base.Events.AddHandler(nameof(EventMouseLeave), value); }
			remove { base.Events.RemoveHandler(nameof(EventMouseLeave), value); }
		}

		/// <summary>
		/// Triggered when the a <see cref="SchedulerResource"/> object changes.
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
		/// Raises the <see cref="EventMouseEnter"/> event.
		/// </summary>
		/// <remarks>This method is called to invoke the <see cref="EventMouseEnter"/> event handlers.  Derived
		/// classes can override this method to provide custom handling for the event. When overriding, ensure to call the
		/// base implementation to maintain event invocation.</remarks>
		/// <param name="e">The event data associated with the mouse enter event.</param>
		protected virtual void OnEventMouseEnter(EventMouseEnterArgs e)
		{
			((EventMouseEnterHandler)base.Events[nameof(EventMouseEnter)])?.Invoke(this, e);
		}

		/// <summary>
		/// Raises the <see cref="EventMouseLeave"/> event.
		/// </summary>
		/// <remarks>This method is called to invoke the <see cref="EventMouseLeave"/> event handlers.  Derived
		/// classes can override this method to provide custom handling for the event. When overriding, ensure to call the
		/// base implementation to maintain event invocation.</remarks>
		/// <param name="e">The event data associated with the mouse enter event.</param>
		protected virtual void OnEventMouseLeave(EventMouseLeaveArgs e)
		{
			((EventMouseLeaveHandler)base.Events[nameof(EventMouseLeave)])?.Invoke(this, e);
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
		/// Returns or sets the view the calendar uses to display the events.
		/// </summary>
		/// <exception cref="T:System.Exception">
		/// The value is one of the timeline views (<see cref="F:Wisej.Web.Ext.FullCalendar.ViewType.TimelineDay"/>,
		/// <see cref="F:Wisej.Web.Ext.FullCalendar.ViewType.TimelineWeek"/>, <see cref="F:Wisej.Web.Ext.FullCalendar.ViewType.TimelineMonth"/>,
		/// <see cref="F:Wisej.Web.Ext.FullCalendar.ViewType.TimelineYear"/>) and <see cref="SchedulerLicenseKey"/> is empty.
		/// </exception>
		/// <remarks>
		/// The default is <see cref="F:Wisej.Web.Ext.FullCalendar.ViewType.Month"/>. Once the control is created, changing the view
		/// switches the view on the client without recreating the calendar.
		/// The timeline views are part of the scheduler plug-in and require <see cref="SchedulerLicenseKey"/> to be set first.
		/// </remarks>
		/// <example>
		/// Switching between views using buttons:
		/// <code><![CDATA[
		/// private void buttonWeek_Click(object sender, EventArgs e)
		/// {
		///     this.fullCalendar1.View = ViewType.AgendaWeek;
		/// }
		///
		/// private void buttonList_Click(object sender, EventArgs e)
		/// {
		///     this.fullCalendar1.View = ViewType.ListMonth;
		/// }
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the license key for the scheduler plug-in.
		/// See <see href="https://fullcalendar.io/scheduler"/>.
		/// </summary>
		/// <remarks>
		/// Use "GPL-My-Project-Is-Open-Source" for GPL projects, or "CC-Attribution-NonCommercial-NoDerivatives"
		/// for non commercial projects.
		/// <para>
		/// When the key is not empty, the scheduler scripts are added to <see cref="Packages"/> and the
		/// <see cref="Resources"/>, <see cref="ResourceLabelText"/> and <see cref="ResourceAreaWidth"/> properties are sent to the client.
		/// The packages are built only once, the first time they are requested, so set the key in the designer or
		/// before the control is created. The key is also required before setting <see cref="View"/> to a timeline view.
		/// </para>
		/// </remarks>
		/// <example>
		/// Enabling the scheduler plug-in and showing the resources in a timeline view:
		/// <code><![CDATA[
		/// this.fullCalendar1.SchedulerLicenseKey = "GPL-My-Project-Is-Open-Source";
		/// this.fullCalendar1.Resources = new[] {
		///     new SchedulerResource("room1") { Title = "Room 1" },
		///     new SchedulerResource("room2") { Title = "Room 2" }
		/// };
		/// this.fullCalendar1.View = ViewType.TimelineDay;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the theme system used by the calendar.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The ThemeSystem property can be one of these values:
		/// </para>
		/// <list type="bullet">
		///	<item>
		///		<term>Standard</term>
		///		<description>Renders the built-in look &amp; feel.</description>
		///	</item>
		///	<item>
		///		<term>Bootstrap3</term>
		///		<description>Supports Bootstrap 3 themes. The Bootstrap CSS file must be loaded separately in its own link tag.</description>
		///	</item>
		///	<item>
		///		<term>jQueryUI</term>
		///		<description>Supports jQuery UI themes. The jQuery UI CSS file must be loaded separately in its own link tag.</description>
		///	</item>
		/// </list>
		/// <para>
		/// Once the control is created, changing the value updates the option on the client without recreating the calendar.
		/// </para>
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
		/// <remarks>
		/// The value is used to highlight the current day, to place the current time marker (see <see cref="ShowCurrentTime"/>)
		/// and as the target of <see cref="Today"/>. Until it's set, the property returns <see cref="P:System.DateTime.Now"/>
		/// evaluated on the server every time it's read. Setting the property recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Using the time zone of the user instead of the server's time:
		/// <code><![CDATA[
		/// var zone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
		/// this.fullCalendar1.TodayDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// Setting the property keeps only the date part of the value, moves the calendar to the new date
		/// (see <see cref="GotoDate"/>) and fires the <see cref="CurrentDateChanged"/> event.
		/// <para>
		/// The property is also updated, firing <see cref="CurrentDateChanged"/>, when the client reports that the displayed date changed
		/// after a call to <see cref="Previous"/>, <see cref="Next"/>, <see cref="PreviousYear"/>, <see cref="NextYear"/>, <see cref="Today"/>
		/// or <see cref="GotoDate"/>. That happens asynchronously, so the value is not updated yet when those methods return.
		/// </para>
		/// </remarks>
		/// <example>
		/// Moving the calendar to October 2026:
		/// <code><![CDATA[
		/// this.fullCalendar1.CurrentDate = new DateTime(2026, 10, 1);
		/// ]]></code>
		/// Showing the displayed month in a label:
		/// <code><![CDATA[
		/// private void fullCalendar1_CurrentDateChanged(object sender, EventArgs e)
		/// {
		///     this.labelMonth.Text = this.fullCalendar1.CurrentDate.ToString("MMMM yyyy");
		/// }
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the first time slot that will be displayed for each day, even when the scrollbars have been scrolled all the way up.
		/// </summary>
		/// <remarks>
		/// Applies to the views with time slots (agenda and timeline views). The value is a time of the day; the default is 00:00:00.
		/// Setting the property recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Limiting the agenda views to working hours:
		/// <code><![CDATA[
		/// this.fullCalendar1.MinTime = new TimeSpan(7, 0, 0);
		/// this.fullCalendar1.MaxTime = new TimeSpan(19, 0, 0);
		/// this.fullCalendar1.ScrollTime = new TimeSpan(8, 0, 0);
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the last time slot (exclusive) that will be displayed for each day, even when the scrollbars have been scrolled all the way down.
		/// </summary>
		/// <remarks>
		/// Applies to the views with time slots (agenda and timeline views). The default is 24 hours (the end of the day).
		/// Setting the property recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Ending the day view at 6 PM:
		/// <code><![CDATA[
		/// this.fullCalendar1.MaxTime = new TimeSpan(18, 0, 0);
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the time the time-slot views are initially scrolled to.
		/// </summary>
		/// <remarks>
		/// The default is 06:00:00. Setting the property recreates the calendar on the client.
		/// When the calendar becomes visible after having been hidden, the scroll position is computed
		/// assuming 30 minutes slots (the default <see cref="SlotDuration"/>).
		/// </remarks>
		/// <example>
		/// Scrolling to 8 AM when the agenda view is displayed:
		/// <code><![CDATA[
		/// this.fullCalendar1.ScrollTime = new TimeSpan(8, 0, 0);
		/// ]]></code>
		/// </example>
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
		/// Returns or sets how often the time-axis is labeled with text displaying the date/time of the slots.
		/// </summary>
		/// <remarks>
		/// The default is 1 hour. The value is always sent to the client, so it's not computed automatically from
		/// <see cref="SlotDuration"/>: when changing <see cref="SlotDuration"/> adjust this property as well.
		/// Setting the property recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Using 15 minutes slots labeled every 30 minutes:
		/// <code><![CDATA[
		/// this.fullCalendar1.SlotDuration = TimeSpan.FromMinutes(15);
		/// this.fullCalendar1.SlotLabelInterval = TimeSpan.FromMinutes(30);
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the duration of the time slots.
		/// </summary>
		/// <remarks>
		/// The default is 30 minutes. Labels are displayed according to <see cref="SlotLabelInterval"/>.
		/// Setting the property recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Using 15 minutes slots:
		/// <code><![CDATA[
		/// this.fullCalendar1.SlotDuration = TimeSpan.FromMinutes(15);
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the next-day threshold time.
		/// </summary>
		/// <remarks>
		/// <para>
		/// When an event's end time spans into another day, this is the minimum time
		/// it must be in order for it to render as if it were on that day.
		/// The default is 09:00:00.
		/// </para>
		/// <para>
		/// Only affects timed events that appear on whole-days.
		/// Whole-day cells occur in the <see cref="F:Wisej.Web.Ext.FullCalendar.ViewType.Month"/>, <see cref="F:Wisej.Web.Ext.FullCalendar.ViewType.BasicDay"/>
		/// and <see cref="F:Wisej.Web.Ext.FullCalendar.ViewType.BasicWeek"/> views and in the all-day slots of the agenda views.
		/// </para>
		/// </remarks>
		/// <example>
		/// Rendering an event that ends at 2 AM only on its first day:
		/// <code><![CDATA[
		/// this.fullCalendar1.NextDayThreshold = new TimeSpan(3, 0, 0);
		/// this.fullCalendar1.Events.Add(new DateTime(2026, 10, 3, 20, 0, 0), new DateTime(2026, 10, 4, 2, 0, 0)).Title = "Party";
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the time-text that will be displayed on the vertical axis of the agenda views
		/// using momentjs format patterns: <see href="http://momentjs.com/docs/#/displaying/format/"/>.
		/// </summary>
		/// <remarks>
		/// Returns "Default" when not set, in which case the format of the current locale is used.
		/// Setting it to null or an empty string restores the default. Setting the property recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Showing the axis labels in 24 hours format:
		/// <code><![CDATA[
		/// this.fullCalendar1.SlotLabelFormat = "HH:mm";
		/// ]]></code>
		/// </example>
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
		/// Returns the formats of the column headers in the different views
		/// using momentjs format patterns: <see href="http://momentjs.com/docs/#/displaying/format/"/>.
		/// </summary>
		/// <remarks>
		/// Each format that is not set returns "Default" and uses the format of the current locale.
		/// Changing any of the formats recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Customizing the column headers of the day, week and month views:
		/// <code><![CDATA[
		/// this.fullCalendar1.HeaderFormats.DayViewFormat = "dddd, MMMM D";
		/// this.fullCalendar1.HeaderFormats.WeekViewFormat = "ddd D/M";
		/// this.fullCalendar1.HeaderFormats.MonthViewFormat = "dddd";
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// Setting the property recreates the calendar on the client.
		/// </remarks>
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
		/// Returns or sets the business hours to emphasize on the calendar.
		/// </summary>
		/// <remarks>
		/// The default is null: no business hours are emphasized. Each <see cref="T:Wisej.Web.Ext.FullCalendar.BusinessHours"/> item
		/// defines a time range (9 AM to 5 PM by default) for a set of days, which allows different hours on different days.
		/// Assign a new array to apply changes: modifying the items of the current array doesn't update the calendar.
		/// Setting the property recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Emphasizing Monday to Friday 8 AM - 6 PM and Saturday 9 AM - 1 PM:
		/// <code><![CDATA[
		/// this.fullCalendar1.BusinessHours = new[] {
		///     new BusinessHours {
		///         Start = new TimeSpan(8, 0, 0),
		///         End = new TimeSpan(18, 0, 0),
		///         Days = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }
		///     },
		///     new BusinessHours {
		///         Start = new TimeSpan(9, 0, 0),
		///         End = new TimeSpan(13, 0, 0),
		///         Days = new[] { DayOfWeek.Saturday }
		///     }
		/// };
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the maximum number of events displayed on a day.
		/// </summary>
		/// <exception cref="T:System.ArgumentException">The value is less than 0.</exception>
		/// <remarks>
		/// The default is 0, which means that the number of events is not limited.
		/// When there are too many events, a link that looks like "+2 more" is displayed; clicking the link
		/// shows the hidden events in a popover. Setting the property recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Showing at most 3 events per day in the month view:
		/// <code><![CDATA[
		/// this.fullCalendar1.View = ViewType.Month;
		/// this.fullCalendar1.EventLimit = 3;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets whether you have provided your own data-management operations for the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> control.
		/// </summary>
		/// <returns>true if the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> uses data-management operations that you provide; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// When true, the <see cref="Events"/> collection doesn't store any event (changing the value clears it) and can't be modified:
		/// <list type="bullet">
		/// <item>the calendar fires <see cref="VirtualEventsNeeded"/> every time it needs the events of the visible date range;</item>
		/// <item>accessing <see cref="Events"/> by index or ID fires <see cref="RetrieveVirtualEvent"/>. This also happens when the user
		/// clicks, drags or resizes an event, so the event must be handled and must return an event;</item>
		/// <item><see cref="P:Wisej.Web.Ext.FullCalendar.EventCollection.Count"/> returns <see cref="VirtualSize"/>.</item>
		/// </list>
		/// Changing the value reloads the events on the client.
		/// </remarks>
		/// <example>
		/// Loading the events from a data source, only for the displayed range:
		/// <code><![CDATA[
		/// this.fullCalendar1.VirtualMode = true;
		/// this.fullCalendar1.VirtualEventsNeeded += (s, e) =>
		/// {
		///     // LoadAppointments is your own data access method.
		///     e.Events = LoadAppointments(e.StartDate, e.EndDate)
		///         .Select(a => new Event(a.Id.ToString(), a.Start, a.End) { Title = a.Subject });
		/// };
		/// this.fullCalendar1.RetrieveVirtualEvent += (s, e) =>
		/// {
		///     var a = LoadAppointment(e.EventID);
		///     e.Event = new Event(a.Id.ToString(), a.Start, a.End) { Title = a.Subject };
		/// };
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// The value is returned by <see cref="P:Wisej.Web.Ext.FullCalendar.EventCollection.Count"/> when <see cref="VirtualMode"/> is true
		/// and limits the indexes that can be used with the <see cref="Events"/> indexer. Changing the value in virtual mode reloads the events on the client.
		/// </remarks>
		/// <example>
		/// Setting the number of virtual events after loading the data:
		/// <code><![CDATA[
		/// this.fullCalendar1.VirtualMode = true;
		/// this.fullCalendar1.VirtualSize = this.appointments.Count;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets whether the events on the calendar can be dragged and resized by the user.
		/// </summary>
		/// <remarks>
		/// The default is true. Individual events can override the value using <see cref="P:Wisej.Web.Ext.FullCalendar.Event.Editable"/>.
		/// </remarks>
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
		/// <remarks>
		/// The default is "No events to display". Setting it to an empty string restores the default.
		/// Setting the property recreates the calendar on the client.
		/// </remarks>
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
		/// <remarks>
		/// The default is "All Day". Setting it to an empty string restores the default.
		/// Setting the property recreates the calendar on the client.
		/// </remarks>
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
		/// Returns or sets the time-text that will be displayed on each event
		/// using momentjs format patterns: <see href="http://momentjs.com/docs/#/displaying/format/"/>.
		/// </summary>
		/// <remarks>
		/// The default is null, in which case the format of the current locale is used.
		/// Setting the property recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Setting the time format to display on the events:
		/// <code><![CDATA[
		/// this.fullCalendar1.TimeFormat = "h:mm";    // shows 5:00
		/// this.fullCalendar1.TimeFormat = "h(:mm)t"; // shows 5p
		/// this.fullCalendar1.TimeFormat = "HH:mm";   // shows 17:00
		/// ]]></code>
		/// </example>
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
		/// Returns or sets whether to display a marker indicating the current time.
		/// </summary>
		/// <remarks>
		/// The default is true. The marker is displayed in the views with time slots at the time of <see cref="TodayDate"/>.
		/// </remarks>
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
		/// Returns or sets whether the "all-day" slot is displayed at the top of the agenda views.
		/// </summary>
		/// <remarks>
		/// The default is true. The title of the slot is set by <see cref="AllDayText"/>.
		/// </remarks>
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
		/// Returns or sets whether ToolTips are shown when the mouse pointer hovers over a <see cref="Wisej.Web.Ext.FullCalendar.Event" />.
		/// </summary>
		/// <returns>true if ToolTips are shown when the mouse pointer hovers over a <see cref="Wisej.Web.Ext.FullCalendar.Event" />; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// The ToolTip shows <see cref="P:Wisej.Web.Ext.FullCalendar.Event.ToolTipText"/> or, when it's not set, <see cref="P:Wisej.Web.Ext.FullCalendar.Event.Title"/>.
		/// </remarks>
		/// <since>3.5.13</since>
		[DefaultValue(false)]
		public bool ShowEventToolTips
		{
			get { return this._showEventToolTips; }
			set
			{
				if (this._showEventToolTips != value)
				{
					this._showEventToolTips = value;
					Update();
				}
			}
		}
		private bool _showEventToolTips;

		/// <summary>
		/// Returns or sets whether timed events in the agenda views should visually overlap.
		/// </summary>
		/// <remarks>
		/// The default is true. When false, events that overlap in time are displayed side by side.
		/// </remarks>
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
		/// Returns or sets the background color for all events in the calendar.
		/// </summary>
		/// <remarks>
		/// Individual events can override the value using <see cref="P:Wisej.Web.Ext.FullCalendar.Event.BackgroundColor"/>.
		/// </remarks>
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
		/// Returns or sets the border color for all events in the calendar.
		/// </summary>
		/// <remarks>
		/// Individual events can override the value using <see cref="P:Wisej.Web.Ext.FullCalendar.Event.BorderColor"/>.
		/// </remarks>
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
		/// Returns or sets the text color for all events in the calendar.
		/// </summary>
		/// <remarks>
		/// Individual events can override the value using <see cref="P:Wisej.Web.Ext.FullCalendar.Event.TextColor"/>.
		/// </remarks>
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
		/// <remarks>
		/// Adding, removing or changing events reloads the events of the visible range on the client after the current request.
		/// Events without an ID receive an automatic ID when added; adding an event with the ID of an existing event replaces it.
		/// When <see cref="VirtualMode"/> is true the collection can't be modified and retrieving events fires <see cref="RetrieveVirtualEvent"/>.
		/// </remarks>
		/// <example>
		/// Adding and finding events:
		/// <code><![CDATA[
		/// this.fullCalendar1.Events.Add("meeting1", new DateTime(2026, 10, 5, 10, 0, 0), TimeSpan.FromMinutes(90)).Title = "Design review";
		/// this.fullCalendar1.Events.Add(new Event("vacation", new DateTime(2026, 10, 12)) { Title = "Vacation", BackgroundColor = Color.SeaGreen });
		///
		/// var ev = this.fullCalendar1.Events["meeting1"];
		/// if (ev != null)
		///     ev.Start = ev.Start.AddHours(1);
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// Events are associated to a resource using <see cref="P:Wisej.Web.Ext.FullCalendar.Event.ResourceId"/>.
		/// The resources are displayed in the timeline views. Assign a new array to add or remove resources;
		/// changing the properties of a resource fires <see cref="ResourceChanged"/> but doesn't redraw the calendar.
		/// Setting the property recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Assigning events to resources:
		/// <code><![CDATA[
		/// this.fullCalendar1.SchedulerLicenseKey = "GPL-My-Project-Is-Open-Source";
		/// this.fullCalendar1.Resources = new[] {
		///     new SchedulerResource("a") { Title = "Auditorium A", EventBackgroundColor = Color.SteelBlue },
		///     new SchedulerResource("b") { Title = "Auditorium B", EventBackgroundColor = Color.IndianRed }
		/// };
		/// this.fullCalendar1.View = ViewType.TimelineDay;
		/// this.fullCalendar1.Events.Add(DateTime.Today.AddHours(10), TimeSpan.FromHours(2)).ResourceId = "a";
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Returns or sets the scheduler resources.")]
		public SchedulerResource[] Resources
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
		private SchedulerResource[] _resources;

		/// <summary>
		/// Returns or sets the text that will appear above the list of resources.
		/// Requires the <see cref="SchedulerLicenseKey"/> to be set to a valid license
		/// or to a GPL or CC license.
		/// </summary>
		/// <remarks>
		/// The default is "Resources". Setting the property recreates the calendar on the client.
		/// </remarks>
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
		/// Returns or sets the width of the area that contains the list of resources.
		/// Requires the <see cref="SchedulerLicenseKey"/> to be set to a valid license
		/// or to a GPL or CC license.
		/// </summary>
		/// <remarks>
		/// The value is a CSS width, either a percentage of the calendar width or a size in pixels. The default is "30%".
		/// Setting the property recreates the calendar on the client.
		/// </remarks>
		/// <example>
		/// Using a fixed width for the resource area:
		/// <code><![CDATA[
		/// this.fullCalendar1.ResourceAreaWidth = "200px";
		/// ]]></code>
		/// </example>
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
		/// Returns the initialization script that creates the FullCalendar instance on the client.
		/// </summary>
		/// <remarks>
		/// The script is built from the embedded startup.js resource and the current values of the properties
		/// of the control; the setter is ignored.
		/// </remarks>
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
		/// The step depends on the current <see cref="View"/>: the month views move back one month, the week views one week
		/// and the day views one day. <see cref="CurrentDate"/> is updated and <see cref="CurrentDateChanged"/> is fired
		/// asynchronously, when the client reports the new date.
		/// </remarks>
		/// <example>
		/// Implementing a custom navigation toolbar:
		/// <code><![CDATA[
		/// private void buttonPrev_Click(object sender, EventArgs e)
		/// {
		///     this.fullCalendar1.Previous();
		/// }
		/// ]]></code>
		/// </example>
		public void Previous()
		{
			Call("exec", "prev");
		}

		/// <summary>
		/// Moves the calendar one step forward (either by a month, week, or day).
		/// </summary>
		/// <remarks>
		/// The step depends on the current <see cref="View"/>: the month views move forward one month, the week views one week
		/// and the day views one day. <see cref="CurrentDate"/> is updated and <see cref="CurrentDateChanged"/> is fired
		/// asynchronously, when the client reports the new date.
		/// </remarks>
		/// <example>
		/// Implementing a custom navigation toolbar:
		/// <code><![CDATA[
		/// private void buttonNext_Click(object sender, EventArgs e)
		/// {
		///     this.fullCalendar1.Next();
		/// }
		/// ]]></code>
		/// </example>
		public void Next()
		{
			Call("exec", "next");
		}

		/// <summary>
		/// Moves the calendar back one year.
		/// </summary>
		/// <remarks>
		/// <see cref="CurrentDate"/> is updated and <see cref="CurrentDateChanged"/> is fired asynchronously, when the client reports the new date.
		/// </remarks>
		/// <example>
		/// Going back one year:
		/// <code><![CDATA[
		/// private void buttonPrevYear_Click(object sender, EventArgs e)
		/// {
		///     this.fullCalendar1.PreviousYear();
		/// }
		/// ]]></code>
		/// </example>
		public void PreviousYear()
		{
			Call("exec", "prevYear");
		}

		/// <summary>
		/// Moves the calendar forward one year.
		/// </summary>
		/// <remarks>
		/// <see cref="CurrentDate"/> is updated and <see cref="CurrentDateChanged"/> is fired asynchronously, when the client reports the new date.
		/// </remarks>
		/// <example>
		/// Going forward one year:
		/// <code><![CDATA[
		/// private void buttonNextYear_Click(object sender, EventArgs e)
		/// {
		///     this.fullCalendar1.NextYear();
		/// }
		/// ]]></code>
		/// </example>
		public void NextYear()
		{
			Call("exec", "nextYear");
		}

		/// <summary>
		/// Moves the calendar to the current date.
		/// </summary>
		/// <remarks>
		/// The current date is the value of <see cref="TodayDate"/>. <see cref="CurrentDate"/> is updated and
		/// <see cref="CurrentDateChanged"/> is fired asynchronously, when the client reports the new date.
		/// </remarks>
		/// <example>
		/// Going back to today:
		/// <code><![CDATA[
		/// private void buttonToday_Click(object sender, EventArgs e)
		/// {
		///     this.fullCalendar1.Today();
		/// }
		/// ]]></code>
		/// </example>
		public void Today()
		{
			Call("exec", "today");
		}

		/// <summary>
		/// Moves the calendar to an arbitrary date.
		/// </summary>
		/// <param name="dateTime">The date to set the calendar view to.</param>
		/// <remarks>
		/// The view is not changed: it displays the month, week or day that contains <paramref name="dateTime"/>.
		/// <see cref="CurrentDate"/> is updated and <see cref="CurrentDateChanged"/> is fired asynchronously, when the client reports the new date.
		/// If the calendar is not created on the client yet, the call is executed when it's ready.
		/// </remarks>
		/// <example>
		/// Moving the calendar to the date selected in a <see cref="T:Wisej.Web.DateTimePicker"/>:
		/// <code><![CDATA[
		/// private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		/// {
		///     this.fullCalendar1.GotoDate(this.dateTimePicker1.Value);
		/// }
		/// ]]></code>
		/// </example>
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

		internal void OnResourceChanged(SchedulerResource resource)
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
		private void ProcessEventMouseWebEvent(WidgetEventArgs e)
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

					switch (e.Type)
					{
						case "eventClick":
							OnEventClick(new EventClickEventArgs(ev, button, 1, location));
							break;
						case "eventDblClick":
							OnEventDoubleClick(new EventClickEventArgs(ev, button, 2, location));
							break;
						case "eventMouseEnter":
							OnEventMouseEnter(new EventMouseEnterArgs(ev,button, location));
							break;
						case "eventMouseLeave":
							OnEventMouseLeave(new EventMouseLeaveArgs(ev, button, location));
							break;
					}
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
				case "eventMouseEnter":
				case "eventMouseLeave":
					ProcessEventMouseWebEvent(e);
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
		/// Overridden, not used. Always returns null and the setter is ignored.
		/// </summary>
		/// <remarks>
		/// The FullCalendar options are generated from the properties of the control.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override dynamic Options
		{
			get { return null; }
			set { }
		}

		/// <summary>
		/// Returns the list of packages loaded by the client before the widget is initialized.
		/// </summary>
		/// <remarks>
		/// The list contains jQuery 3.1.1, moment.js 2.17.1 and FullCalendar 3.9.0 and, when <see cref="SchedulerLicenseKey"/> is not empty,
		/// the scheduler plug-in 1.9.4. The packages are served from the resources embedded in this assembly and the list is built
		/// only the first time the property is read.
		/// </remarks>
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
			options.showEventToolTips = this.ShowEventToolTips;

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

			script = script.Replace("$options", options.ToJSON(WisejSerializerOptions.CamelCase));
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
