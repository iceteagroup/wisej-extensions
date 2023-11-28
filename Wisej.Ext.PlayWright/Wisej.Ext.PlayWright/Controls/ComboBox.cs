using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Ext.PlayWright.Controls
{
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

		public ComboBox(IJSHandle wQxObject) : base(wQxObject)
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the <see cref="ComboBox"/> Current Text
		/// </summary>
		public override AsyncLazy<string> Text
		{
			get
			{
				return new AsyncLazy<string>(async () => await GetText());
			}
		}

		/// <summary>
		/// Gets or Sets the <see cref="ComboBox"/> Selected Index
		/// </summary>
		public AsyncLazy<int> SelectedIndex
		{
			get
			{
				return new AsyncLazy<int>(async () => await GetSelectedIndex());
			}
		}

		#endregion

		#region Methods

		private async Task<string> GetText()
		{
			return (await this.Value).ToString();
		}

		private async Task<int> GetSelectedIndex()
		{
			return await this.Driver.Eval<int>("getComboBoxSelectedIndex", this.Element);
		}

		public async Task<int> SetSelectedIndex(int index)
		{
			return await this.Driver.Eval<int>("setComboBoxSelectedIndex", this.Element, index);
		}

		#endregion
	}
}
