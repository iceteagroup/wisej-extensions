using Microsoft.Playwright;

namespace Wisej.Ext.PlayWright.Controls.List;

public class CheckedListBoxItem : ListItem
{

	#region Constructors

	public CheckedListBoxItem()
	{
	}

	public CheckedListBoxItem(IElementHandle element) : base(element)
	{
	}

	public CheckedListBoxItem(string elementAsString) : base(elementAsString)
	{
	}

	public CheckedListBoxItem(ILocator locator) : base(locator)
	{
	}

    #endregion

	#region Properties

	public AsyncLazy<CheckBox> CheckBoxAsync => new(async () => new CheckBox(await GetChildControlAsync("checkbox")));

	#endregion

    #region Methods

    public async Task Click()
	{
		var checkBox = await this.CheckBoxAsync;
		await ScrollIntoViewAsync(this);
		checkBox.Element.ClickAsync();
	}

	public override AsyncLazy<object> ValueAsync => new(async () => await GetValue());

	private async Task<bool> GetValue()
	{
		var checkBox = await this.CheckBoxAsync;
		var value = await checkBox.ValueAsync;

		//Qooxdoo sometimes likes to return "true" as a string
		if (value is not null && value.ToString() == "true") return true;

		return (bool)await checkBox.ValueAsync;
	}

	#endregion

}