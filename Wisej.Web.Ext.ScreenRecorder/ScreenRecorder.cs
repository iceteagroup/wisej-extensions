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
	/// Records the user's screen (a monitor, window or browser tab chosen by the user) and uploads the recording to the server.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The component uses the browser's <c>getDisplayMedia()</c> API: the browser asks the user which screen to share when the
	/// component is rendered on the client. Call <see cref="StartRecording"/> and <see cref="StopRecording"/> to record the shared
	/// screen with a <c>MediaRecorder</c>; the recorded video is posted back to the component and delivered through the <see cref="Uploaded"/> event.
	/// </para>
	/// <para>
	/// Screen capture requires a secure context (HTTPS or localhost) and a browser that supports the Screen Capture API.
	/// </para>
	/// </remarks>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(ScreenRecorder))]
	[Description("The Screen Recorder component makes it possible to record the user's screen.")]
	public class ScreenRecorder : Wisej.Web.Component, IWisejHandler
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ScreenRecorder" /> class.
		/// </summary>
		public ScreenRecorder()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ScreenRecorder" /> class with a specified container.
		/// </summary>
		/// <param name="container">An <see cref="IContainer"/> that represents the container of the component.</param>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
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
		/// Returns or sets whether audio should be recorded together with the screen.
		/// </summary>
		/// <remarks>
		/// The value is passed as the <c>audio</c> constraint to <c>getDisplayMedia()</c>. Changing it after the component
		/// is rendered requests the screen capture again, and the browser prompts the user again. Whether audio is actually
		/// captured depends on the browser and on the source selected by the user.
		/// </remarks>
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
		/// Retrieves the current frame of the captured screen as an <see cref="Image"/>.
		/// </summary>
		/// <param name="callback">Callback method that receives the <see cref="Image"/>, or null when the image is not available.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
		/// <remarks>
		/// The image is captured asynchronously on the client and <paramref name="callback"/> is invoked when the image is received.
		/// </remarks>
		/// <example>
		/// Taking a snapshot of the shared screen:
		/// <code><![CDATA[
		/// private void buttonSnapshot_Click(object sender, EventArgs e)
		/// {
		///     this.screenRecorder1.GetImage(image =>
		///     {
		///         if (image != null)
		///             this.pictureBox1.Image = image;
		///     });
		/// }
		/// ]]></code>
		/// </example>
		public void GetImage(Action<Image> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Call("getImage", (base64) => {

				callback(ImageFromBase64(base64));

			}, null);
		}

		/// <summary>
		/// Asynchronously retrieves the current frame of the captured screen as an <see cref="Image"/>.
		/// </summary>
		/// <returns>An awaitable <see cref="Task"/> that contains the <see cref="Image"/>, or null when the image is not available.</returns>
		/// <example>
		/// Taking a snapshot of the shared screen using <c>await</c>:
		/// <code><![CDATA[
		/// private async void buttonSnapshot_Click(object sender, EventArgs e)
		/// {
		///     var image = await this.screenRecorder1.GetImageAsync();
		///     if (image != null)
		///         this.pictureBox1.Image = image;
		/// }
		/// ]]></code>
		/// </example>
		public Task<Image> GetImageAsync()
		{
			var tcs = new TaskCompletionSource<Image>();
			GetImage((image) => {

				tcs.SetResult(image);

			});
			return tcs.Task;
		}


		/// <summary>
		/// Starts recording the shared screen.
		/// </summary>
		/// <param name="format">The video encoding mime type format, i.e. "video/webm" or "video/webm;codecs=vp9". See <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types"/>.</param>
		/// <param name="bitsPerSecond">Audio and video bits per second. See <see href="https://developer.mozilla.org/en-US/docs/Web/API/MediaRecorder/MediaRecorder"/>.</param>
		/// <param name="updateInterval">Update interval in seconds. The default is zero causing the video to be uploaded on <see cref="StopRecording"/>.</param>
		/// <remarks>
		/// <para>
		/// You must call <see cref="StopRecording"/> to end recording. The user must have already shared a screen, otherwise
		/// the recording doesn't start and an error is reported on the client.
		/// </para>
		/// <para>
		/// When <paramref name="updateInterval"/> is greater than zero, the data recorded so far is uploaded every <paramref name="updateInterval"/>
		/// seconds and the <see cref="Uploaded"/> event is fired for each chunk; only the first chunk contains the video header.
		/// </para>
		/// </remarks>
		/// <example>
		/// Starting and stopping the recording:
		/// <code><![CDATA[
		/// private void buttonStart_Click(object sender, EventArgs e)
		/// {
		///     this.screenRecorder1.StartRecording("video/webm", 4000000);
		/// }
		///
		/// private void buttonStop_Click(object sender, EventArgs e)
		/// {
		///     this.screenRecorder1.StopRecording();
		/// }
		///
		/// private void screenRecorder1_Uploaded(object sender, UploadedEventArgs e)
		/// {
		///     var file = e.Files[0];
		///     file.SaveAs(Path.Combine(Application.StartupPath, "Recordings", $"{DateTime.Now:yyyyMMdd-HHmmss}.webm"));
		/// }
		/// ]]></code>
		/// </example>
		public void StartRecording(string format = "video/webm", int bitsPerSecond = 2500000, int updateInterval = 0)
		{
			Call("startRecording", format, bitsPerSecond, updateInterval);
		}

		/// <summary>
		/// Stops recording and uploads the recorded stream to the <see cref="Uploaded"/> event.
		/// </summary>
		/// <remarks>
		/// The upload is asynchronous: the <see cref="Uploaded"/> event is fired when the browser has finished posting the recording.
		/// Calling this method when there is no active recording reports an error on the client.
		/// </remarks>
		/// <example>
		/// Stopping the recording:
		/// <code><![CDATA[
		/// private void buttonStop_Click(object sender, EventArgs e)
		/// {
		///     this.screenRecorder1.StopRecording();
		/// }
		/// ]]></code>
		/// </example>
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
