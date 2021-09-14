///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.ComponentModel;
using System.Web.UI.WebControls;
using Wisej.Core;

namespace Wisej.Web.Ext.AspNetControl
{

    /// <summary>
    /// The AspNetWrapperBase is the base class of all wrapped ASP.NET controls.
    /// </summary>
    /// <remarks>
    /// When you using a wrapped ASP.NET control  you must add the <see cref="T:Wisej.Web.Ext.AspNetControl.HttpModule"/> to
    /// Web.config, like this:
    /// <code lang="xml">
    /// <![CDATA[
    /// 
    /// <system.webServer>
    ///		<modules>
    ///			<add name = "WisejAspNetControl" type="Wisej.Web.Ext.AspNetControl.HttpModule, Wisej.Web.Ext.AspNetControl" />
    ///			...
    /// ]]>
    /// </code>
    /// </remarks>
	[ApiCategory("ASPNetControl")]
    public class AspNetWrapper<T> : AspNetWrapperBase where T: WebControl, new()
	{
		/// <summary>
		/// Returns the instance of the wrapped ASP.NET control.
		/// It is valid only during the processing of the Init or the Load events.
		/// </summary>
		[Browsable(false)]
		public new T WrappedControl
		{
			get { return (T)base.WrappedControl; }
		}

		/// <summary>
		/// Returns source Url of the IFrame.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Url
		{
			get
			{
				// when in design mode, return the name of the wrapped control to display
				// it in the designer panel.
				if (((IWisejControl)this).DesignMode)
				{
					return typeof(T).Name;
				}

				// return the postback url.
				return base.AspNetHostUrl;
			}

			set { }
		}

		/// <summary>
		/// Creates the wrapped control.
		/// </summary>
		/// <returns></returns>
		protected override WebControl CreateHostedControl()
		{
			var control = new T();
			control.ID = ((IWisejControl)this).Id + "_" + typeof(T).Name;
			control.Width = new Unit("100%");
			control.Height = new Unit("100%");

			return control;
		}

	}
}