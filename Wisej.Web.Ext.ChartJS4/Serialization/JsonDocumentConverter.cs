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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Wisej.Web.Ext.ChartJS4.Serialization
{
	/// <summary>
	/// Converts JsonDocument or JsonElement to a plain object structure suitable for Wisej serialization.
	/// This avoids the "ValueKind: Object" issue when deserializing to ExpandoObject.
	/// </summary>
	public static class JsonDocumentConverter
	{
		/// <summary>
		/// Converts a JsonDocument to a plain object structure (Dictionary/List/primitives).
		/// </summary>
		/// <param name="document">The JsonDocument to convert.</param>
		/// <returns>A plain object structure that can be serialized by Wisej.</returns>
		public static object? ConvertToObject(JsonDocument? document)
		{
			if (document == null)
				return null;

			return ConvertElement(document.RootElement);
		}

		/// <summary>
		/// Converts a JsonElement to a plain object structure (Dictionary/List/primitives).
		/// </summary>
		/// <param name="element">The JsonElement to convert.</param>
		/// <returns>A plain object structure that can be serialized by Wisej.</returns>
		public static object? ConvertElement(JsonElement element)
		{
			switch (element.ValueKind)
			{
				case JsonValueKind.Object:
					var dict = new Dictionary<string, object>();
					foreach (var property in element.EnumerateObject())
					{
						dict[property.Name] = ConvertElement(property.Value);
					}
					return dict;

				case JsonValueKind.Array:
					var list = new List<object>();
					foreach (var item in element.EnumerateArray())
					{
						list.Add(ConvertElement(item));
					}
					return list;

				case JsonValueKind.String:
					return element.GetString();

				case JsonValueKind.Number:
					// Try to preserve integer vs. decimal distinction
					if (element.TryGetInt32(out int intValue))
						return intValue;
					if (element.TryGetInt64(out long longValue))
						return longValue;
					return element.GetDouble();

				case JsonValueKind.True:
					return true;

				case JsonValueKind.False:
					return false;

				case JsonValueKind.Null:
					return null;

				default:
					return null;
			}
		}

		/// <summary>
		/// Removes empty objects and null values from a dictionary structure recursively.
		/// This helps reduce payload size by removing unnecessary empty objects.
		/// </summary>
		/// <param name="obj">The object to clean (Dictionary, List, or primitive).</param>
		/// <returns>The cleaned object, or null if it became empty.</returns>
		public static object? RemoveEmptyObjects(object? obj)
		{
			if (obj == null)
				return null;

			if (obj is Dictionary<string, object> dict)
			{
				var keysToRemove = new List<string>();
				
				// First, recursively clean nested objects
				foreach (var key in dict.Keys.ToList())
				{
					dict[key] = RemoveEmptyObjects(dict[key]);
					
					// Mark for removal if the value is null or an empty dictionary
					if (dict[key] == null || 
					    (dict[key] is Dictionary<string, object> nestedDict && nestedDict.Count == 0))
					{
						keysToRemove.Add(key);
					}
				}
				
				// Remove marked keys
				foreach (var key in keysToRemove)
				{
					dict.Remove(key);
				}
				
				return dict.Count > 0 ? dict : null;
			}
			
			if (obj is List<object> list)
			{
				// Clean list items and remove nulls in one pass
				var cleanedList = new List<object>();
				foreach (var item in list)
				{
					var cleanedItem = RemoveEmptyObjects(item);
					if (cleanedItem != null)
					{
						cleanedList.Add(cleanedItem);
					}
				}
				
				return cleanedList.Count > 0 ? cleanedList : null;
			}
			
			// Return primitive values as-is
			return obj;
		}

		/// <summary>
		/// Removes properties that match their DefaultValue attribute from a dictionary structure.
		/// This handles custom default values defined via [DefaultValue] attributes that System.Text.Json
		/// doesn't automatically filter (it only filters CLR default values).
		/// </summary>
		/// <param name="obj">The object to clean (Dictionary, List, or primitive).</param>
		/// <param name="sourceType">The type of the object being serialized (used to look up DefaultValue attributes).</param>
		/// <returns>The cleaned object with default values removed.</returns>
		public static object? RemoveDefaultValues(object? obj, Type? sourceType)
		{
			if (obj == null || sourceType == null)
				return obj;

			if (obj is Dictionary<string, object> dict)
			{
				var keysToRemove = new List<string>();
				
				// Get all properties with DefaultValue attributes
				var properties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				var defaultValueMap = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
				
				foreach (var prop in properties)
				{
					var defaultValueAttr = prop.GetCustomAttribute<DefaultValueAttribute>();
					if (defaultValueAttr != null)
					{
						// Store the default value for comparison (use camelCase key to match JSON property names)
						var jsonPropertyName = ToCamelCase(prop.Name);
						defaultValueMap[jsonPropertyName] = defaultValueAttr.Value;
					}
				}
				
				// Check each key in the dictionary
				foreach (var key in dict.Keys.ToList())
				{
					var value = dict[key];
					
					// Recursively process nested objects
					// Try to find the property type for nested objects
					var prop = properties.FirstOrDefault(p => 
						string.Equals(ToCamelCase(p.Name), key, StringComparison.OrdinalIgnoreCase));
					
					if (prop != null && value is Dictionary<string, object>)
					{
						dict[key] = RemoveDefaultValues(value, prop.PropertyType);
					}
					else if (value is List<object> list)
					{
						// For lists, we can't easily determine the element type without more context
						// So we just recursively process items without type information
						var cleanedList = new List<object>();
						foreach (var item in list)
						{
							var cleanedItem = RemoveDefaultValues(item, null);
							if (cleanedItem != null)
								cleanedList.Add(cleanedItem);
						}
						dict[key] = cleanedList;
					}
					
					// Check if this property matches its default value
					if (defaultValueMap.TryGetValue(key, out var defaultValue))
					{
						if (ValuesAreEqual(value, defaultValue))
						{
							keysToRemove.Add(key);
						}
					}
				}
				
				// Remove properties with default values
				foreach (var key in keysToRemove)
				{
					dict.Remove(key);
				}
				
				return dict;
			}
			
			return obj;
		}

		/// <summary>
		/// Converts a property name to camelCase (matches JSON property naming).
		/// </summary>
		private static string ToCamelCase(string str)
		{
			if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
				return str;
			
			return char.ToLowerInvariant(str[0]) + str.Substring(1);
		}

		/// <summary>
		/// Compares two values for equality, handling different numeric types and null values.
		/// </summary>
		private static bool ValuesAreEqual(object? value1, object? value2)
		{
			// Handle null cases
			if (value1 == null && value2 == null)
				return true;
			if (value1 == null || value2 == null)
				return false;

			// Direct equality check
			if (value1.Equals(value2))
				return true;

			// Handle numeric type conversions (e.g., int vs double)
			if (IsNumeric(value1) && IsNumeric(value2))
			{
				try
				{
					double d1 = Convert.ToDouble(value1);
					double d2 = Convert.ToDouble(value2);
					return Math.Abs(d1 - d2) < 0.0000001; // Use epsilon for floating point comparison
				}
				catch
				{
					return false;
				}
			}

			return false;
		}

		/// <summary>
		/// Checks if a value is a numeric type.
		/// </summary>
		private static bool IsNumeric(object value)
		{
			return value is int || value is long || value is short || value is byte ||
			       value is uint || value is ulong || value is ushort || value is sbyte ||
			       value is float || value is double || value is decimal;
		}
	}
}
