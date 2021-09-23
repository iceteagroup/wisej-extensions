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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.Html2Canvas
{
	/// <summary>
	/// Implementation of the html2canvas (https://html2canvas.hertzen.com/)
	/// library. It's a singleton instance that can take a screenshot of
	/// a specific control or the entire browser and send the image back
	/// to the server.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Usage is quite easy:
	/// </para>
	/// <code language="cs">
	///		Html2Canvas.Screenshot(this, (image) => {
	///		image.Save(@"\images\screen.png");
	/// });
	///		
	///		// or using asynchronous code:
	///		
	///		var image = await Html2Canvas.Screenshot(this.panel1);
	///		image.Save(@"\images\screen.png");
	///		
	/// </code>
	/// </remarks>
	[ToolboxItem(false)]
	[Description("Implementation of the html2canvas library (https://html2canvas.hertzen.com/).")]
	public class Html2Canvas : Component
	{
		#region Constructor

		/// <summary>
		/// This class cannot be instantiated directly. Use the static methods instead.
		/// </summary>
		private Html2Canvas()
		{
		}

		/// <summary>
		/// Returns the singleton session instance of <see cref="Html2Canvas"/>
		/// </summary>
		private static Html2Canvas Instance
		{
			get
			{
				lock (typeof(Html2Canvas))
				{
					var instance = (Html2Canvas)Application.Session[typeof(Html2Canvas).FullName];
					if (instance == null)
					{
						instance = new Html2Canvas();
						Application.Session[typeof(Html2Canvas).FullName] = instance;
					}
					return instance;
				}
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Takes a screenshot of the browser.
		/// </summary>
		/// <param name="callback">Callback method that receives the <see cref="Image"/> object.</param>
		/// <exception cref="ArgumentNullException">If any of the arguments is null.</exception>
		public static void Screenshot(Action<Image> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Instance.ScreenshotCore(null, null, (result)=> {

				if (result is Exception)
					throw (Exception)result;
				else
					callback(result as Image);
			});
		}

		/// <summary>
		/// Takes a screenshot of the browser.
		/// </summary>
		/// <param name="options">The <see cref="Html2CanvasOptions"/> to pass to the html2canvas call.</param>
		/// <param name="callback">Callback method that receives the <see cref="Image"/> object.</param>
		/// <exception cref="ArgumentNullException">If any of the arguments is null.</exception>
		public static void Screenshot(Html2CanvasOptions options, Action<Image> callback)
		{
			if (options == null)
				throw new ArgumentNullException(nameof(options));
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Instance.ScreenshotCore(null, options, (result) => {

				if (result is Exception)
					throw (Exception)result;
				else
					callback(result as Image);
			});
		}

		/// <summary>
		/// Takes a screenshot of the specified <see cref="Control"/>.
		/// </summary>
		/// <param name="target">
		/// The <see cref="Control"/> to render to an <see cref="Image"/>
		/// </param>
		/// <param name="callback">Callback method that receives the <see cref="Image"/> object.</param>
		/// <exception cref="ArgumentNullException">If any of the arguments is null.</exception>
		public static void Screenshot(Control target, Action<Image> callback)
		{
			if (target == null)
				throw new ArgumentNullException(nameof(target));
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Instance.ScreenshotCore(target, null, (result) => {

				if (result is Exception)
					throw (Exception)result;
				else
					callback(result as Image);
			});
		}

		/// <summary>
		/// Takes a screenshot of the specified <see cref="Control"/>.
		/// </summary>
		/// <param name="target">
		/// The <see cref="Control"/> to render to an <see cref="Image"/>
		/// </param>
		/// <param name="callback">Callback method that receives the <see cref="Image"/> object.</param>
		/// <param name="options">The <see cref="Html2CanvasOptions"/> to pass to the html2canvas call.</param>
		/// <exception cref="ArgumentNullException">If any of the arguments is null.</exception>
		public static void Screenshot(Control target, Html2CanvasOptions options, Action<Image> callback)
		{
			if (target == null)
				throw new ArgumentNullException(nameof(target));
			if (options == null)
				throw new ArgumentNullException(nameof(options));
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Instance.ScreenshotCore(target, options, (result) => {

				if (result is Exception)
					throw (Exception)result;
				else
					callback(result as Image);
			});
		}

		/// <summary>
		/// Asynchronously returns an <see cref="Image"/> that represents a screenshot
		/// of the browser.
		/// </summary>
		/// <param name="options">The <see cref="Html2CanvasOptions"/> to pass to the html2canvas call.</param>
		/// <returns>An awaitable <see cref="Task"/> that contains the screenshot.</returns>
		public static Task<Image> ScreenshotAsync(Html2CanvasOptions options = null)
		{
			var tcs = new TaskCompletionSource<Image>();

			Instance.ScreenshotCore(null, options, (result) => {

				if (result is Exception)
					tcs.SetException((Exception)result);
				else
					tcs.SetResult(result as Image);
			});

			return tcs.Task;
		}

		/// <summary>
		/// Asynchronously returns an <see cref="Image"/> that represents a screenshot
		/// of the specified <see cref="Control"/> as it appears on the browser.
		/// </summary>
		/// <param name="target">
		/// The <see cref="Control"/> to render to an <see cref="Image"/>
		/// </param>
		/// <param name="options">The <see cref="Html2CanvasOptions"/> to pass to the html2canvas call.</param>
		/// <returns>An awaitable <see cref="Task"/> that contains the screenshot.</returns>
		public static Task<Image> ScreenshotAsync(Control target, Html2CanvasOptions options = null)
		{
			if (target == null)
				throw new ArgumentNullException(nameof(target));

			var tcs = new TaskCompletionSource<Image>();

			Instance.ScreenshotCore(target, options, (result)=> {

				if (result is Exception)
					tcs.SetException((Exception)result);
				else if (result is Image)
					tcs.SetResult((Image)result);
				else
					tcs.SetResult(null);
			});

			return tcs.Task;
		}

		// Implementation
		private void ScreenshotCore(Control target, Html2CanvasOptions options, Action<object> callback)
		{
			Call("screenshot",
				(result) =>
				{
					if (result is string)
						result = ImageFromBase64((string)result);

					callback(result);
				},
				new object[] { target, options });
		}

		/// <summary>
		/// Returns the Image encoded in a base64 string.
		/// </summary>
		/// <param name="base64">The base64 string representation of the screenshot from the client.</param>
		/// <returns>An <see cref="Image"/> created from the <paramref name="base64"/> string.</returns>
		internal static Image ImageFromBase64(string base64)
		{
			// data:image/gif;base64,R0lGODlhCQAJAIABAAAAAAAAACH5BAEAAAEALAAAAAAJAAkAAAILjI+py+0NojxyhgIAOw==
			try
			{
				if (String.IsNullOrEmpty(base64))
					return null;

				int pos = base64.IndexOf("base64,");
				if (pos < 0)
					return null;

				base64 = base64.Substring(pos + 7);
				byte[] buffer = Convert.FromBase64String(base64);
				MemoryStream stream = new MemoryStream(buffer);
				return new Bitmap(stream);
			}
			catch { }

			return null;
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.Html2Canvas";
		}

		#endregion

	}
}
