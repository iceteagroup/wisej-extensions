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
using System.ComponentModel;
using Wisej.Core;

namespace Wisej.Ext.WebWorker
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Ext.WebWorker.WebWorker.PostMessage"/> event.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Ext.WebWorker.WebWorkerPostMessageEventArgs" /> that contains the event data. </param>
	/// <example>
	/// The following example attaches a handler to the <see cref="E:Wisej.Ext.WebWorker.WebWorker.PostMessage"/> event:
	/// <code><![CDATA[
	/// this.webWorker.PostMessage += new WebWorkerPostMessageEventHandler(webWorker_PostMessage);
	///
	/// private void webWorker_PostMessage(object sender, WebWorkerPostMessageEventArgs e)
	/// {
	///     var worker = (WebWorker)sender;
	///     this.labelResult.Text = e.Data?.ToString();
	/// }
	/// ]]></code>
	/// </example>
	public delegate void WebWorkerPostMessageEventHandler(object sender, WebWorkerPostMessageEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Ext.WebWorker.WebWorker.PostMessage"/> event.
	/// </summary>
	/// <example>
	/// The following example reads a complex object posted by the worker with
	/// <c>postMessage({ progress: 50, done: false })</c>:
	/// <code><![CDATA[
	/// private void webWorker_PostMessage(object sender, WebWorkerPostMessageEventArgs e)
	/// {
	///     dynamic message = e.Data;
	///
	///     this.progressBar.Value = (int)message.progress;
	///     if ((bool)message.done)
	///         AlertBox.Show("Completed!");
	/// }
	/// ]]></code>
	/// </example>
	[ApiCategory("WebWorker")]
	public class WebWorkerPostMessageEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of WebWorkerPostMessageEventArgs from the
		/// specified event data.
		/// </summary>
		/// <param name="e">An instance of <see cref="T:Wisej.Core.WisejEventArgs"/> with the event data sent by the client. </param>
		/// <remarks>
		/// This constructor is used by the <see cref="T:Wisej.Ext.WebWorker.WebWorker"/> component when it processes the
		/// <c>postMessage</c> event from the client. It's typically used in a derived class that processes
		/// additional client events.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// public class MyWebWorker : WebWorker
		/// {
		///     protected override void OnWebEvent(WisejEventArgs e)
		///     {
		///         if (e.Type == "postMessage")
		///         {
		///             var args = new WebWorkerPostMessageEventArgs(e);
		///             System.Diagnostics.Debug.WriteLine(args.Data);
		///         }
		///
		///         base.OnWebEvent(e);
		///     }
		/// }
		/// ]]></code>
		/// </example>
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
