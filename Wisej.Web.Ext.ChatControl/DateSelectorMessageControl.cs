using System;
using Wisej.Web;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// A simple message control for displaying a calendar.
	/// </summary>
	public partial class DateSelectorMessageControl : MessageControl
	{
		public DateSelectorMessageControl()
		{
			InitializeComponent();
		}

		private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
		{
			this.SetMessageResult(new { e.Start, e.End });
		}
	}
}
