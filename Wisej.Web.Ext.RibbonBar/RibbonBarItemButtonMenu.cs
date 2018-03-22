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

using System.ComponentModel;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a drop down menu associated to a 
	/// <see cref="RibbonBarItemButton"/> or a
	/// <see cref="RibbonBarItemSplitButton"/>.
	/// </summary>
	[ToolboxItem(false)]
	internal class RibbonBarItemButtonMenu : ContextMenu
	{
		internal RibbonBarItemButtonMenu(RibbonBarItemButton button)
		{
			this.Button = button;
		}

		/// <summary>
		/// Returns the <see cref="RibbonBarItemButton"/> or <see cref="RibbonBarItemSplitButton"/>
		/// that own this <see cref="RibbonBarItemButtonMenu"/>.
		/// </summary>
		public RibbonBarItemButton Button
		{
			get;
			private set;
		}

		/// <summary>
		/// Disposes of the resources, other than memory, used by the <see cref="RibbonBarItemButtonMenu" />.
		/// </summary>
		/// <param name="disposing">true when this method is called by the application rather than a finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Button = null;
			}

			base.Dispose(disposing);
		}
	}

	/// <summary>
	/// Extends the <see cref="MenuItem"/> class to support
	/// retrieving the <see cref="RibbonBarItemButton"/> that owns the
	/// context menu.
	/// </summary>
	/// <exclude/>
	public static class MenuItemExtensions
	{
		/// <summary>
		/// Returns the <see cref="RibbonBarItemButton"/> or <see cref="RibbonBarItemSplitButton"/>
		/// that are related to the <see cref="MenuItem"/>.
		/// </summary>
		/// <param name="item">The <see cref="MenuItem"/> to extend.</param>
		/// <returns></returns>
		public static RibbonBarItemButton GetRibbonBarItem(this MenuItem item)
		{
			var contextMenu = (RibbonBarItemButtonMenu)item.GetContextMenu();
			return contextMenu?.Button;
		}
	}
}