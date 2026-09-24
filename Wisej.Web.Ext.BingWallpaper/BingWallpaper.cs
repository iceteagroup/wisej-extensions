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
	/// <remarks>
	/// The server downloads the list of the most recent Bing images of the day (up to <see cref="MaxImages"/>)
	/// when the component is rendered. The browser then shows the images as the background of the target
	/// <see cref="Control"/>, or of the current <see cref="T:Wisej.Web.Desktop"/> (or main page) when <see cref="Control"/> is null,
	/// and changes the image every <see cref="RotationInterval"/> milliseconds.
	/// If the images can't be downloaded, the error is written to the trace log and the background doesn't change.
	/// </remarks>
	/// <example>
	/// The following example rotates five Bing images every 30 seconds as the background of the desktop:
	/// <code><![CDATA[
	/// public partial class MyDesktop : Desktop
	/// {
	///     private BingWallpaper bingWallpaper;
	///
	///     public MyDesktop()
	///     {
	///         InitializeComponent();
	///
	///         this.bingWallpaper = new BingWallpaper(this.components)
	///         {
	///             MaxImages = 5,
	///             RotationInterval = 30000,
	///             FadeTime = 2000
	///         };
	///     }
	/// }
	/// ]]></code>
	/// </example>
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
		/// <remarks>
		/// The new component rotates up to 10 images every 60 seconds, with a 1 second fade and the zoom animation enabled.
		/// </remarks>
		/// <example>
		/// The following example uses Bing images as the background of a panel, without the zoom animation:
		/// <code><![CDATA[
		/// var wallpaper = new BingWallpaper();
		/// wallpaper.Control = this.panelBackground;
		/// wallpaper.EnableAnimation = false;
		/// ]]></code>
		/// </example>
		public BingWallpaper()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.BingWallpaper" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the component. </param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="container"/> is null.</exception>
		/// <remarks>
		/// Adding the component to a container ensures that it's disposed together with the container,
		/// for example when the owning page, form or desktop is disposed.
		/// </remarks>
		/// <example>
		/// The following example shows a new Bing image every 10 minutes as the background of the main page:
		/// <code><![CDATA[
		/// // "components" is the container created by the designer for the page.
		/// var wallpaper = new BingWallpaper(this.components)
		/// {
		///     Control = this,
		///     RotationInterval = 600000
		/// };
		/// ]]></code>
		/// </example>
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
		/// <value>
		/// true to zoom the new image while it fades in; otherwise false. The default is true.
		/// </value>
		/// <remarks>
		/// When enabled, each new image slowly zooms to 105% of its size while it fades in. The zoom lasts
		/// five times the <see cref="FadeTime"/>, so with the default <see cref="FadeTime"/> of 1000 it takes 5 seconds.
		/// Disable it for a static background, for example when the images are behind content that should stay easy to read.
		/// </remarks>
		/// <example>
		/// The following example turns off the zoom animation and only cross-fades the images:
		/// <code><![CDATA[
		/// var wallpaper = new BingWallpaper(this.components)
		/// {
		///     EnableAnimation = false,
		///     FadeTime = 1500
		/// };
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the duration, in milliseconds, of the cross-fade between two images.
		/// </summary>
		/// <value>
		/// The duration of the cross-fade in milliseconds, from 0 to 10000 (10 seconds). The default is 1000.
		/// </value>
		/// <remarks>
		/// When the image changes, the current image fades out while the next one fades in, and both
		/// transitions take <see cref="FadeTime"/> milliseconds. A value of 0 switches the images without fading.
		/// When <see cref="EnableAnimation"/> is true, the zoom animation lasts five times the <see cref="FadeTime"/>.
		/// Keep the <see cref="FadeTime"/> shorter than the <see cref="RotationInterval"/>, otherwise the next image
		/// starts fading in before the previous transition ends.
		/// </remarks>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 or greater than 10000.</exception>
		/// <example>
		/// The following example uses a slow 3 second cross-fade and a new image every 2 minutes:
		/// <code><![CDATA[
		/// var wallpaper = new BingWallpaper(this.components)
		/// {
		///     FadeTime = 3000,
		///     RotationInterval = 120000
		/// };
		/// ]]></code>
		/// </example>
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
		/// <value>
		/// The time in milliseconds between two images, from 0 to 36000000 (10 hours). The default is 60000 (1 minute).
		/// </value>
		/// <remarks>
		/// The images are shown in order and start again from the first one after the last. The timer runs in the
		/// browser, so rotating the images doesn't cause any requests to the server.
		/// Set the value to 0 to show only the first image, without rotating.
		/// </remarks>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 or greater than 36000000.</exception>
		/// <example>
		/// The following example lets the user choose how often the image changes, or stop the rotation:
		/// <code><![CDATA[
		/// private void comboBoxRotation_SelectedIndexChanged(object sender, EventArgs e)
		/// {
		///     switch (this.comboBoxRotation.Text)
		///     {
		///         case "Every minute":
		///             this.bingWallpaper1.RotationInterval = 60 * 1000;
		///             break;
		///
		///         case "Every hour":
		///             this.bingWallpaper1.RotationInterval = 60 * 60 * 1000;
		///             break;
		///
		///         case "Never":
		///             this.bingWallpaper1.RotationInterval = 0;
		///             break;
		///     }
		/// }
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
		/// Returns or sets the number of images to rotate.
		/// </summary>
		/// <value>
		/// The number of recent Bing images of the day to download, from 0 to 100. The default is 10.
		/// </value>
		/// <remarks>
		/// The list of images is downloaded from bing.com by the server when the component is rendered, and it's
		/// reused until <see cref="MaxImages"/> changes. The server must be able to reach https://www.bing.com.
		/// A value of 0 downloads one image. Bing may return fewer images than requested; in that case
		/// all the returned images are rotated.
		/// </remarks>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 or greater than 100.</exception>
		/// <example>
		/// The following example shows only today's image, without rotating:
		/// <code><![CDATA[
		/// var wallpaper = new BingWallpaper(this.components)
		/// {
		///     MaxImages = 1,
		///     RotationInterval = 0
		/// };
		/// ]]></code>
		/// </example>
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
		/// <value>
		/// The <see cref="T:Wisej.Web.Control"/> that shows the images, or null to use the current
		/// <see cref="T:Wisej.Web.Desktop"/>. The default is null.
		/// </value>
		/// <remarks>
		/// When the value is null and the application doesn't have a <see cref="T:Wisej.Web.Desktop"/>, the images are
		/// shown on the <see cref="P:Wisej.Web.Application.MainPage"/>. The images are scaled to cover the whole
		/// control. When the target control is disposed, the property is reset to null.
		/// </remarks>
		/// <example>
		/// The following example shows the Bing images behind a login panel:
		/// <code><![CDATA[
		/// private void LoginPage_Load(object sender, EventArgs e)
		/// {
		///     this.bingWallpaper1.Control = this.panelLogin;
		///     this.bingWallpaper1.RotationInterval = 15000;
		/// }
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
