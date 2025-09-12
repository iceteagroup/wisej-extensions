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

public class Button : Widget
{

	#region Constructors

	public Button(string elementAsString) : base(elementAsString)
	{
	}

	public Button(ILocator locator) : base(locator)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// Returns whether the button was clicked.
    /// </summary>
    public bool IsClicked { get; set; }

	/// <summary>
	/// Returns whether the button was selected.
	/// </summary>
	public bool IsSelected { get; set; }

	#endregion

	#region Methods

	/// <summary>
	/// Focuses the Button
	/// </summary>
	/// <returns></returns>
	public async Task SelectAsync()
	{
		await CreateWidgetAsync(this);

		(await this.ElementAsync).FocusAsync();
		this.IsSelected = true;
	}

	/// <summary>
	/// Clicks the button
	/// </summary>
	/// <returns></returns>
	public async Task ClickAsync()
	{
		await CreateWidgetAsync(this);

		(await this.ElementAsync).ClickAsync();
		this.IsClicked = true;
	}

	/// <summary>
	/// Double clicks the button
	/// </summary>
	/// <returns></returns>
	public async Task DoubleClickAsync()
	{
		await CreateWidgetAsync(this);

		(await this.ElementAsync).DblClickAsync();
		this.IsClicked = true;
	}

	#endregion

}