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


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Wisej.Web.Ext.MobileIntegration
{
	public sealed partial class Device
	{
		/// <summary>
		/// Provides functionality related to the client device's camera.
		/// </summary>
		[ApiCategory("API")]
		public class Camera
		{

			#region Methods

			/// <summary>
			/// Detects and scans documents using the camera.
			/// </summary>
			/// <returns>An array <see cref="Image"/> objects containing the documents.</returns>
			public static Image[] ScanDocument()
			{
				var result = PostModalMessage("document.scan");
				if (result.ErrorCode != 0)
					throw new Exception(result.Value);

				var images = new List<Image>();
				foreach (var image in result.Value)
				{
					var bytes = Convert.FromBase64String(image);
					using (MemoryStream ms = new MemoryStream(bytes))
					{
						var bitmap = new Bitmap(Image.FromStream(ms));
						images.Add(bitmap);
					}
				}
				return images.ToArray();
			}

			/// <summary>
			/// Toggles the device's flashlight.
			/// </summary>
			/// <param name="on">Specifies whether the flashlight should be on.</param>
			/// <returns>A bool indicating whether the action was successful.</returns>
			public static bool SetFlashlight(bool on)
			{
				var result = PostModalMessage("flashlight.toggle", on);
				return result.ErrorCode == 0;
			}

			/// <summary>
			/// Allows the user to take a picture using the device's camera.
			/// </summary>
			/// <param name="compressionQuality">The quality of the resulting image, expressed as a value from 0.0 to 1.0.</param>
			/// <returns>The jpg's base64 encoded URL</returns>
			public static ImageCapture TakePicture(float compressionQuality = 1.0F)
			{
				var response = PostModalMessage("camera.picture", compressionQuality);
				if (response.ErrorCode != 0)
					throw new Exception(response.Value);

				var imageData = response.Value;
				try
				{
					var base64 = imageData.Substring(imageData.IndexOf(',') + 1);

					byte[] bytes = Convert.FromBase64String(base64);
					using (var ms = new MemoryStream(bytes))
						return new ImageCapture(new Bitmap(Image.FromStream(ms)));
				}
				catch
				{
					return new ImageCapture("Image capture failed.");
				}
			}

			/// <summary>
			/// Allows the user to take a video using the device's camera.
			/// </summary>
			/// <returns>The video's base64 encoded URL</returns>
			public static string TakeVideo()
			{
				var result = PostModalMessage("camera.video");
				if (result.ErrorCode != 0)
					throw new Exception(result.Value);

				return result.Value;
			}

			#endregion

		}
	}
}
