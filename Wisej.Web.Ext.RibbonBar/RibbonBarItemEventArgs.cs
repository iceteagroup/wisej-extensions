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

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Contains the data for the events in a <see cref="RibbonBar"/> control
	/// fired by its <see cref="RibbonBarItem"/> components.
	/// </summary>
	public class RibbonBarItemEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of <see cref="RibbonBarItemEventArgs"/>
		/// using the specified parameters.
		/// </summary>
		/// <param name="item">The <see cref="RibbonBarItem"/> that originated the event.</param>
		public RibbonBarItemEventArgs(RibbonBarItem item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			this.Item = item;
		}

		/// <summary>
		/// The <see cref="RibbonBarItem"/> that originated the event.
		/// </summary>
		/// <returns>A reference to the <see cref="RibbonBarItem"/> that originated this event.</returns>
		public RibbonBarItem Item
		{
			get;
			private set;
		}
	}
}
