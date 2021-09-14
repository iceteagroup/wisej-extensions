///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Contains the data for the events in a <see cref="RibbonBar"/> control
	/// fired by its <see cref="RibbonBarItem"/> components with drop down menu items..
	/// </summary>
	[ApiCategory("RibbonBar")]
	public class RibbonBarMenuItemEventArgs : MenuItemEventArgs
	{
		/// <summary>
		/// Initializes a new instance of <see cref="RibbonBarItemEventArgs"/>
		/// using the specified parameters.
		/// </summary>
		/// <param name="item">The <see cref="RibbonBarItemButton"/> that originated the event.</param>
		/// <param name="menuItem">The <see cref="MenuItem"/> that was clicked or tapped.</param>
		public RibbonBarMenuItemEventArgs(RibbonBarItemButton item, MenuItem menuItem)
			: base(menuItem)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));
			if (menuItem == null)
				throw new ArgumentNullException(nameof(menuItem));

			this.Item = item;
		}

		/// <summary>
		/// The <see cref="RibbonBarItemButton"/> that originated the event.
		/// </summary>
		/// <returns>A reference to the <see cref="RibbonBarItem"/> that originated this event.</returns>
		public RibbonBarItemButton Item
		{
			get;
			private set;
		}
	}
}
