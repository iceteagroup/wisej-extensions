using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.FullCalendar
{
	public class EventMouseLeaveArgs : MouseEventArgs
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.EventMouseLeaveArgs"/>.
		/// </summary>
		/// <param name="button">One of the <see cref="T:Wisej.Web.MouseButtons"/> values that indicate which mouse button was pressed.</param>
		/// <param name="location">The location of a pointer click, in pixels.</param>
		public EventMouseLeaveArgs(Event ev, MouseButtons button, Point location)
			: base(button, 1, location.X, location.Y, 0)
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
