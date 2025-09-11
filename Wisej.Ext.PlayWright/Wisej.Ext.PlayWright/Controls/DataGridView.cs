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

public class DataGridView : Widget
{

	#region Constructors

	public DataGridView()
	{

	}

	public DataGridView(IElementHandle element) : base(element)
	{

	}

	public DataGridView(string elementAsString) : base(elementAsString)
	{

	}

	#endregion

	#region Properties

	/// <summary>
	/// Returns the <see cref="DataGridView" /> focused cell.
	/// </summary>
	public AsyncLazy<object> FocusedCellValueAsync
	{
		get
		{
			return new AsyncLazy<object>(async () => await GetCellValueAsync());
		}
	}

	/// <summary>
	/// Gets the <see cref="DataGridView" /> focused column.
	/// </summary>
	public AsyncLazy<int> FocusedColumnAsync
	{
		get
		{
			return new AsyncLazy<int>(async () => await GetFocusedColumnAsync());
		}
	}

	/// <summary>
	/// Gets the <see cref="DataGridView" /> focused column.
	/// </summary>
	public AsyncLazy<int> FocusedRowAsync
	{
		get
		{
			return new AsyncLazy<int>(async () => await GetFocusedRowAsync());
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// Gets a <see cref="DataGridView" /> cell ValueAsync.
	/// </summary>
	/// <param name="columnIndex"><see cref="DataGridView" />column index</param>
	/// <param name="rowIndex"> <see cref="DataGridView" />row index</param>
	public async Task<object> GetCellValueAsync(int columnIndex, int rowIndex)
	{
		await CreateWidgetAsync(this);

		return await this.EvalAsync<object>("dataGridViewgetCellValue", columnIndex, rowIndex);
	}

	//Gets the focused cell ValueAsync.
	private async Task<object> GetCellValueAsync()
	{
		await CreateWidgetAsync(this);

		var currentColumn = await this.FocusedColumnAsync;
		var currentRow = await this.FocusedRowAsync;

		return await this.EvalAsync<object>("dataGridViewgetCellValue", await this.ElementAsync, currentColumn, currentRow);
	}

	//Gets the focused column.
	private async Task<int> GetFocusedColumnAsync()
	{
		await CreateWidgetAsync(this);

		return await this.EvalAsync<int>("dataGridViewgetFocusedColumn", await this.ElementAsync);
	}

	//Gets the focused Row.
	private async Task<int> GetFocusedRowAsync()
	{
		await CreateWidgetAsync(this);

		return await this.EvalAsync<int>("dataGridViewgetFocusedRow", await this.ElementAsync);
	}

	/// <summary>
	/// Sets a <see cref="DataGridView" /> cell ValueAsync.
	/// </summary>
	/// <param name="ValueAsync">Cell ValueAsync</param>
	/// <param name="columnIndex">Column Index</param>
	/// <param name="rowIndex">Row Index</param>
	/// <param name="error">Error string</param>
	/// <param name="tooltip">Tooltip ValueAsync</param>
	public async Task SetCellValueAsync(string ValueAsync, int columnIndex, int rowIndex, string error, string tooltip)
	{
		await CreateWidgetAsync(this);

		await this.EvalAsync<object>("dataGridViewsetCellValue", 
			ValueAsync, columnIndex, rowIndex, error, tooltip);
	}

	#endregion

}