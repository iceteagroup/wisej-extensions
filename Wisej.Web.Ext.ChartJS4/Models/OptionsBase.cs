using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Wisej.Web.Ext.ChartJS4.Models
{

	/// <summary>
	/// Base class for Chart.js option groups (e.g. legend, title, scales) that can detect and restore
	/// their default state.
	/// </summary>
	/// <remarks>
	/// Default values are determined from a fresh instance of the concrete type with every
	/// <see cref="DefaultValueAttribute"/> applied. Only public, readable, writable properties that are not marked
	/// <c>[Browsable(false)]</c> are considered. The designer uses <see cref="IsDefault"/> in the
	/// <c>ShouldSerializeX()</c> methods of the parent options to skip serializing unchanged option groups.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var legend = chart.ChartOptions.Plugins.Legend;
	/// legend.Position = "bottom";
	/// if (!legend.IsDefault)
	///     legend.Reset();
	/// ]]></code>
	/// </example>
	public abstract class OptionsBase : ChartModelBase
	{
		/// <summary>
		/// Returns whether all the options in this group still have their default values.
		/// </summary>
		/// <value>
		/// <c>true</c> if every relevant property equals its default value and every nested <see cref="OptionsBase"/>
		/// is either <c>null</c> or itself default; otherwise <c>false</c>.
		/// </value>
		/// <remarks>
		/// Compares the public, readable, writable and browsable properties with the values of a default instance
		/// (with the <see cref="DefaultValueAttribute"/> values applied). The property is excluded from JSON serialization
		/// and hidden from the property grid.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = chart.ChartOptions.Plugins.Title;
		/// bool unchanged = title.IsDefault; // true
		/// title.Text = "Sales";
		/// unchanged = title.IsDefault;      // false
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public bool IsDefault => OptionsDefaults.IsDefault(this);

		/// <summary>Sets all properties back to their default values.</summary>
		/// <remarks>
		/// Assigns to every public, readable, writable and browsable property the value it has on a default instance
		/// (with the <see cref="DefaultValueAttribute"/> values applied). Nested option-group properties are assigned the
		/// value read from that default instance. Derived classes can override this method to reset additional state.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.Reset();
		/// ]]></code>
		/// </example>
		public virtual void Reset() => OptionsDefaults.Reset(this);
	}

	internal static class OptionsDefaults
	{
		private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _propsCache = new();
		private static readonly ConcurrentDictionary<Type, object> _defaultsCache = new();

		public static bool IsDefault(object instance)
		{
			var type = instance.GetType();
			var props = GetRelevantProps(type);
			var defaultInstance = GetDefaultInstance(type, props);

			foreach (var p in props)
			{
				var cur = p.GetValue(instance);
				var def = p.GetValue(defaultInstance);

				// Recurse for nested options
				if (typeof(OptionsBase).IsAssignableFrom(p.PropertyType))
				{
					if (cur is OptionsBase ob && !ob.IsDefault)
						return false;
					continue;
				}

				if (!object.Equals(cur, def))
					return false;
			}
			return true;
		}

		public static void Reset(object instance)
		{
			var type = instance.GetType();
			var props = GetRelevantProps(type);
			var defaults = GetDefaultInstance(type, props);

			foreach (var p in props)
			{
				var def = p.GetValue(defaults);
				p.SetValue(instance, def);
			}
		}

		private static PropertyInfo[] GetRelevantProps(Type t) =>
			_propsCache.GetOrAdd(t, _ =>
				t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				 .Where(p => p.CanRead && p.CanWrite && IsBrowsable(p)).ToArray());

		private static bool IsBrowsable(PropertyInfo p)
		{
			var b = p.GetCustomAttribute<BrowsableAttribute>();
			return b?.Browsable ?? true; // default: browsable
		}

		private static object GetDefaultInstance(Type t, PropertyInfo[] props) =>
			_defaultsCache.GetOrAdd(t, _ =>
			{
				var inst = Activator.CreateInstance(t)!;

				// Apply [DefaultValue] attributes explicitly
				foreach (var p in props)
				{
					var dv = p.GetCustomAttribute<DefaultValueAttribute>();
					if (dv != null && p.CanWrite)
						p.SetValue(inst, dv.Value);
				}
				return inst;
			});
	}

}
