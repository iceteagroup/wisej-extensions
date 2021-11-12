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

using System.ComponentModel;

namespace Wisej.Web.Ext.MobileIntegration
{
	public partial class Device
	{
		/// <summary>
		/// Available device permissions.
		/// </summary>
		[ApiCategory("API")]
		public enum PermissionType 
		{
			/// <summary>
			/// Requests access to the device's camera.
			/// </summary>
			Camera = 0,

			/// <summary>
			/// Requests access to the device's photos.
			/// </summary>
			Photos = 1,

			/// <summary>
			/// Requests access to the device's location.
			/// </summary>
			Location = 2,

			/// <summary>
			/// Requests access to the device's microphone.
			/// </summary>
			Microphone = 3,

			/// <summary>
			/// Requests permission to send the device notifications.
			/// </summary>
			Notifications = 4,
		}
	}
}
