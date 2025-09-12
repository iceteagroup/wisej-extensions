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
using Wisej.Ext.PlayWright.Controls.List;

namespace Wisej.Ext.PlayWright.Controls;

public class CheckedListBox : ListBox
{

	#region Constructors

	public CheckedListBox()
	{
	}

	public CheckedListBox(IElementHandle element) : base(element)
	{
	}

	public CheckedListBox(string elementAsString) : base(elementAsString)
	{
	}

	#endregion

	#region Properties

	public AsyncLazy<Dictionary<string, CheckedListBoxItem>> ListItemsAsync =>
		new(async () => await GetListItemsAsync());

	#endregion

    #region Methods

    private async Task<Dictionary<string, CheckedListBoxItem>> GetListItemsAsync()
	{
		await CreateWidgetAsync(this);

		var items = await this.Locator.Locator(".qx-listitem").AllAsync();
		var listItems = new Dictionary<string, CheckedListBoxItem>();

		for (var i = 0; i < items.Count; i++)
		{
			var listItem = new CheckedListBoxItem(await items[i].ElementHandleAsync())
			{
				Parent = this
			};

			listItems.Add(await items[i].InnerTextAsync(), listItem);
		}

		return listItems;
	}

	public override async Task<IListItem> GetListItemByLabelAsync(string label)
	{
		await CreateWidgetAsync(this);

		var item = this.Locator.Locator("[class*=qx-listitem]").Locator("div[name=label]").GetByText(label)
						.Locator("..");

		var listItem = new CheckedListBoxItem(item)
		{
			Parent = this
		};

		return listItem;
	}

	public async Task CheckMultipleItemsAsync(params string[] labels)
	{
		await CreateWidgetAsync(this);

		foreach (var label in labels)
		{
			var item = this.Locator.Locator("[class*=qx-listitem]").Locator("div[name=label]").GetByText(label)
							.Locator("..").First;

			await item.ClickAsync();
		}
	}

	#endregion

}