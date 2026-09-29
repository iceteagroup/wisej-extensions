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
using System.Drawing;
using Wisej.Core;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Represents the up or down arrow button used to scroll the items of a <see cref="ToolStripDropDownMenu"/> that doesn't fit on the screen.
	/// </summary>
	public partial class ToolStripScrollButton : ToolStripControlHost
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripScrollButton"/> class.
		/// </summary>
		/// <param name="up">true to create the button that scrolls up, displaying <see cref="UpImage"/>; false to create the button that scrolls down, displaying <see cref="DownImage"/>.</param>
		public ToolStripScrollButton(bool up)
			: base(CreateControlInstance(up))
		{
			this._up = up;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the default internal spacing, in pixels, of the scroll button.
		/// </summary>
		/// <returns>A <see cref="Padding"/> object representing the spacing.</returns>
		public new Padding DefaultPadding
		{
			get
			{
				return this._defaultPadding;
			}
		}

		/// <summary>
		/// Returns the image displayed on the scroll up buttons.
		/// </summary>
		/// <returns>The image shared by all the scroll up buttons.</returns>
		/// <remarks>
		/// The value is shared by all the instances and is read when a button is created.
		/// </remarks>
		/// <example>
		/// Reading the shared scroll images:
		/// <code><![CDATA[
		/// object up = ToolStripScrollButton.UpImage;
		/// object down = ToolStripScrollButton.DownImage;
		/// ]]></code>
		/// </example>
		public static object UpImage { get; private set; }
		/// <summary>
		/// Returns the image displayed on the scroll down buttons.
		/// </summary>
		/// <returns>The image shared by all the scroll down buttons.</returns>
		/// <remarks>
		/// The value is shared by all the instances and is read when a button is created.
		/// </remarks>
		public static object DownImage { get; private set; }

		private Padding _defaultPadding;
		private bool _up;

		#endregion

		#region Methods

		private static Control CreateControlInstance(bool up)
			=> new StickyLabel(up)
			{
				ImageAlign = ContentAlignment.MiddleCenter,
				Image = (up) ? UpImage : DownImage
			};

		protected override void Dispose(bool disposing)
		{
			// TODO: Implement
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			// TODO: Implement
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			// TODO: Implement
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			// TODO: Implement
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
