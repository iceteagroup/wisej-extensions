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
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.DayClick" /> event 
	/// in a <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.FullCalendar.DayClickEventArgs" /> that contains the event data. </param>
	public delegate void DayClickEventHandler(object sender, DayClickEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.DayClick" /> event of 
	/// the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> control.
	/// </summary>
	public class DayClickEventArgs : MouseEventArgs
	{
		/// <summary>
		///  Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.EventClickEventArgs"/>.
		/// </summary>
		/// <param name="day">The day/time that was clicked by the user.</param>
		/// <param name="button">One of the <see cref="T:Wisej.Web.MouseButtons"/> values that indicate which mouse button was pressed.</param>
		/// <param name="location">The location of a pointer click, in pixels.</param>
		internal DayClickEventArgs(DateTime day, MouseButtons button, Point location)
			: base(button, 1, location.X, location.Y, 0)
		{
			this.Day = day;
		}

		/// <summary>
		///  Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.EventClickEventArgs"/>.
		/// </summary>
		/// <param name="day">The day/time that was clicked by the user.</param>
		/// <param name="button">One of the <see cref="T:Wisej.Web.MouseButtons"/> values that indicate which mouse button was pressed.</param>
		/// <param name="clicks">The number of times a pointer was pressed.</param>
		/// <param name="location">The location of a pointer click, in pixels.</param>
		internal DayClickEventArgs(DateTime day, MouseButtons button, int clicks, Point location)
			: base(button, clicks, location.X, location.Y, 0)
		{
			this.Day = day;
		}

		/// <summary>
		/// Returns the <see cref="T:System.DateTime"/> that represents the day/time that was clicked in the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> control.
		/// </summary>
		public DateTime Day
		{
			get;
			private set;
		}
	}
}
