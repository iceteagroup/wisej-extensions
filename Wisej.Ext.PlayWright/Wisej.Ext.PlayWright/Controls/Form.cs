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

//WIP
public class Form : Widget
{

	#region Constructors

	public Form()
	{
	}

	public Form(IElementHandle element) : base(element)
	{
	}

	public Form(string elementAsString) : base(elementAsString)
	{
	}

	#endregion

	#region Methods

	/// <summary>
	/// Closes the form.
	/// </summary>
	public async Task CloseAsync()
	{
		await CreateWidgetAsync(this);

		await this.Driver.CallAsync("close", await this.ElementAsync);
	}

	/// <summary>
	/// Maximizes the form.
	/// </summary>
	public async Task MaximizeAsync()
	{
		await CreateWidgetAsync(this);

		await this.Driver.CallAsync("maximize", await this.ElementAsync);
	}

	/// <summary>
	/// Minimizes the form.
	/// </summary>
	public async Task MinimizeAsync()
	{
		await CreateWidgetAsync(this);

		await this.Driver.CallAsync("minimize", await this.ElementAsync);
	}

	/// <summary>
	/// Restores the form.
	/// </summary>
	public async Task RestoreAsync()
	{
		await CreateWidgetAsync(this);

		await this.Driver.CallAsync("restore", await this.ElementAsync);
	}

	#endregion

}