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

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Represents a response from the NotificationManager.
	/// </summary>
	public class NotificationResponse
	{
		/// <summary>
		/// The success status.
		/// </summary>
		public bool Success
		{
			get;
			internal set;
		}

		/// <summary>
		/// The timestamp.
		/// </summary>
		public DateTime TimeStamp
		{
			get;
			internal set;
		}

		/// <summary>
		/// The response reason.
		/// </summary>
		public string Reason
		{
			get;
			internal set;
		}

		/// <summary>
		/// Creates a new instance of <see cref="NotificationResponse"/> with the given config.
		/// </summary>
		/// <param name="success"></param>
		/// <param name="reasonString"></param>
		/// <param name="dateTime"></param>
		public NotificationResponse(bool success, string reasonString, DateTime dateTime)
		{
			this.Success = success;
			this.TimeStamp = dateTime;
			this.Reason = reasonString;
		}

		/// <summary>
		/// Generates a string representation of the current instance.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return JSON.Stringify(this);
		}
	}
}
