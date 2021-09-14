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
	/// fired by its <see cref="RibbonBarGroup"/> components.
	/// </summary>
	[ApiCategory("RibbonBar")]
	public class RibbonBarGroupEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of <see cref="RibbonBarGroupEventArgs"/>
		/// using the specified parameters.
		/// </summary>
		/// <param name="group">The <see cref="RibbonBarGroup"/> that originated the event.</param>
		public RibbonBarGroupEventArgs(RibbonBarGroup group)
		{
			if (group == null)
				throw new ArgumentNullException(nameof(group));

			this.Group = group;
		}

		/// <summary>
		/// The <see cref="RibbonBarGroup"/> that originated the event.
		/// </summary>
		/// <returns>A reference to the <see cref="RibbonBarGroup"/> that originated this event.</returns>
		public RibbonBarGroup Group
		{
			get;
			private set;
		}
	}
}
