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

namespace Wisej.Ext.PlayWright.Controls;

public class AlertBox : Widget
{

	#region Constructors

	public AlertBox()
	{

	}

	private AlertBox(ILocator locator) : base(locator)
	{

	}

    #endregion

    #region Properties

    /// <summary>
    /// Get the <see cref="AlertBox" /> Widget icon.
    /// </summary>
    public AsyncLazy<string> Icon
	{
		get { return new AsyncLazy<string>(async () => await GetPropertyValueAsync("icon")); }
	}

	public AsyncLazy<string[]> Messages
	{
		get
		{
			return new AsyncLazy<string[]>(async () => await GetAllMessagesAsync());
		}
	}

	public AsyncLazy<int> Count
	{
		get
		{
			return new AsyncLazy<int>(async () => await GetAlertBoxesCountAsync());
		}
	}

	public AsyncLazy<AlertBox[]> AlertBoxes
	{
		get
		{
			return new AsyncLazy<AlertBox[]>(async () => await GetAlertBoxesAsync());
		}
	}

	#endregion

	#region Methods

	public async Task<string> GetMessageAsync(int index)
	{
		var alertBox = (await this.AlertBoxes)[index];

		var child =  this.Driver.Page.Locator("div[name=message]");
		var message = await child.InnerTextAsync();

		return message;
	}

	private async Task<string[]> GetAllMessagesAsync()
	{
		var messages = new string[(await this.AlertBoxes).Length];

		for (var i = 0; i < messages.Length - 1; i++) messages[i] = await GetMessageAsync(i);

		return messages;
	}

	public async Task<AlertBox[]> GetAlertBoxesAsync()
	{
		var elements = await this.Driver.Page.Locator("div[name=AlertBox]").AllAsync();
		var alertBoxes = new List<AlertBox>();

		foreach (var el in elements)
			if (el != null)
				alertBoxes.Add(new AlertBox(el));

		return alertBoxes.ToArray();
	}

	public async Task<int> GetAlertBoxesCountAsync()
	{
		return await this.Driver.EvalAsync<int>("getAlertBoxCount");
	}

	#endregion

}