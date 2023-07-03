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
	/// target <see cref="Control"/>  to use a custom list of images.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(CustomWallpaper))]
	[SRDescription("Changes the background image of the Desktop or any Control to use a custom list of images.")]
	[ApiCategory("CustomWallpaper")]
	public class CustomWallpaper : Wisej.Web.Component, IWisejHandler
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.CustomWallpaper" /> class.
		/// </summary>
		public CustomWallpaper()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.CustomWallpaper" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the component. </param>
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
		/// Enables or disables a simple zoom animation when rotating images.
		/// </summary>
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
		/// Returns or sets if the images will be displayed in random order.
		/// </summary>
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
		/// Returns or sets the control that will receive the background images. If left to null it will
		/// automatically use the current Desktop.
		/// </summary>
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
		/// List of images to rotate.
		/// </summary>
		/// <returns>The collection of images.</returns>
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
