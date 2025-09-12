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

using System.ComponentModel;
using System.Drawing;
using System;
using Wisej.Base;

namespace Wisej.Web.Ext.ToolStrip
{
	public partial class ToolStripTextBox
	{
		public class ToolStripTextBoxControl : TextBox
		{

			#region Constructors

			public ToolStripTextBoxControl()
			{
				// TODO: Implement
			}

			#endregion

			#region Properties

			public override Font Font
			{
				get
				{
					return this._font;
				}
				set
				{
					if ((this._font != value))
					{
						this._font = value;
					}
				}
			}

			private Font _font;

			public ToolStripTextBox Owner
			{
				get
				{
					return this._owner;
				}
				set
				{
					if ((this._owner != value))
					{
						this._owner = value;
					}
				}
			}

			private ToolStripTextBox _owner;

			#endregion

			#region Methods

			[EditorBrowsable(EditorBrowsableState.Advanced)]
			protected override void OnGotFocus(EventArgs e)
			{
				base.OnGotFocus(e);
				// TODO: Implement
			}

			[EditorBrowsable(EditorBrowsableState.Advanced)]
			protected override void OnLostFocus(EventArgs e)
			{
				base.OnLostFocus(e);
				// TODO: Implement
			}

			[EditorBrowsable(EditorBrowsableState.Advanced)]
			protected override void OnMouseEnter(EventArgs e)
			{
				base.OnMouseEnter(e);
				// TODO: Implement
			}

			[EditorBrowsable(EditorBrowsableState.Advanced)]
			protected override void OnMouseLeave(EventArgs e)
			{
				base.OnMouseLeave(e);
				// TODO: Implement
			}

			[EditorBrowsable(EditorBrowsableState.Advanced)]
			protected override void OnVisibleChanged(EventArgs e)
			{
				base.OnVisibleChanged(e);
				// TODO: Implement
			}

			[EditorBrowsable(EditorBrowsableState.Advanced)]
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				// TODO: Implement
				return new System.Windows.Forms.AccessibleObject();
			}

			protected override void Dispose(bool disposing)
			{
				// TODO: Implement
			}

			#endregion
		}

	}
}
