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
	public partial class DeviceToolbar
	{
		/// <summary>
		/// Represents a button in the <see cref="Device.Toolbar"/>.
		/// </summary>
		[ApiCategory("API")]
		public enum ButtonType
		{
			/// <summary>
			/// Default button. Shows either an icon or test.
			/// </summary>
			Default = -1,

			/// <summary>
			/// The system Done button, localized.
			/// </summary>
			Done,

			/// <summary>
			/// The system Cancel button, localized.
			/// </summary>
			Cancel,

			/// <summary>
			/// The system Edit button, localized.
			/// </summary>
			Edit,

			/// <summary>
			/// The system Save button, localized.
			/// </summary>
			Save,

			/// <summary>
			/// The system plus button containing an icon of a plus sign.
			/// </summary>
			Add,

			/// <summary>
			/// Blank space to add between other items. The space is distributed equally between the other items. Other item properties are ignored when this value is set.
			/// </summary>
			FlexibleSpace,

			/// <summary>
			/// Blank space to add between other items. Only the
			/// width property is used when this value is set.
			/// </summary>
			FixedSpace,
			
			/// <summary>
			/// The system compose button.
			/// </summary>
			Compose,

			/// <summary>
			/// The system reply button.
			/// </summary>
			Reply,

			/// <summary>
			/// The system action button.
			/// </summary>
			Action,

			/// <summary>
			/// The system organize button.
			/// </summary>
			Organize,

			/// <summary>
			/// The system bookmarks button.
			/// </summary>
			Bookmarks,

			/// <summary>
			/// The system search button.
			/// </summary>
			Search,

			/// <summary>
			/// The system refresh button.
			/// </summary>
			Refresh,

			/// <summary>
			/// The system stop button.
			/// </summary>
			Stop,

			/// <summary>
			/// The system camera button.
			/// </summary>
			Camera,

			/// <summary>
			/// The system trash button.
			/// </summary>
			Trash,

			/// <summary>
			/// The system play button.
			/// </summary>
			Play,

			/// <summary>
			/// The system pause button.
			/// </summary>
			Pause,

			/// <summary>
			/// The system rewind button.
			/// </summary>
			Rewind,

			/// <summary>
			/// The system fast forward button.
			/// </summary>
			FastForward,

			/// <summary>
			/// The system undo button.
			/// </summary>
			Undo,

			/// <summary>
			/// The system redo button.
			/// </summary>
			Redo
		}
	}
}
