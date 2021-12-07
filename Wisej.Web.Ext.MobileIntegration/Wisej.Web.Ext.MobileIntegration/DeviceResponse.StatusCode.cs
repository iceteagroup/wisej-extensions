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

namespace Wisej.Web.Ext.MobileIntegration
{
	public partial class DeviceResponse
	{
		/// <summary>
		/// Provides several
		/// </summary>
		public enum StatusCode
		{
			/// <summary>
			/// The resulting operation completed successfully.
			/// </summary>
			Success,

			/// <summary>
			/// The user cancelled the operation.
			/// </summary>
			UserCancelled,

			/// <summary>
			/// The operation resulted in an error.
			/// </summary>
			OperationError,
			
			/// <summary>
			/// The OS doesn't support this feature.
			/// </summary>
			UnsupportedOS,

			/// <summary>
			/// The hardware needed to run this command isn't available or responding.
			/// </summary>
			HardwareUnavailable,
		}
	}
}
