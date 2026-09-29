///////////////////////////////////////////////////////////////////////////////
//
// (C) 2022 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.NavigationBar
{
	/// <summary>
	/// Represents the context menu shown when <see cref="NavigationBar.CompactView"/> is true
	/// and the user expands a top-level item with child items.
	/// </summary>
	/// <remarks>
	/// The menu is created by <see cref="NavigationBarItem"/> with one <see cref="NavigationBarMenuItem"/> for
	/// each child item (nested items become submenus) and it's disposed when closed. On the client it uses the
	/// "navbar-menu" appearance, and its items use "navbar-menu/item".
	/// </remarks>
	[ToolboxItem(false)]
	public class NavigationBarMenu : ContextMenu
	{
		#region Wisej Implementation

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.NavigationBarMenu";
		}

		#endregion
	}
}
