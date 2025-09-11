using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Ext.PlayWright
{
	
	public static class LoadedScriptsCollection
	{
		public static Dictionary<string, bool> LoadedScripts
		{
			get
			{
				if (_loadedScripts == null)
				{
					_loadedScripts = new Dictionary<string, bool>();
				}

				return _loadedScripts;
			}
		}

		private static Dictionary<string, bool> _loadedScripts;

		public static void Add(string key, bool value)
		{
			if (LoadedScripts.ContainsKey(key))
			{
				LoadedScripts[key] = value;
			}
			else
			{
				LoadedScripts.Add(key, value);
			}
		}

		public static bool ContainsKey(string key)
		{
			try
			{
				return LoadedScripts.ContainsKey(key);
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static bool Remove(string key)
		{
			return LoadedScripts.Remove(key);
		}

		public static bool TryGetValue(string key, out bool value)
		{
			return LoadedScripts.TryGetValue(key, out value);
		}

		public static void Clear()
		{
			LoadedScripts.Clear();
		}

	}
}
