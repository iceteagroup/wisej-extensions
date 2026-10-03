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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.Camera
{
    /// <summary>
    /// The Camera component makes it possible to take pictures with the device's camera and upload them to the server.
    /// </summary>
    /// <remarks>
    /// The video stream starts automatically as soon as the control is rendered with
    /// <see cref="Video"/> set to true; there is no explicit Start method. Set
    /// <see cref="Video"/> to false (or dispose the control) to release the device.
    /// <para>
    /// The application must be served over HTTPS (or localhost), otherwise the browser
    /// will not grant access to the camera. When the user denies permission, the failure
    /// is reported through the <see cref="Error"/> event; no exception is thrown on the server.
    /// </para>
    /// </remarks>
    /// <example>
    /// This example adds a camera preview to a form and lets the user start and stop the stream.
    /// <code><![CDATA[
    /// using Wisej.Web;
    /// using Wisej.Web.Ext.Camera;
    ///
    /// public partial class Form1 : Form
    /// {
    ///     private Camera camera;
    ///
    ///     public Form1()
    ///     {
    ///         InitializeComponent();
    ///
    ///         this.camera = new Camera
    ///         {
    ///             Dock = DockStyle.Fill,
    ///             Video = true,                                  // starts the stream on render
    ///             Audio = false,
    ///             Mirror = true,                                 // selfie-style preview
    ///             ObjectFit = ObjectFit.Cover,
    ///             FacingMode = Camera.VideoFacingMode.User,      // Environment = rear camera
    ///             WidthCapture = 1280,
    ///             HeightCapture = 720
    ///         };
    ///
    ///         this.camera.Error += (s, e) =>
    ///             AlertBox.Show(e.Message, MessageBoxIcon.Error);
    ///
    ///         this.Controls.Add(this.camera);
    ///     }
    ///
    ///     // Turning the stream off releases the device and turns off the camera LED.
    ///     private void stopButton_Click(object sender, EventArgs e)
    ///         => this.camera.Video = false;
    ///
    ///     private void startButton_Click(object sender, EventArgs e)
    ///         => this.camera.Video = true;
    /// }
    ///
    /// // Picking a specific camera:
    /// var devices = await this.camera.GetDevicesAsync(refresh: true);
    /// this.camera.DeviceName = devices.FirstOrDefault();   // matches the browser's device label
    /// ]]></code>
    /// </example>
    [ToolboxItem(true)]
	[ToolboxBitmap(typeof(Camera))]
	[Description("The Camera component makes it possible to take pictures with the device's camera and upload them to the server.")]
	[ApiCategory("Camera")]
	public partial class Camera : Control, IWisejHandler
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.Camera" /> class.
		/// </summary>
		public Camera()
		{
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
		/// Fired while the <see cref="Camera" /> control receives the recording stream being uploaded.
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
		/// Fired when an error occurs in the camera setup or usage.
		/// </summary>
		public event CameraErrorHandler Error
		{
			add { base.AddHandler(nameof(Error), value); }
			remove { base.RemoveHandler(nameof(Error), value); }
		}

        /// <summary>
        /// Triggers the <see cref="Error"/> event, indicating that an error has occurred in the camera component.
        /// </summary>
        /// <param name="e">An instance of <see cref="CameraErrorEventArgs"/> containing details about the specific error that occurred.</param>
		/// <example>
		/// <code><![CDATA[
		/// camera.OnError(new CameraErrorEventArgs("Camera not found"));
		/// ]]></code>
		/// </example>
        protected virtual void OnError(CameraErrorEventArgs e)
		{
			((CameraErrorHandler)base.Events[nameof(Error)])?.Invoke(this, e);
		}

        /// <summary>
        /// Triggers the <see cref="Uploaded"/> event, signaling that a file has been successfully uploaded.
        /// </summary>
        /// <param name="e">
        /// An instance of <see cref="UploadedEventArgs"/> containing the details of the uploaded file, including its metadata and any associated information.
        /// </param>
        /// <remarks>
        /// This method is called internally when the browser finishes posting the recorded stream, after
        /// <see cref="StopRecording"/> or at each interval specified by <see cref="StartRecording"/>. Subscribe to the
        /// <see cref="Uploaded"/> event, or override this method in a derived class, to process the uploaded data.
        /// <para>
        /// The posted data is available through <see cref="UploadedEventArgs.Files"/>. Each entry exposes the file name
        /// assigned by the client, the size of the payload in bytes, and the stream containing the data. When overriding,
        /// call <c>base.OnUploaded(e)</c> so that subscribers still receive the event.
        /// </para>
        /// </remarks>
        /// <example>
        /// Reading the file name and size from the uploaded recording and saving it to disk:
        /// <code><![CDATA[
        /// this.camera.Uploaded += (sender, e) =>
        /// {
        ///     var file = e.Files[0];
        ///
        ///     string fileName = file.FileName;         // "recording.webm"
        ///     int fileSize = file.ContentLength;       // 184320 (180 KB)
        ///
        ///     string path = Path.Combine(Application.StartupPath, "uploads", fileName);
        ///     using (var output = System.IO.File.Create(path))
        ///         file.InputStream.CopyTo(output);
        ///
        ///     AlertBox.Show($"Saved {fileName} ({fileSize / 1024} KB).");
        /// };
        /// ]]></code>
        /// The same values are available when overriding the method in a derived control:
        /// <code><![CDATA[
        /// protected override void OnUploaded(UploadedEventArgs e)
        /// {
        ///     base.OnUploaded(e);
        ///
        ///     foreach (var file in e.Files)
        ///         Application.Log($"Uploaded {file.FileName}, {file.ContentLength} bytes.");
        /// }
        /// ]]></code>
        /// </example>
        /// <seealso cref="Uploaded"/>
        /// <seealso cref="StartRecording(string, int, int)"/>
        /// <seealso cref="StopRecording"/>
        protected virtual void OnUploaded(UploadedEventArgs e)
        {
            ((UploadedEventHandler)base.Events[nameof(Uploaded)])?.Invoke(this, e);
        }

        /// <summary>
        /// Fires the <see cref="Progress"/> event, reporting how much of the recorded stream has been uploaded.
        /// </summary>
        /// <param name="e">
        /// An instance of <see cref="T:Wisej.Web.UploadProgressEventArgs"/> containing the event data for the upload
        /// currently in progress: the number of bytes transferred so far and the total number of bytes to transfer.
        /// </param>
        /// <remarks>
        /// This event fires only if there is a handler attached to it. A simple overload of the On[Event] method in
        /// a derived class will not be invoked unless there is at least one handler attached to the event, since the
        /// progress notification is wired to the client only when the <see cref="Progress"/> event has subscribers.
        /// <para>
        /// The client reports raw byte counts rather than a percentage; calculate the percentage from
        /// <c>Loaded</c> and <c>Total</c> as shown below. Progress notifications are sent while the recording started by
        /// <see cref="StartRecording"/> is uploaded, either at the interval requested there or once on <see cref="StopRecording"/>.
        /// </para>
        /// </remarks>
        /// <example>
        /// Displaying the upload progress of a recording in a <see cref="ProgressBar"/>:
        /// <code><![CDATA[
        /// this.camera.Progress += (sender, e) =>
        /// {
        ///     int percentage = e.Total > 0
        ///         ? (int)(e.Loaded * 100 / e.Total)
        ///         : 0;
        ///
        ///     this.progressBar.Value = percentage;      // 0 ... 100
        ///     this.statusLabel.Text = $"Uploading: {percentage}% ({e.Loaded} of {e.Total} bytes)";
        /// };
        /// ]]></code>
        /// Derived controls can override the method instead. Call <c>base.OnProgress(e)</c> so that subscribers still
        /// receive the event:
        /// <code><![CDATA[
        /// protected override void OnProgress(UploadProgressEventArgs e)
        /// {
        ///     base.OnProgress(e);
        ///
        ///     Application.Log($"Uploaded {e.Loaded} of {e.Total} bytes.");
        /// }
        /// ]]></code>
        /// </example>
        /// <seealso cref="Progress"/>
        /// <seealso cref="Uploaded"/>
        /// <seealso cref="StartRecording(string, int, int)"/>
        protected virtual void OnProgress(UploadProgressEventArgs e)
        {
            ((UploadProgressEventHandler)base.Events[nameof(Progress)])?.Invoke(this, e);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the CSS filter to apply to the video stream, enhancing its appearance or applying visual effects.
        /// For more information on available filter options, refer to the <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/filter"/>.
        /// </summary>
        /// <example>
        /// /// <code><![CDATA[
		/// // Full grayscale.
        /// camera.VideoFilter = "grayscale(100%)";
        /// ]]></code>
        /// </example>
        [DefaultValue(null)]
		public string VideoFilter
		{
			get { return this._videoFilter; }
			set
			{
				value = value == "" ? null : value;

				if (this._videoFilter != value)
				{
					this._videoFilter = value;
					Update();
				}
			}
		}
		private string _videoFilter = null;

        /// <summary>
        /// Gets or sets the <see cref="ObjectFit"/> property that specifies how the content of a video should be resized to fit its container.
        /// </summary>
        /// <remarks>
        /// The <c>ObjectFit</c> property determines the method by which the video content is adjusted to match the dimensions of its container.
        /// Refer to the following link for more information on CSS object-fit values:
        /// <see href="https://www.w3schools.com/css/css3_object-fit.asp"/>.
        /// </remarks>
		/// /// <example>
		/// This example shows how to set the object fit for the camera:
		/// <code><![CDATA[
		/// camera.ObjectFit = ObjectFit.Cover;
		/// ]]></code>
		/// </example>
        [DesignerActionList]
		[DefaultValue(true)]
		public ObjectFit ObjectFit
		{
			get
			{
				return this._objectfit;
			}
			set
			{
				if (this._objectfit != value)
				{
					this._objectfit = value;
					Update();
				}
			}
		}
		private ObjectFit _objectfit = ObjectFit.Contain;

        /// <summary>
        /// Gets or sets a value indicating whether audio recording is enabled.
        /// </summary>
		/// <value>
		/// <c>true</c> if audio should be recorded; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// This property controls the audio capture capability of the camera extension.
		/// When set to <c>true</c>, audio will be recorded alongside the video stream.
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

        /// <summary>
        /// Gets or sets a value indicating whether video recording is enabled.
        /// </summary>
		/// /// <value>
		/// <c>true</c> if video recording should be enabled; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// When set to <c>true</c>, the camera will record video. Ensure that the necessary permissions
		/// and resources are available for video recording to function properly.
		/// </remarks>
        [DesignerActionList]
		[DefaultValue(true)]
		public bool Video
		{
			get
			{
				return this._video;
			}
			set
			{
				if (this._video != value)
				{
					this._video = value;
					Update();
				}
			}
		}
		private bool _video = true;

        /// <summary>
        /// Gets or sets a value indicating whether the media output should be mirrored.
        /// When set to <c>true</c>, the media will be displayed as a mirrored image, which is commonly used for cameras to provide a preview that reflects the way a user sees themselves.
        /// </summary>
        /// <value><c>true</c> if the media is to be mirrored; otherwise, <c>false</c>.</value>
        /// <example>
        /// <code><![CDATA[
        /// camera.Mirror = true; // Enable mirroring for the camera output
        /// ]]></code>
        /// </example>
        [DesignerActionList]
        [DefaultValue(false)]
        public bool Mirror
        {
            get
            {
                return this._mirror;
            }
            set
            {
                if (this._mirror != value)
                {
                    this._mirror = value;
                    Update();
                }
            }
        }
        private bool _mirror = false;

        /// <summary>
        /// Gets or sets the name of the video device to be used for capturing video input.
        /// </summary>
        /// <remarks>
        /// If the <c>DeviceName</c> is not specified or if it refers to an invalid device, the first available video device returned by the browser will be used by default.
        /// </remarks>
		/// /// <example>
		/// <code><![CDATA[
		/// string deviceName = camera.DeviceName;
		/// AlertBox.Show($"Current device: {deviceName}");
		/// ]]></code>
		/// </example>
        [DefaultValue("")]
		public string DeviceName
		{
			get
			{
				return this._deviceName ?? String.Empty;
			}
			set
			{
				if (this._deviceName != value)
				{
					this._deviceName = value;
					Update();
				}
			}
		}
		private string _deviceName = null;

        /// <summary>
        /// Gets or sets the direction the camera producing the video track is facing.
        /// <para>
        /// Possible values are:
        /// <list type="bullet">
        /// <item><description><see cref="VideoFacingMode.User"/> — faces the user, e.g. the front camera on a smartphone.</description></item>
        /// <item><description><see cref="VideoFacingMode.Environment"/> — faces away from the user, e.g. the rear camera.</description></item>
        /// <item><description><see cref="VideoFacingMode.Left"/> — faces the user from their left.</description></item>
        /// <item><description><see cref="VideoFacingMode.Right"/> — faces the user from their right.</description></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <value>
        /// A <see cref="VideoFacingMode"/> value representing the camera's facing mode. The default is <see cref="VideoFacingMode.User"/>.
        /// </value>
        /// <remarks>
        /// This property is relevant only on devices with more than one camera, typically phones and tablets. On desktop
        /// browsers the constraint is generally ignored and the default device is used. Use <see cref="DeviceName"/> to
        /// select a specific camera instead.
        /// <para>
        /// For more information on camera facing modes, see
        /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/MediaTrackSettings/facingMode"/>.
        /// </para>
        /// </remarks>
        /// <example>
        /// Switching to the rear-facing camera:
        /// <code><![CDATA[
        /// this.camera.FacingMode = Camera.VideoFacingMode.Environment;
        /// ]]></code>
        /// </example>
        /// <seealso cref="DeviceName"/>
        [DesignerActionList]
        [DefaultValue(VideoFacingMode.User)]
        public VideoFacingMode FacingMode
        {
            get
            {
                return this._facingMode;
            }
            set
            {
                if (this._facingMode != value)
                {
                    this._facingMode = value;
                    Update();
                }
            }
        }
        private VideoFacingMode _facingMode = VideoFacingMode.User;


        /// <summary>
        /// Gets or sets the width of the video capture resolution, measured in pixels.
		/// This property determines the horizontal dimension of the captured video,
		/// allowing for adjustments to optimize video quality or performance based on the target display format.
		/// </summary>
		/// <value>
		/// An integer representing the width of the video capture resolution in pixels.
		/// The default value is typically set by the camera configuration or underlying framework.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// camera.WidthCapture = 1280; // Set the video capture width to 1280 pixels
		/// int currentWidth = camera.WidthCapture; // Retrieve the current video capture width
		/// ]]></code>
		/// </example>
        [DesignerActionList]
		[DefaultValue(true)]
		public int WidthCapture
		{
			get
			{
				return this._widthCapture;
			}
			set
			{
				if (this._widthCapture != value)
				{
					this._widthCapture = value;
					Update();
				}
			}
		}
		private int _widthCapture = 640;

        /// <summary>
        /// Gets or sets the height of the video capture resolution in pixels.
        /// This property defines the vertical dimension of the video feed from the camera.
        /// </summary>
        /// <value>
        /// An integer representing the height of the video capture resolution.
        /// The value must be a positive integer, typically in accordance with standard video resolutions.
        /// </value>
        /// <example><![CDATA[
        /// int captureHeight = camera.HeightCapture;
        /// camera.HeightCapture = 720; // Set height to 720 pixels
        /// ]]></example>
        [DesignerActionList]
		[DefaultValue(true)]
		public int HeightCapture
		{
			get
			{
				return this._heightCapture;
			}
			set
			{
				if (this._heightCapture != value)
				{
					this._heightCapture = value;
					Update();
				}
			}
		}
		private int _heightCapture = 480;


        /// <summary>
        /// Gets or sets the border style of the control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:Wisej.Web.BorderStyle"/> value that specifies the border style.
        /// The default value is <see cref="F:Wisej.Web.BorderStyle.None"/>.
        /// </returns>
        [DefaultValue(BorderStyle.Solid)]
		[SRCategory("CatAppearance")]
		[SRDescription("Indicates the border style for the control.")]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				return this._borderStyle;
			}
			set
			{
				if (this._borderStyle != value)
				{
					this._borderStyle = value;

					Refresh();

					OnStyleChanged(EventArgs.Empty);
				}
			}
		}
		private BorderStyle _borderStyle = BorderStyle.Solid;

        /// <summary>
        /// Gets or sets the zoom level of the camera. The zoom value adjusts the camera's field of view,
        /// allowing you to zoom in or out on the scene captured by the camera.
        /// </summary>
        /// <remarks>
        /// Please note that not all camera models support zoom functionality.
		/// Ensure that the camera in use has zoom capabilities before modifying this property.
        /// </remarks>
		/// /// <value>
		/// An integer representing the zoom level. Positive values indicate zooming in,
		/// while negative values indicate zooming out. The range of acceptable values may vary
		/// depending on the specific camera implementation.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// camera.Zoom = 5; // Set zoom to a value for zooming in
		/// camera.Zoom = -3; // Set zoom to a value for zooming out
		/// ]]></code>
		/// </example>
        [DefaultValue(1)]
		public int Zoom
		{
			get
			{
				return this._zoom;
			}
			set
			{
				if (this._zoom != value)
				{
					this._zoom = value;

					Update();
				}
			}
		}
		private int _zoom = 1;

        #endregion

        #region Methods

        /// <summary>
        /// Captures the current image from the camera and invokes the specified callback
        /// with the captured image as an argument. The callback will receive <c>null</c>
        /// if the image capture fails or if no image is available.
        /// </summary>
        /// <param name="callback">A delegate that processes the captured <see cref="Image"/>.
		/// This parameter cannot be <c>null</c>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="callback"/> is <c>null</c>.</exception>
		/// <example>
		/// <code><![CDATA[
		/// camera.GetImage(image =>
		/// {
		///     if (image != null)
		///     {
		///         Console.WriteLine("Image captured successfully.");
		///     }
		///     else
		///     {
		///         Console.WriteLine("No image available.");
		///     }
		/// });
		/// ]]></code>
		/// </example>
        public void GetImage(Action<Image> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Call("getImage", (base64) =>
			{

				callback(ImageFromBase64(base64));

			}, null);
		}

        /// <summary>
        /// Asynchronously retrieves the current image captured by the camera.
        /// </summary>
        /// <returns>
		/// A task representing the asynchronous operation, which, when completed, returns
		/// an <see cref="Image"/> object containing the current image from the camera.
		/// </returns>
		/// <remarks>
		/// This method can be awaited to obtain the latest image captured by the camera hardware.
		/// Ensure that the camera is properly initialized and active before calling this method.
		/// </remarks>
        public Task<Image> GetImageAsync()
		{
			var tcs = new TaskCompletionSource<Image>();
			GetImage((image) =>
			{
				tcs.SetResult(image);
			});
			return tcs.Task;
		}

        /// <summary>
        /// Initiates the recording of audio and video using the specified parameters.
        /// </summary>
        /// <param name="format">The MIME type for the video encoding format. For more information on MIME types, see <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types"/>.</param>
		/// <param name="bitsPerSecond">The bitrate for both audio and video in bits per second. This determines the quality of the recording. For additional details, refer to <see href="https://developer.mozilla.org/en-US/docs/Web/API/MediaRecorder/MediaRecorder"/>.</param>
		/// <param name="updateInterval">The interval at which updates are sent, in seconds. A value of zero indicates that the video will be uploaded only upon calling <see cref="StopRecording"/>.</param>
		/// <remarks>
		/// It is essential to invoke <see cref="StopRecording"/> to properly end the recording process.
		/// </remarks>
        public void StartRecording(string format=null, int bitsPerSecond = 2500000, int updateInterval = 0)
		{
			Call("startRecording", format, bitsPerSecond, updateInterval);
		}

        /// <summary>
        /// Stops the recording process of the camera. 
		/// Once the recording is stopped, the recorded stream is uploaded and made available through the <see cref="Uploaded"/> event.
        /// </summary>
		/// /// <remarks>
		/// This method should be called when you want to finalize the recording session. Make sure to handle the <see cref="Uploaded"/> event to process the uploaded stream accordingly.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// camera.StopRecording();
		/// camera.Uploaded += (sender, e) =>
		/// {
		///     Console.WriteLine("The recorded stream has been uploaded.");
		/// };
		/// ]]></code>
		/// </example>
        public void StopRecording()
		{
			Call("stopRecording");
		}

        /// <summary>
        /// Asynchronously retrieves the names of the available video devices and invokes the specified callback with the results.
        /// </summary>
        /// <param name="callback">The callback method that will be invoked with an array of device names. This parameter must not be null.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="callback"/> is null.</exception>
		/// <example>
		/// <code><![CDATA[
		/// camera.GetDevices(deviceNames =>
		/// {
		///     foreach (var name in deviceNames)
		///     {
		///         Console.WriteLine(name);
		///     }
		/// });
		/// ]]></code>
		/// </example>
        public void GetDevices(Action<string[]> callback)
		{
			GetDevices(false, callback);
		}

        /// <summary>
        /// Retrieves the names of the video devices (cameras) available on the client.
        /// </summary>
        /// <param name="refresh">
        /// A <see langword="bool"/> that indicates whether to re-read the list of devices from the browser.
        /// If <c>true</c>, the device list is enumerated again; otherwise the list cached on the client is used.
        /// </param>
        /// <param name="callback">
        /// An <see cref="Action{T}"/> delegate invoked with the array of device names once the client responds.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="callback"/> parameter is <c>null</c>,
        /// indicating that a valid callback method must be provided.
        /// </exception>
        /// <remarks>
        /// The names are the labels the browser reports for its video input devices, for example
        /// <c>"HD Pro Webcam C920 (046d:082d)"</c> or <c>"FaceTime HD Camera"</c>. Assign one of them to
        /// <see cref="DeviceName"/> to select that camera.
        /// <para>
        /// Browsers only expose device labels after the user has granted camera permission, and devices without a
        /// label are skipped. Calling this method before the stream has started may therefore return an empty array.
        /// </para>
        /// <para>
        /// The call is asynchronous: it returns immediately and <paramref name="callback"/> is invoked on a later
        /// round-trip from the client. Use <see cref="GetDevicesAsync"/> to await the result instead.
        /// </para>
        /// </remarks>
        /// <example>
        /// Filling a <see cref="ComboBox"/> with the available cameras and selecting one:
        /// <code><![CDATA[
        /// this.camera.GetDevices(true, deviceNames =>
        /// {
        ///     // deviceNames: { "HD Pro Webcam C920 (046d:082d)", "Integrated Webcam", "OBS Virtual Camera" }
        ///
        ///     this.deviceComboBox.Items.Clear();
        ///     this.deviceComboBox.Items.AddRange(deviceNames);
        ///
        ///     if (deviceNames.Length > 0)
        ///         this.camera.DeviceName = deviceNames[0];
        ///     else
        ///         AlertBox.Show("No camera detected, or permission has not been granted yet.");
        /// });
        /// ]]></code>
        /// </example>
        /// <seealso cref="GetDevicesAsync"/>
        /// <seealso cref="DeviceName"/>
        public void GetDevices(bool refresh, Action<string[]> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            Call("getDevices", (list) =>
            {

                callback(list);

            }, new object[] { refresh });
        }

        /// <summary>
        /// Returns the names of the available video devices.
        /// </summary>
        /// <param name="refresh">Refreshes the list of devices from the browser. Default is false.</param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        public Task<string[]> GetDevicesAsync(bool refresh = false)
		{
			var tcs = new TaskCompletionSource<string[]>();
			GetDevices(refresh, (list) =>
			{

				tcs.SetResult(list);

			});
			return tcs.Task;
		}

        /// <summary>
        /// Converts the base64 data URL returned by the client into an <see cref="Image"/>.
        /// </summary>
        /// <param name="base64">
        /// A data URL containing the base64 encoded snapshot, as received from the client — for example
        /// <c>"data:image/png;base64,iVBORw0KGgoAAAANSUhEUg..."</c>. Everything up to and including the
        /// <c>"base64,"</c> marker is discarded before decoding.
        /// </param>
        /// <returns>
        /// An <see cref="Image"/> created from the decoded bytes, or <see langword="null"/> when
        /// <paramref name="base64"/> is null or empty, does not contain the <c>"base64,"</c> marker,
        /// or cannot be decoded into a valid image.
        /// </returns>
        /// <remarks>
        /// This method never throws: any decoding failure is swallowed and reported as a <see langword="null"/>
        /// return value, so callers must check the result before using it. It is used internally by
        /// <see cref="GetImage"/> to translate the client's response into an image.
        /// </remarks>
        /// <example>
        /// Consumers reach this conversion through <see cref="GetImage"/> or <see cref="GetImageAsync"/>,
        /// which return the decoded image directly:
        /// <code><![CDATA[
        /// var image = await this.camera.GetImageAsync();
        ///
        /// if (image != null)
        ///     this.pictureBox.Image = image;
        /// else
        ///     AlertBox.Show("The snapshot could not be captured.");
        /// ]]></code>
        /// </example>
        /// <seealso cref="GetImage"/>
        /// <seealso cref="GetImageAsync"/>
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

        /// <summary>
        /// Translates the "progress" event posted by the client into a <see cref="UploadProgressEventArgs"/>
        /// and fires the <see cref="Progress"/> event.
        /// </summary>
        /// <param name="e">
        /// The event arguments received from the client. <c>e.Parameters.Data</c> carries the upload state as
        /// <c>loaded</c> (bytes transferred so far) and <c>total</c> (bytes to transfer).
        /// </param>
        /// <remarks>
        /// Called from <see cref="OnWebEvent"/> when the client reports upload progress while the recording started by
        /// <see cref="StartRecording"/> is being posted to the server. The method performs no UI or state updates of its
        /// own; it only forwards the values to <see cref="OnProgress"/>, which raises <see cref="Progress"/> for
        /// subscribers to handle.
        /// <para>
        /// <c>e.Parameters.Data</c> is a dynamic payload, so its members are not guaranteed to be present; missing
        /// <c>loaded</c> or <c>total</c> values are coalesced to zero. Handlers should therefore guard against a
        /// <c>Total</c> of zero before calculating a percentage.
        /// </para>
        /// </remarks>
        /// <seealso cref="OnProgress"/>
        /// <seealso cref="Progress"/>
        private void ProcessProgressWebEvent(WisejEventArgs e)
		{
			var data = e.Parameters.Data;
			OnProgress(new UploadProgressEventArgs(data.loaded ?? 0, data.total ?? 0));
		}

        /// <summary>
        /// Handles events triggered from the client side.
        /// This method processes the provided <paramref name="e"/> event arguments,
        /// allowing for custom behavior in response to web events related to the camera extension.
        /// </summary>
        /// <param name="e">The event arguments encapsulating information about the web event.</param>
		/// <remarks>
		/// Implementing this method allows the customization of event handling
		/// for specific camera interactions, such as capturing images or handling device status changes.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // Example of handling a web event from the camera
		/// OnWebEvent(new WisejEventArgs(/* parameters */));
		/// ]]></code>
		/// </example>
        protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "error":
					OnError(new CameraErrorEventArgs(e.Parameters.Message));
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
        /// Renders the client component using the specified configuration settings.
        /// </summary>
        /// <param name="config">A dynamic object containing configuration options for the rendering process.
		/// This can include various properties related to the camera component's behavior and appearance.</param>
        protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.Camera";

			config.zoom = this.Zoom;
			config.mirror = this.Mirror;
			config.objectFit = this.ObjectFit;
			config.borderStyle = this.BorderStyle;
			config.videoFilter = this.VideoFilter;
			config.submitURL = this.GetPostbackURL();

			// apply video constraints.
			if (this.Video)
			{
				config.constraints = new
				{
					video = new
					{
						width = this._widthCapture,
						height = this._heightCapture,
						facingMode = this._facingMode,
						deviceName = this._deviceName
					},
					audio = this.Audio
				};
			}
			else
			{
				config.constraints = new
				{
					video = false,
					audio = this.Audio
				};
			}

			config.wiredEvents.Add("error(Message)");

			if (base.Events[nameof(Progress)] != null)
				config.wiredEvents.Add("progress(Data)");
		}

        #endregion

        #region IWisejHandler

        /// <summary>
        /// Gets or sets a value indicating whether to compress the output from the camera.
        /// When this property is set to <c>true</c>, the camera will produce compressed output,
        /// which can reduce the amount of data transmitted, but may also affect image quality.
        /// </summary>
        /// <value>
        /// <c>true</c> if the output should be compressed; otherwise, <c>false</c>.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// camera.Compress = true; // Enable output compression
        /// ]]></code>
        /// </example>
        bool IWisejHandler.Compress { get { return false; } }

        /// <summary>
        /// Processes the incoming HTTP request and performs the necessary operations
		/// related to the Camera extension in the Wisej framework.
        /// </summary>
        /// <param name="context">The current <see cref="System.Web.HttpContext"/> that contains
        /// information about the HTTP request and response.</param>
        /// <remarks>
        /// This method is responsible for handling the specific logic depending on
        /// the request type. Ensure that the <paramref name="context"/> is valid
        /// and check for the required parameters before invoking this method.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// HttpContext context = HttpContext.Current;
        /// camera.ProcessRequest(context);
        /// ]]></code>
        /// </example>
        void IWisejHandler.ProcessRequest(HttpContext context)
		{
			OnUploaded(new UploadedEventArgs(context.Request.Files));
		}

		#endregion
	}
}
