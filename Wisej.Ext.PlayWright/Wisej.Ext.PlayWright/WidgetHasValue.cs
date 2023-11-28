using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Ext.PlayWright
{
	public class WidgetHasValue : Widget
	{
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

		public WidgetHasValue(IJSHandle wQxObject) : base(wQxObject)
		{
		}

		#endregion

		#region Properties

		public virtual AsyncLazy<object> Value
		{
			get
			{
				return new AsyncLazy<object>(async () => await GetValue());
			}
			set 
			{ 
				new AsyncLazy<object>(async ()=> await SetValue(value));
			}
		}
			//{ get => GetValue().Result; set => SetValue(value); }

		#endregion

		#region Methods

		/// <summary>
		/// Gets the <see cref="Widget"/> Value
		/// </summary>
		/// <returns></returns>
		public async Task<object> GetValue()
		{
			return await Driver.Eval<object>("getValue", this.Element); /*await Eval("getValue");*/
		}

		/// <summary>
		/// Sets the <see cref="Widget"/> Value
		/// </summary>
		/// <returns></returns>
		public async Task<object> SetValue(object value)
		{
			return await Driver.Eval<object>("setValue", this.Element, value);
		}

		#endregion
	}
}
