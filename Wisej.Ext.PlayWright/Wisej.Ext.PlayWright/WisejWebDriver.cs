using Microsoft.Playwright;
using System;
using System.IO;
using System.Reflection;

namespace Wisej.Ext.PlayWright
{
	/// <summary>
	/// TODO: Find a better description.
	/// </summary>
	public class WisejWebDriver
	{
		#region Constructors

		public WisejWebDriver(IBrowser browser, IPage page)
		{
			Browser = browser;
			Page = page;
			_instance = this;
		}

		#endregion

		#region Properties

		private IBrowser _browser;
		/// <summary>
		/// Represents the <see cref="IBrowser"/> instance.
		/// </summary>
		public IBrowser Browser
		{
			get { return _browser; }
			set { _browser = value; }
		}

		private IPage _page;
		/// <summary>
		/// Represents the <see cref="IPage"/> instance.
		/// </summary>
		public IPage Page
		{
			get { return _page; }
			set { _page = value; }
		}

		private static WisejWebDriver? _instance;

		/// <summary>
		/// Returns the current <see cref="WisejWebDriver"/> instance.
		/// </summary>
		public static WisejWebDriver? Instance
		{
			get
			{
				return _instance;
			}
		}

		//Default script namespace.
		private string _namespace = "Wisej.WebDriver";

		/// <summary>
		/// Returns the current URL.
		/// </summary>
		public string URL
		{
			get { return this.Page.Url; }
		}

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
				return new AsyncLazy<IJSHandle>(async () => await this._page.EvaluateHandleAsync("() => Promise.resolve(window)"));
			}
		}

		#endregion

		#region Methods

		// Loads the automation script.
		public async Task Init()
		{
			await LoadScriptFromEmbeddedResource("Wisej.Ext.PlayWright.Platform.wisej.WebDriver.js");
		}

		/// <summary>
		/// Loads a custom script from a file path.
		/// </summary>
		/// <param name="path">The path to the JavaScript file</param>
		/// <returns></returns>
		public async Task LoadScriptFromPath(string path)
		{
			var fileContent = await File.ReadAllTextAsync(path);
			await this.Page.EvaluateAsync(fileContent);
		}

		/// <summary>
		/// Loads a custom script from an embedded resource.
		/// </summary>
		/// <param name="resourceName">The embedded resource name</param>
		/// <returns></returns>
		public async Task LoadScriptFromEmbeddedResource(string resourceName)
		{
			var assembly = Assembly.GetExecutingAssembly();

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				string result = reader.ReadToEnd();
				await this.Page.EvaluateAsync(result);
			}
		}

		//Executes javascript functions.
		private async Task<T> Exec<T>(string script, params object[] args)
		{
			var result = await this._page.EvaluateAsync<T>(script, args);
			return result;
		}

		/// <summary>
		/// Evaluates a JavaScript function.
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="functionName">Function name</param>
		/// <param name="args">Arguments</param>
		/// <returns></returns>
		public async Task<T> Eval<T>(string functionName, params object[] args)
		{
			var function = $"(arguments)=> {_namespace}.{functionName}.apply(null,arguments)";
			var result = await Exec<T>(function, args);

			return result;
		}

		/// <summary>
		/// Evaluates a JavaScript function in an unrestricted context.
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		/// <param name="functionName">Function name</param>
		/// <param name="args">Arguments</param>
		/// <returns></returns>
		public async Task<T> EvalUnrestricted<T>(string functionName, params object[] args)
		{
			var function = $"(arguments)=> {_namespace}.{functionName}.apply(null,arguments)";
			var result = (T)await Page.EvaluateHandleAsync(function, args);

			return result;
		}

		/// <summary>
		/// Calls a JavaScript function.
		/// </summary>
		/// <param name="functionName">Function Name</param>
		/// <param name="args">Args</param>
		public async Task Call(string functionName, params object[] args)
		{
			var function = $"(arguments)=> {_namespace}.{functionName}.apply(null,arguments)";
			await Page.EvaluateHandleAsync(function, args);
		}

		/// <summary>
		/// Takes a screenshot of the current page.
		/// </summary>
		/// <param name="path">Result path</param>
		public async void TakePageScreenShot(string path)
		{
			var options = new PageScreenshotOptions();
			options.Path = path;
			options.FullPage = true;
			await this._page.ScreenshotAsync();
		}

		public async Task<IJSHandle> GetWidgetObject(IElementHandle element)
		{
			return await EvalUnrestricted<IJSHandle>($"(arguments)=>{_namespace}.getWidgetByElement.apply(null,arguments)", new object[] { element });
		}

		public async Task<IJSHandle> GetWidgetObject(string element)
		{
			return await EvalUnrestricted<IJSHandle>($"(arguments)=>{_namespace}.getWidgetByElement.apply(null,arguments)", new object[] { element });
		}

		#endregion
	}
}
