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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// General utilities.
	/// </summary>
	/// <exclude/>
	internal static class DeviceUtils
	{
		/// <summary>
		/// Returns the color using the #RRGGBB notation.
		/// </summary>
		/// <param name="color">Color to translate.</param>
		/// <returns>The hex code of the color.</returns>
		public static string GetHtmlColor(Color color)
		{
			return ColorTranslator.ToHtml(Color.FromArgb(color.ToArgb()));
		}

		/// <summary>
		/// Returns a URL that represents the specified <paramref name="imageSource"/>.
		/// </summary>
		/// <param name="imageSource">The image source.</param>
		/// <returns>The base64 encoded image url.</returns>
		public static string GetImageUrl(string imageSource)
		{
			if (String.IsNullOrEmpty(imageSource))
				return null;

			var theme = Application.Theme;

			string image = theme.Images[imageSource];
			if (image != null)
			{
				if (image.StartsWith("data:image/"))
					return image;

				if (image.StartsWith("http://") || image.StartsWith("https://"))
					return image;

				return (theme.Images.baseUrl ?? "") + image;
			} else {
				// Load from assembly?
				if (imageSource.StartsWith("resource.wx"))
				{
					var assemblies = AppDomain.CurrentDomain.GetAssemblies();

					var imageInfo = imageSource.Split(new char[] { '/' });
					var imageAssemblyName = imageInfo[1];
					var imageName = imageInfo[2];

					foreach (var assembly in assemblies)
					{ 
						if (assembly.ManifestModule.Name == imageAssemblyName + ".dll")
						{
							var resources = assembly.GetManifestResourceNames();
							using (Stream stream = assembly.GetManifestResourceStream(imageAssemblyName + ".Resources." + imageName))
							using (StreamReader reader = new StreamReader(stream))
							{
								byte[] svgBytes = Encoding.ASCII.GetBytes(reader.ReadToEnd());
								var svgBase64 = "data:image/svg+xml;base64," + Convert.ToBase64String(svgBytes);
								return svgBase64;
							}
						}
					}
				}
			}

			return imageSource.Replace("\\", "/");
		}

		/// <summary>
		/// Gets a base64 string of the given image.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <returns>A base64 image representation.</returns>
		public static string GetImageUrl(Image image)
		{
			Debug.Assert(image != null);

			if (image == null)
				return null;

			try
			{
				using (MemoryStream mem = new MemoryStream())
				{
					// save the image to memory.
					var format = GetImageFormat(image);
					var mediaType = GetImageMediaType(image);
					image.Save(mem, format);

					// return the buffer converted to a base64 string.
					string base64 = Convert.ToBase64String(mem.GetBuffer(), 0, (int)mem.Length);
					return String.Concat(new[] { "data:", mediaType, ";base64,", base64 });
				}
			}
			catch
			{
			}

			return null;
		}

		/// <summary>
		/// Returns the normalized image media type. Defaults to image/png if the format is not recognized.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <returns>The media type of the image.</returns>
		private static string GetImageMediaType(Image image)
		{
			var format = image.RawFormat;
			if (format.Equals(ImageFormat.Png))
				return "image/png";
			if (format.Equals(ImageFormat.Gif))
				return "image/gif";
			if (format.Equals(ImageFormat.Jpeg))
				return "image/jpeg";
			if (format.Equals(ImageFormat.Bmp))
				return "image/bmp";

			return "image/png";
		}

		/// <summary>
		/// Returns the normalized image format: gif, png, bmp, jpeg. If not recognized it returns png.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <returns>The format of the image.</returns>
		private static ImageFormat GetImageFormat(Image image)
		{
			var format = image.RawFormat;
			if (format.Equals(ImageFormat.Png))
				return ImageFormat.Png;
			if (format.Equals(ImageFormat.Gif))
				return ImageFormat.Gif;
			if (format.Equals(ImageFormat.Jpeg))
				return ImageFormat.Jpeg;
			if (format.Equals(ImageFormat.Bmp))
				return ImageFormat.Bmp;

			return ImageFormat.Png;
		}
	}
}
