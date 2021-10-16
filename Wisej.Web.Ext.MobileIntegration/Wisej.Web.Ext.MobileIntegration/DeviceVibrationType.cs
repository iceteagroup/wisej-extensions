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
	/// <summary>
	/// Indicates the type of notification feedback to use on the device.
	/// </summary>
	[ApiCategory("API")]
	public enum  DeviceVibrationType
	{
		/// <summary>
		/// A notification feedback type, indicating that a task has failed.
		/// </summary>
		Error = 1,

		/// <summary>
		/// A notification feedback type, indicating that a task has completed successfully.
		/// </summary>
		Success = 2,

		/// <summary>
		/// A notification feedback type, indicating that a task has produced a warning.
		/// </summary>
		Warning = 3
	}
}
