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
using System.Drawing;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wisej.Web.Ext.ChartJS4.Serialization
{
	/// <summary>
	/// JSON converter for <see cref="Color"/> objects.
	/// Converts colors to RGBA format for Chart.js.
	/// </summary>
	public class ColorJsonConverter : JsonConverter<Color>
	{
		/// <summary>
		/// Reads a Color from JSON.
		/// </summary>
		public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = reader.GetString();
			if (string.IsNullOrEmpty(value))
				return Color.Empty;

			// Parse RGBA format: rgba(r, g, b, a)
			if (value.StartsWith("rgba("))
			{
				var parts = value.Substring(5, value.Length - 6).Split(',');
				if (parts.Length == 4)
				{
					int r = int.Parse(parts[0].Trim());
					int g = int.Parse(parts[1].Trim());
					int b = int.Parse(parts[2].Trim());
					double a = double.Parse(parts[3].Trim());
					return Color.FromArgb((int)(a * 255), r, g, b);
				}
			}

			return ColorTranslator.FromHtml(value);
		}

		/// <summary>
		/// Writes a Color to JSON in RGBA format.
		/// </summary>
		public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
		{
			if (value == Color.Empty || value.A == 0)
			{
				writer.WriteNullValue();
				return;
			}

			// Write as rgba(r, g, b, a) format using invariant culture to ensure period as decimal separator
			var alpha = (value.A / 255.0).ToString("F2", CultureInfo.InvariantCulture);
			writer.WriteStringValue($"rgba({value.R}, {value.G}, {value.B}, {alpha})");
		}
	}
}
