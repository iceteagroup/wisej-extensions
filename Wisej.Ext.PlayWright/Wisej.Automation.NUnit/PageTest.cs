/*
 * MIT License
 *
 * Copyright (c) Microsoft Corporation.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using Microsoft.Playwright;
using Microsoft.Playwright.TestAdapter;
using NUnit.Framework;
using Wisej.Ext.PlayWright;

namespace Wisej.Automation.NUnit;

public class PageTest : ContextTest
{
	public IPage Page { get; private set; } = null!;

	private WisejWebDriver _driver = null!;

	public virtual string? URL
	{
		get
		{
			if (this._url is null)
			{
				if(!(WisejAutomationSettingsProvider.URL is null))
					this._url = WisejAutomationSettingsProvider.URL;
			}

			return _url;
		}

		set => this._url = value;
	}
	private string? _url;

	[SetUp]
	public async Task PageSetup()
	{
		this.Page = await this.Context.NewPageAsync().ConfigureAwait(false);

		await this.Page.GotoAsync(URL, new PageGotoOptions()
		{
			WaitUntil = WaitUntilState.NetworkIdle
		});

		this._driver = new WisejWebDriver(this.Browser, this.Page);

		await this._driver.InitAsync();
	}
}
