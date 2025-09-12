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
using System.ComponentModel;
using System.Drawing;
using Wisej.Core;

namespace Wisej.Web.Ext.ToolStrip
{
	public class ToolStripGrip : ToolStripButton
	{

		#region Properties

		[Browsable(false)]
		public override bool CanSelect
		{
			get
			{
				return this._canSelect;
			}
		}

		private bool _canSelect;

		#endregion

		#region Methods

		protected override void OnMouseDown(MouseEventArgs mea)
		{
			base.OnMouseDown(mea);
			// TODO: Implement
		}

		protected override void OnMouseMove(MouseEventArgs mea)
		{
			base.OnMouseMove(mea);
			// TODO: Implement
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			// TODO: Implement
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			// TODO: Implement
		}

		protected override void OnMouseUp(MouseEventArgs mea)
		{
			base.OnMouseUp(mea);
			// TODO: Implement
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			// TODO: Implement
			return new Wisej.Web.Ext.ToolStrip.AccessibleObject();
		}

		#endregion

		#region Wisej Implementation

		protected override void OnWebEvent(WisejEventArgs e)
		{
			base.OnWebEvent(e);
		}

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender(config);
		}

		protected override void OnWebUpdate(dynamic config)
		{
			base.OnWebUpdate(config);
		}

		#endregion
	}

}
