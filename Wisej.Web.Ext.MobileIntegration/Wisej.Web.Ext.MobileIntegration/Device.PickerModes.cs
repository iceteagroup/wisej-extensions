///////////////////////////////////////////////////////////////////////////////
//
// (C) 2020 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
	partial class Device
	{
		/// <summary>
		/// The mode displayed by the date picker.
		/// </summary>
		[ApiCategory("API")]
		public enum PickerModes
		{
			/// <summary>
			/// A mode that displays the date in hours, minutes, and (optionally) an AM/PM designation.
			/// </summary>
			Time,

			/// <summary>
			/// A mode that displays the date in months, days of the month, and years.
			/// </summary>
			Date,

			/// <summary>
			/// A mode that displays the date as unified day of the week, month, and day of the month values, plus hours, minutes, and (optionally) an AM/PM designation.
			/// </summary>
			DateAndTime,

			/// <summary>
			/// A mode that displays hour and minute values, for example [ 1 | 53 ].
			/// </summary>
			CountDownTimer
		}
	}
}
