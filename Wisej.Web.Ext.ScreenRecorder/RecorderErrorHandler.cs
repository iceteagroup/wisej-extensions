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

namespace Wisej.Web.Ext.Camera
{
	/// <summary>
	/// Represents the method that will handle the <see cref="ScreenRecorder.Error"/> event.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="RecorderErrorEventArgs" /> that contains the event data. </param>
	public delegate void RecorderErrorHandler(object sender, RecorderErrorEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="Camera.Error"/> event of the <see cref="Camera"/> control.
	/// </summary>
	public class RecorderErrorEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RecorderErrorEventArgs"/> class.
		/// </summary>
		/// <param name="message"></param>
		public RecorderErrorEventArgs(string message)
		{
			this.Message = message;
		}

		/// <summary>
		/// Returns the error message.
		/// </summary>
		public string Message
		{
			get;
			private set;
		}
	}
}
