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
using System.Diagnostics;
using System.Web.UI.WebControls;
using Wisej.Core;

namespace Wisej.Web.Ext.AspNetControl
{
	/// <summary>
	/// Base non-generic class for <see cref="T:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase"/>.
	/// </summary>
	[ToolboxItem(false)]
	public abstract class AspNetWrapperBase : IFramePanel
	{
		// used to change the url of the inner panel when calling Update. 
		private int _version;

		/// <summary>
		/// Initializes a new instance of <see cref="T:Wisej.Web.Ext.AspNetControl.AspNetWrapper"/>.
		/// </summary>
		public AspNetWrapperBase()
		{
			this.UseSessionViewState = true;
		}

		#region Events

		/// <summary>
		/// Corresponds to the Page.PreInit event in the ASP.NET page life cycle.
		/// </summary>
		public event EventHandler PreInit;

		/// <summary>
		/// Corresponds to the Page.Init event in the ASP.NET page life cycle.
		/// </summary>
		public event EventHandler Init;

		/// <summary>
		/// Corresponds to the Page.InitComplete event in the ASP.NET page life cycle.
		/// </summary>
		public event EventHandler InitComplete;

		/// <summary>
		/// Corresponds to the Page.PreLoad event in the ASP.NET page life cycle.
		/// </summary>
		public event EventHandler PreLoad;

		/// <summary>
		/// Corresponds to the Page.Load event in the ASP.NET page life cycle.
		/// </summary>
		public event EventHandler Load;

		/// <summary>
		/// Corresponds to the Page.LoadComplete event in the ASP.NET page life cycle.
		/// </summary>
		public event EventHandler LoadComplete;

		/// <summary>
		/// Corresponds to the Page.PreRender event in the ASP.NET page life cycle.
		/// </summary>
		public event EventHandler PreRender;

		/// <summary>
		/// Corresponds to the Page.PreRenderComplete event in the ASP.NET page life cycle.
		/// </summary>
		public event EventHandler PreRenderComplete;

		/// <summary>
		/// Corresponds to the Page.Unload event in the ASP.NET page life cycle.
		/// </summary>
		public event EventHandler Unload;

		/// <summary>
		/// Corresponds to the Page.AbortTransaction event.
		/// </summary>
		public event EventHandler AbortTransaction;

		/// <summary>
		/// Corresponds to the Page.CommitTransaction event.
		/// </summary>
		public event EventHandler CommitTransaction;

		/// <summary>
		/// Corresponds to the Page.DataBinding event.
		/// </summary>
		public event EventHandler DataBinding;

		/// <summary>
		/// Corresponds to the Page.Error event.
		/// </summary>
		public event EventHandler Error;

		/// <summary>
		/// Corresponds to the Page.SaveStateComplete event.
		/// </summary>
		public event EventHandler SaveStateComplete;

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.PreInit"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnPreInit(EventArgs e)
		{
			this.PreInit?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.Init"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnInit(EventArgs e)
		{
			this.Init?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.InitComplete"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnInitComplete(EventArgs e)
		{
			this.InitComplete?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.PreLoad"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnPreLoad(EventArgs e)
		{
			this.PreLoad?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.Load"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnLoad(EventArgs e)
		{
			this.Load?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.LoadComplete"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnLoadComplete(EventArgs e)
		{
			this.LoadComplete?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.PreRender"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnPreRender(EventArgs e)
		{
			this.PreRender?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.PreRenderComplete"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnPreRenderComplete(EventArgs e)
		{
			this.PreRenderComplete?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.Unload"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnUnload(EventArgs e)
		{
			this.Unload?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.Error"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnError(EventArgs e)
		{
			this.Error?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.CommitTransaction"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnCommitTransaction(EventArgs e)
		{
			this.CommitTransaction?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.AbortTransaction"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnAbortTransaction(EventArgs e)
		{
			this.AbortTransaction?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.DataBinding"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnDataBinding(EventArgs e)
		{
			this.DataBinding?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.AspNetControl.AspNetWrapperBase.SaveStateComplete"/> event.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnSaveStateComplete(EventArgs e)
		{
			this.SaveStateComplete?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Determines whether Wisej saves the VIEWSTATE in memory. The default is true.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool UseSessionViewState
		{
			get;
			set;
		}

		/// <summary>
		/// Returns the instance of the wrapped ASP.NET control.
		/// It is valid only during the processing of the Init or the Load events.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebControl WrappedControl
		{
			get { return this._hostedControl; }
		}
		private WebControl _hostedControl;

		/// <summary>
		/// Returns whether the current request is a postback.
		/// </summary>
		[Browsable(false)]
		public bool IsPostBack
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns whether the current request is a callback.
		/// </summary>
		[Browsable(false)]
		public bool IsCallback
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns whether the current request is an asynchronous postback.
		/// </summary>
		[Browsable(false)]
		public bool IsAsync
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the instance of the internal page used to wrap the ASP.NET control.
		/// It is valid only during the processing of the Init or the Load events.
		/// </summary>
		[Browsable(false)]
		public System.Web.UI.Page Page
		{
			get { return this._page; }
		}
		private System.Web.UI.Page _page;

		/// <summary>
		/// VIEWSTATE saved using Wisej sessions.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object ViewState
		{
			get;
			set;
		}

		/// <summary>
		/// Returns the instance of the <see cref="T:System.Web.UI.ScriptManager"/> in the AspNetHost.asp page
		/// used to wrap AspNet controls.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public System.Web.UI.ScriptManager ScriptManager
		{
			get { return System.Web.UI.ScriptManager.GetCurrent(this.Page); }
		}

		/// <summary>
		/// Returns the postback url.
		/// </summary>
		internal string AspNetHostUrl
		{
			get
			{
				// use the embedded AspNetHost page passing the id to this instance of the
				// Wisej wrapper control. the page code will use the id to call back this control
				// during the page life cycle.

				return "Wisej.AspNetHost.aspx"
					+ "?sid=" + Application.SessionId
					+ "&_cid=" + ((IWisejComponent)this).Id
					+ "&_sc=" + this._version;
			}
		}

		#endregion

		#region Implementation

		/// <summary>
		/// Page.PreInit callback. Call comes from Wisej.AspNetHost.aspx.
		/// </summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page"/> that is processing OnPreInit.</param>
		/// <param name="container">The container form to add the wrapped control to.</param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnPagePreInitCallback(System.Web.UI.Page page, System.Web.UI.HtmlControls.HtmlForm container)
		{
			System.Diagnostics.Trace.TraceInformation("AspNetWrapperBase.OnPagePreInitCallback");

			Debug.Assert(page != null);

			this.IsAsync = page.IsAsync;
			this.IsPostBack = page.IsPostBack;
			this.IsCallback = page.IsCallback;

			page.Culture = page.UICulture = Application.CurrentCulture.Name;

			page.PreInit += Page_PreInit;
			page.Init += Page_Init;
			page.InitComplete += Page_InitComplete;
			page.PreLoad += Page_PreLoad;
			page.Load += Page_Load;
			page.LoadComplete += Page_LoadComplete;
			page.PreRender += Page_PreRender;
			page.PreRenderComplete += Page_PreRenderComplete;
			page.SaveStateComplete += Page_SaveStateComplete;
			page.AbortTransaction += Page_AbortTransaction;
			page.CommitTransaction += Page_CommitTransaction;
			page.DataBinding += Page_DataBinding;
			page.Error += Page_Error;
			page.Unload += Page_Unload;

			this._page = page;
			this._hostedControl = CreateHostedControl();
			container.Controls.Add(this._hostedControl);
		}

		private void Page_Error(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnError(e);
		}

		private void Page_DataBinding(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnDataBinding(e);
		}

		private void Page_CommitTransaction(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnCommitTransaction(e);
		}

		private void Page_AbortTransaction(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnAbortTransaction(e);
		}

		private void Page_Unload(object sender, EventArgs e)
		{
			try
			{
				this._page = (System.Web.UI.Page)sender;
				OnUnload(e);
			}
			finally
			{
				this._page = null;
				this._hostedControl = null;
			}
		}

		private void Page_SaveStateComplete(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnSaveStateComplete(e);
		}

		private void Page_PreRenderComplete(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnPreRenderComplete(e);
		}

		private void Page_PreRender(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnPreRender(e);
		}

		private void Page_PreLoad(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnPreLoad(e);
		}

		private void Page_PreInit(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnPreInit(e);
		}

		private void Page_LoadComplete(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnLoadComplete(e);
		}

		private void Page_Load(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnLoad(e);
		}

		private void Page_InitComplete(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnInitComplete(e);
		}

		private void Page_Init(object sender, EventArgs e)
		{
			this._page = (System.Web.UI.Page)sender;
			OnInit(e);
		}

		/// <summary>
		/// Causes the control to update the corresponding client side widget.
		/// When in design mode, causes the rendered control to update its
		/// entire surface in the designer.
		/// </summary>
		public new void Update()
		{
			this._version++;
			if (this._version == int.MaxValue)
				this._version = 0;

			base.Update();
		}

		/// <summary>
		/// Creates the wrapped control.
		/// </summary>
		/// <returns></returns>
		protected abstract WebControl CreateHostedControl();

		#endregion
	}
}
