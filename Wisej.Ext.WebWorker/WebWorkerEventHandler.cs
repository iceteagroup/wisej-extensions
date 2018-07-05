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


using System;
using Wisej.Core;

namespace Wisej.Ext.WebWorker
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Ext.WebWorker.WebWorker.PostMessage"/> event.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Ext.WebWorker.WebWorkerPostMessageEventArgs" /> that contains the event data. </param>
	public delegate void WebWorkerPostMessageEventHandler(object sender, WebWorkerPostMessageEventArgs e);

	/// <summary>
	/// Provides data for the PostMessage event.
	/// </summary>
	public class WebWorkerPostMessageEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of WebWorkerPostMessageEventArgs from the
		/// specified event data.
		/// </summary>
		/// <param name="e">An instance of <see cref="T:Wisej.Core.WisejEventArgs"/> with the event data sent by the client. </param>
		public WebWorkerPostMessageEventArgs(WisejEventArgs e)
		{
			this.Data = e.Parameters.Data;
		}

		/// <summary>
		/// The data object sent with postMessage.
		/// </summary>
		public object Data { get; private set;}
	}
}
