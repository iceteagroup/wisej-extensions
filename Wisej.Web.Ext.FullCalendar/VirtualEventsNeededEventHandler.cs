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

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.FullCalendar.VirtualEventsNeeded" /> event of 
	/// a <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" />.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.FullCalendar.VirtualEventsNeededEventArgs" /> that contains the event data.</param>        
	public delegate void VirtualEventsNeededEventHandler(object sender, VirtualEventsNeededEventArgs e);


	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.FullCalendar.VirtualEventsNeededEventArgs" /> event.
	/// </summary>
	[ApiCategory("FullCalendar")]
	public class VirtualEventsNeededEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.FullCalendar.VirtualEventsNeededEventArgs" /> class with the specified starting and ending indices.
		/// </summary>
		/// <param name="startDate">The starting date/time of a range of events needed by the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> for the next <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.RetrieveVirtualEvent" /> event that occurs.</param>
		/// <param name="endDate">The ending date/time of a range of events needed by the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> for the next <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.RetrieveVirtualEvent" /> event that occurs.</param>
		public VirtualEventsNeededEventArgs(DateTime startDate, DateTime endDate)
		{
			this.StartDate = startDate;
			this.EndDate = endDate;
		}

		/// <summary>
		/// Returns the ending date/time for the range of events needed by 
		/// a <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> control in virtual mode.
		/// </summary>
		/// <returns>The date/time at the end of the range of events needed by the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> control.</returns>        
		public DateTime EndDate
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the starting date/time for a range of events needed by 
		/// a <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> control in virtual mode.
		/// </summary>
		/// <returns>The date/time at the start of the range of events needed by the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> control.</returns>
		public DateTime StartDate
		{
			get;
			private set;
		}

		/// <summary>
		/// Represents the set of <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> objects retrieved.
		/// </summary>
		public IEnumerable<Event> Events
		{
			get;
			set;
		}
	}
}