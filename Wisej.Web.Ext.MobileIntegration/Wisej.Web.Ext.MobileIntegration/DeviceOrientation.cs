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

using System.ComponentModel;

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Indicates the physical orientation of the device.
	/// </summary>
	[ApiCategory("API")]
	public enum DeviceOrientation
	{
		/// <summary>
		/// The orientation of the device cannot be determined.
		/// </summary>
		Unknown,

		/// <summary>
		/// The device is in portrait mode, with the device held upright and the home button at the bottom.
		/// </summary>
		Portrait,

		/// <summary>
		/// The device is in portrait mode but upside down, with the device held upright and the home button at the top.
		/// </summary>
		PortraitUpSideDown,

		/// <summary>
		/// The device is in landscape mode, with the device held upright and the home button on the right side.
		/// </summary>
		LandscapeLeft,

		/// <summary>
		/// The device is in landscape mode, with the device held upright and the home button on the left side.
		/// </summary>
		LandscapeRight,

		/// <summary>
		/// The device is held parallel to the ground with the screen facing upwards.
		/// </summary>
		FaceUp,

		/// <summary>
		/// The device is held parallel to the ground with the screen facing downwards.
		/// </summary>
		FaceDown
	}
}
