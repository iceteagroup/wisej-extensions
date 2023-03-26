///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.NavigationBar
{
	public partial class NavigationBarItem
	{
		/// <summary>
		/// Implementation of the top header part in the <see cref="NavigationBarItem"/>.
		/// Used to switch the sender when redirecting pointer events.
		/// </summary>
		public class Header : FlexLayoutPanel
		{
			private NavigationBarItem NavigationBarItem
				=> (NavigationBarItem)this.Parent;

			protected override void OnClick(EventArgs e)
			{
				this.NavigationBarItem.OnClick(e);
			}

			protected override void OnTap(EventArgs e)
			{
				this.NavigationBarItem.OnTap(e);
			}

			protected override void OnLongTap(EventArgs e)
			{
				this.NavigationBarItem.OnLongTap(e);
			}

			protected override void OnSwipe(SwipeEventArgs e)
			{
				this.NavigationBarItem.OnSwipe(e);
			}

			protected override void OnMouseClick(MouseEventArgs e)
			{
				this.NavigationBarItem.OnMouseClick(e);
			}

			protected override void OnMouseDown(MouseEventArgs e)
			{
				this.NavigationBarItem.OnMouseDown(e);
			}

			protected override void OnMouseUp(MouseEventArgs e)
			{
				this.NavigationBarItem.OnMouseUp(e);
			}

			protected override void OnMouseEnter(EventArgs e)
			{
				this.NavigationBarItem.OnMouseEnter(e);
			}

			protected override void OnMouseLeave(EventArgs e)
			{
				this.NavigationBarItem.OnMouseLeave(e);
			}
		}
	}
}
