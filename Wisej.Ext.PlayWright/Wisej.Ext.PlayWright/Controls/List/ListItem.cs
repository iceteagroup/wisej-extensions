using Microsoft.Playwright;

namespace Wisej.Ext.PlayWright.Controls.List;

public class ListItem : WidgetHasValue, IListItem
{

	#region Constructors

	public ListItem()
	{
	}

	public ListItem(IElementHandle element) : base(element)
	{
	}

	public ListItem(string elementAsString) : base(elementAsString)
	{
	}

	public ListItem(ILocator locator) : base(locator)
	{

	}

	#endregion

	#region Properties

	public AsyncLazy<Label> LabelAsync => new(async () => new Label(await GetChildControlAsync("label")));

	public AsyncLazy<string> TextAsync => new(async () => await GetLabel());

	public Widget Parent { get; set; }

	#endregion

	#region Methods

	public async Task Select()
	{
		await ScrollIntoViewAsync(this);
		(await this.ElementAsync).ClickAsync();
	}

	private async Task<string> GetLabel()
	{
		var label = await this.LabelAsync;

		return (string)await label.ValueAsync;
	}

	#endregion

}