///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.NavigationBar
{
	/// <summary>
	/// Represents a method that handles the <see cref="NavigationBar.ItemClick"/> event.
	/// </summary>
	/// <param name="sender">The <see cref="NavigationBar"/> that fired the event.</param>
	/// <param name="e">An instance of <see cref="NavigationBarItemClickEventArgs"/> containing the event data.</param>
	public delegate void NavigationBarItemClickEventHandler(object sender, NavigationBarItemClickEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="NavigationBar.ItemClick"/> event.
	/// </summary>
	public class NavigationBarItemClickEventArgs: EventArgs
	{
		/// <summary>
		/// Initializes a new instance of <see cref="NavigationBarItemClickEventArgs"/>.
		/// </summary>
		/// <param name="item">The <see cref="NavigationBarItem"/> that triggered the event.</param>
		public NavigationBarItemClickEventArgs(NavigationBarItem item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			this.Item = item;
		}

		/// <summary>
		/// The <see cref="NavigationBarItem"/> that triggered the event.
		/// </summary>
		public NavigationBarItem Item
		{
			get;
			private set;
		}
	}
}
