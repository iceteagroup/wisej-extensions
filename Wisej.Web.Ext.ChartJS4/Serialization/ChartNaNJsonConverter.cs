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
	/// JSON converter for <see cref="ChartNaN"/>.
	/// Writes the sentinel string <c>"__NaN__"</c> which the client-side startup script
	/// replaces with the JavaScript <c>NaN</c> value before passing the config to Chart.js.
	/// </summary>
	internal sealed class ChartNaNJsonConverter : JsonConverter<ChartNaN>
	{
		/// <inheritdoc/>
		public override ChartNaN Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			=> new ChartNaN();

		/// <inheritdoc/>
		public override void Write(Utf8JsonWriter writer, ChartNaN value, JsonSerializerOptions options)
			=> writer.WriteStringValue(ChartNaN.Sentinel);
	}
}
