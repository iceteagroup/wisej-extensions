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

public class ComboBox : WidgetHasValue
{

	#region Constructors

	public ComboBox()
	{
	}

	public ComboBox(IElementHandle element) : base(element)
	{
	}

	public ComboBox(string elementAsString) : base(elementAsString)
	{
	}

	#endregion

	#region Properties

	/// <summary>
	/// Returns the <see cref="ComboBox" /> Current Text
	/// </summary>
	public override AsyncLazy<string> TextAsync
	{
		get
		{
			return new AsyncLazy<string>(async () => await GetTextAsync());
		}
	}

	/// <summary>
	/// Gets or Sets the <see cref="ComboBox" /> Selected Index
	/// </summary>
	public AsyncLazy<int> SelectedIndexAsync
	{
		get
		{
			return new AsyncLazy<int>(async () => await GetSelectedIndexAsync());
		}
	}

	#endregion

	#region Methods

	private async Task<string> GetTextAsync()
	{
		return (await this.ValueAsync).ToString();
	}

	private async Task<int> GetSelectedIndexAsync()
	{
		await CreateWidgetAsync(this);

		return await this.Driver.EvalAsync<int>("getComboBoxSelectedIndex", await this.ElementAsync);
	}

	public async Task<int> SetSelectedIndexAsync(int index)
	{
		await CreateWidgetAsync(this);

		return await this.Driver.EvalAsync<int>("setComboBoxSelectedIndex", await this.ElementAsync, index);
	}

	#endregion

}