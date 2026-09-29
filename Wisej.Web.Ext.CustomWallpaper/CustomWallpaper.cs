///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.CustomWallpaper
{
	/// <summary>
	/// Changes the background image of the <see cref="T:Wisej.Web.Desktop"/> or any
	/// target <see cref="Control"/> to rotate through a custom list of images.
	/// </summary>
	/// <remarks>
	/// The images are cross-faded (see <see cref="FadeTime"/>) every <see cref="RotationInterval"/> milliseconds,
	/// optionally with a slow zoom effect (see <see cref="EnableAnimation"/>). Images can be
	/// <see cref="Image"/> objects, which are served by the component itself, or URLs.
	/// </remarks>
	/// <example>
	/// Rotating three images on the desktop every 30 seconds:
	/// <code><![CDATA[
	/// var wallpaper = new CustomWallpaper
	/// {
	///     RotationInterval = 30000,
	///     FadeTime = 2000,
	///     Images = new[]
	///     {
	///         new ImageListEntry("Images/Wallpapers/beach.jpg"),
	///         new ImageListEntry("Images/Wallpapers/mountains.jpg"),
	///         new ImageListEntry("https://example.com/images/city.jpg")
	///     }
	/// };
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(CustomWallpaper))]
	[SRDescription("Changes the background image of the Desktop or any Control to use a custom list of images.")]
	[ApiCategory("CustomWallpaper")]
	public class CustomWallpaper : Wisej.Web.Component, IWisejHandler
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomWallpaper" /> class.
		/// </summary>
		public CustomWallpaper()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomWallpaper" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="IContainer" /> that represents the container for the component.</param>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
		public CustomWallpaper(IContainer container)
			: this()
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets whether a simple zoom animation is applied when rotating images.
		/// </summary>
		/// <remarks>
		/// When enabled, the incoming image is slowly scaled to 105% over five times the <see cref="FadeTime"/>.
		/// </remarks>
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[Description("Enables or disables a simple zoom animation when rotating images.")]
		public bool EnableAnimation
		{
			get { return this._enableAnimation; }
			set
			{
				if (this._enableAnimation != value)
				{
					this._enableAnimation = value;
					Update();
				}
			}
		}
		private bool _enableAnimation = true;

		/// <summary>
		/// Returns or sets the fade in/out time in milliseconds.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">The value is less than 0 or greater than 10000 (10 seconds).</exception>
		/// <remarks>
		/// This is the duration of the cross-fade between the current image and the next one.
		/// When <see cref="EnableAnimation"/> is <c>true</c>, the zoom animation lasts five times this value.
		/// The default is 1000.
		/// </remarks>
		[DefaultValue(1000)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the fade in/out interval in milliseconds.")]
		public int FadeTime
		{
			get { return this._fadeTime; }
			set
			{
				if (value < 0 || value > 10000 /*10 s*/)
					throw new ArgumentOutOfRangeException("TransitionInterval", SR.GetString("InvalidBoundArgument", "TransitionInterval", value, 0, 10000));

				if (this._fadeTime != value)
				{
					this._fadeTime = value;
					Update();
				}
			}
		}
		private int _fadeTime = 1000;

		/// <summary>
		/// Returns or sets the rotation interval in milliseconds.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">The value is less than 0 or greater than 36000000 (10 hours).</exception>
		/// <remarks>
		/// Set to 0 to disable the rotation: only the first image is displayed. The default is 60000 (one minute).
		/// </remarks>
		/// <example>
		/// Changing the image every 5 minutes with a 3 seconds cross-fade:
		/// <code><![CDATA[
		/// this.customWallpaper1.RotationInterval = 5 * 60 * 1000;
		/// this.customWallpaper1.FadeTime = 3000;
		/// ]]></code>
		/// </example>
		[DefaultValue(60000)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the rotation interval in milliseconds.")]
		public int RotationInterval
		{
			get { return this._rotationInterval; }
			set
			{
				if (value < 0 || value > 36000000 /*10 h*/)
					throw new ArgumentOutOfRangeException("RotationInterval", SR.GetString("InvalidBoundArgument", "RotationInterval", value, 0, 36000000));

				if (this._rotationInterval != value)
				{
					this._rotationInterval = value;
					Update();
				}
			}
		}
		private int _rotationInterval = 60000;

		/// <summary>
		/// Returns or sets whether the images are displayed in random order.
		/// </summary>
		/// <remarks>
		/// The list is shuffled every time the component is updated and sent to the client.
		/// </remarks>
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets if the images will be displayed in random order.")]
		public bool RandomOrder
		{
			get { return this._randomOrder; }
			set
			{				
				if (this._randomOrder != value)
				{
					this._randomOrder = value;
					Update();
				}
			}
		}
		private bool _randomOrder = false;

		/// <summary>
		/// Returns or sets the control that will receive the background images.
		/// </summary>
		/// <remarks>
		/// When null (the default), the images are applied to the current <see cref="T:Wisej.Web.Desktop"/> or,
		/// if the application doesn't use a desktop, to the main <see cref="Page"/>.
		/// The property is reset to null automatically when the target control is disposed.
		/// </remarks>
		/// <example>
		/// Using the wallpaper as the background of a panel:
		/// <code><![CDATA[
		/// this.customWallpaper1.Control = this.panelHeader;
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the control that will receive the background images.")]
		[TypeConverter(typeof(ReferenceConverter))]
		public Control Control
		{
			get { return this._control; }
			set
			{
				if (this._control != value)
				{
					if (this._control != null)
						this._control.Disposed -= Control_Disposed;

					this._control = value;

					if (this._control != null)
						this._control.Disposed += Control_Disposed;

					Update();
				}
			}
		}
		private Control _control;

		private void Control_Disposed(object sender, EventArgs e)
		{
			this.Control = null;
		}

		/// <summary>
		/// Returns or sets the list of images to rotate.
		/// </summary>
		/// <remarks>
		/// Each <see cref="ImageListEntry"/> can specify either an <see cref="Image"/> object, which is streamed to the
		/// browser by the component, or an image source (URL or application-relative path). Images are scaled
		/// to cover the target control. Assigning a new array immediately fades in the next image;
		/// changing the elements of the existing array doesn't update the client.
		/// </remarks>
		/// <example>
		/// Mixing an embedded image with image URLs:
		/// <code><![CDATA[
		/// this.customWallpaper1.Images = new[]
		/// {
		///     new ImageListEntry(Properties.Resources.CompanyBackground),
		///     new ImageListEntry("Images/Wallpapers/office.jpg")
		/// };
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[MergableProperty(false)]
		[SRCategory("CatAppearance")]
		[Description("List of images to rotate.")]
		public ImageListEntry[] Images
		{
			get { return this._images; }
			set
			{
				if (this._images != value)
				{
					this._images = value;
					Update();
				}
			}
		}
		private ImageListEntry[] _images;

		#endregion

		#region Methods

		// Returns an array of image URLs to the widget for rendering on the client.
		private string[] GetImageList()
		{
			if (this._images == null || this._images.Length == 0)
				return null;			

			string[] list = new string[this._images.Length];
			for (int i = 0; i < list.Length; i++)
			{
				var entry = this._images[i];
				if (entry.Image != null)
					list[i] = this.GetPostbackURL() + "&ix=" + i;
				else if (entry.ImageSource != null)
					list[i] = entry.ImageSource.Replace("\\", "/");
			}

			if (RandomOrder)
				Shuffle(list);

			return list;
		}

		// Returns the normalized image media type. Defaults to image/png if the format is not recognized.
		internal static string GetImageMediaType(Image image)
		{
			var format = image.RawFormat;
			if (format.Equals(ImageFormat.Png))
				return "image/png";
			if (format.Equals(ImageFormat.Gif))
				return "image/gif";
			if (format.Equals(ImageFormat.Jpeg))
				return "image/jpeg";

			// convert bmp to png.
			// bmp needs a seekable stream and is not compressed.
			if (format.Equals(ImageFormat.Bmp))
				return "image/png";

			return "image/png";
		}

		// Returns the normalized image format: gif, png, bmp, jpeg. If not recognized it returns png.
		private static ImageFormat GetImageFormat(Image image)
		{
			var format = image.RawFormat;
			if (format.Equals(ImageFormat.Png))
				return ImageFormat.Png;
			if (format.Equals(ImageFormat.Gif))
				return ImageFormat.Gif;
			if (format.Equals(ImageFormat.Jpeg))
				return ImageFormat.Jpeg;

			// convert bmp to png.
			// bmp needs a seekable stream and is not compressed.
			if (format.Equals(ImageFormat.Bmp))
				return ImageFormat.Png;

			return ImageFormat.Png;
		}

		/// <summary>
		/// When the reference count goes down to zero, kill also the static timer.
		/// </summary>
		/// <param name="disposing">true when this method is called by the application rather than a finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		#endregion

		#region IWisejHandler

		/// <summary>
		/// Don't compress the output. Images are already compressed.
		/// </summary>
		bool IWisejHandler.Compress { get { return false; } }

		/// <summary>
		/// Process the http request.
		/// </summary>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext"/>.</param>
		void IWisejHandler.ProcessRequest(HttpContext context)
		{
			var request = context.Request;
			var response = context.Response;

			int index = -1;
			if (!int.TryParse(request["ix"], out index))
				return;

			if (this._images == null || index < 0 || index >= this._images.Length)
				return;

			ImageListEntry entry = this._images[index];
			if (entry.Image != null)
			{
				try
				{
					Image image = entry.Image;
					lock (image)
					{
						var format = GetImageFormat(image);
						var mediaType = GetImageMediaType(image);

						response.ContentType = mediaType;
						response.AppendHeader("Cache-Control", "private, max-age=86400");

						image.Save(response.OutputStream, format);
					}
				}
				catch (Exception ex)
				{
					LogManager.Log(ex);
				}
				response.Flush();
			}
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns a collection of referenced components or collection of components.
		/// </summary>
		/// <param name="list">List of referenced components or collection of components.</param>
		protected override void OnAddReferences(IList list)
		{
			if (this.Control != null)
				list.Add(this.Control);

			base.OnAddReferences(list);
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.CustomWallpaper";
			config.control = ((IWisejControl)this._control)?.Id;
			config.images = GetImageList();
			config.fadeTime = this.FadeTime;
			config.rotationInterval = this.RotationInterval;
			config.enableAnimation = this.EnableAnimation;

		}

		/// <summary>
		/// Shuffles the list of images for random display
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="images"></param>
		private void Shuffle<T>(IList<T> images)
		{
			Random random = new Random();

			for (int i = images.Count - 1; i > 0; i--)
			{
				int rnd = random.Next(i + 1);

				T value = images[rnd];
				images[rnd] = images[i];
				images[i] = value;
			}
		}

		#endregion

	}
}
