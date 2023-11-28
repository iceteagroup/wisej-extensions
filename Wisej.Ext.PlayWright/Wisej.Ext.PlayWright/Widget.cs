using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wisej.Ext.PlayWright
{
	/// <summary>
	/// Represents a client side widget Wisej.NET Control
	/// </summary>
	public class Widget
	{
		#region Constructors

		public Widget() 
		{ 
		
		}

		public Widget(IElementHandle element)
		{
			this.Element = element;
		}

		public Widget(string elementAsString)
		{
			this.ElementAsString = elementAsString;
			SetQxObject(elementAsString);
		}

		public Widget(IJSHandle wQxObject)
		{
			SetQxObject(wQxObject);
		}

		#endregion

		#region Properties

		///<summary>
		///Represents the <see cref="Widget"/> qooxdoo hash.
		///</summary>
		public AsyncLazy<string>? QxHash { get;}

		///<summary>
		///Represents the <see cref="Widget"/> Class Name.
		///</summary>
		public AsyncLazy<string>? ClassName { get;}

		///<summary>
		///Represents the <see cref="Widget"/> DOM element as a string.
		///</summary>
		public string? ElementAsString { get;}

		///<summary>
		///Represents the <see cref="Widget"/> DOM element object.
		///</summary>
		public IElementHandle? Element { get;}

		/// <summary>
		/// Represents the <see cref="Widget"/> Text value.
		/// </summary>
		public virtual AsyncLazy<string>? Text { get;}

		/// <summary>
		/// Represents the <see cref="Widget"/> content element.
		/// </summary>
		public AsyncLazy<IJSHandle>? ContentElement 
		{ 
			get 
			{
				if (_contentElement == null)
				{
					if (this.Element != null && this.ElementAsString == null)
					{
						return new AsyncLazy<IJSHandle>(async () => await this.SetContentElement(this.Element));
					}
					else
					{
						return new AsyncLazy<IJSHandle>(async () => await this.SetContentElement(this.ElementAsString));
					}
				}

				return new AsyncLazy<IJSHandle>(async () => await GetContentElement());
			}
		}
		private IJSHandle? _contentElement;

		//Wisej.NET Web Driver instance
		public WisejWebDriver Driver => WisejWebDriver.Instance;

		public AsyncLazy<IJSHandle> QxObject
		{
			get
			{
				if(_qxObject == null)
				{
					if(this.Element!= null && this.ElementAsString == null)
					{
						return new AsyncLazy<IJSHandle>(async () => await this.SetQxObject(this.Element));
					}
					else
					{
						return new AsyncLazy<IJSHandle>(async () => await this.SetQxObject(this.ElementAsString));
					}
				}

				return new AsyncLazy<IJSHandle>(async () => await GetQxObject());
			}
		}
		private IJSHandle _qxObject;

		private string _name = string.Empty;
		public string Name
		{
			get
			{
				//if (_name.Equals(string.Empty))
				//	this._name = Driver.RunJavaScript<string>("getName", this.Element).Result;

				return _name;
			}
			set => _name = value;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns a <see cref="Widget"/> child control.
		/// </summary>
		/// <param name="childControlId">Child control widget id</param>
		/// <returns></returns>
		public async Task<Widget> GetChildControl(string childControlId)
		{
			var childElement = await Driver.Eval<IElementHandle>("getChildControl", this.Element, childControlId);
			Widget widget = new Widget(childElement);

			return widget;
		}

		public async Task<Widget> WaitForChildControl(string childControlId, int? timeoutInSeconds)
		{
			var childElement = await Driver.Eval<IElementHandle>("getChildControl", this.Element, childControlId);
			Widget widget = new Widget(childElement);

			return widget;
		}

		/// <summary>
		/// Returns the <see cref="Widget"/> property value as a JSON string.
		/// </summary>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public async Task<string> GetPropertyValueAsJson(string propertyName)
		{
			return await Driver.Eval<string>("getPropertyValueAsJson", this.Element, propertyName);
		}

		/// <summary>
		/// Returns the <see cref="Widget"/> property value.
		/// </summary>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public async Task<string> GetPropertyValue(string propertyName)
		{
			return await Driver.Eval<string>("getPropertyValue", this.Element, propertyName);
		}

		/// <summary>
		/// TODO: Implements
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<IDictionary<string, IJSHandle>> GetChildren()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Scrolls the <see cref="Widget"/> into view.
		/// </summary>
		public async void ScrollIntoView()
		{
			await this.Driver.Call("scrollChildNodeToView", this.Element);
		}

		/// <summary>
		/// Gets the <see cref="Widget"/> content element from the qooxdoo object.
		/// </summary>
		/// <param name="QxObject"></param>
		/// <returns></returns>
		public async Task<IJSHandle> GetWidgetContentElement(IJSHandle QxObject)
		{
			return await this.Driver.EvalUnrestricted<IJSHandle>("(arguments)=>qxwebdriver.getContentElement.apply(null,arguments)", new object[] { QxObject });
		}

		/// <summary>
		/// Gets the <see cref="Widget"/> content element from the qooxdoo object.
		/// </summary>
		/// <param name="QxObject"></param>
		/// <returns></returns>
		public async Task<IJSHandle> GetWidgetContentElement(string elementAsString)
		{
			return await this.Driver.EvalUnrestricted<IJSHandle>("(arguments)=>qxwebdriver.getContentElement.apply(null,arguments)", new object[] { elementAsString });
		}

		/// <summary>
		/// Gets the <see cref="Widget"/> child widgets.
		/// </summary>
		/// <returns></returns>
		public async Task<IList<Widget>> GetChildWidgets()
		{
			var children = await Driver.EvalUnrestricted<IJSHandle>("function(args){return args.__childControls}", this.QxObject);


			List<Widget> childWidgets = new List<Widget>();
			var childCollection = await children.GetPropertiesAsync();
			foreach (var child in childCollection)

			{
				childWidgets.Add(new Widget(child.Value) { Name = child.Key });
			}

			return childWidgets;
		}

		#region Getters And Setters Methods
		
		private void SetContentElement()
		{

		}

		private async Task<IJSHandle> SetQxObject(string elementAsString)
		{
			this._qxObject = await this.Driver.GetWidgetObject(elementAsString);
			return _qxObject;
		}

		private async Task<IJSHandle> SetQxObject(IElementHandle element)
		{
			this._qxObject = await this.Driver.GetWidgetObject(element);
			return _qxObject;
		}

		private void SetQxObject(IJSHandle qxObject)
		{
			this._qxObject = qxObject;
		}

		private async Task<IJSHandle> GetQxObject()
		{
			return await Task.Run(() =>
			{
				return this._qxObject;
			});
		}

		private async Task<IJSHandle> SetContentElement(string elementAsString)
		{
			this._contentElement = await GetWidgetContentElement(elementAsString);
			return _contentElement;
		}

		private async Task<IJSHandle> SetContentElement(IElementHandle element)
		{
			this._contentElement = await GetWidgetContentElement(element);
			return _contentElement;
		}

		private void SetContentElement(IJSHandle contentElement)
		{
			this._contentElement = contentElement;
		}

		private async Task<IJSHandle> GetContentElement()
		{
			return this._contentElement;
		}

		#endregion

		#endregion
	}
}
