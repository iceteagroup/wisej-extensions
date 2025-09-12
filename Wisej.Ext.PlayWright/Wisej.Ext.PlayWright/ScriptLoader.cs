using Microsoft.Playwright;
using System.Reflection;

namespace Wisej.Ext.PlayWright
{
	public static class ScriptLoader
	{
		static WisejWebDriver Driver = WisejWebDriver.Instance;

		public static async Task LoadScriptFromEmbeddedResourceAsync(IWidget widget)
		{
			var typeName = widget.GetType().Name;

			if (!LoadedScriptsCollection.ContainsKey(typeName))
			{
				var assembly = Assembly.GetExecutingAssembly();
				var resourceName = typeName.Equals("Widget") ? $"Wisej.Ext.PlayWright.Platform.wisej.WebDriver.js" : $"Wisej.Ext.PlayWright.Platform.wisej.WebDriver.{typeName}.js";

				var driver = WisejWebDriver.Instance;

				LoadedScriptsCollection.Add(typeName, true);

				using (var stream = assembly.GetManifestResourceStream(resourceName))
				{
					if(stream != null)
					using (var reader = new StreamReader(stream))
					{
						var result = reader.ReadToEnd();

						await driver.Page.AddScriptTagAsync(new PageAddScriptTagOptions { Content = result });
					}
				}
			}
		}

		public static async Task LoadScriptFromEmbeddedResourceAsync(IWidget widget, string customNamespace)
		{
			var typeName = widget.GetType().Name;

			if (LoadedScriptsCollection.ContainsKey(typeName) &&
				LoadedScriptsCollection.LoadedScripts[typeName] == false)
			{
				var assembly = Assembly.GetExecutingAssembly();
				var resourceName = typeName.Equals("Widget") ? $"Wisej.Ext.PlayWright.Platform.wisej.WebDriver.js" : $"Wisej.Ext.PlayWright.Platform.wisej.WebDriver.{typeName}.js";

				var driver = WisejWebDriver.Instance;

				LoadedScriptsCollection.Add(typeName, true);

				using (var stream = assembly.GetManifestResourceStream(resourceName))
				{
					using (var reader = new StreamReader(stream))
					{
						var result = reader.ReadToEnd();

						await driver.Page.AddScriptTagAsync(new PageAddScriptTagOptions { Content = result });
					}
				}
			}
		}

		public static async Task LoadScriptFromEmbeddedResourceAsync(string customNamespace)
		{
			if (LoadedScriptsCollection.ContainsKey(customNamespace) && LoadedScriptsCollection.LoadedScripts[customNamespace] == false)
			{
				var assembly = Assembly.GetExecutingAssembly();

				var resourceName = customNamespace;

				var driver = WisejWebDriver.Instance;

				LoadedScriptsCollection.Add(customNamespace, true);

				using (var stream = assembly.GetManifestResourceStream(resourceName))
				{
					using (var reader = new StreamReader(stream))
					{
						var result = reader.ReadToEnd();

						await driver.Page.AddScriptTagAsync(new PageAddScriptTagOptions { Content = result });
					}
				}
			}
		}
	}
}
