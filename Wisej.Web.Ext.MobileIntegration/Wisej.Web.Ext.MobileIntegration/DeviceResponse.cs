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
	/// <summary>
	/// Represents a response from the native device.
	/// </summary>
	[ApiCategory("API")]
	public partial class DeviceResponse
	{

		#region Properties

		/// <summary>
		/// Represents the return value.
		/// </summary>
		public dynamic Value;

		/// <summary>
		/// Represents the error code, if applicable.
		/// </summary>
		public StatusCode Status;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance of <see cref="DeviceResponse"/>.
		/// </summary>
		public DeviceResponse() 
		{ 
		}

		/// <summary>
		/// Creates a new instance of <see cref="DeviceResponse"/> with the given arguments.
		/// </summary>
		/// <param name="value">The message value.</param>
		/// <param name="status">The message status.</param>
		public DeviceResponse(dynamic value, string status)
		{
			this.Value = value;
			this.Status = (StatusCode) Enum.Parse(typeof(StatusCode), status);
		}

		#endregion

	}
}
