// Copyright © 2010-2017 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using CefSharp;

namespace Wisej.Application
{
	public class DownloadHandler : IDownloadHandler
	{
		public event EventHandler<DownloadItem> OnBeforeDownloadFired;

		public event EventHandler<DownloadItem> OnDownloadUpdatedFired;

		public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
		{
			var handler = OnBeforeDownloadFired;
			handler?.Invoke(this, downloadItem);

			if (!callback.IsDisposed)
			{
				using (callback)
				{
					callback.Continue(downloadItem.SuggestedFileName, true);
				}
			}
		}

		public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
		{
			var handler = OnDownloadUpdatedFired;
			handler?.Invoke(this, downloadItem);
		}
	}
}