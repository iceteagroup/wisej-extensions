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

namespace Wisej.Web.Ext.Camera
{
	/// <summary>
	/// Represents the method that will handle the <see cref="Camera.Error"/> event.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="CameraErrorEventArgs" /> that contains the event data. </param>
	public delegate void CameraErrorHandler(object sender, CameraErrorEventArgs e);

    /// <summary>
    /// Represents the data for the <see cref="Camera.Error"/> event of the <see cref="Camera"/> control.
    /// This class contains information about errors that occur during camera operations.
    /// </summary>
	/// <remarks>
	/// The <see cref="Camera.Error"/> event is triggered when an error occurs, providing the necessary details
	/// to handle the situation appropriately. This can include instances of hardware failure, permission issues,
	/// or other operational errors that prevent the camera from functioning correctly.
	/// </remarks>
    [ApiCategory("Camera")]
	public class CameraErrorEventArgs : EventArgs
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraErrorEventArgs"/> class
        /// with the message describing the error reported by the client.
        /// </summary>
        /// <param name="message">
        /// The error message returned by the browser's media API, for example
        /// <c>"Permission denied"</c>, <c>"Requested device not found"</c>, or
        /// <c>"Could not start video source"</c>. The exact wording is browser-specific
        /// and is exposed through the <see cref="Message"/> property.
        /// </param>
        /// <remarks>
        /// This constructor is called by the <see cref="Camera"/> control when the client reports a failure in the
        /// camera setup or usage; applications normally receive the instance through the <see cref="Camera.Error"/>
        /// event rather than creating one. No validation is performed on <paramref name="message"/>, so
        /// <see cref="Message"/> can be <see langword="null"/> if the browser supplied no description.
        /// </remarks>
        /// <seealso cref="Camera.Error"/>
        public CameraErrorEventArgs(string message)
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
