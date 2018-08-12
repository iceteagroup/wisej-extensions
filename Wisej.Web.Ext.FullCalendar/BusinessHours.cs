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

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Emphasizes certain time slots on the calendar. By default, Monday-Friday, 9am-5pm.
	/// </summary>
	public class BusinessHours : Wisej.Core.IWisejSerializable
	{
		/// <summary>
		/// Initializes an new instance of <see cref="BusinessHours"/> 
		/// </summary>
		public BusinessHours()
		{
			this.Start = new TimeSpan(9, 0, 0);
			this.End = new TimeSpan(17, 0, 0);
		}

		/// <summary>
		/// Indicates the start time for the business hours period.
		/// </summary>
		public TimeSpan Start
		{
			get;
			set;
		}

		/// <summary>
		/// Indicates the end time for the business hours period.
		/// </summary>
		public TimeSpan End
		{
			get;
			set;
		}

		/// <summary>
		/// Indicates the days of the week for this business hours period.
		/// </summary>
		public DayOfWeek[] Days
		{
			get;
			set;
		}

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
