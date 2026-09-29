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
	/// Represents a split button in a <see cref="RibbonBarGroup"/>.
	/// </summary>
	/// <remarks>
	/// Unlike the <see cref="RibbonBarItemButton"/>, which opens the drop down menu when clicked anywhere, the split button
	/// is made of two parts: clicking the main part fires the <see cref="RibbonBarItem.Click"/> and <see cref="RibbonBar.ItemClick"/>
	/// events, while clicking the arrow opens the drop down menu defined in <see cref="RibbonBarItemButton.MenuItems"/>.
	/// </remarks>
	/// <example>
	/// Creating a split button that executes the default action or shows more options:
	/// <code><![CDATA[
	/// var undo = new RibbonBarItemSplitButton { Name = "undo", Text = "Undo", ImageSource = "icon-undo" };
	/// undo.MenuItems.Add(new MenuItem("Undo All") { Name = "undoAll" });
	/// undo.Click += (s, e) => Undo();
	/// undo.ItemClicked += (s, e) =>
	/// {
	///     if (e.MenuItem.Name == "undoAll")
	///         UndoAll();
	/// };
	/// this.ribbonBarGroup1.Items.Add(undo);
	/// ]]></code>
	/// </example>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[ApiCategory("RibbonBar")]
	public class RibbonBarItemSplitButton : RibbonBarItemButton
	{

		#region Properties

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(Core.WisejEventArgs e)
		{
			switch (e.Type)
			{
				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ribbonBar.ItemSplitButton";

			config.Delete("showArrow");
			config.wiredEvents.Add("execute");
		}

		#endregion

	}
}