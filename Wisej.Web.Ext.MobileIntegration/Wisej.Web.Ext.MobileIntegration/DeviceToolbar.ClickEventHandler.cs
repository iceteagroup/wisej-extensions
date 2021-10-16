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

namespace Wisej.Web.Ext.MobileIntegration
{
	public partial class DeviceToolbar
	{
		/// <summary>
		/// Represents the methods that handle the <see cref="DeviceToolbar.Click"/> event.
		/// </summary>
		/// <param name="sender">Instance of <see cref="DeviceToolbar"/>.</param>
		/// <param name="e">A <see cref="ClickEventArgs"/> containing the event's data.</param>
		[ApiCategory("API")]
		public delegate void ClickEventHandler(object sender, ClickEventArgs e);

		/// <summary>
		/// Represents the event data sent with the <see cref="DeviceToolbar.Click"/> event.
		/// </summary>
		[ApiCategory("API")]
		public class ClickEventArgs : EventArgs
		{
			/// <summary>
			/// Initializes a new instance of <see cref="ClickEventArgs"/>.
			/// </summary>
			/// <param name="button">The selected <see cref="DeviceToolbar.Button"/>.</param>
			public ClickEventArgs(DeviceToolbar.Button button)
			{
				if (button == null)
					throw new ArgumentNullException(nameof(button));

				this.Button = button;
			}

			/// <summary>
			/// Returns the clicked button.
			/// </summary>
			public DeviceToolbar.Button Button
			{
				get;
				private set;
			}
		}
	}
}
