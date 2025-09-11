using Microsoft.Playwright;

namespace Wisej.Ext.PlayWright.Controls;

public class Label : WidgetHasValue
{
	public Label()
	{
	}

	public Label(IElementHandle element) : base(element)
	{
	}

	public Label(string elementAsString) : base(elementAsString)
	{
	}

	public Label(ILocator locator) : base(locator)
	{
	}
}