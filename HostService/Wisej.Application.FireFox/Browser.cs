///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using Gecko;
using Gecko.Events;
using System;
using System.Windows.Forms;

namespace Wisej.Application
{
	/// <summary>
	/// GeckoFx/FireFox web browser.
	/// </summary>
	internal class Browser : GeckoWebBrowser
	{
		static Browser()
		{
			Control.CheckForIllegalCrossThreadCalls = false;
			Xpcom.Initialize("Firefox64");
		}

		public Browser(string url)
		{
			Navigate(url);
		}

		protected override void OnDocumentCompleted(GeckoDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);
			this.DocumentTitle = base.DocumentTitle;
			this.DocumentCompleted?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Fired when the browser has finished loading the document.
		/// </summary>
		public new EventHandler DocumentCompleted;

		/// <summary>
		/// Returns the title of the loaded document.
		/// </summary>
		public new string DocumentTitle
		{
			get;
			private set;
		}
	}
}
