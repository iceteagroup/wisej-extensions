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
	public partial class DeviceScreen
	{
		/// <summary>
		/// Represents a possible screen orientation.
		/// </summary>
		[ApiCategory("API")]
		public enum OrientationType
		{
			/// <summary>
			/// The device is in portrait mode, with the device held upright and the home button at the bottom.
			/// </summary>
			Portrait = 2,

			/// <summary>
			/// The device is in landscape mode, with the device held upright and the home button on the right side.
			/// </summary>
			LandscapeLeft = 16,

			/// <summary>
			/// The device is in landscape mode, with the device held upright and the home button on the left side.
			/// </summary>
			LandcapeRight = 8,

			/// <summary>
			/// The device is in portrait mode but upside down, with the device held upright and the home button at the top.
			/// </summary>
			PortraitUpsideDown = 4,

			/// <summary>
			/// The device is in landscape mode.
			/// </summary>
			Landscape = 24,

			/// <summary>
			/// The device supports all interface orientations.
			/// </summary>
			All = 30,

			/// <summary>
			/// The device supports all but the upside-down portrait interface orientation.
			/// </summary>
			AllButUpsideDown = 26
		}
	}
}
