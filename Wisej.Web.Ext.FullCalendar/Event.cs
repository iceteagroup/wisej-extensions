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
using Wisej.Core;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Represents an event in the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> control.
	/// </summary>
	/// <remarks>
	/// Add events to the calendar using <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.Events"/>.
	/// Once an event belongs to a calendar, changing any of its properties fires <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.EventChanged"/>
	/// and reloads the events on the client.
	/// </remarks>
	/// <example>
	/// Creating a timed event and an all-day event:
	/// <code><![CDATA[
	/// var meeting = new Event("m42", new DateTime(2026, 10, 7, 14, 0, 0), TimeSpan.FromHours(1))
	/// {
	///     Title = "Budget meeting",
	///     ToolTipText = "Room 3, bring the Q3 figures",
	///     BackgroundColor = Color.DarkOrange
	/// };
	/// this.fullCalendar1.Events.Add(meeting);
	/// this.fullCalendar1.Events.Add(new Event(new DateTime(2026, 10, 9)) { Title = "Release day" });
	/// ]]></code>
	/// </example>
	[ApiCategory("FullCalendar")]
	public class Event
	{
		internal FullCalendar owner;

		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.
		/// </summary>
		public Event()
		{
		}

		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.
		/// </summary>
		/// <param name="id">A string that represents the ID of this event.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="id"/> is null or empty.</exception>
		public Event(string id)
		{
			if (String.IsNullOrEmpty(id))
				throw new ArgumentNullException("id");

			this._id = id;
		}

		/// <summary>
		/// Constructs a new instance of an all-day <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.
		/// </summary>
		/// <param name="day">The <see cref="T:System.DateTime"/> date of the all-day event.</param>
		public Event(DateTime day)
		{
			this._start = day;
			this._allDay = true;
		}

		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.
		/// </summary>
		/// <param name="start">The starting <see cref="T:System.DateTime"/> date and time of the event.</param>
		/// <param name="end">The ending <see cref="T:System.DateTime"/> date and time of the event.</param>
		public Event(DateTime start, DateTime end)
		{
			this._start = start;
			this._end = end;
		}

		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.
		/// </summary>
		/// <param name="start">The starting <see cref="T:System.DateTime"/> date and time of the event.</param>
		/// <param name="duration">The <see cref="T:System.TimeSpan"/> duration of the event.</param>
		public Event(DateTime start, TimeSpan duration)
		{
			this._start = start;
			this._end = start + duration;
		}

		/// <summary>
		/// Constructs a new instance of an all-day <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.
		/// </summary>
		/// <param name="id">A string that represents the ID of this event.</param>
		/// <param name="day">The <see cref="T:System.DateTime"/> date of the all-day event.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="id"/> is null or empty.</exception>
		public Event(string id, DateTime day)
		{
			if (String.IsNullOrEmpty(id))
				throw new ArgumentNullException("id");

			this._id = id;
			this._start = day;
			this._allDay = true;
		}

		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.
		/// </summary>
		/// <param name="id">A string that represents the ID of this event.</param>
		/// <param name="start">The starting <see cref="T:System.DateTime"/> date and time of the event.</param>
		/// <param name="end">The ending <see cref="T:System.DateTime"/> date and time of the event.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="id"/> is null or empty.</exception>
		public Event(string id, DateTime start, DateTime end)
		{
			if (String.IsNullOrEmpty(id))
				throw new ArgumentNullException("id");

			this._id = id;
			this._start = start;
			this._end = end;
		}

		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.
		/// </summary>
		/// <param name="id">A string that represents the ID of this event.</param>
		/// <param name="start">The starting <see cref="T:System.DateTime"/> date and time of the event.</param>
		/// <param name="duration">The <see cref="T:System.TimeSpan"/> duration of the event.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="id"/> is null or empty.</exception>
		public Event(string id, DateTime start, TimeSpan duration)
		{
			if (String.IsNullOrEmpty(id))
				throw new ArgumentNullException("id");

			this._id = id;
			this._start = start;
			this._end = start + duration;
		}

		/// <summary>
		/// Returns or sets the text that appears when the mouse pointer hovers over the event.
		/// </summary>
		/// <returns>The text that appears when the mouse pointer hovers over a <see cref="Wisej.Web.Ext.FullCalendar.Event" />.</returns>
		/// <remarks>
		/// The ToolTip is displayed only when <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.ShowEventToolTips"/> is true.
		/// When the value is null, the ToolTip shows the <see cref="Title"/>.
		/// </remarks>
		/// <since>3.5.13</since>
		[DefaultValue(null)]
		[Localizable(false)]
		public string ToolTipText
		{
			get { return this._tooltipText; }
			set
			{
				if (this._tooltipText != value)
				{
					this._tooltipText = value;
					OnEventChanged(this.Start, this.End);
				}
			}
		}
		private string _tooltipText;

		/// <summary>
		/// Returns or sets the unique ID for this event.
		/// </summary>
		/// <remarks>
		/// An empty string is converted to null. Events added to <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.Events"/> without an ID
		/// receive an automatic ID ("event_1", "event_2", ...). Adding an event with the same ID of an event already in the collection
		/// replaces the existing event. The ID identifies the event in the calendar's events such as
		/// <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.EventClick"/>, and in virtual mode it's passed to
		/// <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.RetrieveVirtualEvent"/>.
		/// </remarks>
		public string Id
		{
			get { return this._id; }
			set
			{
				value = value == "" ? null : value;
				if (this._id != value)
				{
					this._id = value;
					OnEventChanged(this.Start, this.End);
				}
			}
		}
		private string _id = null;

		/// <summary>
		/// Returns or sets the id of the <see cref="SchedulerResource"/> associated to this event.
		/// </summary>
		/// <remarks>
		/// Must match the <see cref="P:Wisej.Web.Ext.FullCalendar.SchedulerResource.Id"/> of one of the resources in
		/// <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.Resources"/>. The value is updated when the user drags the event to a different resource.
		/// </remarks>
		public string ResourceId
		{
			get { return this._resourceId; }
			set
			{
				value = value == "" ? null : value;
				if (this._resourceId != value)
				{
					this._resourceId = value;
					OnEventChanged(this.Start, this.End);
				}
			}
		}
		private string _resourceId = null;

		internal string ResourceIdInternal
		{
			get { return this._resourceId; }
			set { this._resourceId = value; }
		}

		/// <summary>
		/// Returns or sets the title of this event.
		/// </summary>
		public string Title
		{
			get { return this._title; }
			set
			{
				value = value == "" ? null : value;
				if (this._title != value)
				{
					this._title = value;
					OnEventChanged(this.Start, this.End);
				}
			}
		}
		private string _title = null;

		/// <summary>
		/// Returns or sets the start date/time of this event.
		/// </summary>
		/// <remarks>
		/// The value is updated when the user drags or resizes the event; in that case
		/// <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.EventChanged"/> is fired with the previous value.
		/// Changing the value of an event that belongs to a calendar fires <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.EventChanged"/>
		/// and reloads the events on the client.
		/// </remarks>
		public DateTime Start
		{
			get { return this._start; }
			set
			{
				if (this._start != value)
				{
					var oldStart = this._start;
					this._start = value;
					OnEventChanged(oldStart, this.End);
				}
			}
		}
		private DateTime _start = DateTime.MinValue;

		// Sets the start date without triggering the EventChanged event.
		internal DateTime StartInternal
		{
			get { return this._start; }
			set { this._start = value; }
		}

		/// <summary>
		/// Returns or sets the end date/time of this event.
		/// </summary>
		/// <remarks>
		/// The end is exclusive: an all-day event that lasts two days, from October 5 to October 6, ends on October 7.
		/// The value is updated when the user drags or resizes the event.
		/// Changing the value of an event that belongs to a calendar fires <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.EventChanged"/>
		/// and reloads the events on the client.
		/// </remarks>
		/// <example>
		/// Creating a three day all-day event:
		/// <code><![CDATA[
		/// var trip = new Event(new DateTime(2026, 10, 5)) { Title = "Trade fair" };
		/// trip.End = new DateTime(2026, 10, 8);
		/// this.fullCalendar1.Events.Add(trip);
		/// ]]></code>
		/// </example>
		public DateTime End
		{
			get { return this._end; }
			set
			{
				if (this._end != value)
				{
					var oldEnd = this._end;
					this._end = value;
					OnEventChanged(this.Start, oldEnd);
				}
			}
		}
		private DateTime _end = DateTime.MinValue;

		// Sets the end date without triggering the EventChanged event.
		internal DateTime EndInternal
		{
			get { return this._end; }
			set { this._end = value; }
		}

		/// <summary>
		/// Returns or sets whether this is an all-day event.
		/// </summary>
		/// <remarks>
		/// The constructors that take a single day create all-day events. The value is updated when the user drags
		/// the event in or out of the all-day slot.
		/// </remarks>
		public bool AllDay
		{
			get { return this._allDay; }
			set
			{
				if (this._allDay != value)
				{
					this._allDay = value;
					OnEventChanged(this.Start, this.End);
				}
			}
		}
		private bool _allDay = false;

		// Sets the allDay flag without triggering the EventChanged event.
		internal bool AllDayInternal
		{
			get { return this._allDay; }
			set { this._allDay = value; }
		}

		/// <summary>
		/// Returns or sets whether the event can be dragged and resized by the user.
		/// </summary>
		/// <remarks>
		/// The default is true.
		/// </remarks>
		public bool Editable
		{
			get { return this._editable; }
			set
			{
				if (this._editable != value)
				{
					this._editable = value;
					OnEventChanged(this.Start, this.End);
				}
			}
		}
		private bool _editable = true;

		/// <summary>
		/// Returns or sets the background color for this event.
		/// </summary>
		/// <remarks>
		/// When empty, the event uses <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.EventBackgroundColor"/>.
		/// </remarks>
		public Color BackgroundColor
		{
			get { return this._backgroundColor; }
			set
			{
				if (this._backgroundColor != value)
				{
					this._backgroundColor = value;
					OnEventChanged(this.Start, this.End);
				}
			}
		}
		private Color _backgroundColor = Color.Empty;

		/// <summary>
		/// Returns or sets the border color for this event.
		/// </summary>
		/// <remarks>
		/// When empty, the event uses <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.EventBorderColor"/>.
		/// </remarks>
		public Color BorderColor
		{
			get { return this._borderColor; }
			set
			{
				if (this._borderColor != value)
				{
					this._borderColor = value;
					OnEventChanged(this.Start, this.End);
				}
			}
		}
		private Color _borderColor = Color.Empty;

		/// <summary>
		/// Returns or sets the text color for this event.
		/// </summary>
		/// <remarks>
		/// When empty, the event uses <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.EventTextColor"/>.
		/// </remarks>
		public Color TextColor
		{
			get { return this._textColor; }
			set
			{
				if (this._textColor != value)
				{
					this._textColor = value;
					OnEventChanged(this.Start, this.End);
				}
			}
		}
		private Color _textColor = Color.Empty;

		/// <summary>
		/// Returns or sets the CSS class name (or several names separated by spaces) that will be attached to this event's element.
		/// </summary>
		/// <remarks>
		/// The CSS classes must be defined in a style sheet loaded by the application.
		/// </remarks>
		/// <example>
		/// Styling an event with a custom CSS class:
		/// <code><![CDATA[
		/// // .urgent { font-weight: bold; } is defined in a css file loaded by the application.
		/// this.fullCalendar1.Events["m42"].ClassName = "urgent";
		/// ]]></code>
		/// </example>
		public string ClassName
		{
			get { return this._className; }
			set
			{
				if (this._className != value)
				{
					this._className = value;
					OnEventChanged(this.Start, this.End);
				}
			}
		}
		private string _className = string.Empty;

		private void OnEventChanged(DateTime oldStart, DateTime oldEnd)
		{
			this.owner?.OnEventChanged(this, oldStart, oldEnd);
		}

		/// <summary>
		/// Returns a dynamic object that can be used to store custom data.
		/// </summary>
		/// <remarks>
		/// The object is created the first time the property is read.
		/// </remarks>
		/// <example>
		/// Storing the database key of an appointment:
		/// <code><![CDATA[
		/// var ev = this.fullCalendar1.Events.Add(new DateTime(2026, 10, 7, 9, 0, 0), TimeSpan.FromHours(1));
		/// ev.Title = "Dentist";
		/// ev.UserData.AppointmentId = 1234;
		/// ]]></code>
		/// Reading the value when the event is clicked:
		/// <code><![CDATA[
		/// private void fullCalendar1_EventClick(object sender, EventClickEventArgs e)
		/// {
		///     int id = e.Event.UserData.AppointmentId ?? 0;
		///     OpenAppointment(id);
		/// }
		/// ]]></code>
		/// </example>
		public dynamic UserData
		{
			get { return _userData = _userData ?? new DynamicObject(); }
		}
		private dynamic _userData = null;

	}
}
