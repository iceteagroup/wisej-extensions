///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Linq;
using System.IO;
using Wisej.Core;
using System.ComponentModel;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Represents a period of business hours emphasized on the calendar.
	/// </summary>
	/// <remarks>
	/// Assign an array of <see cref="BusinessHours"/> to <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.BusinessHours"/>.
	/// A new instance covers 9 AM to 5 PM.
	/// </remarks>
	/// <example>
	/// Emphasizing Monday to Friday 8:30 AM - 5:30 PM:
	/// <code><![CDATA[
	/// this.fullCalendar1.BusinessHours = new[] {
	///     new BusinessHours {
	///         Start = new TimeSpan(8, 30, 0),
	///         End = new TimeSpan(17, 30, 0),
	///         Days = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }
	///     }
	/// };
	/// ]]></code>
	/// </example>
	[ApiCategory("FullCalendar")]
	public class BusinessHours : Wisej.Core.IWisejSerializable
	{
		/// <summary>
		/// Initializes a new instance of <see cref="BusinessHours"/> with <see cref="Start"/> set to 9 AM and <see cref="End"/> set to 5 PM.
		/// </summary>
		public BusinessHours()
		{
			this.Start = new TimeSpan(9, 0, 0);
			this.End = new TimeSpan(17, 0, 0);
		}

		/// <summary>
		/// Returns or sets the start time of the business hours period.
		/// </summary>
		/// <remarks>
		/// The value is a time of the day. The default is 09:00:00.
		/// </remarks>
		public TimeSpan Start
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the end time of the business hours period.
		/// </summary>
		/// <remarks>
		/// The value is a time of the day. The default is 17:00:00.
		/// </remarks>
		public TimeSpan End
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the days of the week for this business hours period.
		/// </summary>
		/// <example>
		/// Setting the business hours for the weekend:
		/// <code><![CDATA[
		/// var weekend = new BusinessHours
		/// {
		///     Start = new TimeSpan(10, 0, 0),
		///     End = new TimeSpan(14, 0, 0),
		///     Days = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }
		/// };
		/// ]]></code>
		/// </example>
		public DayOfWeek[] Days
		{
			get;
			set;
		}

		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string with the start and end times followed by the list of days, if any. i.e. "09:00:00 - 17:00:00 (Monday, Tuesday)".</returns>
		/// <example>
		/// Showing the business hours in a list box:
		/// <code><![CDATA[
		/// foreach (var hours in this.fullCalendar1.BusinessHours)
		///     this.listBox1.Items.Add(hours.ToString());
		/// ]]></code>
		/// </example>
		public override string ToString()
		{
			return String.Concat(
				this.Start.ToString(),
				" - ",
				this.End.ToString(),
				this.Days == null || this.Days.Length == 0
					? ""
					: " (" + String.Join(", ", this.Days.Select(d => d.ToString())) + ")"
			);
		}

		#region IWisejSerializable

		bool IWisejSerializable.Serialize(TextWriter writer, WisejSerializerOptions options)
		{
			writer.Write(
				WisejSerializer.Serialize(new
				{
					dow = this.Days?.Select(d => (int)d),
					start = this.Start.ToString(),
					end = this.End.ToString()
				})
			);

			return true;
		}

		#endregion
	}
}
