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

using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Wisej.Base;
using Wisej.Core;
using static Wisej.Web.Ext.MobileIntegration.DeviceResponse;

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

		/// <summary>
		/// Gets or creates the device singleton.
		/// </summary>
		private static Device Instance
		{
			get
			{
				var instance = (Device)Application.Session[typeof(Device).FullName];
				if (instance == null)
				{
					instance = new Device();
					Application.Session[typeof(Device).FullName] = instance;
					Application.SessionTimeout += Application_SessionTimeout;
					Application.ApplicationRefresh += instance.Application_ApplicationRefresh;
					
				}
				return instance;
			}
		}

		private static void Application_SessionTimeout(object sender, HandledEventArgs e)
		{
			e.Handled = true;
		}

		private void Application_ApplicationRefresh(object sender, EventArgs e)
		{
			this._tabBar.Update(true);
			this._toolbar.Update(true);
			this._statusbar.Update(true);
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

		/// <summary>
		/// Fired when the device is shaken.
		/// </summary>
		public static event DeviceEventHandler Shake
		{
			add { Instance._shake += value; }
			remove { Instance._shake -= value; }
		}
		private event DeviceEventHandler _shake;

		/// <summary>
		/// Fired when a device permission value changes.
		/// </summary>
		public static event DeviceEventHandler PermissionStateChanged
		{
			add { Instance._permissionStateChanged += value; }
			remove { Instance._permissionStateChanged -= value; }
		}
		private event DeviceEventHandler _permissionStateChanged;

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

				case "app.permission":
					var updated = Device.Info.UpdatePermission(args);
					if (updated)
						this._permissionStateChanged?.Invoke(this, args);
					break;

				case "color.pick":
					var hex = args.Data.color;
					var alpha = args.Data.alpha;
					var color = Color.FromArgb(alpha, ColorTranslator.FromHtml(hex));

					args.Data.color = color;
					this._selectedColorChanged?.Invoke(this, args);
					break;

				case "device.back":
					this._backButtonPressed?.Invoke(this, args);
					break;

				case "device.shake":
					this._shake?.Invoke(this, args);
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
		/// Returns the <see cref="DeviceInfo"/> object containing
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
		/// Returns a dynamic object that can be used to store custom data in relation to the device.
		/// The data is persistent and can be accessed across different applications.
		/// </summary>
		/// <example>
		/// <code>
		/// // saving data.
		/// Device.UserData["myTest"] = "Hello, World!";
		/// 
		/// // reading data.
		/// var text = Device.UserData["myTest"];
		/// 
		/// // clearing data.
		/// Device.UserData.Remove("myTest");
		/// </code>
		/// </example>
		/// <remarks>
		/// The data is serialized and deserialized using JSONConvert before and after retrieval from the device.
		/// </remarks>
		public static new UserDataDictionary<string, object> UserData
		{
			get
			{
				if (Instance._userData == null)
					LoadUserData();

				return Instance._userData;
			}
			set
			{
				if (Instance._userData == null || !Instance._userData.Equals(value))
				{
					Instance._userData = value;
					PostMessage("device.setUserData", JsonConvert.SerializeObject(value));
				}
			}
		}
		private UserDataDictionary<string, object> _userData;

		/// <summary>
		/// Loads the userdata config from the client device.
		/// </summary>
		private static void LoadUserData()
		{
			var result = PostModalMessage("device.getUserData");
			if (result.Status == StatusCode.Success && !string.IsNullOrEmpty(result.Value))
				Instance._userData = JsonConvert.DeserializeObject<UserDataDictionary<string, object>>(result.Value);
			else
				Instance._userData = new UserDataDictionary<string, object>();

			Instance._userData.PropertyChanged += Instance.userData_PropertyChanged;
		}

		/// <summary>
		/// Processes changes in UserData.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void userData_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "": // update everything.
					PostMessage("device.setUserData", JsonConvert.SerializeObject(UserData));
					break;

				default: // update only the given property.
					PostMessage("device.updateUserData", e.PropertyName, UserData[e.PropertyName]);
					break;
			}
		}

		/// <summary>
		/// Checks if the <see cref="UserData"/> dynamic object was created and has any value.
		/// </summary>
		public new bool HasUserData
		{
			get { return this._userData != null && this._userData.Count > 0; }
		}

		/// <summary>
		/// Specifies the background color of the device frame.
		/// </summary>
		/// <example>
		/// <code>
		/// // set the application frame to red.
		/// Device.SetBackgroundColor(Color.Red);
		/// </code>
		/// </example>
		public static void SetBackgroundColor(Color value)
		{
			PostMessage("device.backcolor", ColorTranslator.ToHtml(value));
		}

		#endregion

		#region Methods

		/// <summary>
		/// Triggers notification feedback on the device.
		/// </summary>
		/// <param name="type">The type of feedback to post to the device.</param>
		public static void Vibrate(DeviceVibrationType type)
		{
			PostMessage("action.vibrate", (int)type);
		}

		/// <summary>
		/// Requests the specified permission from the device if access hasn't already been granted.
		/// </summary>
		/// <param name="permission">The permission to request.</param>
		/// <exception cref="DeviceException">
		/// Occurs when the permission could not be requested.
		/// See <see cref="DeviceException.ErrorCode"/> and <see cref="DeviceException.Reason"/>.
		/// </exception>
		public static bool RequestPermission(PermissionType permission)
		{
			var result = PostModalMessage($"permissions.{permission}");
			if (result.Status != StatusCode.Success || permission != (PermissionType)result.Value.type)
				ThrowDeviceException(result);

			return result.Value.result;
		}

		/// <summary>
		/// Asks the device to authenticate the user using biometrics if available, or the device's passcode.
		/// </summary>
		/// <param name="message">The reason for authenticating.</param>
		/// <exception cref="DeviceException">
		/// Occurs when the device did not successfully authenticate the user.
		/// See <see cref="DeviceException.ErrorCode"/> and <see cref="DeviceException.Reason"/>.
		/// </exception>
		public static void Authenticate(string message)
		{
			var result = PostModalMessage("action.authenticate", message);
			if (result.Status != StatusCode.Success)
				ThrowDeviceException(result);
		}

		/// <summary>
		/// Processes exceptions from the device.
		/// </summary>
		/// <param name="result"></param>
		/// <param name="callerName"></param>
		/// <exception cref="DeviceException">
		/// Occurs when there was an error executing a command.
		/// See <see cref="DeviceException.ErrorCode"/> and <see cref="DeviceException.Reason"/>.
		/// </exception>
		private static void ThrowDeviceException(DeviceResponse result, [CallerMemberName] string callerName = "")
		{
			throw new DeviceException(result.Value, callerName, result.Status);
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

		// TODO: Later...
		///// <summary>
		///// Posts a message to the device and passes the response to a callback.
		///// </summary>
		///// <param name="command"></param>
		///// <param name="callback"></param>
		///// <param name="args"></param>
		//public static void PostMessage(string command, Action<DeviceResponse> callback, params object[] args)
		//{
			
		//}

		///// <summary>
		///// Posts a message to the device and returns the result asynchronously.
		///// </summary>
		///// <param name="command"></param>
		///// <param name="args"></param>
		///// <returns></returns>
		//public static Task<DeviceResponse> PostMessageAsync(string command, params object[] args)
		//{
		//	var tcs = new TaskCompletionSource<DeviceResponse>();

		//	PostMessage(command, (response) => {
		//		// TODO: tcs.SetException
		//		tcs.SetResult(response);
		//	}, args);

		//	return tcs.Task;
		//}

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
