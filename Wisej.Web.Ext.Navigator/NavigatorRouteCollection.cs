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
using System.Diagnostics;
using Wisej.Core;

namespace Wisej.Web.Ext.Navigator
{
	/// <summary>
	/// Represents the collection of routes registered with the <see cref="Navigator"/>.
	/// </summary>
	/// <remarks>
	/// Routes are stored by path without the leading "/" and without the argument patterns. Use <see cref="Navigator.Routes"/>
	/// to access the collection of the current session.
	/// </remarks>
	public class NavigatorRouteCollection
	{

		private Dictionary<string, NavigatorRouteEntry> routes = new Dictionary<string, NavigatorRouteEntry>();

		/// <summary>
		/// Returns or sets the <see cref="NavigatorRouteEntry"/> that matches the specified <paramref name="path"/>.
		/// </summary>
		/// <param name="path">Path to match. A leading "/" is ignored.</param>
		/// <returns>The route registered with <paramref name="path"/>; otherwise the registered route with the longest path that
		/// <paramref name="path"/> starts with (case insensitive), or null when there is no match.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
		/// <remarks>
		/// <para>
		/// Since the "/" route (the home page) is registered with an empty path, it matches any path that doesn't match a longer route.
		/// </para>
		/// <para>
		/// The setter adds or replaces the entry stored for <paramref name="path"/>; setting it to null removes the route.
		/// </para>
		/// </remarks>
		/// <example>
		/// Looking up the route that handles a URL:
		/// <code><![CDATA[
		/// // "customer/{id}" is stored as "customer/".
		/// var entry = Navigator.Routes["customer/1042"];
		/// if (entry != null)
		///     AlertBox.Show("Mode: " + entry.ViewMode);
		/// ]]></code>
		/// </example>
		public NavigatorRouteEntry this[string path]
		{
			get
			{
				if (path == null)
					throw new ArgumentNullException(nameof(path));

				if (path.StartsWith("/"))
					path = path.Substring(1);

				if (!this.routes.TryGetValue(path, out NavigatorRouteEntry entry))
				{
					// search closest with arguments.
					foreach (var r in this.routes)
					{
						if (path.StartsWith(r.Key, StringComparison.OrdinalIgnoreCase))
						{
							if (entry == null || entry.Path.Length < r.Key.Length)
								entry = r.Value;
						}
					}
				}

				return entry;
			}
			set
			{
				if (path.StartsWith("/"))
					path = path.Substring(1);

				if (value == null)
					this.routes.Remove(path);
				else
					this.routes[path] = value;
			}
		}

		internal void Remove(string path)
		{
			Debug.Assert(path != null);

			if (path.StartsWith("/"))
				path = path.Substring(1);

			this.routes.Remove(path);
		}

		internal void Map(string path, Func<Page> callback, NavigatorPageMode mode)
		{
			Debug.Assert(path != null);
			Debug.Assert(callback != null);

			var entry = new NavigatorRouteEntry(path, callback, mode);
			this.routes[entry.Path] = entry;
		}

		internal void Map(string path, Page page, NavigatorPageMode mode)
		{
			Debug.Assert(path != null);
			Debug.Assert(page != null);

			var entry = new NavigatorRouteEntry(path, page, mode);
			this.routes[entry.Path] = entry;
		}

		internal void Map(string path, Type pageType, NavigatorPageMode mode)
		{
			Debug.Assert(path != null);
			Debug.Assert(pageType != null);
			Debug.Assert(typeof(Page).IsAssignableFrom(pageType));


				var entry = new NavigatorRouteEntry(path, pageType, mode);
			this.routes[entry.Path] = entry;
		}
	}
}
