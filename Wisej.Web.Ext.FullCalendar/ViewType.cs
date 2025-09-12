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

using System.ComponentModel;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Enumerates the different "views", or ways of displaying days and events.
	/// </summary>
	[ApiCategory("FullCalendar")]
	public enum ViewType
	{
		/// <summary>
		/// Shows the events in a monthly calendar view.
		/// </summary>
		Month,

		/// <summary>
		/// Shows the events in a simple weekly view.
		/// </summary>
		BasicWeek,

		/// <summary>
		/// Shows the events in a single day view. 
		/// </summary>
		BasicDay,

		/// <summary>
		/// Shows the events in a weekly view with time slots.
		/// </summary>
		AgendaWeek,

		/// <summary>
		/// Shows the events in a single day view with time slots.
		/// </summary>
		AgendaDay,

		/// <summary>
		/// Displays all the events in a year in a flat list. If there are no events during a specific interval of time, the <see cref="P:Wisej.Web.Ext.FullCalendar.NoEventsMessage"/> string is displayed.
		/// </summary>
		ListYear,

		/// <summary>
		/// Displays all the events in a month in a flat list. If there are no events during a specific interval of time, the <see cref="P:Wisej.Web.Ext.FullCalendar.NoEventsMessage"/> string is displayed.
		/// </summary>
		ListMonth,

		/// <summary>
		/// Displays all the events in a week in a flat list. If there are no events during a specific interval of time, the <see cref="P:Wisej.Web.Ext.FullCalendar.NoEventsMessage"/> string is displayed.
		/// </summary>
		ListWeek,

		/// <summary>
		/// Displays all the events in a day in a flat list. If there are no events during a specific interval of time, the <see cref="P:Wisej.Web.Ext.FullCalendar.NoEventsMessage"/> string is displayed.
		/// </summary>
		ListDay,

		/// <summary>
		/// Shows a customizable horizontal time-axis and resources as rows.
		/// This is part of the scheduler commercial plug-in.
		/// </summary>
		TimelineDay,

		/// <summary>
		/// Shows a customizable horizontal time-axis and resources as rows.
		/// This is part of the scheduler commercial plug-in.
		/// </summary>
		TimelineWeek,

		/// <summary>
		/// Shows a customizable horizontal time-axis and resources as rows.
		/// This is part of the scheduler commercial plug-in.
		/// </summary>
		TimelineMonth,

		/// <summary>
		/// Shows a customizable horizontal time-axis and resources as rows.
		/// This is part of the scheduler commercial plug-in.
		/// </summary>
		TimelineYear
	}
}
