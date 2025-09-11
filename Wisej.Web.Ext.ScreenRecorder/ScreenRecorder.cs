using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Wisej.Core;
using Wisej.Design;
using Wisej.Web.Ext.Camera;

namespace Wisej.Web.Ext.ScreenRecorder
{
	/// <summary>
	/// TODO:
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(ScreenRecorder))]
	[Description("The Screen Recorder component makes it possible to record the user's screen.")]
	public class ScreenRecorder : Wisej.Web.Component, IWisejHandler
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.Camera" /> class.
		/// </summary>
		public ScreenRecorder()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.Camera" /> class with a specified container.
		/// </summary>
		/// <param name="component"></param>
		public ScreenRecorder(IContainer container)
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the current recording is available for download.
		/// </summary>
		public event UploadedEventHandler Uploaded
		{
			add { base.AddHandler(nameof(Uploaded), value); }
			remove { base.RemoveHandler(nameof(Uploaded), value); }
		}

		/// <summary>
		/// Fired while the <see cref="ScreenRecorder" /> control receives the recording stream being uploaded.
		/// </summary>
		/// <remarks>
		/// This event fires only if there is an handler attached to it. A simple overload of the On[Event] method in 
		/// a derived class will not be invoked unless there is at least one handler attached to the event.
		/// </remarks>
		public event UploadProgressEventHandler Progress
		{
			add { base.AddHandler(nameof(Progress), value); }
			remove { base.RemoveHandler(nameof(Progress), value); }
		}

		/// <summary>
		/// Fired when an error occurs in the screen recorder setup or usage.
		/// </summary>
		public event RecorderErrorHandler Error
		{
			add { base.AddHandler(nameof(Error), value); }
			remove { base.RemoveHandler(nameof(Error), value); }
		}

		/// <summary>
		/// Fires the <see cref="Error"/> event.
		/// </summary>
		/// <param name="e">A <see cref="RecorderErrorEventArgs" /> that contains the event data. </param>
		protected virtual void OnError(RecorderErrorEventArgs e)
		{
			((RecorderErrorHandler)base.Events[nameof(Error)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="Uploaded" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.UploadedEventArgs" /> that contains the event data. </param>
		protected virtual void OnUploaded(UploadedEventArgs e)
		{
			((UploadedEventHandler)base.Events[nameof(Uploaded)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="Progress" /> event.
		/// </summary>
		/// <remarks>
		/// This event fires only if there is an handler attached to it. A simple overload of the On[Event] method in 
		/// a derived class will not be invoked unless there is at least one handler attached to the event.
		/// </remarks>
		/// <param name="e">A <see cref="T:Wisej.Web.UploadProgressEventArgs" /> that contains the event data. </param>
		protected virtual void OnProgress(UploadProgressEventArgs e)
		{
			((UploadProgressEventHandler)base.Events[nameof(Progress)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Specifies whether audio should be recorded.
		/// </summary>
		[DesignerActionList]
		[DefaultValue(false)]
		public bool Audio
		{
			get
			{
				return this._audio;
			}
			set
			{
				if (this._audio != value)
				{
					this._audio = value;
					Update();
				}
			}
		}
		private bool _audio = false;

		#endregion

		#region Methods

		/// <summary>
		/// Returns the current image from the screen recorder.
		/// </summary>
		/// <param name="callback">Callback method to receive the <see cref="Image"/> or null.</param>
		public void GetImage(Action<Image> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Call("getImage", (base64) => {

				callback(ImageFromBase64(base64));

			}, null);
		}

		/// <summary>
		/// Returns the current image from the screen recorder asynchronously.
		/// </summary>
		/// <returns>An awaitable <see cref="Task"/>.</returns>
		public Task<Image> GetImageAsync()
		{
			var tcs = new TaskCompletionSource<Image>();
			GetImage((image) => {

				tcs.SetResult(image);

			});
			return tcs.Task;
		}


		/// <summary>
		/// Starts recording.
		/// </summary>
		/// <param name="format">The video encoding mime type format, <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types"/>.</param>
		/// <param name="bitsPerSecond">Audio and video bits per second. <see href="https://developer.mozilla.org/en-US/docs/Web/API/MediaRecorder/MediaRecorder"/>.</param>
		/// <param name="updateInterval">Update interval in seconds. The default is zero causing the video to be uploaded on <see cref="StopRecording"/>.</param>
		/// <remarks>You must call <see cref="StopRecording"/>to end recording.</remarks>
		public void StartRecording(string format = "video/webm", int bitsPerSecond = 2500000, int updateInterval = 0)
		{
			Call("startRecording", format, bitsPerSecond, updateInterval);
		}

		/// <summary>
		/// Stops recording and uploads the recorded stream to the <see cref="Uploaded"/> event.
		/// </summary>
		public void StopRecording()
		{
			Call("stopRecording");
		}

		/// <summary>
		/// Returns the Image encoded in a base64 string.
		/// </summary>
		/// <param name="base64">The base64 string representation of the image from the client.</param>
		/// <returns>An <see cref="Image"/> created from the <paramref name="base64"/> string.</returns>
		private static Image ImageFromBase64(string base64)
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

		// Handles progress events from the client.
		private void ProcessProgressWebEvent(WisejEventArgs e)
		{
			var data = e.Parameters.Data;
			OnProgress(new UploadProgressEventArgs(data.loaded ?? 0, data.total ?? 0));
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "error":
					OnError(new RecorderErrorEventArgs(e.Parameters.Message));
					break;

				case "progress":
					ProcessProgressWebEvent(e);
					break;

				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.ScreenRecorder";
			config.submitURL = this.GetPostbackURL();

			dynamic videoConstraints = new
			{
				mediaSource = "screen"
			};
			
			config.constraints = new
			{
				video = videoConstraints,
				audio = this.Audio
			};

			// TODO: config.wiredEvents.Add("error(Message)");

			// if (base.Events[nameof(Progress)] != null)
			// TODO:	config.wiredEvents.Add("progress(Data)");
		}

		#endregion

		#region IWisejHandler

		/// <summary>
		/// Compress the output.
		/// </summary>
		bool IWisejHandler.Compress { get { return false; } }

		/// <summary>
		/// Process the HTTP request.
		/// </summary>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext"/>.</param>
		void IWisejHandler.ProcessRequest(HttpContext context)
		{
			OnUploaded(new UploadedEventArgs(context.Request.Files));
		}

		#endregion
	}
}
