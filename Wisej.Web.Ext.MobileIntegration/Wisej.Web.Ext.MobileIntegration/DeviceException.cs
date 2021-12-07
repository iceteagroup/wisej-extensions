///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using static Wisej.Web.Ext.MobileIntegration.DeviceResponse;

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Represents an error that occurs during device command execution.
	/// </summary>
	public class DeviceException : Exception
	{

		#region Properties

		/// <summary>
		/// The reason for the exception.
		/// </summary>
		public string Reason;

		/// <summary>
		/// The calling method.
		/// </summary>
		public string CallerName;

		/// <summary>
		/// The error status code.
		/// </summary>
		public StatusCode ErrorCode;

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new instance of <see cref="DeviceException"/> with the given configuration.
		/// </summary>
		/// <param name="reason"></param>
		/// <param name="callerName"></param>
		/// <param name="errorCode"></param>
		internal DeviceException(string reason, string callerName, StatusCode errorCode)
		{
			this.Reason = reason;
			this.ErrorCode = errorCode;
			this.CallerName = callerName;
		}

		#endregion

		#region Hidden Members

		/// <summary>
		/// Not applicable for this class.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string HelpLink;

		/// <summary>
		/// Not applicable for this class.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string Data;

		/// <summary>
		/// Not applicable for this class.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string HResult;

		/// <summary>
		/// Not applicable for this class.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string Message;

		#endregion

	}
}
