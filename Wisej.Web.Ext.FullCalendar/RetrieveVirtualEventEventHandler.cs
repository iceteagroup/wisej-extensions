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

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.RetrieveVirtualEvent" /> event. 
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.FullCalendar.RetrieveVirtualEventEventArgs" />  that contains the event data. </param>
	/// <exception cref="T:System.InvalidOperationException">
	/// The <see cref="P:Wisej.Web.Ext.FullCalendar.RetrieveVirtualEventEventArgs.Event" /> 
	/// property is null when the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.RetrieveVirtualEvent" /> event is handled.
	/// </exception>    
	public delegate void RetrieveVirtualEventEventHandler(object sender, RetrieveVirtualEventEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.RetrieveVirtualEvent" /> event.
	/// </summary>
	/// <remarks>
	/// The event is requested either by index (<see cref="EventIndex"/>) or by ID (<see cref="EventID"/>).
	/// </remarks>
	/// <example>
	/// Returning the requested event from a cache:
	/// <code><![CDATA[
	/// private void fullCalendar1_RetrieveVirtualEvent(object sender, RetrieveVirtualEventEventArgs e)
	/// {
	///     // this.cache is your own List<Event>.
	///     e.Event = e.EventID != null
	///         ? this.cache.Find(ev => ev.Id == e.EventID)
	///         : this.cache[e.EventIndex];
	/// }
	/// ]]></code>
	/// </example>
	[ApiCategory("FullCalendar")]
	public class RetrieveVirtualEventEventArgs : EventArgs
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.FullCalendar.RetrieveVirtualEventEventArgs" /> class.
		/// </summary>
		/// <param name="index">The index of the event to retrieve.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.</exception>
		public RetrieveVirtualEventEventArgs(int index)
        {
			if (index < 0)
				throw new ArgumentOutOfRangeException("index");

            this.EventIndex = index;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.FullCalendar.RetrieveVirtualEventEventArgs" /> class.
		/// </summary>
		/// <param name="id">The ID of the event to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="id"/> is null.</exception>
		public RetrieveVirtualEventEventArgs(string id)
		{
			if (id == null)
				throw new ArgumentNullException("id");

			this.EventID = id;
		}

		/// <summary>
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> retrieved from the cache.
		/// </summary>
		/// <returns>The <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> retrieved from the cache.</returns>
		/// <remarks>
		/// The handler must set this property: when it's null, the <see cref="T:Wisej.Web.Ext.FullCalendar.EventCollection"/> indexer throws an <see cref="T:System.InvalidOperationException"/>.
		/// </remarks>
		public Event Event
        {
            get;
            set;
        }

		/// <summary>
		/// Returns the index of the <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> to retrieve from the cache.
		/// </summary>
		/// <returns>The index of the <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> to retrieve from the cache.</returns>
		/// <remarks>
		/// The value is 0 when the event is requested by ID; check <see cref="EventID"/> first.
		/// </remarks>
		public int EventIndex
        {
            get;
            private set;
        }

		/// <summary>
		/// Returns the ID of the <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> to retrieve from the cache.
		/// </summary>
		/// <returns>The ID of the <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> to retrieve from the cache.</returns>
		/// <remarks>
		/// The value is null when the event is requested by index (<see cref="EventIndex"/>). The calendar requests events by ID
		/// when the user clicks, drags or resizes an event.
		/// </remarks>
		public string EventID
		{
			get;
			private set;
		}
	}
}
