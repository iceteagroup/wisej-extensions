using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Wisej.Web.Ext.ChartJS4.Models
{

	public abstract class OptionsBase
	{
		[Browsable(false)]
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public bool IsDefault => OptionsDefaults.IsDefault(this);

		/// <summary>Sets all properties back to their default values.</summary>
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
