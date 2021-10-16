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

namespace Wisej.Web.Ext.MobileIntegration
{
	public sealed partial class Device
	{
		/// <summary>
		/// Provides functionality for displaying popups to the user.
		/// </summary>
		[ApiCategory("API")]
		public class Popups
		{
			/// <summary>
			/// Displays a native alert box with the specified configuration.
			/// </summary>
			/// <param name="title">The title of the alert.</param>
			/// <param name="message">Message to display to the user.</param>
			/// <param name="options">The alert buttons.</param>
			/// <returns>The result of the alert.</returns>
			public static string Alert(string title, string message, params DevicePopupOption[] options)
			{
				return PostModalMessage("action.alert", title, message, options).Value;
			}

			/// <summary>
			/// Displays a native prompt with a set of options for the user to click.
			/// </summary>
			/// <param name="title">The title of the prompt.</param>
			/// <param name="message">Message to display to the user.</param>
			/// <param name="options">Options to show to the user. If null, it will display the default OK button.</param>
			/// <returns>The result of the prompt.</returns>
			public static string Prompt(string title, string message, params DevicePopupOption[] options)
			{
				return PostModalMessage("action.prompt", title, message, options).Value;
			}

			/// <summary>
			/// Shows a picker with the specified configuration.
			/// </summary>
			/// <param name="mode">The picker style.</param>
			/// <param name="minimumDateTime">The minimum date or time to show on the picker.</param>
			/// <param name="maximumDateTime">The maximum date or time to show on the picker.</param>
			/// <param name="startDateTime">The starting date or time for the picker.</param>
			/// <returns>The result of the picker operation or null if an error occurred.</returns>
			public static DateTime? ShowPicker(PickerModes mode, DateTime minimumDateTime, DateTime maximumDateTime, DateTime startDateTime)
			{
				var result = PostModalMessage("picker.show", new
				{
					mode = (int)mode,
					minimumDateTime,
					maximumDateTime,
					startDateTime
				});
				var success = result.ErrorCode == 0;
				if (!success)
					throw new Exception(result.Value);

				return Convert.ToDateTime(result.Value);
			}
		}
	}
}
