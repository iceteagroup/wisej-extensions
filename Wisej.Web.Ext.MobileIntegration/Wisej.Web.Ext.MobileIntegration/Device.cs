///////////////////////////////////////////////////////////////////////////////
//
// (C) 2019 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.IO;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Represents the user's mobile device. The Wisej application
	/// interacts with the device through this class.
	/// </summary>
	[ApiCategory("API")]
	public sealed partial class Device : Base.Component
	{
		#region Constructor

		// Singleton constructor.
		private Device()
		{
			// throw if the app is trying to use this class outside of the container.
			try
			{
				var info = new DeviceInfo(Application.QueryString);
				if (String.IsNullOrEmpty(info.Model))
					throw new InvalidOperationException("Device not found.");
			}
			catch
			{
				throw new InvalidOperationException("Device not found.");
			}
		}

		// The Device singleton.
		private static Device Instance
		{
			get
			{
				var instance = (Device)Application.Session[typeof(Device).FullName];
				if (instance == null)
				{
					instance = new Device();
					Application.Session[typeof(Device).FullName] = instance;
				}
				return instance;
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the device sends an event back to the application.
		/// </summary>
		public static event DeviceEventHandler Event
		{
			add { Instance._event += value; }
			remove { Instance._event -= value; }
		}
		private event DeviceEventHandler _event;

		/// <summary>
		/// Fired when there's an update from the device's accelerometers.
		/// </summary>
		public static event DeviceEventHandler AccelerometerUpdate
		{
			add { Instance._accelerometerUpdate += value; }
			remove { Instance._accelerometerUpdate -= value; }
		}
		private event DeviceEventHandler _accelerometerUpdate;

		/// <summary>
		/// Fired when there's an update from the device's magnetometer.
		/// </summary>
		public static event DeviceEventHandler MagnetometerUpdate
		{
			add { Instance._magnetometerUpdate += value; }
			remove { Instance._magnetometerUpdate -= value; }
		}
		private event DeviceEventHandler _magnetometerUpdate;

		/// <summary>
		/// Fired when the mobile application is brought to the foreground.
		/// </summary>
		public static event EventHandler Foreground
		{
			add { Instance._foreground += value; }
			remove { Instance._foreground -= value; }
		}
		private event EventHandler _foreground;

		/// <summary>
		/// Fired when the mobile application is sent to the background.
		/// </summary>
		public static event EventHandler Background
		{
			add { Instance._background += value; }
			remove { Instance._background -= value; }
		}
		private event EventHandler _background;

		/// <summary>
		/// Fired when the mobile application is terminated.
		/// </summary>
		public static event EventHandler Terminate
		{
			add { Instance._terminate += value; }
			remove { Instance._terminate -= value; }
		}
		private event EventHandler _terminate;

		/// <summary>
		/// Fired when the orientation of the device changes.
		/// </summary>
		public static event DeviceEventHandler OrientationChanged
		{
			add { Instance._orientationChanged += value; }
			remove { Instance._orientationChanged -= value; }
		}
		private event DeviceEventHandler _orientationChanged;

		/// <summary>
		/// Fired when the brightness of the device's screen changes.
		/// </summary>
		public static event DeviceEventHandler BrightnessChanged
		{
			add { Instance._brightnessChanged += value; }
			remove { Instance._brightnessChanged -= value; }
		}
		private event DeviceEventHandler _brightnessChanged;

		/// <summary>
		/// Fired when the device gets attached to a new screen
		/// </summary>
		public static event DeviceEventHandler ScreenAdded
		{
			add { Instance._screenAdded += value; }
			remove { Instance._screenAdded -= value; }
		}
		private event DeviceEventHandler _screenAdded;

		/// <summary>
		/// Fired when a screen gets removed from the device
		/// </summary>
		public static event DeviceEventHandler ScreenRemoved
		{
			add { Instance._screenRemoved += value; }
			remove { Instance._screenRemoved -= value; }
		}
		private event DeviceEventHandler _screenRemoved;

		/// <summary>
		/// Fired when a screen gets removed from the device
		/// </summary>
		public static event DeviceEventHandler ModeChanged
		{
			add { Instance._modeChanged += value; }
			remove { Instance._modeChanged -= value; }
		}
		private event DeviceEventHandler _modeChanged;

		/// <summary>
		/// Fired when there's an update from the device's gyroscope.
		/// </summary>
		public static event DeviceEventHandler GyroscopeUpdate
		{
			add { Instance._gyroscopeUpdate += value; }
			remove { Instance._gyroscopeUpdate -= value; }
		}
		private event DeviceEventHandler _gyroscopeUpdate;

		/// <summary>
		/// Fired when the device subscribes to remote notifications and receives a remote token
		/// </summary>
		public static event DeviceEventHandler SubscribedNotifications
		{
			add { Instance._subscribedNotifications += value; }
			remove { Instance._subscribedNotifications -= value; }
		}
		private event DeviceEventHandler _subscribedNotifications;

		/// <summary>
		/// Fired when the device receives a push notification.
		/// </summary>
		public static event DeviceEventHandler ReceivedNotification
		{
			add { Instance._receivedNotification += value; }
			remove { Instance._receivedNotification -= value; }
		}
		private event DeviceEventHandler _receivedNotification;

		/// <summary>
		/// Fired when the back button is pressed on the device (Android only).
		/// </summary>
		public static event DeviceEventHandler BackButtonPressed
		{
			add { Instance._backButtonPressed += value; }
			remove { Instance._backButtonPressed -= value; }
		}
		private event DeviceEventHandler _backButtonPressed;

		// Handles "device.response" event from the device.
		private void ProcessDeviceResponseEvent(WisejEventArgs e)
		{
			// read the response info and token to terminate
			// the related modal state.
			var data = e.Parameters.Data;
			EndModal(new DeviceResponse(data.result, data.errorCode));
		}

		// Handles "device.action" event from the device.
		private void ProcessDeviceActionEvent(WisejEventArgs e)
		{
			var args = new DeviceEventArgs(e);
			switch (args.Type)
			{
				case "accelerometer.update":
					this._accelerometerUpdate?.Invoke(this, args);
					break;

				case "app.foreground":
					this._foreground?.Invoke(this, args);
					break;

				case "app.background":
					this._background?.Invoke(this, args);
					break;

				case "app.terminate":
					this._terminate?.Invoke(this, args);
					break;

				case "app.orientation":
					Device.Info.UpdateOrientation(args.Data);
					this._orientationChanged?.Invoke(this, args);
					break;

				case "app.modeChanged":
					Device.Info.UpdateStyleMode(args.Data);
					this._modeChanged?.Invoke(this, args);
					break;

				case "device.back":
					this._backButtonPressed?.Invoke(this, args);
					break;

				case "gyroscope.update":
					this._gyroscopeUpdate?.Invoke(this, args);
					break;

				case "magnetometer.update":
					this._magnetometerUpdate?.Invoke(this, args);
					break;

				case "toolbar.tap":
					this._toolbar.RaiseClick(args.Data);
					break;

				case "tabbar.select":
					this._tabBar.RaiseSelected(args.Data);
					break;

				case "notifications.subscribe":
					Device.Info.UpdateDeviceToken(args.Data);
					this._subscribedNotifications?.Invoke(this, args);
					break;

				case "notifications.unsubscribe":
					Device.Info.UpdateDeviceToken("");
					this._subscribedNotifications?.Invoke(this, args);
					break;

				case "notifications.receive":
					this._receivedNotification?.Invoke(this, args);
					break;

				case "screen.brightness":
					Device.Screen.UpdateBrightness(args.Data);
					this._brightnessChanged?.Invoke(this, args);
					break;

				case "screen.added":
					this._screenAdded?.Invoke(this, args);
					break;

				case "screen.removed":
					this._screenRemoved?.Invoke(this, args);
					break;

			}
		}

		// Handles "device.custom" event from the device.
		private void ProcessDeviceCustomEvent(WisejEventArgs e)
		{
			this._event?.Invoke(this, new DeviceEventArgs(e));
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the <see cref="DeviceInfo"/> object containing the
		/// information related to the user's mobile device.
		/// </summary>
		public static DeviceInfo Info
		{
			get
			{
				return Instance._info;
			}
		}
		private DeviceInfo _info = new DeviceInfo(Application.QueryString);

		/// <summary>
		/// Returns the <see cref="DeviceScreen"/> object containing the
		/// information related to the mobile device's screen.
		/// </summary>
		public static DeviceScreen Screen
		{
			get
			{
				return Instance._screen;
			}
		}
		private DeviceScreen _screen = new DeviceScreen(Application.QueryString);

		/// <summary>
		/// Returns the <see cref="DeviceToolbar"/> object representing the
		/// toolbar on the mobile device.
		/// </summary>
		public static DeviceToolbar Toolbar
		{
			get
			{
				return Instance._toolbar;
			}
		}
		private DeviceToolbar _toolbar = new DeviceToolbar();

		/// <summary>
		/// Returns the <see cref="DeviceTabBar"/> object representing the
		/// tabBar on the mobile device.
		/// </summary>
		public static DeviceTabBar TabBar
		{
			get
			{
				return Instance._tabBar;
			}
		}
		private DeviceTabBar _tabBar = new DeviceTabBar();

		/// <summary>
		/// Returns the <see cref="DeviceStatusbar"/> object representing the
		/// statusbar on the mobile device.
		/// </summary>
		public static DeviceStatusbar Statusbar
		{
			get
			{
				return Instance._statusbar;
			}
		}
		private DeviceStatusbar _statusbar = new DeviceStatusbar();

		/// <summary>
		/// Verifies that the application is running inside the mobile container.
		/// </summary>
		public static bool Valid
		{
			get
			{
				// throw if the app is trying to use this class outside of the container.
				try
				{
					return Instance != null;
				}
				catch { }

				return false;
			}
		}

		/// <summary>
		/// The HTML page to display on the device when it loses network connectivity.
		/// </summary>
		/// <remarks>Pass the relative path of the file.</remarks>
		public static string OfflinePage
		{
			get
			{
				return Instance._offlinePage;
			}
			set
			{
				var device = Instance;
				if (device._offlinePage != value)
				{
					device._offlinePage = value;

					// read the contents of the file and send them to the device
					try {
						var html = File.ReadAllText(Application.MapPath(value));

						PostMessage("device.offlineHtml", html);
					}
					catch (FileNotFoundException)
					{
						throw new FileNotFoundException("The path to the file could not be found.");
					}
					catch (Exception e)
					{
						throw new Exception(e.Message);
					}
				}
			}
		}
		private string _offlinePage;

		#endregion

		#region Methods

		#region Utilities

		/// <summary>
		/// Triggers notification feedback on the device.
		/// </summary>
		/// <param name="type">The type of feedback to post to the device.</param>
		public static void Vibrate(DeviceVibrationType type)
		{
			PostMessage("action.vibrate", (int)type);
		}

		/// <summary>
		/// Requests the specified permission from the device.
		/// </summary>
		/// <param name="permission">The permission to request.</param>
		/// <returns>The success of the permission request.</returns>
		public static bool RequestPermission(PermissionType permission)
		{
			var result = PostModalMessage($"permissions.{permission}");
			return result.ErrorCode == 0;
		}

		/// <summary>
		/// If the device is connected to an external display, opens the page on the display.
		/// </summary>
		/// <param name="url">The URL to display on the external screen.</param>
		public static void SetExternalScreenData(string url)
		{
			Device.PostMessage("screen.data", url);
		}

		/// <summary>
		/// Asks the device to authenticate the user using biometrics if available, or the device's passcode.
		/// </summary>
		/// <param name="message">The reason for authenticating.</param>
		/// <returns>The success of the authentication.</returns>
		public static bool Authenticate(string message)
		{
			var result = PostModalMessage("action.authenticate", message);
			return result.ErrorCode == 0;
		}

		/// <summary>
		/// EXPERIMENTAL: Sets the device's language settings to the specified culture.
		/// </summary>
		/// <param name="culture">The culture to apply (i.e, "en", "it", etc.)</param>
		public static void SetLocalization(string culture)
		{
			Device.PostMessage("localization.change", culture);
		}

		/// <summary>
		/// EXPERIMENTAL: Attempts to bind the native application to the specified URL.
		/// </summary>
		/// <param name="link">The link of the Wisej application.</param>
		/// <returns>The success of binding the app.</returns>
		public static bool BindApplication(string link)
		{
			var result = Device.PostModalMessage("device.bind", link);
			return result.ErrorCode == 0;
		}

		/// <summary>
		/// EXPERIMENTAL: Removes the bound-app configuration from the device.
		/// </summary>
		public static void FreeBoundApplication()
		{
			Device.PostMessage("device.freeBind");
		}

		#endregion

		#region Modal Core

		/// <summary>
		/// Posts a message to the device.
		/// </summary>
		/// <param name="command">The command to post to the device, i.e., "action.prompt".</param>
		/// <param name="args">The arguments to pass with the command.</param>
		/// <returns>The response from the device.</returns>
		public static void PostMessage(string command, params object[] args)
		{
			Instance.Call("postMessage", command, args);
		}

		/// <summary>
		/// Posts a message to the device and enters the modal state waiting for the response.
		/// </summary>
		/// <param name="command">The command to post to the device, i.e., "action.prompt".</param>
		/// <param name="args">The arguments to pass with the command.</param>
		/// <returns>The response from the device.</returns>
		public static DeviceResponse PostModalMessage(string command, params object[] args)
		{
			Instance.Call("postMessage", command, args);
			return DoModal();
		}

		// Starts the modal state.
		private static DeviceResponse DoModal()
		{
			var device = Instance;
			Application.DoModal(device);
			var result = device._result;
			device._result = null;

			return result;
		}

		// Terminates the modal state.
		private static void EndModal(DeviceResponse result)
		{
			var device = Instance;
			device._result = result;
			Application.EndModal(device, DialogResult.OK);
		}

		// last result receive in the action.response event from the device.
		private DeviceResponse _result;

		#endregion

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "response":
					ProcessDeviceResponseEvent(e);
					break;

				case "action":
					ProcessDeviceActionEvent(e);
					break;

				case "custom":
					ProcessDeviceCustomEvent(e);
					break;

			}

			base.OnWebEvent(e);
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);
			config.className = "wisej.web.ext.MobileIntegration";

			config.wiredEvents = new WiredEvents();
			config.wiredEvents.Add("response(Data)", "action(Data)", "custom(Data)");
		}

		#endregion
	}
}
