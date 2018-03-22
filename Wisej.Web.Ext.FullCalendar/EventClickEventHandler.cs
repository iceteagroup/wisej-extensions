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
using System.Drawing;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.EventClick" /> event 
	/// in a <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.FullCalendar.EventClickEventArgs" /> that contains the event data. </param>
	public delegate void EventClickEventHandler(object sender, EventClickEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.EventClick" /> event of 
	/// the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> control.
	/// </summary>
	public class EventClickEventArgs : MouseEventArgs
	{
		/// <summary>
		///  Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.EventClickEventArgs"/>.
		/// </summary>
		/// <param name="ev">The <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> that was clicked by the user.</param>
		/// <param name="button">One of the <see cref="T:Wisej.Web.MouseButtons"/> values that indicate which mouse button was pressed.</param>
		/// <param name="clicks">The number of times a mouse button was pressed.</param>
		/// <param name="location">The location of a pointer click, in pixels.</param>
		internal EventClickEventArgs(Event ev, MouseButtons button, int clicks, Point location)
			: base(button, clicks, location.X, location.Y, 0)
		{
			this.Event = ev;
		}

		/// <summary>
		/// Returns the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> that was clicked in the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> control.
		/// </summary>
		public Event Event
		{
			get;
			private set;
		}
	}
}
