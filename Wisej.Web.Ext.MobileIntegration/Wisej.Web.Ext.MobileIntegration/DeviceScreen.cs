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
	/// Represents the screen of the mobile device.
	/// </summary>
	[ApiCategory("API")]
	public partial class DeviceScreen
	{
		/// <summary>
		/// Initializes a new instance of <see cref="DeviceScreen"/> using the
		/// incoming arguments from the native application to initialize its values.
		/// </summary>
		/// <param name = "queryString" > Parameters received from the native app.</param>
		internal DeviceScreen(NameValueCollection queryString)
		{
			if (queryString == null)
				throw new ArgumentNullException(nameof(queryString));

			ReadDeviceProperties(queryString["info"]);
		}

		/// <summary>
		/// Converts the base64 representation of device properties into a <see cref="DeviceScreen"/> object.
		/// </summary>
		/// <param name="base64">The base64-encoded screen information.</param>
		private void ReadDeviceProperties(string base64)
		{
			if (String.IsNullOrEmpty(base64))
				return;

			using (var stream = new MemoryStream(Convert.FromBase64String(base64)))
			{
				var info = JSON.Parse(stream);

				this._idleTimerDisabled = info.idleTimerDisabled ?? false;
				this._brightness = (int)(100 * (double)(info.brightness ?? 0D));
				this._size = new Size(info.screenWidth ?? 0, info.screenHeight ?? 0);
				this._orientation = (OrientationType)(info.orientationType ?? OrientationType.All);
			}
		}

		/// <summary>
		/// Updates the brightness of the device based on a 0-100 scale.
		/// </summary>
		/// <param name="value"></param>
		internal void UpdateBrightness(double value)
		{
			this._brightness = (int)(100 * value);
		}

		/// <summary>
		/// Returns or sets whether the idle timer locks the device.
		/// </summary>
		public bool IdleTimerDisabled
		{
			get { return this._idleTimerDisabled; }
			set
			{
				if (this._idleTimerDisabled != value)
				{
					this._idleTimerDisabled = value;
					Device.PostMessage("screen.idletimerdisabled", this._idleTimerDisabled);
				}
			}
		}
		private bool _idleTimerDisabled;

		/// <summary>
		/// Returns the size of the screens in pixels.
		/// </summary>
		public Size Size
		{
			get { return this._size; }
			internal set { this._size = value; }
		}
		private Size _size;

		/// <summary>
		/// Returns or sets the brightness of the screen as a percentage.
		/// </summary>
		public int Brightness
		{
			get { return this._brightness; }
			set
			{
				if (this._brightness != value)
				{
					this._brightness = value;
					Device.PostMessage("screen.brightness", this._brightness / 100F);
				}
			}
		}
		private int _brightness;

		/// <summary>
		/// Returns or sets the orientation types allowed for the device.
		/// </summary>
		public OrientationType Orientation
		{
			get { return this._orientation; }
			set {
				if (this._orientation != value)
				{
					this._orientation = value;
					Device.PostMessage("screen.rotation", (int)this._orientation);
				}
			}
		}
		private OrientationType _orientation;
	}
}
