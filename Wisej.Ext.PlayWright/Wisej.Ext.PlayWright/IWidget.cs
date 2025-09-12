using Microsoft.Playwright;

namespace Wisej.Ext.PlayWright;

public interface IWidget
{
	/// <summary>
	/// Represents the <see cref="Widget" /> qooxdoo hash.
	/// </summary>
	AsyncLazy<string>? QxHashAsync { get; }

	/// <summary>
	/// Represents the <see cref="Widget" /> Class Name.
	/// </summary>
	AsyncLazy<string>? ClassNameAsync { get; }

	/// <summary>
	/// Represents the <see cref="Widget" /> DOM element as a string.
	/// </summary>
	string? ElementAsString { get; }

	/// <summary>
	/// Represents the <see cref="Widget" /> DOM element object.
	/// </summary>
	IElementHandle? Element { get; }

	/// <summary>
	/// Represents the <see cref="Widget" /> DOM element object.
	/// </summary>
	AsyncLazy<IElementHandle?> ElementAsync { get; }

	/// <summary>
	/// Represents the <see cref="Widget" /> Text ValueAsync.
	/// </summary>
	AsyncLazy<string>? TextAsync { get; }

	/// <summary>
	/// Represents the <see cref="Widget" /> content element.
	/// </summary>
	AsyncLazy<IJSHandle>? ContentElementAsync { get; }

	WisejWebDriver? Driver { get; }

	AsyncLazy<string> NameAsync { get; }

	/// <summary>
	/// Returns a <see cref="Widget" /> child control.
	/// </summary>
	/// <param name="childControlId">Child control widget id</param>
	/// <returns></returns>
	Task<IElementHandle> GetChildControlAsync(string childControlId);

	Task<Widget> WaitForChildControlAsync(string childControlId, int? timeoutInSeconds);

	/// <summary>
	/// Returns the <see cref="Widget" /> property ValueAsync as a JSON string.
	/// </summary>
	/// <param name="propertyName"></param>
	/// <returns></returns>
	Task<string> GetPropertyValueAsJsonAsync(string propertyName);

	/// <summary>
	/// Returns the <see cref="Widget" /> property ValueAsync.
	/// </summary>
	/// <param name="propertyName"></param>
	/// <returns></returns>
	Task<string> GetPropertyValueAsync(string propertyName);

	/// <summary>
	/// Returns the <see cref="Widget" /> child widgets.
	/// </summary>
	/// <returns></returns>
	Task<IDictionary<string, IJSHandle>> GetChildrenAsync();

	Task<IReadOnlyCollection<IElementHandle>> GetChildrenElementsAsync(string tagName = "div");

	/// <summary>
	/// Scrolls the <see cref="Widget" /> into view.
	/// </summary>
	Task ScrollIntoViewAsync();

	Task ScrollIntoViewAsync(Widget widget);

	/// <summary>
	/// Gets the <see cref="Widget" /> content element from the qooxdoo object.
	/// </summary>
	/// <param name="QxObject"></param>
	/// <returns></returns>
	Task<IJSHandle> GetWidgetContentElementAsync(IJSHandle QxObject);

	/// <summary>
	/// Gets the <see cref="Widget" /> content element from the qooxdoo object.
	/// </summary>
	/// <param name="QxObject"></param>
	/// <returns></returns>
	Task<IJSHandle> GetWidgetContentElementAsync(string elementAsString);

	/// <summary>
	/// Finds and returns a DOM element by its ID.
	/// </summary>
	Task<ILocator> GetDomElementByIdAsync(string id);

	/// <summary>
	/// Finds and returns a DOM element by its Name.
	/// </summary>
	Task<ILocator> GetDomElementByNameAsync(string name);
}