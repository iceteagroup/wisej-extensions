///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Specifies image capture data.
	/// </summary>
	[ApiCategory("API")]
	public class ImageCapture
	{
		/// <summary>
		/// The image captured from the device.
		/// </summary>
		public Image Image;

		/// <summary>
		/// The error associated with the image capture.
		/// </summary>
		public string Error;

		/// <summary>
		/// Creates a new instance of <see cref="ImageCapture"/> with the given image.
		/// </summary>
		/// <param name="image"></param>
		public ImageCapture(Image image)
		{
			this.Image = image;
		}

		/// <summary>
		/// Creates a new instance of <see cref="ImageCapture"/> with the given error message.
		/// </summary>
		/// <param name="error"></param>
		public ImageCapture(string error)
		{
			this.Error = error;
		}
	}
}
