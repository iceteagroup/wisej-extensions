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
		public Event(string id, DateTime start, TimeSpan duration)
		{
			if (String.IsNullOrEmpty(id))
				throw new ArgumentNullException("id");

			this._id = id;
			this._start = start;
			this._end = start + duration;
		}

		/// <summary>
		/// Returns or sets the unique ID for this event.
		/// </summary>
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
		/// Returns or sets the id of the <see cref="Resource"/> associated to this event.
		/// </summary>
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
		/// Returns or sets a flag indicating that this is an all-day event.
		/// </summary>
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
		/// Determines whether the event can be modified.
		/// </summary>
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
		/// Sets the background color for this event.
		/// </summary>
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
		/// Sets the border color for this event.
		/// </summary>
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
		/// Sets the text color for this event.
		/// </summary>
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
		/// A CSS class (or array of classes) that will be attached to this event's element.
		/// </summary>
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
		public dynamic UserData
		{
			get { return _userData = _userData ?? new DynamicObject(); }
		}
		private dynamic _userData = null;

	}
}
