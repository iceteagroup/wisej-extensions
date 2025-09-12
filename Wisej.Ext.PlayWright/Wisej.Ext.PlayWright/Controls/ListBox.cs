using Microsoft.Playwright;
using Wisej.Ext.PlayWright.Controls.List;

namespace Wisej.Ext.PlayWright.Controls;

public class ListBox : Widget
{

	#region Constructors

	public ListBox()
	{
	}

	public ListBox(IElementHandle element) : base(element)
	{
	}

	public ListBox(string elementAsString) : base(elementAsString)
	{
	}

	#endregion

	#region Properties

	public AsyncLazy<Dictionary<string, ListItem>> ListItems => new(async () => await GetListItemsAsync());

	public AsyncLazy<int> SelectedItemIndex
	{
		get => new(async () => await GetSelectedItemAsync());
		set => new AsyncLazy<int>(async () => await SelectItemAsync(await value));
	}

	#endregion

	#region Methods

	public async Task<Dictionary<string, ListItem>> GetListItemsAsync()
	{
		await CreateWidgetAsync(this);

		var items = await this.Locator.Locator(".qx-listitem").AllAsync();
		var listItems = new Dictionary<string, ListItem>();

		foreach (var locator in items)
		{
			var listItem = new ListItem(await locator.ElementHandleAsync())
			{
				Parent = this
			};

			listItems.Add(await listItem.Element.InnerTextAsync(), listItem);
		}

		return listItems;
	}

	public async Task<int> SelectItemAsync(int index)
	{
		await CreateWidgetAsync(this);

		await this.Driver.CallAsync("setListBoxSelectedItem", await this.ElementAsync, index);

		return index;
	}

	public async Task<int> GetSelectedItemAsync()
	{
		return await this.Driver.EvalAsync<int>("getListBoxSelectedItem", await this.ElementAsync);
	}

	public virtual async Task<IListItem> GetListItemByLabelAsync(string label)
	{
		await CreateWidgetAsync(this);

		var item = this.Locator.Locator(".qx-listitem").Locator("div[name=label]").GetByText(label).Locator("..");

		var listItem = new ListItem(item)
		{
			Parent = this
		};

		return listItem;
	}

	#endregion

}