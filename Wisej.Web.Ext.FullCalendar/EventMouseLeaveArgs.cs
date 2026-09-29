using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.EventMouseLeave" /> event of
	/// the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> control.
	/// </summary>
	/// <remarks>
	/// The mouse location is relative to the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> control.
	/// </remarks>
	public class EventMouseLeaveArgs : MouseEventArgs
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.EventMouseLeaveArgs"/>.
		/// </summary>
		/// <param name="ev">The <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> that the mouse pointer leaves.</param>
		/// <param name="button">One of the <see cref="T:Wisej.Web.MouseButtons"/> values that indicate which mouse button was pressed.</param>
		/// <param name="location">The location of the mouse pointer, in pixels.</param>
		public EventMouseLeaveArgs(Event ev, MouseButtons button, Point location)
			: base(button, 1, location.X, location.Y, 0)
		{
			this.Event = ev;
		}

		/// <summary>
		/// Returns the <see cref="T:Wisej.Web.Ext.FullCalendar.Event"/> that the mouse pointer leaves in the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> control.
		/// </summary>
		public Event Event
		{
			get;
			private set;
		}
	}
}
