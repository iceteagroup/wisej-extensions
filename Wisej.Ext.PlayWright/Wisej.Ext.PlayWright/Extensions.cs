using Microsoft.Playwright;
using System.Reflection;

namespace Wisej.Ext.PlayWright;

static class Extensions
{
	public static async Task<ILocator> GetDomElementByNameAsync(this ILocator locator, string name)
	{
		var element = locator.Locator($"[name*='{name}']");

		return element;
	}

	public static async Task LoadScriptFromEmbeddedResourceAsync(this IWidget widget)
	{
		var assembly = Assembly.GetExecutingAssembly();
		var resourceName = $"Wisej.Ext.PlayWright.Platform.wisej.WebDriver.{widget.GetType().Name}.js";

		var driver = WisejWebDriver.Instance;
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