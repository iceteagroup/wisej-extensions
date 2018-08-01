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
using System.Collections.Generic;

namespace Wisej.Web.Ext.GoogleMaps
{
	/// <summary>
	/// Represents a single geocoding result.
	/// </summary>
	/// <remarks>A geocode request may return multiple result objects.</remarks>
	public class GeocoderResult
	{
		internal GeocoderResult(string errorCode)
		{
			ResultCode = errorCode;
		}

		internal GeocoderResult(dynamic data)
		{
			Types = data.types;
			FormattedAddress = data.formatted_address;

			var addressComponents = new List<AddressComponent>();
			foreach (var addressComponent in data.address_components)
			{
				addressComponents.Add(new AddressComponent(addressComponent));
			}

			AddressComponents = addressComponents.ToArray();

			PartialMatch = data.partial_match != null && data.partial_match;
			PlaceId = data.place_idis;
			PostcodeLocalities = data.postcode_localities;

			GeocodeGeometry = new Geometry(data.geometry);
		}


		/// <summary>
		/// Gets a value indicating whether this instance is error.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
		/// </value>
		public bool IsError
		{
			get { return !string.IsNullOrWhiteSpace(ResultCode); }
		}


		/// <summary>
		/// Gets the result code.
		/// </summary>
		/// <value>
		/// The result code.
		/// </value>
		public string ResultCode { get; internal set; }

		/// <summary>
		/// Gets an array indicating the address type of the returned result.
		/// </summary>
		/// <value>
		/// The array indicating the address type of the returned result.
		/// </value>
		/// <remarks>This array contains a set of zero or more tags identifying the type of feature
		/// returned in the result.</remarks>
		public string[] Types { get; private set; }

		/// <summary>
		/// Gets the human-readable address of this location.
		/// </summary>
		/// <value>
		/// The human-readable address of this location.
		/// </value>
		public string FormattedAddress { get; private set; }

		/// <summary>
		/// Gets an array of separate components applicable to this address.
		/// </summary>
		/// <value>
		/// The array of separate components applicable to this address.
		/// </value>
		public AddressComponent[] AddressComponents { get; private set; }

		/// <summary>
		/// Gets a value indicating that the geocoder did not return an exact match for the original request,
		/// though it was able to match part of the requested address.
		/// </summary>
		/// <value>
		///   <c>true</c> if the geocoder did not return an exact match; otherwise, <c>false</c>.
		/// </value>
		public bool PartialMatch { get; private set; }

		/// <summary>
		/// Gets the unique identifier of a place, which can be used with other Google APIs.
		/// </summary>
		/// <value>
		/// The unique identifier of a place.
		/// </value>
		public string PlaceId { get; private set; }

		/// <summary>
		/// Gets an array denoting all the localities contained in a postal code.
		/// </summary>
		/// <value>
		/// The array denoting all the localities contained in a postal code.
		/// </value>
		/// <remarks>Is only present when the result is a postal code that contains multiple localities.
		/// This array can contain up to 10 localities</remarks>
		public string[] PostcodeLocalities { get; private set; }

		/// <summary>
		/// Gets the geocode geometry information.
		/// </summary>
		/// <value>
		/// The geocode geometry information.
		/// </value>
		public Geometry GeocodeGeometry { get; private set; }

		/// <summary>
		/// Indicates the separate components applicable to an address.
		/// </summary>
		public class AddressComponent
		{
			internal AddressComponent(dynamic data)
			{
				ShortName = data.short_name;
				LongName = data.long_name;
				PostcodeLocalities = data.postcode_localities;
				Types = data.types;
			}

			/// <summary>
			/// Gets the abbreviated textual name for the address component, if available.
			/// </summary>
			/// <value>
			/// The abbreviated textual name for the address component, if available.
			/// </value>
			public string ShortName { get; private set; }

			/// <summary>
			/// Gets the full text description or name of the address component as returned by the Geocoder.
			/// </summary>
			/// <value>
			/// The full text description or name of the address component as returned by the Geocoder.
			/// </value>
			public string LongName { get; private set; }

			/// <summary>
			/// Gets an array denoting all the localities contained in a postal code.
			/// </summary>
			/// <value>
			/// The array denoting all the localities contained in a postal codes.
			/// </value>
			/// <remarks>Is only present when the result is a postal code that contains multiple localities.
			/// This array can contain up to 10 localities.</remarks>
			public string[] PostcodeLocalities { get; private set; }

			/// <summary>
			/// Gets an array indicating the type of the address component.
			/// </summary>
			/// <value>
			/// The array indicating the type of the address component.
			/// </value>
			public string[] Types { get; private set; }
		}

		/// <summary>
		/// Contains geocode geometry information.
		/// </summary>
		public class Geometry
		{
			internal Geometry(dynamic data)
			{
				Location = new LatLng(data.lat, data.lng);
				LocationType = Parse(data.location_type);
			}

			/// <summary>
			/// Gets the geocoded latitude,longitude value.
			/// </summary>
			/// <value>
			/// The geocoded latitude,longitude value.
			/// </value>
			public LatLng Location { get; private set; }

			/// <summary>
			/// Gets additional data about the specified location.
			/// </summary>
			/// <value>
			/// The additional data about the specified location.
			/// </value>
			public GeocoderLocationType? LocationType { get; private set; }

			internal GeocoderLocationType? Parse(string locationType)
			{
				var parsing = locationType.ToLowerInvariant();
				var parts = parsing.Split('_');
				parsing = string.Empty;
				foreach (var part in parts)
				{
					parsing += char.ToUpperInvariant(part[0]) + part.Substring(1);
				}

				GeocoderLocationType result;

				if (Enum.TryParse(parsing, out result))
					return result;

				return null;
			}
		}
	}
}