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
	/// <summary>
	/// Represents the move handle (grip) displayed on a <see cref="ToolStrip"/> when <see cref="ToolStrip.GripStyle"/> is <see cref="ToolStripGripStyle.Visible"/>.
	/// </summary>
	/// <remarks>
	/// The grip lets the user drag the <see cref="ToolStrip"/> within or between the panels of a <see cref="ToolStripContainer"/>.
	/// </remarks>
	public class ToolStripGrip : ToolStripButton
	{

		#region Properties

		/// <summary>
		/// Returns a value indicating whether the grip can be selected.
		/// </summary>
		/// <returns>false; the grip cannot receive the selection.</returns>
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
