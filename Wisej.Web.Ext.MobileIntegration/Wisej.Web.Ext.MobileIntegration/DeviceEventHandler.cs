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
using Wisej.Core;

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Represents the method that will handle the <see cref="Device.Event"/> event.
	/// </summary>
	/// <param name="sender">The <see cref="Device"/> instance created for the current session.</param>
	/// <param name="e">A <see cref="DeviceEventArgs"/> that contains the event data.</param>
	[ApiCategory("API")]
	public delegate void DeviceEventHandler(object sender, DeviceEventArgs e);

	/// <summary>
	/// Provides data to the <see cref="Device.Event"/> event.
	/// </summary>
	[ApiCategory("API")]
	public class DeviceEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of <see cref="DeviceEventArgs"/>.
		/// </summary>
		/// <param name="args">Event args received from the client.</param>
		public DeviceEventArgs(WisejEventArgs args)
		{
			if (args == null)
				throw new ArgumentNullException(nameof(args));

			dynamic data = args.Parameters.Data;

			this.Data = data.data;
			this.Type = data.type ?? ""; ;
			this.Target = data.target as string;
			this.Orientation = Device.Info.Orientation;
		}

		/// <summary>
		/// Event type.
		/// </summary>
		public string Type
		{
			get;
			private set;
		}

		/// <summary>
		/// Name of the native component that triggered the event.
		/// </summary>
		public string Target
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the additional data received with the event.
		/// </summary>
		public dynamic Data
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the current device orientation.
		/// </summary>
		public DeviceOrientation Orientation
		{
			get;
			private set;
		}
	}

}
