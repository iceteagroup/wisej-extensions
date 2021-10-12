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

namespace Wisej.Ext.Geolocation
{
	/// <summary>
	///  Represents the status of the Position object.
	/// </summary>
	[ApiCategory("Geolocation")]
	public enum StatusCode
	{
		/// <summary>
		/// The position was retrieved successfully.
		/// </summary>
		[Description("The position was retrieved successfully")]
		Success,

		/// <summary>
		/// The acquisition of the geolocation information failed because the page didn't have the permission to do it.
		/// </summary>
		[Description("The acquisition of the geolocation information failed because the page didn't have the permission to do it.")]
		PermissionDenied,

		/// <summary>
		/// The acquisition of the geolocation failed because at least one internal source of position returned an internal error.
		/// </summary>
		[Description("The acquisition of the geolocation failed because at least one internal source of position returned an internal error.")]
		PositionUnavailable,

		/// <summary>
		/// The time allowed to acquire the geolocation, defined by the Timeout information was reached before the information was obtained.
		/// </summary>
		[Description("The time allowed to acquire the geolocation, defined by the Timeout information was reached before the information was obtained.")]
		Timeout,

		/// <summary>
		/// The response from the client was received in an invalid format.
		/// </summary>
		[Description("The response from the client was received in an invalid format.")]
		InvalidResponse,
	}

	/// <summary>
	///  Represents the position of the concerned device at a given time.
	///  The position, represented by a Coordinates object, comprehends the 2D position of the 
	///  device, on a spheroid representing the Earth, but also its altitude and its speed.
	/// </summary>
	public class Position
	{
		// base ticks to convert milliseconds to local time.
		private static long minTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;

		/// <summary>
		/// Creates a new instance of the <see cref="T:Wisej.Ext.Getlocation.Position"/> class.
		/// </summary>
		/// <param name="data">The position information from the client.</param>
		internal Position(dynamic data)
		{
			this.Status = StatusCode.InvalidResponse;
			if (data == null)
				return;

			int error = data.errorCode ?? 4 /* InvalidResponse */;
			this.Status = (StatusCode)error;
			this.ErrorMessage = data.errorMessage;

			dynamic position = data.position;
			if (position == null)
				return;
			
			long milliseconds = position.timestamp;
			this.TimeStamp = new DateTime(minTicks, DateTimeKind.Utc).AddMilliseconds(milliseconds);
			if (this.Status != StatusCode.Success)
				return;

			dynamic coords = position.coords ?? null;
			if (coords == null)
			{
				this.Status = StatusCode.InvalidResponse;
				return;
			}

			this.Speed = coords.Speed ?? double.NaN;
			this.Heading = coords.Heading ?? double.NaN;
			this.Altitude = coords.altitude ?? double.NaN;
			this.Accuracy = coords.accuracy ?? double.NaN;
			this.Latitude = coords.latitude ?? double.NaN;
			this.Longitude = coords.longitude ?? double.NaN;
			this.AltitudeAccuracy = coords.altitudeAccuracy ?? double.NaN;
		}

		/// <summary>
		/// Returns the time at which the position was retrieved.
		/// </summary>
		public DateTime TimeStamp { get; internal set; }

		/// <summary>
		/// Returns the status of the position object.
		/// </summary>
		public StatusCode Status { get; internal set; }

		/// <summary>
		/// Returns the error message returned by the client or null.
		/// </summary>
		public string ErrorMessage { get; internal set; }

		/// <summary>
		/// Returns a double representing the position's latitude in decimal degrees.
		/// </summary>
		public double Latitude{get; internal set;}

		/// <summary>
		/// Returns a double representing the position's longitude in decimal degrees.
		/// </summary>
		public double Longitude { get; internal set; }

		/// <summary>
		/// Returns a double representing the position's altitude in meters, relative to sea level.
		/// </summary>
		/// <remarks>
		/// This value can be NaN if the implementation cannot provide the data.
		/// </remarks>
		public double Altitude { get; internal set; }

		/// <summary>
		/// Returns a double representing the accuracy of the latitude and longitude properties, expressed in meters.
		/// </summary>
		public double Accuracy { get; internal set; }

		/// <summary>
		/// Returns a double representing the accuracy of the altitude expressed in meters. This value can be NaN.
		/// </summary>
		public double AltitudeAccuracy { get; internal set; }

		/// <summary>
		/// Returns a double representing the direction in which the device is traveling. 
		/// </summary>
		/// <remarks>
		/// This value, specified in degrees, indicates how far off from heading true north the device is. 
		/// 0 degrees represents true north, and the direction is determined 
		/// clockwise (which means that east is 90 degrees and west is 270 degrees). 
		/// If speed is 0, heading is NaN. If the device is unable to provide heading information, this value is NaN.
		/// </remarks>
		public double Heading { get; internal set; }

		/// <summary>
		/// Returns a double representing the velocity of the device in meters per second. This value can be NaN.
		/// </summary>
		public double Speed { get; internal set; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Concat(
				"Status=", this.Status.ToString(),
				"; Message=", this.ErrorMessage ?? "",
				"; Latitude=", this.Latitude,
				"; Longitude=" , this.Longitude,
				"; Altitude=", this.Altitude,
				"; Speed=", this.Speed);
		}
	}
}
