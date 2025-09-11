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
	public partial class ToolStripScrollButton : ToolStripControlHost
	{

		#region Constructors

		public ToolStripScrollButton(bool up)
			: base(CreateControlInstance(up))
		{
			this._up = up;
			// TODO: Implement
		}

		#endregion

		#region Properties

		public new Padding DefaultPadding
		{
			get
			{
				return this._defaultPadding;
			}
		}

		public static object UpImage { get; private set; }
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
