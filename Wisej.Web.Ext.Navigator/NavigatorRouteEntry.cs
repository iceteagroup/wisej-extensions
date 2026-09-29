///////////////////////////////////////////////////////////////////////////////
//
// (C) 2022 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Wisej.Core;

namespace Wisej.Web.Ext.Navigator
{
	/// <summary>
	/// Represents a route registered with the <see cref="Navigator"/>: the path and the <see cref="Page"/>, page type
	/// or callback used to create the page.
	/// </summary>
	public class NavigatorRouteEntry
	{
		internal NavigatorRouteEntry(string path, Type viewType, NavigatorPageMode mode)
		{
			ProcessPath(path);
			this.ViewMode = mode;
			this.Type = viewType;
		}

		internal NavigatorRouteEntry(string path, Func<Page> callback, NavigatorPageMode mode)
		{
			ProcessPath(path);
			this.ViewMode = mode;
			this.Callback = callback;
		}

		internal NavigatorRouteEntry(string path, Page page, NavigatorPageMode mode)
		{
			ProcessPath(path);
			this.Page = page;
			this.ViewMode = mode;
		}

		/// <summary>
		/// Returns or sets the path of the route, without the leading "/" and without the argument patterns.
		/// </summary>
		/// <remarks>
		/// A route mapped as "/user/{id}" has the path "user/". When the path contains arguments, it ends with "/".
		/// </remarks>
		public string Path { get; set; }

		/// <summary>
		/// Returns or sets the type of the <see cref="Page"/> created when the route is used, or null if the route was mapped to a page instance or a callback.
		/// </summary>
		public Type Type { get; set; }

		/// <summary>
		/// Returns or sets the <see cref="Page"/> instance associated with the route.
		/// </summary>
		/// <remarks>
		/// For routes mapped with a page type or a callback it's null until the route is used for the first time, and it's
		/// set back to null when the page is disposed in <see cref="NavigatorPageMode.Dispose"/> mode.
		/// </remarks>
		public Page Page { get; set; }

		/// <summary>
		/// Returns or sets whether the page is disposed or kept when the browser navigates to another page.
		/// </summary>
		public NavigatorPageMode ViewMode { get; set; }

		/// <summary>
		/// Returns or sets the callback that creates the <see cref="Page"/> when the route is used, or null if the route was mapped to a page instance or a type.
		/// </summary>
		public Func<Page> Callback { get; set; }

		/// <summary>
		/// 
		/// </summary>
		internal string[] Args { get; set; }

		private void ProcessPath(string path)
		{
			if (path.StartsWith("/"))
				path = path.Substring(1);

			if (path.Contains("{"))
			{
				var match = Regex.Match(path, @"([^\/]+)", RegexOptions.Compiled);

				if (match.Success)
				{
					List<string> args = null;

					path = "";
					for (; match != null && match.Success; match = match.NextMatch())
					{
						if (match.Value.StartsWith("{"))
						{
							args = args ?? new List<string>();
							args.Add(match.Value.Substring(1, match.Value.Length - 2));
						}
						else if (args == null)
						{
							path += match.Value + "/";
						}
					}

					this.Args = args?.ToArray();
				}
			}

			this.Path = path;
		}
	}
}
