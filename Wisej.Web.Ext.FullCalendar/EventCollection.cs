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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Wisej.Core;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Collection of <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> objects.
	/// </summary>
	/// <remarks>
	/// The collection is returned by <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.Events"/>. Any change reloads the events
	/// on the client after the current request. Event IDs are unique in the collection: adding or inserting an event with the ID of
	/// an existing event replaces it, and events without an ID receive an automatic ID.
	/// When <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.VirtualMode"/> is true, the collection can't be modified and the
	/// indexers retrieve the events by firing <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.RetrieveVirtualEvent"/>.
	/// </remarks>
	[ApiCategory("FullCalendar")]
	public class EventCollection : IList<Event>
	{
		// the calendar control that owns this collection.
		private FullCalendar owner;

		// the inner data collection.
		private SynchronizedList<Event> list;

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.FullCalendar.EventCollection"/> class.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> that owns this collection.</param>
		internal EventCollection(FullCalendar owner)
		{
			if (owner == null)
				throw new ArgumentNullException("owner");

			this.owner = owner;
			this.list = new SynchronizedList<Event>(syncLock: this);
		}

		/// <summary>
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> at the specified position.
		/// </summary>
		/// <param name="index">The index of the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to get or set.</param>
		/// <returns>The <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> at the specified position.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or not less than <see cref="Count"/>.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		/// The calendar is in virtual mode and either the event is being set, or the
		/// <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.RetrieveVirtualEvent"/> handler didn't return an event.
		/// </exception>
		/// <remarks>
		/// In virtual mode, reading an event fires <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.RetrieveVirtualEvent"/>
		/// with <see cref="P:Wisej.Web.Ext.FullCalendar.RetrieveVirtualEventEventArgs.EventIndex"/> set to <paramref name="index"/>.
		/// </remarks>
		/// <example>
		/// Iterating the events by index:
		/// <code><![CDATA[
		/// for (int i = 0; i < this.fullCalendar1.Events.Count; i++)
		/// {
		///     var ev = this.fullCalendar1.Events[i];
		///     ev.Editable = ev.Start > DateTime.Now;
		/// }
		/// ]]></code>
		/// </example>
		public Event this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
					throw new ArgumentOutOfRangeException("index");

				// retrieve the event if in virtual mode.
				if (this.owner.VirtualMode)
				{
					var ev = this.owner.OnRetrieveVirtualEvent(index);
					if (ev == null)
						throw new InvalidOperationException("You must return an Event object when handling RetrieveVirtualEvent.");

					BindEvent(ev);
					return ev;
				}

				return this.list[index];

			}
			set
			{
				if (this.owner.VirtualMode)
					throw new InvalidOperationException("Can't modify the event collection when in virtual mode.");

				BindEvent(value);
				UnbindEvent(this.list[index]);

				this.list[index] = value;
				this.owner.OnEventAdded(value);
				OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, value));
			}
		}

		/// <summary>
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> identified by the ID.
		/// </summary>
		/// <param name="id">The ID of the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to get or set.</param>
		/// <returns>The <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> with the specified ID, or null if not found.</returns>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="id"/> is null when setting the event.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		/// The calendar is in virtual mode and either the event is being set, or the
		/// <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.RetrieveVirtualEvent"/> handler didn't return an event.
		/// </exception>
		/// <remarks>
		/// Setting an event replaces the event with the same ID or, when not found, adds the event to the collection.
		/// In virtual mode, reading an event fires <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.RetrieveVirtualEvent"/>
		/// with <see cref="P:Wisej.Web.Ext.FullCalendar.RetrieveVirtualEventEventArgs.EventID"/> set to <paramref name="id"/>.
		/// </remarks>
		/// <example>
		/// Finding an event by its ID:
		/// <code><![CDATA[
		/// var ev = this.fullCalendar1.Events["m42"];
		/// if (ev != null)
		///     ev.Title = "Budget meeting (moved)";
		/// ]]></code>
		/// </example>
		public Event this[string id]
		{
			get
			{

				// retrieve the event if in virtual mode.
				if (this.owner.VirtualMode)
				{
					var ev = this.owner.OnRetrieveVirtualEvent(id);
					if (ev == null)
						throw new InvalidOperationException("You must return an Event object when handling RetrieveVirtualEvent.");

					BindEvent(ev);
					return ev;
				}

				return this.list.Find(o => o.Id == id);
			}
			set
			{
				if (this.owner.VirtualMode)
					throw new InvalidOperationException("Can't modify the event collection when in virtual mode.");

				if (id == null)
					throw new ArgumentNullException("id");

				int index = 0;
				BindEvent(value);

				index = this.list.FindIndex(o => o.Id == id);
				if (index > -1)
				{
					UnbindEvent(this.list[index]);

					this.list[index] = value;
					this.owner.OnEventAdded(value);

				}

				if (index > -1)
				{
					OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, value));
				}
				else
				{
					Add(value);
				}

			}
		}

		/// <summary>
		/// Returns the number of <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> objects in the collection.
		/// </summary>
		/// <remarks>
		/// In virtual mode returns <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.VirtualSize"/>.
		/// </remarks>
		public int Count
		{
			get
			{
				return
				  this.owner.VirtualMode
					 ? this.owner.VirtualSize
					: this.list.Count;
			}
		}

		/// <summary>
		/// Adds a new all-day <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to the collection.
		/// </summary>
		/// <param name="day">The <see cref="T:System.DateTime"/> date of the all-day event.</param>
		/// <returns>The newly added all-day <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.</returns>
		/// <exception cref="T:System.InvalidOperationException">The calendar is in virtual mode.</exception>
		/// <example>
		/// Adding an all-day event:
		/// <code><![CDATA[
		/// this.fullCalendar1.Events.Add(new DateTime(2026, 12, 25)).Title = "Christmas";
		/// ]]></code>
		/// </example>
		public Event Add(DateTime day)
		{
			var ev = new Event(day);
			Add(ev);
			return ev;
		}

		/// <summary>
		/// Adds a new <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to the collection.
		/// </summary>
		/// <param name="start">The starting <see cref="T:System.DateTime"/> date and time of the event.</param>
		/// <param name="end">The ending <see cref="T:System.DateTime"/> date and time of the event.</param>
		/// <returns>The newly added <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.</returns>
		/// <exception cref="T:System.InvalidOperationException">The calendar is in virtual mode.</exception>
		/// <example>
		/// Adding a timed event:
		/// <code><![CDATA[
		/// var ev = this.fullCalendar1.Events.Add(new DateTime(2026, 10, 6, 9, 30, 0), new DateTime(2026, 10, 6, 11, 0, 0));
		/// ev.Title = "Workshop";
		/// ]]></code>
		/// </example>
		public Event Add(DateTime start, DateTime end)
		{
			var ev = new Event(start, end);
			Add(ev);
			return ev;
		}

		/// <summary>
		/// Adds a new <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to the collection.
		/// </summary>
		/// <param name="start">The starting <see cref="T:System.DateTime"/> date and time of the event.</param>
		/// <param name="duration">The <see cref="T:System.TimeSpan"/> duration of the event.</param>
		/// <returns>The newly added <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.</returns>
		/// <exception cref="T:System.InvalidOperationException">The calendar is in virtual mode.</exception>
		/// <example>
		/// Adding a 45 minutes event:
		/// <code><![CDATA[
		/// this.fullCalendar1.Events.Add(new DateTime(2026, 10, 6, 14, 0, 0), TimeSpan.FromMinutes(45)).Title = "Interview";
		/// ]]></code>
		/// </example>
		public Event Add(DateTime start, TimeSpan duration)
		{
			var ev = new Event(start, duration);
			Add(ev);
			return ev;
		}

		/// <summary>
		/// Adds a new all-day <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to the collection.
		/// </summary>
		/// <param name="id">A string that represents the ID of this event.</param>
		/// <param name="day">The <see cref="T:System.DateTime"/> date of the all-day event.</param>
		/// <returns>The newly added <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.</returns>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="id"/> is null or empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">The calendar is in virtual mode.</exception>
		/// <remarks>
		/// If the collection already contains an event with the same <paramref name="id"/>, it is replaced.
		/// </remarks>
		/// <example>
		/// Adding an all-day event with a known ID:
		/// <code><![CDATA[
		/// this.fullCalendar1.Events.Add("holiday-2026-12-25", new DateTime(2026, 12, 25)).Title = "Christmas";
		/// ]]></code>
		/// </example>
		public Event Add(string id, DateTime day)
		{
			var ev = new Event(id, day);
			Add(ev);
			return ev;
		}

		/// <summary>
		/// Adds a new <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to the collection.
		/// </summary>
		/// <param name="id">A string that represents the ID of this event.</param>
		/// <param name="start">The starting <see cref="T:System.DateTime"/> date and time of the event.</param>
		/// <param name="end">The ending <see cref="T:System.DateTime"/> date and time of the event.</param>
		/// <returns>The newly added <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.</returns>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="id"/> is null or empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">The calendar is in virtual mode.</exception>
		/// <remarks>
		/// If the collection already contains an event with the same <paramref name="id"/>, it is replaced.
		/// </remarks>
		/// <example>
		/// Adding an event using the primary key of a record as the ID:
		/// <code><![CDATA[
		/// var ev = this.fullCalendar1.Events.Add(appointment.Id.ToString(), appointment.Start, appointment.End);
		/// ev.Title = appointment.Subject;
		/// ]]></code>
		/// </example>
		public Event Add(string id, DateTime start, DateTime end)
		{
			var ev = new Event(id, start, end);
			Add(ev);
			return ev;
		}

		/// <summary>
		/// Adds a new <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to the collection.
		/// </summary>
		/// <param name="id">A string that represents the ID of this event.</param>
		/// <param name="start">The starting <see cref="T:System.DateTime"/> date and time of the event.</param>
		/// <param name="duration">The <see cref="T:System.TimeSpan"/> duration of the event.</param>
		/// <returns>The newly added <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/>.</returns>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="id"/> is null or empty.</exception>
		/// <exception cref="T:System.InvalidOperationException">The calendar is in virtual mode.</exception>
		/// <remarks>
		/// If the collection already contains an event with the same <paramref name="id"/>, it is replaced.
		/// </remarks>
		/// <example>
		/// Adding a two hours event with an ID:
		/// <code><![CDATA[
		/// this.fullCalendar1.Events.Add("training", new DateTime(2026, 10, 8, 13, 0, 0), TimeSpan.FromHours(2)).Title = "Training";
		/// ]]></code>
		/// </example>
		public Event Add(string id, DateTime start, TimeSpan duration)
		{
			var ev = new Event(id, start, duration);
			Add(ev);
			return ev;
		}

		/// <summary>
		/// Adds a new <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to the collection.
		/// </summary>
		/// <param name="ev">The <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="ev"/> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The calendar is in virtual mode.</exception>
		/// <remarks>
		/// If the collection already contains an event with the same ID, it is replaced. If the event has no ID, an automatic ID is assigned.
		/// If the event belongs to another calendar, it's removed from it.
		/// </remarks>
		/// <example>
		/// Adding an event configured in advance:
		/// <code><![CDATA[
		/// this.fullCalendar1.Events.Add(new Event(new DateTime(2026, 10, 6, 9, 0, 0), TimeSpan.FromHours(1))
		/// {
		///     Title = "Sprint planning",
		///     BackgroundColor = Color.MediumPurple
		/// });
		/// ]]></code>
		/// </example>
		public void Add(Event ev)
		{
			if (this.owner.VirtualMode)
				throw new InvalidOperationException("Can't add events when in virtual mode.");

			if (ev == null)
				throw new ArgumentNullException("event");

			// replace an event with the same ID?
			var existingIndex = this.list.FindIndex(o => o.Id == ev.Id);
			if (existingIndex > -1)
			{
				var oldEvent = this.list[existingIndex];
				this.list[existingIndex] = ev;

				if (oldEvent != ev)
					oldEvent.owner = null;
			}
			else
			{
				this.list.Add(ev);
			}

			BindEvent(ev);
			this.owner.OnEventAdded(ev);

			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, ev));
		}

		/// <summary>
		/// Adds the list of <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> items to the collection.
		/// </summary>
		/// <param name="list">The list of events to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="list"/> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The calendar is in virtual mode.</exception>
		/// <remarks>
		/// Events with the same ID of an event already in the collection replace the existing events.
		/// The events on the client are reloaded only once.
		/// </remarks>
		/// <example>
		/// Loading a set of events at once:
		/// <code><![CDATA[
		/// var events = appointments
		///     .Select(a => new Event(a.Id.ToString(), a.Start, a.End) { Title = a.Subject })
		///     .ToList();
		///
		/// this.fullCalendar1.Events.Clear();
		/// this.fullCalendar1.Events.AddRange(events);
		/// ]]></code>
		/// </example>
		public void AddRange(ICollection<Event> list)
		{
			if (this.owner.VirtualMode)
				throw new InvalidOperationException("Can't add events when in virtual mode.");

			if (list == null)
				throw new ArgumentNullException("list");

			foreach (var ev in list)
			{
				// replace an event with the same ID?
				var existingIndex = this.list.FindIndex(o => o.Id == ev.Id);
				if (existingIndex > -1)
				{
					var oldEvent = this.list[existingIndex];
					this.list[existingIndex] = ev;

					if (oldEvent != ev)
						oldEvent.owner = null;
				}
				else
				{
					this.list.Add(ev);
				}

				BindEvent(ev);
				this.owner.OnEventAdded(ev);
			}

			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>
		/// Removes all events.
		/// </summary>
		/// <remarks>
		/// Does nothing when the calendar is in virtual mode.
		/// </remarks>
		/// <example>
		/// Removing all the events:
		/// <code><![CDATA[
		/// this.fullCalendar1.Events.Clear();
		/// ]]></code>
		/// </example>
		public void Clear()
		{
			if (this.owner.VirtualMode)
				return;

			var array = this.list.ToArray();
			this.list.Clear();

			foreach (var ev in array)
			{
				UnbindEvent(ev);
			}

			this.owner.OnEventRemoved(null);

			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>
		/// Checks if the specified <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> exists in the collection.
		/// </summary>
		/// <param name="ev">The <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to look for.</param>
		/// <returns>true if <paramref name="ev"/> is in the collection; otherwise, false.</returns>
		/// <remarks>
		/// The search compares the event instances, not the IDs. In virtual mode it always returns false.
		/// </remarks>
		/// <example>
		/// Adding an event only if it's not already in the calendar:
		/// <code><![CDATA[
		/// if (!this.fullCalendar1.Events.Contains(ev))
		///     this.fullCalendar1.Events.Add(ev);
		/// ]]></code>
		/// </example>
		public bool Contains(Event ev)
		{
			return this.list.Contains(ev);
		}

		/// <summary>
		/// Copies all events to the specified array.
		/// </summary>
		/// <param name="array">The destination array.</param>
		/// <param name="arrayIndex">The index at which to begin the copy.</param>
		/// <remarks>
		/// In virtual mode there are no events to copy.
		/// </remarks>
		/// <example>
		/// Copying the events to an array:
		/// <code><![CDATA[
		/// var array = new Event[this.fullCalendar1.Events.Count];
		/// this.fullCalendar1.Events.CopyTo(array, 0);
		/// ]]></code>
		/// </example>
		public void CopyTo(Event[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Returns the index of the specified <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> in the collection.
		/// </summary>
		/// <param name="ev">The <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to look for.</param>
		/// <returns>The index of <paramref name="ev"/> in the collection, or -1 if not found.</returns>
		/// <example>
		/// Removing an event using its index:
		/// <code><![CDATA[
		/// int index = this.fullCalendar1.Events.IndexOf(ev);
		/// if (index > -1)
		///     this.fullCalendar1.Events.RemoveAt(index);
		/// ]]></code>
		/// </example>
		public int IndexOf(Event ev)
		{
			return this.list.IndexOf(ev);
		}

		/// <summary>
		/// Inserts the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> in the collection at the specified position.
		/// </summary>
		/// <param name="index">The position in the collection where to insert the event.</param>
		/// <param name="ev">The <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to insert.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="ev"/> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The calendar is in virtual mode.</exception>
		/// <remarks>
		/// If the collection already contains an event with the same ID, the existing event is replaced in its current position
		/// and <paramref name="index"/> is ignored. The position in the collection doesn't affect how the events are displayed.
		/// </remarks>
		/// <example>
		/// Inserting an event at the beginning of the collection:
		/// <code><![CDATA[
		/// this.fullCalendar1.Events.Insert(0, new Event(new DateTime(2026, 10, 1)) { Title = "Quarter start" });
		/// ]]></code>
		/// </example>
		public void Insert(int index, Event ev)
		{
			if (this.owner.VirtualMode)
				throw new InvalidOperationException("Can't add events when in virtual mode.");

			if (ev == null)
				throw new ArgumentNullException("event");

			// replace an event with the same ID?
			var existingIndex = this.list.FindIndex(o => o.Id == ev.Id);
			if (existingIndex > -1)
			{
				var oldEvent = this.list[existingIndex];
				this.list[existingIndex] = ev;

				if (oldEvent != ev)
					oldEvent.owner = null;
			}
			else
			{
				this.list.Insert(index, ev);
			}

			BindEvent(ev);
			this.owner?.OnEventAdded(ev);

			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, ev));
		}

		/// <summary>
		/// Removes the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> from the collection and updates the calendar.
		/// </summary>
		/// <param name="ev">The <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to remove.</param>
		/// <returns>Always true, also when <paramref name="ev"/> was not in the collection.</returns>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="ev"/> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The calendar is in virtual mode.</exception>
		/// <example>
		/// Removing the event that was double clicked:
		/// <code><![CDATA[
		/// private void fullCalendar1_EventDoubleClick(object sender, EventClickEventArgs e)
		/// {
		///     this.fullCalendar1.Events.Remove(e.Event);
		/// }
		/// ]]></code>
		/// </example>
		public bool Remove(Event ev)
		{
			if (this.owner.VirtualMode)
				throw new InvalidOperationException("Can't remove an event when in virtual mode.");

			if (ev == null)
				throw new ArgumentNullException("event");

			UnbindEvent(ev);
			this.list.Remove(ev);
			this.owner?.OnEventRemoved(ev);

			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, ev));
			return true;
		}

		/// <summary>
		/// Removes the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> at the specified index from the collection and updates the calendar.
		/// </summary>
		/// <param name="index">The index of the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or not less than <see cref="Count"/>.</exception>
		/// <exception cref="T:System.InvalidOperationException">The calendar is in virtual mode.</exception>
		/// <example>
		/// Removing the last event:
		/// <code><![CDATA[
		/// var events = this.fullCalendar1.Events;
		/// if (events.Count > 0)
		///     events.RemoveAt(events.Count - 1);
		/// ]]></code>
		/// </example>
		public void RemoveAt(int index)
		{
			if (this.owner.VirtualMode)
				throw new InvalidOperationException("Can't remove an event when in virtual mode.");

			Event ev = this[index];
			UnbindEvent(ev);

			this.list.RemoveAt(index);
			this.owner?.OnEventRemoved(ev);

			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, ev));
		}

		/// <summary>
		/// Returns an enumerator that iterates all the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> objects in the collection.
		/// </summary>
		/// <returns>An enumerator for the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> objects in the collection.</returns>
		/// <remarks>
		/// In virtual mode the enumerator doesn't return any event.
		/// </remarks>
		/// <example>
		/// Iterating the events using foreach:
		/// <code><![CDATA[
		/// foreach (var ev in this.fullCalendar1.Events)
		/// {
		///     if (ev.Start.Date == DateTime.Today)
		///         ev.BackgroundColor = Color.Red;
		/// }
		/// ]]></code>
		/// </example>
		public IEnumerator<Event> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Updates the related chart when the properties change.
		private void Update()
		{
			if (this.owner != null)
				this.owner.Update();
		}

		// Binds the event to the owner and unbinds it from the previous one, if any.
		private void BindEvent(Event ev)
		{
			UnbindEvent(ev);

			ev.owner = this.owner;
			if (String.IsNullOrEmpty(ev.Id))
				ev.Id = GenerateAutomaticId();
		}

		// Binds the event to the owner and unbinds it from the previous one, if any.
		private void UnbindEvent(Event ev)
		{
			if (ev.owner != null)
			{
				if (ev.owner == this.owner)
					ev.owner = null;
				else if (!ev.owner.VirtualMode)
					ev.owner.Events.Remove(ev);
			}
		}

		// Create a new automatic event ID
		private string GenerateAutomaticId()
		{
			lock (this)
			{
				var id = "";
				var name = "event_";
				var autoId = this._autoId;
				do
				{
					++autoId;
					if (autoId == int.MaxValue)
					{
						autoId = 1;
					}
					id = name + autoId;
				}
				while (this.list.FindIndex(o => o.Id == id) > -1);

				this._autoId = autoId;

				return id;
			}
		}
		private int _autoId = 0;

		#region Events

		/// <summary>
		/// Fired when the collection changes.
		/// </summary>
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.FullCalendar.EventCollection.CollectionChanged" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.CollectionChanged != null)
				CollectionChanged(this, e);
		}

		#endregion

		#region IList

		bool ICollection<Event>.IsReadOnly
		{
			get { return false; }
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		#endregion
	}
}
