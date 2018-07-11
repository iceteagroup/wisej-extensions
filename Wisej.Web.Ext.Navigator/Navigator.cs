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

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Wisej.Core;

namespace Wisej.Web.Ext.Navigator
{
	/// <summary>
	/// Manages the navigation between views through deep linking. Views (pages and windows) are
	/// matched with a registered path that appears in the URL after the #.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The Navigator component matches registered paths and arguments with a <see cref="Page"/> or
	/// <see cref="Form"/> in the application and automatically shows/hides (or created and disposes) the
	/// view that matches the path.
	/// </para>
	/// <para>
	/// This component is a "session singleton". Use it by addressing the class directly:
	/// <code>
	/// Navigator.Add("user/{id}", typeof(Views.UserPage));
	/// Navigator.Navigate("user/16635");
	/// </code>
	/// </para>
	/// <para>
	/// If the path definition also specifies a pattern for arguments, the Navigator component extracts the
	/// parameters from the URL and makes them available in the <see cref="Parameters"/> collection.
	/// </para>
	/// </remarks>
	public sealed class Navigator
	{
		// Event handlers attached to the Navigator instance.
		private EventHandlerList Events = new EventHandlerList();

		// Collection of navigation paths.
		private NavigatorPathCollection RegisteredPaths = new NavigatorPathCollection();

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
				lock (Application.Session)
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

		/// <summary>
		/// Fired when the a new view is loaded.
		/// </summary>
		public static event EventHandler Load
		{
			add { AddHandler(nameof(Load), value); }
			remove { RemoveHandler(nameof(Load), value); }
		}

		/// <summary>
		/// Fired when the current view is unloaded.
		/// </summary>
		public static event EventHandler Unload
		{
			add { AddHandler(nameof(Unload), value); }
			remove { RemoveHandler(nameof(Unload), value); }
		}

		/// <summary>
		/// Raise the <see cref="Load"/> event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		internal static void RaiseLoad(EventArgs e)
		{
			var nav = Instance;
			((EventHandler)nav.Events[nameof(Load)])?.Invoke(null, e);
		}

		/// <summary>
		/// Raise the <see cref="Unload"/> event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		internal static void RaiseUnload(EventArgs e)
		{
			var nav = Instance;
			((EventHandler)nav.Events[nameof(Unload)])?.Invoke(null, e);
		}

		/// <summary>
		/// Adds the event handler.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="handler"></param>
		private static void AddHandler(string name, Delegate handler)
		{
			var nav = Instance;
			nav.Events.AddHandler(name, handler);
		}

		/// <summary>
		/// Removes the event handler.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="handler"></param>
		private static void RemoveHandler(string name, Delegate handler)
		{
			var nav = Instance;
			nav.Events.RemoveHandler(name, handler);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the parameters that have been extracted from the current URL.
		/// </summary>
		public static NameValueCollection Parameters
		{
			get;
		}

		/// <summary>
		/// Returns the collection of paths registered with the <see cref="Navigator"/>.
		/// </summary>
		public static string[] Paths
		{
			get;
		}

		/// <summary>
		/// Returns or sets whether the current user is authenticated and can
		/// navigate the views registered with the <see cref="Navigator"/>.
		/// </summary>
		/// <remarks>
		/// Setting this property to true or false has no effect unless there is also a
		/// valid <see cref="LoginView"/> assigned to the <see cref="Navigator"/>.
		/// </remarks>
		public static bool Authenticated
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the view to automatically create and dispose by the
		/// <see cref="Navigator"/> before navigating to any other
		/// view, unless <see cref="Navigator.Authenticated"/> is set to true.
		/// </summary>
		/// <remarks>
		/// <para>
		/// When the <see cref="LoginView"/> is set, the <see cref="Navigator"/> will
		/// always show this view before navigating anywhere else (unless it's already authenticated).
		/// </para>
		/// <para>
		/// In order to authenticate the user and navigate to the intended view, The <see cref="LoginView"/>
		/// must set the <see cref="Authenticated"/> property to true. As soon as <see cref="Authenticated"/> is set to
		/// true, the <see cref="Navigator"/> will dispose the <see cref="LoginView"/> and load the
		/// intended destination view.
		/// </para>
		/// <para>
		/// If the applications sets <see cref="Authenticated"/> to false, the <see cref="Navigator"/> will
		/// automatically show the <see cref="LoginView"/> again.
		/// </para>
		/// </remarks>
		public static Type LoginView
		{
			get;
		}

		/// <summary>
		/// Returns or sets the view to navigate to when
		/// the application is terminated. It can be the same
		/// as <see cref="LoginView"/>.
		/// </summary>
		public static Type ExitView
		{
			get;
		}

		public static string[] Breadcrumbs
		{
			get;
		}

		public static bool CanGoBack
		{
			get;
		}

		public static bool CanGoForward
		{
			get;
		}

		#endregion

		#region Methods

		// /users?name=luca&age=12
		// /user
		// /user/{name} =? /user/luca?view=detailed
		// /user/*
		// /user/{name*}
		// /user/{name?}

		public static void Add(string path, Type viewType, NavigatorViewMode mode = NavigatorViewMode.Dispose)
		{
		}

		public static void Add(string path, Page view, NavigatorViewMode mode = NavigatorViewMode.Persist)
		{
		}

		public static void Add(string path, Form view, NavigatorViewMode mode = NavigatorViewMode.Persist)
		{
		}

		public static void Remove(string path)
		{

		}

		public static bool Contains(string path)
		{
			return false;
		}

		public static T Get<T>(string path) where T : class, IWisejWindow
		{
			return null;
		}

		public static bool Navigate(string path)
		{
			return false;
		}

		public static void GoBack()
		{

		}

		public static void GoForward()
		{

		}

		#endregion

		#region Handlers

		private static void Application_ApplicationStart(object sender, EventArgs e)
		{
		}

		private static void Application_ApplicationRefresh(object sender, EventArgs e)
		{
		}

		private static void Application_ApplicationExit(object sender, EventArgs e)
		{
			Application.HashChanged -= Application_HashChanged;
			Application.ApplicationExit -= Application_ApplicationExit;
			Application.ApplicationStart -= Application_ApplicationStart;
			Application.ApplicationRefresh -= Application_ApplicationRefresh;
		}

		private static void Application_HashChanged(object sender, HashChangedEventArgs e)
		{
		}

		#endregion

	}
}
