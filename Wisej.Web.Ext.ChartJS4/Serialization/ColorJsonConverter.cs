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
	/// <remarks>
	/// Colors are written as <c>"rgba(r, g, b, a)"</c> strings, with the alpha channel expressed as a value
	/// between 0 and 1 formatted with two decimals using the invariant culture. <see cref="Color.Empty"/> and
	/// fully transparent colors are written as <c>null</c>. When reading, both the <c>rgba(…)</c> format and any
	/// HTML color (e.g. <c>"#FF0000"</c> or <c>"red"</c>) are accepted.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var options = new JsonSerializerOptions();
	/// options.Converters.Add(new ColorJsonConverter());
	/// var json = JsonSerializer.Serialize(Color.FromArgb(128, 255, 0, 0), options);
	/// // json == "\"rgba(255, 0, 0, 0.50)\""
	/// ]]></code>
	/// </example>
	public class ColorJsonConverter : JsonConverter<Color>
	{
		/// <summary>
		/// Reads a <see cref="Color"/> from a JSON string value.
		/// </summary>
		/// <param name="reader">The reader positioned on the JSON string token.</param>
		/// <param name="typeToConvert">The type to convert (<see cref="Color"/>).</param>
		/// <param name="options">The serializer options in use.</param>
		/// <returns>
		/// The parsed <see cref="Color"/>, or <see cref="Color.Empty"/> if the value is <c>null</c> or empty.
		/// </returns>
		/// <remarks>
		/// Values in the <c>rgba(r, g, b, a)</c> format are parsed directly (the alpha value is scaled from 0–1 to 0–255);
		/// any other value is parsed with <see cref="ColorTranslator.FromHtml"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new JsonSerializerOptions();
		/// options.Converters.Add(new ColorJsonConverter());
		/// var color = JsonSerializer.Deserialize<Color>("\"rgba(0, 128, 255, 1)\"", options);
		/// ]]></code>
		/// </example>
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
		/// Writes a <see cref="Color"/> to JSON in RGBA format.
		/// </summary>
		/// <param name="writer">The writer to write the value to.</param>
		/// <param name="value">The color to write.</param>
		/// <param name="options">The serializer options in use.</param>
		/// <remarks>
		/// Writes a string in the <c>rgba(r, g, b, a)</c> format, with the alpha channel as a 0–1 value with two
		/// decimals formatted using the invariant culture. <see cref="Color.Empty"/> and colors with an alpha of 0
		/// are written as <c>null</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new JsonSerializerOptions();
		/// options.Converters.Add(new ColorJsonConverter());
		/// var json = JsonSerializer.Serialize(Color.SteelBlue, options);
		/// // json == "\"rgba(70, 130, 180, 1.00)\""
		/// ]]></code>
		/// </example>
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
