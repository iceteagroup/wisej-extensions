///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.WebSockets;
using System.Web;
using System.Web.WebSockets;

namespace Wisej.HostService.Owin
{

	internal class WisejWebSocketContext : AspNetWebSocketContext
	{
		HttpRequest request;
		HttpContext context;
		WebSocketContext webSocketContext;

		public WisejWebSocketContext(IDictionary<string, object> environment)
		{
			this.context = HttpContext.Current;
			this.request = this.context.Request;
			this.webSocketContext = (WebSocketContext)environment["System.Net.WebSockets.WebSocketContext"];
		}

		public override Uri RequestUri
		{
			get { return this.request.Url; }
		}

		public override NameValueCollection QueryString
		{
			get { return this.request.QueryString; }
		}

		public override IDictionary Items
		{
			get { return this._items = this._items ?? new Hashtable(); }
		}
		private IDictionary _items;

		public override WebSocket WebSocket
		{
			get { return this.webSocketContext.WebSocket; }
		}

		public override bool IsClientConnected
		{
			get { return this.WebSocket.State == WebSocketState.Open; }
		}

		public override HttpServerUtilityBase Server
		{
			get { return new HttpServerUtilityWrapper(this.context.Server); }
		}
	}
}
