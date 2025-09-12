///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Playwright;

namespace Wisej.Ext.PlayWright;

/// <summary>
/// TODO: Find a better description.
/// </summary>
public class WisejWebDriver
{

	#region Constructors

	public WisejWebDriver(IBrowser browser, IPage page)
	{
		this.Browser = browser;
		this.Page = page;
		Instance = this;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Represents the <see cref="IBrowser" /> instance.
	/// </summary>
	public IBrowser Browser { get; set; }

	/// <summary>
	/// Represents the <see cref="IPage" /> instance.
	/// </summary>
	public IPage Page { get; set; }

	/// <summary>
	/// Returns the current <see cref="WisejWebDriver" /> instance.
	/// </summary>
	public static WisejWebDriver? Instance { get; private set; }

	//Default script namespace.
	private readonly string _namespace = "Wisej.WebDriver";

	/// <summary>
	/// Returns the current URL.
	/// </summary>
	public string URL => this.Page.Url;

	/// <summary>
	/// Returns the current page title.
	/// </summary>
	public AsyncLazy<string> Title
	{
		get
		{
			return new AsyncLazy<string>(async () => await this.Page.TitleAsync());
		}
	}

	/// <summary>
	/// Returns the current window handle
	/// </summary>
	public AsyncLazy<IJSHandle> CurrentWindowHandle
	{
		get
		{
			return new AsyncLazy<IJSHandle>(async () =>
				await this.Page.EvaluateHandleAsync("() => Promise.resolve(window)"));
		}
	}

	#endregion

	#region Methods

	// Loads the automation script.
	public async Task InitAsync()
	{
		await LoadScriptFromEmbeddedResourceAsync("Wisej.Ext.PlayWright.Platform.wisej.WebDriver.js");
		Instance = this;
	}

	/// <summary>
	/// Loads a custom script from a file path.
	/// </summary>
	/// <param name="path">The path to the JavaScript file</param>
	/// <returns></returns>
	public async Task LoadScriptFromPathAsync(string path)
	{
		var fileContent = await File.ReadAllTextAsync(path);
		await this.Page.EvaluateAsync(fileContent);
	}

	/// <summary>
	/// Loads a custom script from an embedded resource.
	/// </summary>
	/// <param name="resourceName">The embedded resource name</param>
	/// <returns></returns>
	public async Task LoadScriptFromEmbeddedResourceAsync(string resourceName)
	{
		var assembly = Assembly.GetExecutingAssembly();

		using (var stream = assembly.GetManifestResourceStream(resourceName))
		{
			using (var reader = new StreamReader(stream))
			{
				var result = reader.ReadToEnd();
				await this.Page.AddScriptTagAsync(new PageAddScriptTagOptions { Content = result });
			}
		}
	}

	//Executes javascript functions.
	private async Task<T> ExecAsync<T>(string script, params object[] args)
	{
		var result = await this.Page.EvaluateAsync<T>(script, args);

		return result;
	}

	/// <summary>
	/// Evaluates a JavaScript function.
	/// </summary>
	/// <typeparam name="T">Return type</typeparam>
	/// <param name="functionName">Function name</param>
	/// <param name="args">Arguments</param>
	/// <returns></returns>
	public async Task<T> EvalAsync<T>(string functionName, params object[] args)
	{
		var function = $"(arguments)=> {this._namespace}.{functionName}.apply(null,arguments)";
			var result = await ExecAsync<T>(function, args);

			return result;
	}

	public async Task<T> EvalAsync<T>(string functionName)
	{
		var function = $"(arguments)=> {this._namespace}.{functionName}.apply(null,arguments)";
		var result = await ExecAsync<T>(function, null);

		return result;
	}

	public async Task<T> EvalAsync<T>(string resourceName,string functionName, params object[] args)
	{
		var function = $"(arguments)=> {this._namespace}.{resourceName}.{functionName}.apply(null,arguments)";
		var result = await ExecAsync<T>(function, args);

		return result;
	}

	/// <summary>
	/// Evaluates a JavaScript function in an unrestricted context.
	/// </summary>
	/// <typeparam name="T">Return type</typeparam>
	/// <param name="functionName">Function name</param>
	/// <param name="args">Arguments</param>
	/// <returns></returns>
	public async Task<T> EvalUnrestrictedAsync<T>(string functionName, params object[] args)
	{
		var function = $"(arguments)=> {this._namespace}.{functionName}.apply(null,arguments)";
		var result = (T)await this.Page.EvaluateHandleAsync(function, args);

		return result;
	}

	/// <summary>
	/// Calls a JavaScript function.
	/// </summary>
	/// <param name="functionName">Function Name</param>
	/// <param name="args">Args</param>
	public async Task CallAsync(string functionName, params object[] args)
	{
		var function = $"(arguments)=> {this._namespace}.{functionName}.apply(null,arguments)";
		await this.Page.EvaluateHandleAsync(function, args);
	}

	/// <summary>
	/// Takes a screenshot of the current page.
	/// </summary>
	/// <param name="path">Result path</param>
	public async void TakePageScreenShotAsync(string path)
	{
		var options = new PageScreenshotOptions();
		options.Path = path;
		options.FullPage = true;
		await this.Page.ScreenshotAsync();
	}

	public async Task<IJSHandle> GetWidgetObjectAsync(IElementHandle element)
	{
		return await EvalUnrestrictedAsync<IJSHandle>(
			$"(arguments)=>{this._namespace}.getWidgetByElement.apply(null,arguments)", element);
	}

	public async Task<IJSHandle> GetWidgetObjectAsync(string element)
	{
		return await EvalUnrestrictedAsync<IJSHandle>(
			$"(arguments)=>{this._namespace}.getWidgetByElement.apply(null,arguments)", element);
	}

	#endregion

}