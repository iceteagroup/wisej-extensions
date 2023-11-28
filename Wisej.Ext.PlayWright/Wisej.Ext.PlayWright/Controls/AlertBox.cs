using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Ext.PlayWright.Controls
{
	public class AlertBox : Widget
	{
		#region Constructors

		public AlertBox()
		{

		}

		private AlertBox(IElementHandle element) : base(element)
		{

		}

		#endregion

		#region Properties

		/// <summary>
		/// Get the <see cref="AlertBox"/> Widget icon.
		/// </summary>
		public AsyncLazy<string> Icon
		{
			get { return new AsyncLazy<string>(async () => await GetPropertyValue("icon")); }
		}

		public AsyncLazy<string[]> Messages
		{
			get
			{
				return new AsyncLazy<string[]>(async () => await GetAllMessages());
			}
		}

		public AsyncLazy<int> Count
		{
			get
			{
				return new AsyncLazy<int>(async () => await GetAlertBoxesCount());
			}
		}

		public AsyncLazy<AlertBox[]> AlertBoxes
		{
			get
			{
				return new AsyncLazy<AlertBox[]>(async () => await GetAlertBoxes());
			}
		}

		#endregion

		#region Methods

		public async Task<string> GetMessage(int index)
		{
			var alertBox = (await AlertBoxes)[index];

			var child = await alertBox.Element.QuerySelectorAsync("div[name=message]");
			var message = await child.InnerTextAsync();

			return message;
		}

		private async Task<string[]> GetAllMessages()
		{
			string[] messages = new string[(await AlertBoxes).Length];

			for(int i=0; i<messages.Length-1; i++)
			{
				messages[i] = await GetMessage(i);
			}

			return messages;
		}

		public async Task<AlertBox[]> GetAlertBoxes()
		{
			var elements = await this.Driver.Page.QuerySelectorAllAsync("div[name=AlertBox]");
			var alertBoxes = new List<AlertBox>();

			foreach (var el in elements)
			{
				if (el != null)
					alertBoxes.Add(new AlertBox(el.AsElement()));
			}

			return alertBoxes.ToArray();
		}

		public async Task<int> GetAlertBoxesCount()
		{
			return await this.Driver.Eval<int>("getAlertBoxCount", null);
		}

		#endregion
	}
}
