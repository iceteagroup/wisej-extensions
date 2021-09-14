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
using System.Drawing;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.ItemDrop" /> event 
	/// in a <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.FullCalendar.ItemDropEventArgs" /> that contains the event data. </param>
	public delegate void ItemDropEventHandler(object sender, ItemDropEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.ItemDrop" /> event of 
	/// the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar" /> control.
	/// </summary>
	[ApiCategory("FullCalendar")]
	public class ItemDropEventArgs : DayClickEventArgs
	{
		/// <summary>
		///  Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.ItemDropEventArgs"/>.
		/// </summary>
		/// <param name="item">The <see cref="T:Wisej.Web.Control"/> that is being dropped.</param>
		/// <param name="day">The day/time that was clicked by the user.</param>
		/// <param name="location">The location of a pointer click, in pixels.</param>
		/// <param name="resourceId">ID of the <see cref="Resource"/> where the <paramref name="item"/> is being dropped on; or null.</param>
		internal ItemDropEventArgs(Control item, DateTime day, Point location, string resourceId)
			: base(day, MouseButtons.Left, location)
		{
			this.Item = item;
			this.ResourceId = resourceId;
		}

		/// <summary>
		/// Returns the <see cref="T:Wisej.Web.Control"/> that is being dropped on the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/>.
		/// </summary>
		/// <returns>The <see cref="T:Wisej.Web.Control"/> that is being dropped.</returns>
		public Control Item
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the id of the <see cref="Resource"/> where the <see cref="Item"/> is being dropped on.
		/// </summary>
		public string ResourceId
		{
			get;
			private set;
		}
	}
}
