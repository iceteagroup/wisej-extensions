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
using System.Text.Json;
using System.Text.Json.Serialization;
using Wisej.Web.Ext.ChartJS4.Models;

namespace Wisej.Web.Ext.ChartJS4.Serialization
{
	/// <summary>
	/// JSON converter for <see cref="ChartDataSet"/> that serializes using the runtime type.
	/// <para>
	/// When <see cref="ChartDataSet"/> is used as the declared element type of
	/// <c>List&lt;ChartDataSet&gt;</c>, System.Text.Json would otherwise only serialize
	/// the base-class properties and silently discard all derived-type properties
	/// (e.g. <c>PointRadius</c> and <c>Radius</c> on <see cref="LineDataSet"/>).
	/// This converter is registered only for the exact base type (not derived types)
	/// via the <see cref="CanConvert"/> override, which prevents infinite recursion.
	/// </para>
	/// </summary>
	internal sealed class ChartDataSetConverter : JsonConverter<ChartDataSet>
	{
		/// <summary>
		/// Returns <see langword="true"/> only for the exact <see cref="ChartDataSet"/> type,
		/// not for any derived types such as <see cref="LineDataSet"/> or <see cref="BarDataSet"/>.
		/// This ensures derived types are serialized natively (without re-entering this converter).
		/// </summary>
		public override bool CanConvert(Type typeToConvert)
			=> typeToConvert == typeof(ChartDataSet);

		/// <inheritdoc/>
		public override ChartDataSet? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			=> throw new NotSupportedException("ChartDataSet deserialization is not supported.");

		/// <summary>
		/// Serializes the <see cref="ChartDataSet"/> using its actual runtime type so that
		/// derived-class properties are included in the output.
		/// </summary>
		public override void Write(Utf8JsonWriter writer, ChartDataSet value, JsonSerializerOptions options)
		{
			var runtimeType = value.GetType();

			if (runtimeType == typeof(ChartDataSet))
			{
				// Rare: bare ChartDataSet instance. Re-serialize without this converter
				// to avoid infinite recursion, by building a one-off options copy.
				var fallback = new JsonSerializerOptions(options);
				for (var i = fallback.Converters.Count - 1; i >= 0; i--)
				{
					if (fallback.Converters[i] is ChartDataSetConverter)
					{
						fallback.Converters.RemoveAt(i);
						break;
					}
				}
				JsonSerializer.Serialize(writer, value, typeof(ChartDataSet), fallback);
				return;
			}

			// Serialize using the actual runtime type (LineDataSet, BarDataSet, etc.)
			// CanConvert returns false for derived types, so this call does NOT re-enter
			// this converter — no infinite recursion.
			JsonSerializer.Serialize(writer, value, runtimeType, options);
		}
	}
}
