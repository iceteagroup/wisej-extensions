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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;

namespace Wisej.Web.Ext.Navigator
{
	/// <summary>
	/// Manages the navigation between pages through deep linking. Pages are
	/// matched with a registered path that appears in the URL after the #.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The Navigator component matches registered routes and arguments with a <see cref="Page"/> 
	/// in the application and automatically shows/hides (or created and disposes) the page that matches the path.
	/// </para>
	/// <para>
	/// This component is a "session singleton". Use it by addressing the class directly:
	/// </para>
	/// <code>
	/// Navigator.Map("user/{id}", typeof(Views.UserPage));
	/// Navigator.Navigate("user/16635");
	/// </code>
	/// <para>
	/// If the path definition also specifies a pattern for arguments, the Navigator component extracts the
	/// parameters from the URL and makes them available in the <see cref="Parameters"/> collection.
	/// </para>
	/// <para>
	/// Set the main view, or home page, either using the <see cref="HomePage"/> property or by registering
	/// a view with a "/" route.
	/// </para>
	/// </remarks>
	public sealed class Navigator
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance. It can be called
		/// only by this class since we want only 1 instance per session.
		/// </summary>
		private Navigator()
		{
		}

		// Returns the session singleton instance for the Navigator.
		private static Navigator Instance
		{
			get
			{
				lock (typeof(Navigator))
				{
					var nav = Application.Session[typeof(Navigator).FullName] as Navigator;
					if (nav == null)
					{
						nav = new Navigator();
						Application.Session[typeof(Navigator).FullName] = nav;

						// hook up the application events when created.
						Application.HashChanged += Application_HashChanged;
						Application.ApplicationExit += Application_ApplicationExit;
						Application.ApplicationStart += Application_ApplicationStart;
						Application.ApplicationRefresh += Application_ApplicationRefresh;
					}
					return nav;
				}
			}
		}

		#endregion

		#region Events

		// event handlers.
		private EventHandlerList events = new EventHandlerList();

		/// <summary>
		/// Fired when the <see cref="CurrentPage"/> changes.
		/// </summary>
		public static event EventHandler CurrentPageChanged
		{
			add { Instance.events.AddHandler(nameof(CurrentPageChanged), value); }
			remove { Instance.events.RemoveHandler(nameof(CurrentPageChanged), value); }
		}

		/// <summary>
		/// Fired when the <see cref="Parameters"/> in the URL change.
		/// </summary>
		public static event EventHandler ParametersChanged
		{
			add { Instance.events.AddHandler(nameof(ParametersChanged), value); }
			remove { Instance.events.RemoveHandler(nameof(ParametersChanged), value); }
		}

		/// <summary>
		/// Raise the <see cref="CurrentPageChanged"/> event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		internal void RaiseCurrentPageChanged(EventArgs e)
		{
			events[nameof(CurrentPageChanged)]?.DynamicInvoke(null, EventArgs.Empty);
		}

		/// <summary>
		/// Raise the <see cref="ParametersChanged"/> event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		internal void RaiseParametersChanged(EventArgs e)
		{
			events[nameof(ParametersChanged)]?.DynamicInvoke(null, EventArgs.Empty);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the parameters that have been extracted from the current URL.
		/// </summary>
		public static NameValueCollection Parameters
		{
			get => Instance._parameters;
		}
		private NameValueCollection _parameters = new NameValueCollection();

		/// <summary>
		/// Returns the collection of routes registered with the <see cref="Navigator"/>.
		/// </summary>
		public static NavigatorRouteCollection Routes
		{
			get => Instance._routes;
		}
		private NavigatorRouteCollection _routes = new NavigatorRouteCollection();

		/// <summary>
		/// Returns or sets whether the current user is authenticated and can
		/// navigate the pages registered with the <see cref="Navigator"/>.
		/// </summary>
		/// <remarks>
		/// Setting this property to true or false has no effect unless there is also a
		/// valid <see cref="LoginPage"/> assigned to the <see cref="Navigator"/>.
		/// </remarks>
		public static bool Authenticated
		{
			get { return Instance._authenticated; }
			set
			{
				var nav = Instance;
				if (nav._authenticated != value)
				{
					nav._authenticated = value;

					if (value)
						Navigate(Application.Hash);
					else
						Navigate("/");
				}
			}
		}
		private bool _authenticated;

		/// <summary>
		/// Returns or sets the main (or home) page. Corresponds to the "/" or "" route.
		/// </summary>
		public static Page HomePage
		{
			get => Routes["/"]?.Page;
			set => Map("/", value, NavigatorPageMode.Persist);
		}

		/// <summary>
		/// Returns or sets the page to show before navigating to any other
		/// page, unless <see cref="Navigator.Authenticated"/> is set to true.
		/// </summary>
		/// <remarks>
		/// <para>
		/// When the <see cref="LoginPage"/> is set, the <see cref="Navigator"/> will
		/// always show this page before navigating anywhere else (unless it's already authenticated).
		/// </para>
		/// <para>
		/// In order to authenticate the user and navigate to the intended page, The <see cref="LoginPage"/>
		/// must set the <see cref="Authenticated"/> property to true. As soon as <see cref="Authenticated"/> is set to
		/// true, the <see cref="Navigator"/> will dispose the <see cref="LoginPage"/> and load the
		/// intended destination view.
		/// </para>
		/// <para>
		/// If the applications sets <see cref="Authenticated"/> to false, the <see cref="Navigator"/> will
		/// automatically show the <see cref="LoginPage"/> before loading another page.
		/// </para>
		/// </remarks>
		public static Page LoginPage
		{
			get => Instance._loginPage?.Page;
			set => Instance._loginPage = new NavigatorRouteEntry("", value, NavigatorPageMode.Persist);
		}
		private NavigatorRouteEntry _loginPage;

		/// <summary>
		/// Returns or sets the view to navigate to when
		/// the session is terminated. It can be the same
		/// as <see cref="LoginPage"/>.
		/// </summary>
		/// <remarks>
		/// When the session is terminated and the <see cref="ExitPage"/> is assigned, Wisej will create a new session'
		/// to show the <see cref="ExitPage"/>.
		/// </remarks>
		public static Page ExitPage
		{
			get => Instance._exitPage?.Page;
			set => Instance._exitPage = new NavigatorRouteEntry("", value, NavigatorPageMode.Persist);
		}
		private NavigatorRouteEntry _exitPage;

		#endregion

		#region Methods

		/// <summary>
		/// Maps the specified <paramref name="path"/> to the <paramref name="pageType"/>. The actual page
		/// instance is create the first time this route is used.
		/// </summary>
		/// <param name="path">Route that corresponds to the page.</param>
		/// <param name="pageType">The page type to instantiate.</param>
		/// <param name="mode">Whether the page should be disposed when the browser navigates to another page.</param>
		public static void Map(string path, Type pageType, NavigatorPageMode mode = NavigatorPageMode.Persist)
		{
			if (path == null)
				throw new ArgumentNullException(nameof(path));
			if (pageType == null)
				throw new ArgumentNullException(nameof(pageType));

			if (!typeof(Page).IsAssignableFrom(pageType))
				throw new ArgumentException("View is not an IWisejWindow.", nameof(pageType));

			Routes.Map(path, pageType, mode);
		}

		/// <summary>
		/// Maps the specified <paramref name="path"/> to the <paramref name="page"/>.
		/// </summary>
		/// <param name="path">Route that corresponds to the page.</param>
		/// <param name="page">The page to show.</param>
		/// <param name="mode">Whether the page should be disposed when the browser navigates to another page.</param>
		public static void Map(string path, Page page, NavigatorPageMode mode = NavigatorPageMode.Persist)
		{
			if (path == null)
				throw new ArgumentNullException(nameof(path));
			if (page == null)
				throw new ArgumentNullException(nameof(page));

			Routes.Map(path, page, mode);
		}

		/// <summary>
		/// Maps the specified <paramref name="path"/> to the <paramref name="callback"/>. The actual page
		/// instance is create the first time this route is used.
		/// </summary>
		/// <param name="path">Route that corresponds to the page.</param>
		/// <param name="callback">Callback invoked to create the page when needed.</param>
		/// <param name="mode">Whether the page should be disposed when the browser navigates to another page.</param>
		public static void Map(string path, Func<Page> callback, NavigatorPageMode mode = NavigatorPageMode.Persist)
		{
			if (path == null)
				throw new ArgumentNullException(nameof(path));
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Routes.Map(path, callback, mode);
		}

		/// <summary>
		/// Deletes the specified <paramref name="path"/>.
		/// </summary>
		/// <param name="path">Route to remove from the navigation.</param>
		public static void Remove(string path)
		{
			if (path == null)
				throw new ArgumentNullException(nameof(path));

			Routes.Remove(path);
		}

		/// <summary>
		/// Navigates to the specified <paramref name="path"/>.
		/// </summary>
		/// <param name="path">Route to navigate to.</param>
		/// <returns></returns>
		public static void Navigate(string path)
		{
			if (path == null)
				throw new ArgumentNullException(nameof(path));

			var nav = Instance;
			var oldPage = CurrentPage;
			var oldArgsHash = Parameters.GetHashCode();

			//  process the current view.
			nav.OnNavigateOut(path);

			// process the new view.
			nav.OnNavigateIn(path);

			// fire the CurrentViewChanged event.
			var newView = CurrentPage;
			if (oldPage != newView)
				nav.RaiseCurrentPageChanged(EventArgs.Empty);

			var newArgsHash = Parameters.GetHashCode();
			if (oldArgsHash != newArgsHash)
				nav.RaiseParametersChanged(EventArgs.Empty);

			Application.Hash = path;
		}

		/// <summary>
		/// 
		/// </summary>
		public static Page CurrentPage
		{
			get => Instance._currentView?.Page;
		}
		private NavigatorRouteEntry _currentView;

		#endregion

		#region Handlers

		private static void Application_ApplicationStart(object sender, EventArgs e)
		{
			Navigate(Application.Hash);
		}

		private static void Application_ApplicationRefresh(object sender, EventArgs e)
		{
			Navigate(Application.Hash);
		}

		private static void Application_HashChanged(object sender, HashChangedEventArgs e)
		{
			Navigate(Application.Hash);
		}

		private static void Application_ApplicationExit(object sender, EventArgs e)
		{
			Application.HashChanged -= Application_HashChanged;
			Application.ApplicationExit -= Application_ApplicationExit;
			Application.ApplicationStart -= Application_ApplicationStart;
			Application.ApplicationRefresh -= Application_ApplicationRefresh;
		}

		#endregion

		#region Implementation

		private void OnNavigateIn(string path)
		{
			ProcessArguments(path);

			var current = _currentView;
			if (current != null)
			{
				if (current.Page == null)
				{
					if (current.Type != null)
					{
						current.Page = (Page)Activator.CreateInstance(_currentView.Type);
					}
					else if (current.Callback != null)
					{
						current.Page = _currentView.Callback();
					}

				}

				current.Page?.Show();
			}
		}

		private void OnNavigateOut(string path)
		{
			var current = _currentView;
			if (current != null && current.Page != null)
			{
				current.Page.Hide();

				if (current.ViewMode == NavigatorPageMode.Dispose)
				{
					current.Page.Dispose();
					current.Page = null;
				}
			}
		}

		private void ProcessArguments(string path)
		{
			Debug.Assert(path != null);

			var pos = path.IndexOf('?');
			var args = pos > -1 ? path.Substring(pos + 1) : "";
			var route = pos > -1 ? path.Substring(0, pos) : path;

			if (_loginPage != null && !_authenticated)
				_currentView = _loginPage;
			else
				_currentView = _routes[route];

			if (_currentView != null)
			{
				_parameters = HttpUtility.ParseQueryString(args);

				// add arguments from the path.
				var current = _currentView;
				var parameters = _parameters;
				args = path.Substring(current.Path.Length);
				if (args != "" && current.Args?.Length > 0)
				{
					var values = args.Split('/');
					for (var i = 0; i < values.Length && i < current.Args.Length; i++)
					{
						var key = current.Args[i];
						parameters[key] = values[i];
					}
				}
			}
		}

		#endregion
	}
}
