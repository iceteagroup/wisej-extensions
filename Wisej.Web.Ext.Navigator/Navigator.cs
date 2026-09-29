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
	/// in the application and automatically shows/hides (or creates and disposes) the page that matches the path.
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
	/// <para>
	/// The Navigator attaches to the application's hash, start and refresh events the first time any of its
	/// members is used in a session, and navigates to <see cref="Application.Hash"/> when they occur.
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
		/// <remarks>
		/// <para>
		/// The collection contains both the query string arguments (i.e. <c>orders?year=2024</c>) and the positional
		/// arguments declared in the route pattern (i.e. <c>user/{id}</c>). A new collection is created every time the
		/// <see cref="Navigator"/> navigates to a registered route.
		/// </para>
		/// </remarks>
		/// <example>
		/// Reading the arguments of the "user/{id}" route when the page is shown:
		/// <code><![CDATA[
		/// // Program.Main:
		/// Navigator.Map("user/{id}", typeof(UserPage));
		///
		/// // UserPage:
		/// private void UserPage_VisibleChanged(object sender, EventArgs e)
		/// {
		///     if (this.Visible)
		///     {
		///         var id = Navigator.Parameters["id"];
		///         var tab = Navigator.Parameters["tab"];
		///         LoadUser(id, tab);
		///     }
		/// }
		/// ]]></code>
		/// </example>
		public static NameValueCollection Parameters
		{
			get => Instance._parameters;
		}
		private NameValueCollection _parameters = new NameValueCollection();

		/// <summary>
		/// Returns the collection of routes registered with the <see cref="Navigator"/>.
		/// </summary>
		/// <remarks>
		/// Use <see cref="Map(string, Type, NavigatorPageMode)"/> and <see cref="Remove(string)"/> to add or remove routes.
		/// </remarks>
		/// <example>
		/// Checking which page is registered for a route:
		/// <code><![CDATA[
		/// var entry = Navigator.Routes["user/"];
		/// if (entry != null && entry.Page != null)
		/// {
		///     AlertBox.Show("The user page is already loaded.");
		/// }
		/// ]]></code>
		/// </example>
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
		/// <para>
		/// Setting this property has no effect on the page that is shown unless there is also a
		/// valid <see cref="LoginPage"/> assigned to the <see cref="Navigator"/>.
		/// </para>
		/// <para>
		/// Changing the value navigates immediately: setting it to true navigates to the current
		/// <see cref="Application.Hash"/> (the page originally requested) and setting it to false navigates to "/", which
		/// shows the <see cref="LoginPage"/> when assigned.
		/// </para>
		/// </remarks>
		/// <example>
		/// Authenticating the user from the login page:
		/// <code><![CDATA[
		/// private void buttonLogin_Click(object sender, EventArgs e)
		/// {
		///     if (ValidateUser(this.textBoxUser.Text, this.textBoxPassword.Text))
		///         Navigator.Authenticated = true;
		///     else
		///         AlertBox.Show("Invalid user name or password.", MessageBoxIcon.Error);
		/// }
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// Setting this property maps the "/" route to the page using <see cref="NavigatorPageMode.Persist"/>.
		/// Since routes are matched by prefix, the home page is also shown for paths that don't match any other route.
		/// </remarks>
		/// <example>
		/// Setting the home page at startup:
		/// <code><![CDATA[
		/// static void Main()
		/// {
		///     Navigator.HomePage = new MainPage();
		///     Navigator.Map("orders", typeof(OrdersPage));
		/// }
		/// ]]></code>
		/// </example>
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
		/// In order to authenticate the user and navigate to the intended page, the <see cref="LoginPage"/>
		/// must set the <see cref="Authenticated"/> property to true. As soon as <see cref="Authenticated"/> is set to
		/// true, the <see cref="Navigator"/> hides the <see cref="LoginPage"/> (it's not disposed) and loads the
		/// intended destination page.
		/// </para>
		/// <para>
		/// If the application sets <see cref="Authenticated"/> to false, the <see cref="Navigator"/> will
		/// automatically show the <see cref="LoginPage"/> again.
		/// </para>
		/// </remarks>
		/// <example>
		/// Protecting all the pages with a login page:
		/// <code><![CDATA[
		/// static void Main()
		/// {
		///     Navigator.LoginPage = new LoginPage();
		///     Navigator.HomePage = new MainPage();
		///     Navigator.Map("reports", typeof(ReportsPage));
		/// }
		/// ]]></code>
		/// </example>
		public static Page LoginPage
		{
			get => Instance._loginPage?.Page;
			set => Instance._loginPage = new NavigatorRouteEntry("", value, NavigatorPageMode.Persist);
		}
		private NavigatorRouteEntry _loginPage;

		/// <summary>
		/// Returns or sets the page to navigate to when
		/// the session is terminated. It can be the same
		/// as <see cref="LoginPage"/>.
		/// </summary>
		/// <remarks>
		/// The value is stored but it's currently not used by the <see cref="Navigator"/>.
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
		/// instance is created the first time this route is used.
		/// </summary>
		/// <param name="path">Route that corresponds to the page. It can declare positional arguments in curly braces, i.e. "user/{id}".</param>
		/// <param name="pageType">The page type to instantiate. It must derive from <see cref="Page"/> and have a public parameterless constructor.</param>
		/// <param name="mode">Whether the page should be disposed when the browser navigates to another page.</param>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="pageType"/> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="pageType"/> doesn't derive from <see cref="Page"/>.</exception>
		/// <remarks>
		/// A leading "/" in <paramref name="path"/> is ignored. Mapping a path that is already registered replaces the previous route.
		/// </remarks>
		/// <example>
		/// Registering pages by type:
		/// <code><![CDATA[
		/// Navigator.Map("customers", typeof(CustomersPage));
		/// Navigator.Map("customer/{id}", typeof(CustomerPage), NavigatorPageMode.Dispose);
		///
		/// // shows CustomerPage with Navigator.Parameters["id"] = "1042".
		/// Navigator.Navigate("customer/1042");
		/// ]]></code>
		/// </example>
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
		/// <param name="path">Route that corresponds to the page. It can declare positional arguments in curly braces, i.e. "user/{id}".</param>
		/// <param name="page">The page to show.</param>
		/// <param name="mode">Whether the page should be disposed when the browser navigates to another page.</param>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="page"/> is null.</exception>
		/// <remarks>
		/// When <paramref name="mode"/> is <see cref="NavigatorPageMode.Dispose"/> the page instance is disposed when the
		/// browser navigates away and, since there is no type or callback to recreate it, the route won't show any page afterwards.
		/// Use <see cref="NavigatorPageMode.Persist"/> with page instances.
		/// </remarks>
		/// <example>
		/// Registering an existing page instance:
		/// <code><![CDATA[
		/// var dashboard = new DashboardPage();
		/// Navigator.Map("dashboard", dashboard);
		/// ]]></code>
		/// </example>
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
		/// instance is created the first time this route is used.
		/// </summary>
		/// <param name="path">Route that corresponds to the page. It can declare positional arguments in curly braces, i.e. "user/{id}".</param>
		/// <param name="callback">Callback invoked to create the page when needed.</param>
		/// <param name="mode">Whether the page should be disposed when the browser navigates to another page.</param>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="callback"/> is null.</exception>
		/// <remarks>
		/// With <see cref="NavigatorPageMode.Dispose"/> the <paramref name="callback"/> is invoked again every time the route is used.
		/// </remarks>
		/// <example>
		/// Creating the page with a factory method:
		/// <code><![CDATA[
		/// Navigator.Map("invoices", () => new InvoicesPage(Application.Session["company"] as string), NavigatorPageMode.Dispose);
		/// ]]></code>
		/// </example>
		public static void Map(string path, Func<Page> callback, NavigatorPageMode mode = NavigatorPageMode.Persist)
		{
			if (path == null)
				throw new ArgumentNullException(nameof(path));
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Routes.Map(path, callback, mode);
		}

		/// <summary>
		/// Removes the route registered with the specified <paramref name="path"/>.
		/// </summary>
		/// <param name="path">Route to remove from the navigation, as specified when calling <c>Map</c> but without the argument patterns (i.e. "user/" for "user/{id}").</param>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
		/// <remarks>
		/// The page associated with the route, if already created, is not hidden or disposed.
		/// </remarks>
		/// <example>
		/// Removing a route when the user loses access to it:
		/// <code><![CDATA[
		/// Navigator.Remove("admin");
		/// ]]></code>
		/// </example>
		public static void Remove(string path)
		{
			if (path == null)
				throw new ArgumentNullException(nameof(path));

			Routes.Remove(path);
		}

		/// <summary>
		/// Navigates to the specified <paramref name="path"/>.
		/// </summary>
		/// <param name="path">Route to navigate to, optionally followed by positional arguments and a query string, i.e. "user/16635" or "orders?year=2024".</param>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
		/// <remarks>
		/// <para>
		/// The current page is hidden (and disposed when mapped with <see cref="NavigatorPageMode.Dispose"/>), then
		/// the page that matches <paramref name="path"/> is created if necessary and shown. When a <see cref="LoginPage"/> is assigned
		/// and <see cref="Authenticated"/> is false, the login page is shown instead.
		/// </para>
		/// <para>
		/// Fires <see cref="CurrentPageChanged"/> and <see cref="ParametersChanged"/> when applicable and finally
		/// updates <see cref="Application.Hash"/> with <paramref name="path"/>.
		/// </para>
		/// </remarks>
		/// <example>
		/// Navigating from a button:
		/// <code><![CDATA[
		/// private void buttonDetails_Click(object sender, EventArgs e)
		/// {
		///     Navigator.Navigate("user/" + this.dataGridView1.CurrentRow.Cells["Id"].Value);
		/// }
		/// ]]></code>
		/// </example>
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
		/// Returns the page currently shown by the <see cref="Navigator"/>.
		/// </summary>
		/// <remarks>
		/// It's null when no route matches the current path or before the first navigation.
		/// </remarks>
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
