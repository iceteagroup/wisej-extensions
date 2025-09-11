///////////////////////////////////////////////////////////////////////////////
//
// (C) 2024 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.Drawing;
using Wisej.Web;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// A label that sizes itself on the client.
	/// </summary>
	public class AutoSizeLabel : Label
	{
		private Size _maxSize;

		/// <summary>
		/// Initializes a new instance of <see cref="AutoSizeLabel"/>.
		/// </summary>
		public AutoSizeLabel()
		{
			this.AllowHtml = true;
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			this._maxSize = proposedSize;

			return base.GetPreferredSize(proposedSize);
		}

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.height = null;
			config.width = null;

			if (this._maxSize.Width > 0)
				config.maxWidth = this._maxSize.Width;

			if (this._maxSize.Height > 0)
				config.maxHeight = this._maxSize.Height;

			config.wiredEvents.Add("resize(Size)");
		}
	}
}
