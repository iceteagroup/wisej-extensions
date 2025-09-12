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

namespace Wisej.Ext.PlayWright;

public class WidgetHasValue : Widget
{

	#region Properties

	public virtual AsyncLazy<object> ValueAsync
	{
		get
		{
			return new AsyncLazy<object>(async () => await GetValue());
		}

		set
		{
			new AsyncLazy<object>(async () => await SetValueAsync(this.ValueAsync));
		}
	}

	#endregion

	#region Constructors

	public WidgetHasValue()
	{
	}

	public WidgetHasValue(IElementHandle element) : base(element)
	{
	}

	public WidgetHasValue(string elementAsString) : base(elementAsString)
	{
	}

	public WidgetHasValue(ILocator locator) : base(locator)
	{

	}

	#endregion

	#region Methods

	/// <summary>
	/// Gets the <see cref="Widget" /> ValueAsync
	/// </summary>
	/// <returns></returns>
	public virtual async Task<object> GetValue()
	{
		return await this.Driver.EvalAsync<object>("getValue", await this.ElementAsync); /*await EvalAsync("getValue");*/
	}

	/// <summary>
	/// Sets the <see cref="Widget" /> ValueAsync
	/// </summary>
	/// <returns></returns>
	public virtual async Task<object> SetValueAsync(object ValueAsync)
	{
		return await this.Driver.EvalAsync<object>("SetValueAsync", await this.ElementAsync, ValueAsync);
	}

	#endregion

}