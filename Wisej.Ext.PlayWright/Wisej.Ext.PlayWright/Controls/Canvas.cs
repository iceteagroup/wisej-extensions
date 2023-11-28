using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Ext.PlayWright.Controls
{
	public class Canvas : Widget
	{
		#region Constructors

		public Canvas()
		{
		}

		public Canvas(IElementHandle element) : base(element)
		{
		}

		public Canvas(string elementAsString) : base(elementAsString)
		{
		}

		public Canvas(IJSHandle wQxObject) : base(wQxObject)
		{
		}

		#endregion
	}
}
