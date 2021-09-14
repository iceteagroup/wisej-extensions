///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Diagnostics;
using System.Drawing;
using System.Net;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.BingWallpaper
{
	/// <summary>
	/// Changes the background image of the <see cref="T:Wisej.Web.Desktop"/> or any target 
	/// <see cref="Control"/> Bing's images of the day.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(BingWallpaper))]
	[SRDescription("Changes the background image of the Desktop or any Control to use Bing's images of the day.")]
	[ApiCategory("BingWallpaper")]
    public class BingWallpaper : Wisej.Web.Component
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.BingWallpaper" /> class.
		/// </summary>
		public BingWallpaper()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.BingWallpaper" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the component. </param>
		public BingWallpaper(IContainer container)
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
		/// Returns or sets the number of images to rotate.
		/// </summary>
		[DefaultValue(10)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the number of images to rotate.")]
		public int MaxImages
		{
			get { return this._maxImages; }
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("MaxImages", SR.GetString("InvalidBoundArgument", "MaxImages", value, 0, 100));

				if (this._maxImages != value)
				{
					this._maxImages = value;
					this._images = null;
					Update();
				}
			}
		}
		private int _maxImages = 10;

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

		#endregion

		#region Methods

		/// <summary>
		/// When the reference count goes down to zero, kill also the static timer.
		/// </summary>
		/// <param name="disposing">true when this method is called by the application rather than a finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Retrieves the list of images from Bing.
		private string[] LoadImages(int count)
		{
			if (this._images != null)
				return this._images;

			string url = String.Format("https://www.bing.com/HPImageArchive.aspx?format=js&n={0}&mkt=en-US", Math.Max(count, 1));

			try
			{
				using (WebClient client = new WebClient())
				{
					var json = client.DownloadString(url);
					if (!String.IsNullOrEmpty(json))
					{
						dynamic response = WisejSerializer.Parse(json);
						if (response != null)
						{
							List<string> list = new List<string>();
							foreach (dynamic img in response.images)
							{
								list.Add("https://www.bing.com" + img.url);
							}
							_images = list.ToArray();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Trace.TraceError("{0}: {1}", ex.GetType().FullName, ex.Message);
			}

			return this._images;
		}

		// list of image.
		private string[] _images = null;

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

			config.className = "wisej.web.ext.BingWallpaper";
			config.control = ((IWisejControl)this._control)?.Id;
			config.images = LoadImages(this.MaxImages);
			config.fadeTime = this.FadeTime;
			config.rotationInterval = this.RotationInterval;
			config.enableAnimation = this.EnableAnimation;

		}

		#endregion

	}
}
