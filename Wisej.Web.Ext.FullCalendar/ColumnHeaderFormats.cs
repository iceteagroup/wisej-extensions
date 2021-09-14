///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Globalization;
using Wisej.Core;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Represents the column header formats for the different views.
	/// </summary>
	[TypeConverter(typeof(ColumnHeaderFormats.Converter))]
	[ApiCategory("FullCalendar")]
	public class ColumnHeaderFormats
	{
		private FullCalendar owner;

		internal ColumnHeaderFormats(FullCalendar owner)
		{
			this.owner = owner;
		}

		/// <summary>
		/// Determines the text that will be displayed on the day column headings in day view
		/// using momentjs format patterns: http://momentjs.com/docs/#/displaying/format/.
		/// </summary>
		[Description("Determines the text that will be displayed on the month column headings.")]
		public string DayViewFormat
		{
			get { return this._dayViewFormat ?? "Default"; }
			set
			{
				if (value == string.Empty)
					value = null;

				if (this._dayViewFormat != value)
				{
					this._dayViewFormat = value;
					this.owner?.Update();
				}
			}
		}
		private string _dayViewFormat = null;

		private bool ShouldSerializeDayViewFormat()
		{
			return this._dayViewFormat != null;
		}

		private void ResetDayViewFormat()
		{
			this.DayViewFormat = null;
		}

		/// <summary>
		/// Determines the text that will be displayed on the day column headings in week view
		/// using momentjs format patterns: http://momentjs.com/docs/#/displaying/format/.
		/// </summary>
		[Description("Determines the text that will be displayed on the month column headings.")]
		public string WeekViewFormat
		{
			get { return this._weekViewFormat ?? "Default"; }
			set
			{
				if (value == string.Empty)
					value = null;

				if (this._weekViewFormat != value)
				{
					this._weekViewFormat = value;
					this.owner?.Update();
				}
			}
		}
		private string _weekViewFormat = null;

		private bool ShouldSerializeWeekViewFormat()
		{
			return this._weekViewFormat != null;
		}

		private void ResetWeekViewFormat()
		{
			this.WeekViewFormat = null;
		}

		/// <summary>
		/// Determines the text that will be displayed on the month column headings
		/// using momentjs format patterns: http://momentjs.com/docs/#/displaying/format/.
		/// </summary>
		[Description("Determines the text that will be displayed on the month column headings.")]
		public string MonthViewFormat
		{
			get { return this._monthViewFormat ?? "Default"; }
			set {
				if (value == string.Empty)
					value = null;

				if (this._monthViewFormat != value)
				{
					this._monthViewFormat = value;
					this.owner?.Update();
				}
			}
		}
		private string _monthViewFormat = null;

		private bool ShouldSerializeMonthViewFormat()
		{
			return this._monthViewFormat != null;
		}

		private void ResetMonthViewFormat()
		{
			this.MonthViewFormat = null;
		}

		#region Wisej Implementation

		/// <summary>
		/// Renders the format settings.
		/// </summary>
		/// <param name="config"></param>
		internal void Render(dynamic config)
		{
			dynamic views = new DynamicObject();

			if (ShouldSerializeDayViewFormat())
			{
				views.day = new { columnFormat = this.DayViewFormat };
			}

			if (ShouldSerializeWeekViewFormat())
			{
				views.week = new { columnFormat = this.WeekViewFormat };
			}

			if (ShouldSerializeMonthViewFormat())
			{
				views.month = new { columnFormat = this.MonthViewFormat };
			}

			if (!views.IsEmpty)
				config.views = views;
		}

		#endregion

		#region Type Converter

		private class Converter : ExpandableObjectConverter
		{
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				var formats = value as ColumnHeaderFormats;

				return (formats != null && destinationType == typeof(string))
					? formats.DayViewFormat + "; " + formats.WeekViewFormat + "; " + formats.MonthViewFormat
					: base.ConvertTo(context, culture, value, destinationType);
			}
		}

		#endregion
	}
}
