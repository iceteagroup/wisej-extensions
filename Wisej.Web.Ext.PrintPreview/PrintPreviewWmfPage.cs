///////////////////////////////////////////////////////////////////////////////
//
// (C) 2020 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Drawing.Printing;

namespace Wisej.Web.Ext.PrintPreview
{
	/// <summary>
	/// Represents a preview page in the <see cref="PrintPreviewWmf"/> container.
	/// </summary>
	internal class PrintPreviewWmfPage : Control
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of <see cref="PrintPreviewWmfPage"/>/
		/// </summary>
		public PrintPreviewWmfPage()
		{
			this.BackColor = Color.White;
			this.Paint += this.PrintPreviewWmfPage_Paint;
			this.CssStyle = "box-shadow: -3px 2px 10px 4px rgba(0,0,0,0.32)";
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the <see cref="PreviewPageInfo"/> to display.
		/// </summary>
		public PreviewPageInfo PageInfo
		{
			get { return this._pageInfo; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				this._pageInfo = value;

				this.Size = GetPageSize(96);
			}
		}
		private PreviewPageInfo _pageInfo;

		#endregion

		#region Implementation

		private void PrintPreviewWmfPage_Paint(object sender, PaintEventArgs e)
		{
			var image = this.PageInfo.Image;
			if (image != null)
			{
				e.Graphics.DrawImage(image, this.DisplayRectangle);
			}
		}

		internal Size GetPageSize(int dpi)
		{
			var image = this.PageInfo.Image;

			var size = new Size(
				(int)Math.Round(image.Width / image.HorizontalResolution * dpi),
				(int)Math.Round(image.Height / image.VerticalResolution * dpi)
			);

			return size;
		}

		#endregion
	}
}