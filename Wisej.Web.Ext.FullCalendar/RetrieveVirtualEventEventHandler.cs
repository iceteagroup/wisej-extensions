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
	/// Provides data for the <see cref="E:Wisej.Web.Ext.FullCalendar.RetrieveVirtualEventEventArgs" /> event. 
	/// </summary>    
	[ApiCategory("FullCalendar")]
	public class RetrieveVirtualEventEventArgs : EventArgs
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.FullCalendar.RetrieveVirtualEventEventArgs" /> class. 
		/// </summary>
		/// <param name="index">The index of the event to retrieve.</param>
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
		public Event Event
        {
            get;
            set;
        }

		/// <summary>
		/// Returns the index of the <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> to retrieve from the cache.
		/// </summary>
		/// <returns>The index of the <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> to retrieve from the cache.</returns>        
		public int EventIndex
        {
            get;
            private set;
        }

		/// <summary>
		/// Returns the ID of the <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> to retrieve from the cache.
		/// </summary>
		/// <returns>The ID of the <see cref="T:Wisej.Web.Ext.FullCalendar.Event" /> to retrieve from the cache.</returns>        
		public string EventID
		{
			get;
			private set;
		}
	}
}
