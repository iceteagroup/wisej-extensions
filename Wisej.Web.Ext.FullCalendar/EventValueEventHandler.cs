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
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.EventChanged" /> event.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.FullCalendar.EventChangedEventArgs" /> that contains the event data. </param>
	public delegate void EventValueEventHandler(object sender, EventValueEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.EventChanged" /> event.
	/// </summary>
	[ApiCategory("FullCalendar")]
	public class EventValueEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.EventValueEventArgs"/>.
		/// </summary>
		/// <param name="event">The <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> that has been changed by the user.</param>
		/// <param name="oldStartDate">The previous value of the <see cref="P:Wisej.Web.Ext.FullCalendar.Event.Start"/> property.</param>
		/// <param name="oldEndDate">The previous value of the <see cref="P:Wisej.Web.Ext.FullCalendar.Event.End"/> property.</param>
		public EventValueEventArgs(Event @event, DateTime oldStartDate, DateTime oldEndDate)
		{
			this.Event = @event;
			this.OldEndDate = oldEndDate;
			this.OldStartDate = oldStartDate;
		}

		/// <summary>
		/// Returns the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> that has been changed by the user.
		/// </summary>
		public Event Event
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the previous value of the <see cref="P:Wisej.Web.Ext.FullCalendar.Event.Start"/> property.
		/// </summary>
		public DateTime OldStartDate
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the previous value of the <see cref="P:Wisej.Web.Ext.FullCalendar.Event.End"/> property.
		/// </summary>
		public DateTime OldEndDate
		{
			get;
			private set;
		}
	}
}
