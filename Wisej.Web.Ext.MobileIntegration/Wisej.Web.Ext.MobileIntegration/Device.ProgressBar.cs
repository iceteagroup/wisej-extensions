///////////////////////////////////////////////////////////////////////////////
//
// (C) 2022 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
	public sealed partial class Device
	{
		/// <summary>
		/// The application's progressbar.
		/// </summary>
		public class ProgressBar
		{
			/// <summary>
			/// Applies the given value to the device's progressbar.
			/// </summary>
			/// <param name="value"></param>
			/// <param name="animated"></param>
			public static void SetProgress(float value, bool animated=true)
			{
				PostMessage("progress.value", value, animated);
			}

			/// <summary>
			/// Shows or hides the progressbar.
			/// </summary>
			/// <param name="visible"></param>
			public static void SetVisible(bool visible)
			{
				PostMessage("progress.visible", visible);
			}
		}
	}
}
