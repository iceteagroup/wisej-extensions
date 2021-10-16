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
using System.Collections.Specialized;
using System.Drawing;
using System.IO;

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Represent the information related to the user mobile device.
	/// </summary>
	[ApiCategory("API")]
	public sealed class DeviceInfo
	{
		/// <summary>
		/// Initializes an instance of <see cref="DeviceInfo"/> using the
		/// URL arguments in <paramref name="queryString"/> to initialize its properties.
		/// </summary>
		/// <param name="queryString">Parameters received from the native app.</param>
		internal DeviceInfo(NameValueCollection queryString)
		{
			if (queryString == null)
				throw new ArgumentNullException(nameof(queryString));

			ReadDeviceProperties(queryString["info"]);
		}

		/// <summary>
		/// Converts the base64 representation of device properties into a <see cref="DeviceInfo"/> object.
		/// </summary>
		/// <param name="base64">The base64-encoded JSON values.</param>
		private void ReadDeviceProperties(string base64)
		{
			if (String.IsNullOrEmpty(base64))
				return;

			using (var stream = new MemoryStream(Convert.FromBase64String(base64)))
			{
				var info = JSON.Parse(stream);

				this.ID = info.id ?? "";
				this.Name = info.name ?? "";
				this.Model = info.model ?? "";
				this.VendorID = info.vendorId ?? "";
				this.SystemName = info.systemName ?? "";
				this.DeviceToken = info.deviceToken ?? "";
				this.SystemVersion = info.systemVersion ?? "";
				this.StyleMode = (Device.StyleModes)(info.styleMode ?? Device.StyleModes.Unspecified);
				this.ScreenSize = new Size(info.screenWidth ?? 0, info.screenHeight ?? 0);
				this.AuthenticationType = (DeviceAuthenticationType)(info.authenticationType ?? DeviceAuthenticationType.None);

				UpdateOrientation(info.orientation);
			}
		}

		/// <summary>
		/// Determines the orientation of the device given the enumerated value.
		/// </summary>
		/// <param name="value">The enumerated value of the device's orientation.</param>
		internal void UpdateOrientation(int value)
		{
			this.Orientation = (DeviceOrientation)value;

			if (this.Orientation != DeviceOrientation.FaceUp && this.Orientation != DeviceOrientation.FaceDown) { 

				switch (this.Orientation)
				{
					default:
					case DeviceOrientation.Unknown:
					case DeviceOrientation.Portrait:
					case DeviceOrientation.PortraitUpSideDown:
						this.IsPortrait = true;
						this.IsLandscape = false;
						this.IsFlat = false;
						break;

					case DeviceOrientation.LandscapeLeft:
					case DeviceOrientation.LandscapeRight:
						this.IsLandscape = true;
						this.IsPortrait = false;
						this.IsFlat = false;
						break;
				}
			}

			switch (this.Orientation)
			{
				case DeviceOrientation.FaceUp:
				case DeviceOrientation.FaceDown:
					this.IsFlat = true;
					break;
			}
		}

		/// <summary>
		/// Updates the style mode property of the device (dark, light, etc).
		/// </summary>
		/// <param name="mode">The enumerated value of the device's style mode.</param>
		internal void UpdateStyleMode(int mode)
		{
			this.StyleMode = (Device.StyleModes)mode;
		}

		/// <summary>
		/// Unique device ID.
		/// </summary>
		public string ID
		{
			get;
			internal set;
		}

		/// <summary>
		/// The name identifying the device.
		/// </summary>
		public string Name
		{
			get;
			internal set;
		}

		/// <summary>
		/// The name of the operating system running on the device.
		/// </summary>
		public string SystemName
		{
			get;
			internal set;
		}

		/// <summary>
		/// The current version of the operating system.
		/// </summary>
		public string SystemVersion
		{
			get;
			internal set;
		}

		/// <summary>
		/// The model of the device.
		/// </summary>
		public string Model
		{
			get;
			internal set;
		}

		/// <summary>
		/// An alphanumeric string that uniquely identifies a device for remote notifications.
		/// </summary>
		public string DeviceToken
		{
			get;
			internal set;
		}

		/// <summary>
		/// Updates the token used for remote notifications.
		/// </summary>
		/// <param name="token">The string value of the token</param>
		internal void UpdateDeviceToken(string token) 
		{
			this.DeviceToken = token;
		}

		/// <summary>
		/// An alphanumeric string that uniquely identifies a device to the app’s vendor.
		/// </summary>
		public string VendorID
		{
			get;
			internal set;
		}

		/// <summary>
		/// The physical orientation of the device.
		/// </summary>
		public DeviceOrientation Orientation
		{
			get;
			internal set;
		}

		/// <summary>
		/// The type of authentication supported by the device.
		/// </summary>
		public DeviceAuthenticationType AuthenticationType
		{
			get;
			internal set;
		}

		/// <summary>
		/// Returns a Boolean value indicating whether the device is in a portrait orientation.
		/// </summary>
		public bool IsPortrait
		{
			get;
			internal set;
		}

		/// <summary>
		/// Returns a Boolean value indicating whether the device is in a landscape orientation.
		/// </summary>
		public bool IsLandscape
		{
			get;
			internal set;
		}

		/// <summary>
		/// A Boolean value indicating whether the specified orientation is face up or face down.
		/// </summary>
		public bool IsFlat
		{
			get;
			internal set;
		}

		/// <summary>
		/// A value indicating which mode the device is in (Dark mode, light mode, etc.).
		/// </summary>
		public Device.StyleModes StyleMode
		{
			get;
			internal set;
		}

		/// <summary>
		/// Returns the size of the screens in pixels.
		/// </summary>
		public Size ScreenSize
		{
			get { return this._size; }
			internal set { this._size = value; }
		}
		private Size _size;
	}
}
