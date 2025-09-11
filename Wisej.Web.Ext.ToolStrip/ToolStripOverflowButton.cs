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
using Wisej.Core;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Hosts a <see cref="ToolStripDropDown" /> that displays items that overflow the <see cref="ToolStrip" />.
	///</summary>
	public class ToolStripOverflowButton : ToolStripDropDownButton
	{

		#region Properties

		/// <summary>
		/// Gets a value indicating whether the <see cref="ToolStripOverflowButton" /> has items that overflow the <see cref="ToolStrip" />.
		///</summary>
		/// <returns>true if the <see cref="ToolStripOverflowButton" /> has overflow items; otherwise, false. </returns>
		[Browsable(false)]
		public override bool HasDropDownItems
		{
			get
			{
				return this._hasDropDownItems;
			}
		}

		private bool _hasDropDownItems;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true to enable automatic mirroring; otherwise, false.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool RightToLeftAutoMirrorImage
		{
			get
			{
				return this._rightToLeftAutoMirrorImage;
			}
			set
			{
				if ((this._rightToLeftAutoMirrorImage != value))
				{
					this._rightToLeftAutoMirrorImage = value;
				}
			}
		}

		private bool _rightToLeftAutoMirrorImage;

		#endregion

		#region Methods
		protected override void Dispose(bool disposing)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Creates an empty <see cref="ToolStripDropDown" /> that can be dropped down and to which events can be attached.
		///</summary>
		/// <returns>A <see cref="ToolStripDropDown" /> control.</returns>
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			// TODO: Implement
			return new ToolStripDropDown();
		}

		/// <summary>
		/// Creates a new accessibility object for the control.
		///</summary>
		/// <returns>A new <see cref="ToolStrip.Compatibility.AccessibleObject" /> for the control.</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			// TODO: Implement
			return new Wisej.Web.Ext.ToolStrip.Compatibility.AccessibleObject();
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
