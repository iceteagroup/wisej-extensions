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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Ext.CognitiveServices
{
	/// <summary>
	/// The CognitiveServices component represents an object able to programmatically analyse images.
	/// </summary>
	[ToolboxItem(true)]
	//	[ToolboxBitmap(typeof(CognitiveServices))]
	[ToolboxItemFilter("Wisej.Web", ToolboxItemFilterType.Require)]
	[ToolboxItemFilter("Wisej.Mobile", ToolboxItemFilterType.Require)]
	[Description("The CognitiveServices component represents an object able to programmatically analyse images.")]
	public class CognitiveServices : Wisej.Base.Component, IComponent
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.CognitiveServices.CognitiveServices" /> class.
		/// </summary>
		public CognitiveServices()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.CognitiveServices.CognitiveServices" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the component. </param>
		public CognitiveServices(IContainer container)
            : this()
        {
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Events

		///// <summary>
		///// Fired when the location of the browser changes while this component has an active watch.
		///// </summary>
		//public event EventHandler PositionChanged;

		///// <summary>
		///// Fires the PositionChanged event.
		///// </summary>
		///// <param name="e"></param>
		//protected virtual void OnPositionChanged(EventArgs e)
		//{
		//	if (this.PositionChanged != null)
		//		PositionChanged(this, e);
		//}

		#endregion

		#region Properties

		/// <summary>
		/// Subscription key which provides access to Cognitive Services
		/// </summary>
		[DefaultValue("")]
		[Description("Subscription key which provides access to Cognitive Services")]
		public string SubscriptionKey
		{
			get { return this._subscriptionKey; }
			set
			{
				if (this._subscriptionKey != value)
				{
					this._subscriptionKey = value;
					Update();
				}
			}
		}
		private string _subscriptionKey = "";

		/// <summary>
		/// Base Uri for Cognitive Services. Depends on region server. Please note that the region must match with your subscription.
		/// e.g. https://eastus.api.cognitive.microsoft.com/vision/v1.0/analyze
		/// </summary>
		[DefaultValue("")]
		[Description("Base Uri for Cognitive Services. Depends on region server. Please note that the region must match with your subscription.")]
		public string UriBase
		{
			get { return this._uriBase; }
			set
			{
				if (this._uriBase != value)
				{
					this._uriBase = value;
					Update();
				}
			}
		}
		private string _uriBase = "";

		/// <summary>
		/// Request Parameters that control the Cognitive Services.
		/// e.g. visualFeatures=Categories,Description,Color,Faces&language=en&details=Celebrities
		/// </summary>
		[DefaultValue("")]
		[Description("Request Parameters that control the Cognitive Services.")]
		public string RequestParameters
		{
			get { return this._requestParameters; }
			set
			{
				if (this._requestParameters != value)
				{
					this._requestParameters = value;
					Update();
				}
			}
		}
		private string _requestParameters = "";

		/// <summary>
		/// Returns the last request.
		/// </summary>
		[Browsable(false)]
		public Request LastRequest
		{
			get;
			private set;
		}


		#endregion

		#region Methods

		/// <summary>
		/// Gets the analysis of the specified image file by using the Computer Vision REST API.
		/// </summary>		
		public async void StartAnalysisRequest (byte[] imageData)
		{
			HttpClient client = new HttpClient();

			// Request headers.
			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
			
			// Assemble the URI for the REST API Call.
			string uri = UriBase + "?" + RequestParameters;

			HttpResponseMessage response;

			using (ByteArrayContent content = new ByteArrayContent(imageData))
			{
				// This example uses content type "application/octet-stream".
				// The other content types you can use are "application/json" and "multipart/form-data".
				content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

				// Execute the REST API call.
				response = await client.PostAsync(uri, content);

				// Get the JSON response.
				string contentString = await response.Content.ReadAsStringAsync();

				// TODO: Raise event
			}
		}

		// TODO: Overloads with Image and Url parameters
		#endregion

		#region IComponent

		/// <summary>
		/// Returns or sets the <see cref="T:System.ComponentModel.ISite" /> associated with 
		/// the <see cref="T:System.ComponentModel.IComponent" />.
		/// </summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> object associated with the component; or null, if the component does not have a site.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ISite Site
		{
			get { return this._site; }
			set
			{
				this._site = value;
				((IWisejComponent)this).DesignMode = value == null ? false : value.DesignMode;
			}
		}
		private ISite _site;

		/// <summary>
		/// Returns a value that indicates whether the <see cref="T:System.ComponentModel.IComponent" /> is currently in design mode.
		/// </summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.IComponent" /> is in design mode; otherwise, false.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool DesignMode
		{
			get { return this._site != null && this._site.DesignMode; }
		}

		#endregion

		#region Wisej Implementation

		private byte[] ImageToByteArray (Image image)
		{
			ImageConverter converter = new ImageConverter();
			return ((byte[])converter.ConvertTo(image, typeof(byte[])));
		}

		private byte[] UrlToByteArray (string url)
		{
			using (var webClient = new WebClient())
			{
				return webClient.DownloadData(url);
			}
		}
		///// <summary>
		///// Processes the event from the client.
		///// </summary>
		///// <param name="e">Event arguments.</param>
		//protected override void OnWebEvent(WisejEventArgs e)
		//{
		//	switch (e.Type)
		//	{
		//		case "positionChanged":
		//			this.LastPosition = new Position(e.Parameters.Data);
		//			OnPositionChanged(EventArgs.Empty);
		//			break;

		//		case "callback":
		//			ProcessCallbackWebEvent(e);
		//			break;

		//		default:
		//			base.OnWebEvent(e);
		//			break;
		//	}
		//}

		///// <summary>
		///// Renders the client component.
		///// </summary>
		///// <param name="config">Dynamic configuration object.</param>
		//protected override void OnWebRender(dynamic config)
		//{
		//	base.OnWebRender((object)config);

		//	config.className = "wisej.ext.Geolocation";
		//	config.activeWatch = this.ActiveWatch;
		//	config.highAccuracy = this.EnableHighAccuracy;
		//	config.timeout = this.Timeout;
		//	config.maxAge = this.MaximumAge;

		//	WiredEvents events = new WiredEvents();
		//	events.Add("positionChanged(Data)", "callback(Data)");
		//	config.wiredEvents = events;
		//}

		#endregion


	}
}
