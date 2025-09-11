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

using Microsoft.Playwright;
using System.Reflection;

namespace Wisej.Ext.PlayWright;

/// <summary>
/// Represents a client side widget Wisej.NET Control
/// </summary>
public class Widget : IWidget
{

	#region Constructors

	internal Widget()
	{

	}

	public Widget(IElementHandle element)
	{
		this.Element = element;
	}

	public Widget(string elementAsString)
	{
		SetElementAsync(elementAsString);

		this.ElementAsString = elementAsString;

		//SetQxObject(elementAsString);
	}

	public Widget(ILocator locator)
	{
		this.Locator = locator;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Represents the <see cref="Widget" /> qooxdoo hash.
	/// </summary>
	public AsyncLazy<string>? QxHashAsync { get; }

	/// <summary>
	/// Represents the <see cref="Widget" /> Class Name.
	/// </summary>
	public AsyncLazy<string>? ClassNameAsync { get; }

	/// <summary>
	/// Represents the <see cref="Widget" /> DOM element as a string.
	/// </summary>
	public string? ElementAsString { get; }

	/// <summary>
	/// Represents the <see cref="Widget" /> DOM element object.
	/// </summary>
	public IElementHandle? Element { get; private set; }

	/// <summary>
	/// Represents the <see cref="Widget" /> DOM element object.
	/// </summary>
	public AsyncLazy<IElementHandle?> ElementAsync {
		get
		{
			if (this.Element == null)
			{
				return new AsyncLazy<IElementHandle?>(async () => await this.Locator.ElementHandleAsync());
			}
			
			return new AsyncLazy<IElementHandle?>(() => this.Element);
		}
	}

	/// <summary>
    /// Represents the <see cref="Widget" /> Text ValueAsync.
    /// </summary>
    public virtual AsyncLazy<string>? TextAsync { get; }

	/// <summary>
	/// Represents the <see cref="Widget" /> content element.
	/// </summary>
	public AsyncLazy<IJSHandle>? ContentElementAsync
	{
		get
		{
			if (this._contentElement == null)
			{
				if (this.Element != null && this.ElementAsString == null)
					return new AsyncLazy<IJSHandle>(async () => await SetContentElementAsync(await this.ElementAsync));

				return new AsyncLazy<IJSHandle>(async () => await SetContentElementAsync(this.ElementAsString));
			}

			return new AsyncLazy<IJSHandle>(async () => await GetContentElementAsync());
		}
	}

	private IJSHandle? _contentElement;

	//Wisej.NET Web Driver instance
	public WisejWebDriver? Driver => WisejWebDriver.Instance;

	public AsyncLazy<string> NameAsync => new(async () => await this.Driver.EvalAsync<string>("getClassName", await this.ElementAsync));

	public ILocator Locator;

	public static bool IsScriptLoaded { get; private set; }
	#endregion

	#region Methods

	/// <summary>
	/// Returns a <see cref="Widget" /> child control.
	/// </summary>
	/// <param name="childControlId">Child control widget id</param>
	/// <returns></returns>
	public async Task<IElementHandle> GetChildControlAsync(string childControlId)
	{
		await CreateWidgetAsync(this);

		var element = this.Element ?? await this.Locator.ElementHandleAsync();

		var childElement = await JsExecuter.EvalUnrestrictedAsync<IElementHandle>("getChildControlElement", element, childControlId);

		return childElement;
	}

	public async Task<Widget> WaitForChildControlAsync(string childControlId, int? timeoutInSeconds)
	{
		await CreateWidgetAsync(this);

		var childElement = await JsExecuter.EvalAsync<IElementHandle>("getChildControlElement", await this.ElementAsync, childControlId);

		var widget = new Widget(childElement);

		return widget;
	}

	/// <summary>
	/// Returns the <see cref="Widget" /> property ValueAsync as a JSON string.
	/// </summary>
	/// <param name="propertyName"></param>
	/// <returns></returns>
	public async Task<string> GetPropertyValueAsJsonAsync(string propertyName)
	{
		await CreateWidgetAsync(this);

		return await JsExecuter.EvalAsync<string>("GetPropertyValueAsJsonAsync", await this.ElementAsync, propertyName);
	}

	/// <summary>
	/// Returns the <see cref="Widget" /> property ValueAsync.
	/// </summary>
	/// <param name="propertyName"></param>
	/// <returns></returns>
	public async Task<string> GetPropertyValueAsync(string propertyName)
	{
		await CreateWidgetAsync(this);

		return await JsExecuter.EvalAsync<string>("GetPropertyValueAsync", await this.ElementAsync, propertyName);
	}

	/// <summary>
	/// Returns the <see cref="Widget" /> child widgets.
	/// </summary>
	/// <returns></returns>
	public async Task<IDictionary<string, IJSHandle>> GetChildrenAsync()
	{
		await CreateWidgetAsync(this);

		var children = new Dictionary<string, IJSHandle>();

		var nodeDomElements = await JsExecuter.EvalUnrestrictedAsync<IJSHandle>("GetChildrenElementsAsync", await this.ElementAsync);

		var props = await nodeDomElements.GetPropertiesAsync();

		for (var i = 0; i < props.Count; i++)
		{
			var prop = props.ElementAt(i);
			var key = prop.Key;
			var ValueAsync = prop.Value.AsElement();

			children.Add(key, ValueAsync);
		}

		return children;
	}

	public async Task<IReadOnlyCollection<IElementHandle>> GetChildrenElementsAsync(string tagName = "div")
	{
		await CreateWidgetAsync(this);

		return await this.Element.QuerySelectorAllAsync(tagName);
	}

	/// <summary>
	/// Scrolls the <see cref="Widget" /> into view.
	/// </summary>
	public async Task ScrollIntoViewAsync()
	{
		await CreateWidgetAsync(this);

		await this.Driver.CallAsync("scrollChildToView", await this.Locator.ElementHandleAsync());
	}

	public async Task ScrollIntoViewAsync(Widget widget)
	{
		await this.Driver.CallAsync("scrollChildToView", widget.Element);
	}

	/// <summary>
	/// Gets the <see cref="Widget" /> content element from the qooxdoo object.
	/// </summary>
	/// <param name="QxObject"></param>
	/// <returns></returns>
	public async Task<IJSHandle> GetWidgetContentElementAsync(IJSHandle QxObject)
	{
		return await JsExecuter.EvalUnrestrictedAsync<IJSHandle>(
			"(arguments)=>qxwebdriver.GetContentElementAsync.apply(null,arguments)", QxObject);
	}

	/// <summary>
	/// Gets the <see cref="Widget" /> content element from the qooxdoo object.
	/// </summary>
	/// <param name="QxObject"></param>
	/// <returns></returns>
	public async Task<IJSHandle> GetWidgetContentElementAsync(string elementAsString)
	{
		await CreateWidgetAsync(this);

		return await JsExecuter.EvalUnrestrictedAsync<IJSHandle>(
			"(arguments)=>qxwebdriver.GetContentElementAsync.apply(null,arguments)", elementAsString);
	}

	/// <summary>
	/// Finds and returns a DOM element by its ID.
	/// </summary>
	public async Task<ILocator> GetDomElementByIdAsync(string id)
	{
		return this.Locator.Locator($"div[id={id}]");
	}

	/// <summary>
	/// Finds and returns a DOM element by its Name.
	/// </summary>
	public async Task<ILocator> GetDomElementByNameAsync(string name)
	{
		return this.Locator.Locator($"div[name={name}]");
	}

	#endregion

	#region Getters And Setters Methods

	private async Task<IElementHandle> SetElementAsync(string elementAsString)
	{
		elementAsString = elementAsString.Contains("#") ? elementAsString : "#" + elementAsString;

		this.Locator = this.Driver.Page.Locator(elementAsString);

		this.Element = await this.Locator.ElementHandleAsync();

		return this.Element;
	}

	private async Task<IJSHandle> SetContentElementAsync(string elementAsString)
	{
		this._contentElement = await GetWidgetContentElementAsync(elementAsString);

		return this._contentElement;
	}

	private async Task<IJSHandle> SetContentElementAsync(IElementHandle element)
	{
		this._contentElement = await GetWidgetContentElementAsync(element);

		return this._contentElement;
	}

	private async Task<IJSHandle> GetContentElementAsync()
	{
		return this._contentElement;
	}

	public virtual async Task CreateWidgetAsync(IWidget widget)
	{
		await ScriptLoader.LoadScriptFromEmbeddedResourceAsync(this);

		if (!string.IsNullOrEmpty(ElementAsString)) 
			await SetElementAsync(ElementAsString);
	}

	#endregion

}