using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Ext.PlayWright.Controls
{
	public class Button : Widget
	{

		#region Constructors

		public Button()
		{

		}

		public Button(IElementHandle element) : base(element)
		{

		}

		public Button(string elementAsString) : base(elementAsString)
		{
		}

		public Button(IJSHandle wQxObject) : base(wQxObject)
		{

		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns whether the button was clicked.
		/// </summary>
		public bool IsClicked { get => _isClicked; set => _isClicked = value; }
		
		private bool _isClicked = false;

		/// <summary>
		/// Returns whether the button was selected.
		/// </summary>
		public bool IsSelected { get => _isSelected; set => _isSelected = value; }
		
		private bool _isSelected = false;

		#endregion

		#region Methods

		/// <summary>
		/// Focuses the Button
		/// </summary>
		/// <returns></returns>
		public async Task Select()
		{
			//await this.Driver.Eval<bool>("selectWidget", this.Element);
			await this.Element.FocusAsync();
			_isSelected = true;
		}

		/// <summary>
		/// Clicks the button
		/// </summary>
		/// <returns></returns>
		public async Task Click()
		{
			await this.Element.ClickAsync();
			_isClicked = true;
		}

		/// <summary>
		/// Double clicks the button
		/// </summary>
		/// <returns></returns>
		public async Task DoubleClick()
		{
			await this.Element.DblClickAsync();
			_isClicked = true;
		}

		#endregion
	}
}
