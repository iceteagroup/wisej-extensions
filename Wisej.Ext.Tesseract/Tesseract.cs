﻿///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// Author: Levie Rufenacht
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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Wisej.Core;
using Wisej.Web;
using Component = Wisej.Web.Component;

namespace Wisej.Ext.Tesseract
{
	/// <summary>
	/// A component for extending the functionality of the Wisej Camera to include text detection.
	/// </summary>
	/// <remarks>
	/// See: https://github.com/naptha/tesseract.js#tesseractjs.
	/// </remarks>
	[ToolboxItem(true)]
	[ApiCategory("Tesseract")]
	public class Tesseract : Component, IExtenderProvider
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Tesseract" /> without a specified container.
		/// </summary>
		public Tesseract()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tesseract"/> class with a specified container.
		/// </summary>
		/// <param name="container">An <see cref="IContainer"/>container.</param>
		public Tesseract(IContainer container) : this()
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when text is recognized by the camera.
		/// </summary>
		public event EventHandler<TextRecognizedEventArgs> TextRecognized;

		/// <summary>
		/// Fired when a worker throws an error.
		/// </summary>
		public event EventHandler<string> WorkerError;

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the Wisej.NET Camera instance to attach to.
		/// </summary>
		[TypeConverter(typeof(CameraConverter))]
		public Control Camera
		{
			get
			{
				return this._camera;
			}
			set
			{
				if (this._camera != null)
					if (this._camera.Equals(value))
						return;

				this._camera = value;

				Update();
			}
		}
		private Control _camera;

		/// <summary>
		/// Returns or sets whether text-detection is enabled.
		/// </summary>
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (this._enabled != value)
				{
					this._enabled = value;

					Update();
				}
			}
		}
		private bool _enabled = false;

		/// <summary>
		/// Returns or sets the interval to use in between text detection requests.
		/// </summary>
		/// <remarks>
		/// The interval is specified in milliseconds.
		/// </remarks>
		public int Interval
		{
			get
			{
				return this._interval;
			}
			set
			{
				if (this._interval != value)
				{
					this._interval = value;

					Update();
				}
			}
		}
		private int _interval = 1500;

		/// <summary>
		/// Returns or sets a list of keywords to filter for on the client.
		/// </summary>
		public string[] Keywords
		{
			get
			{
				return this._keywords;
			}
			set
			{
				if (this._keywords != value)
				{
					this._keywords = value;

					Update();
				}
			}
		}
		private string[] _keywords = null;

		/// <summary>
		/// Returns or sets the detection language to use.
		/// </summary>
		/// <remarks>
		/// See: https://tesseract-ocr.github.io/tessdoc/Data-Files#data-files-for-version-400-november-29-2016
		/// </remarks>
		public string Language
		{
			get
			{
				return this._language;
			}
			set
			{
				if (this._language != value)
				{
					this._language = value;

					Update();
				}
			}
		}
		private string _language = "eng";

		/// <summary>
		/// Returns or sets whether to show bounding rectangles around detected words.
		/// </summary>
		public bool ShowWords
		{
			get
			{
				return this._showWords;
			}
			set
			{
				if (this._showWords != value)
				{
					this._showWords = value;

					Update();
				}
			}
		}
		private bool _showWords = true;

		/// <summary>
		/// Returns or sets the white listed characters to scan for.
		/// </summary>
		public string Whitelist
		{
			get
			{
				return this._whitelist;
			}
			set
			{
				if (this._whitelist != value)
				{
					this._whitelist = value;

					Update();
				}
			}
		}
		private string _whitelist = "ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789";

		/// <summary>
		/// Returns or sets the number of workers used to detect text.
		/// </summary>
		/// <remarks>
		/// Higher is better, but results in increased CPU and battery consumption.
		/// </remarks>
		public int WorkerCount
		{
			get
			{
				return this._workerCount;
			}
			set
			{
				if (this._workerCount != value)
				{
					this._workerCount = value;

					Update();
				}
			}
		}
		private int _workerCount = 1;

		#endregion

		#region Methods

		/// <summary>
		/// Scans a given <see cref="Image"/> and return the discovered text.
		/// </summary>
		/// <param name="image">The input <see cref="Image"/>.</param>
		/// <returns>Text discovererd in the image.</returns>
		public async Task<string> ScanImageAsync(Image image)
		{
			using (var ms = new MemoryStream())
			{
				var mime = GetImageMime(image);

				// Save the image to the memory stream
				image.Save(ms, image.RawFormat);

				// Convert image to byte array
				byte[] imageBytes = ms.ToArray();

				// Convert byte array to Base64 string
				var base64 = Convert.ToBase64String(imageBytes);

				return await this.CallAsync("scanImage", $"data:{mime};base64,{base64}");
			}
		}

		private string GetImageMime(Image image)
		{
			var codecs = ImageCodecInfo.GetImageDecoders();
			foreach (var codec in codecs)
			{
				if (codec.FormatID == image.RawFormat.Guid)
				{
					return codec.MimeType; // or codec.MimeType for MIME type
				}
			}
			return "Unknown";
		}

		#endregion

		#region IExtenderProvider Implementation

		/// <summary>
		/// Returns whether the given object can be extended.
		/// </summary>
		/// <param name="extendee"></param>
		/// <returns></returns>
		bool IExtenderProvider.CanExtend(object extendee)
		{
			return extendee is Control;
		}

		#endregion

		#region ControlConverter

		internal class CameraConverter : ReferenceConverter
		{
			public CameraConverter() : base(typeof(Control))
			{
			}

			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return false;
			}

			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				var list = new List<IComponent>();

				if (context != null)
				{
					list.Add(null);
					var service = (IReferenceService)context.GetService(typeof(IReferenceService));
					if (service == null)
					{
						var container = context.Container;
						if (container != null)
						{
							foreach (IComponent component in container.Components)
							{
								if (component == null || component.GetType().FullName != "Wisej.Web.Ext.Camera.Camera")
								{
									continue;
								}
								list.Add(component);
							}
						}
					}
					else
					{
						var references = service.GetReferences(typeof(Control));
						var length = references.Length;
						for (var i = 0; i < length; i++)
						{
							if (references[i].GetType().FullName == "Wisej.Web.Ext.Camera.Camera")
							{
								list.Add((IComponent)references[i]);
							}
						}
					}
				}
				return new TypeConverter.StandardValuesCollection(list.ToArray());
			}
		}

		#endregion

		#region Wisej Implementation

		private void OnTextRecognized(dynamic data)
		{
			TextRecognized?.Invoke(this, new TextRecognizedEventArgs(data.confidence, data.text, data.words));
		}

		/// <summary>
		/// Fires events from the client.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "textRecognized":
					OnTextRecognized(e.Parameters.Data);
					break;

				default:
					base.OnWebEvent(e);
					break;

			}
		}

		/// <summary>
		/// Provides the client widget's configuration.
		/// </summary>
		/// <param name="config">The configuration.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.ext.Tesseract";

			config.workerCount = this.WorkerCount;
			config.showWords = this.ShowWords;
			config.whitelist = this.Whitelist;
			config.language = this.Language;
			config.keywords = this.Keywords;
			config.interval = this.Interval;
			config.camera = this.Camera;
			config.enabled = this.Enabled;

			config.wiredEvents = new Base.WiredEvents();
			config.wiredEvents.Add("scanError(Data)");
			config.wiredEvents.Add("workerError(Data)");
			config.wiredEvents.Add("textRecognized(Data)");
		}

		/// <summary>
		/// Ensures the Camera widget exists in the ComponentManager.
		/// </summary>
		/// <param name="list"></param>
		protected override void OnAddReferences(IList list)
		{
			base.OnAddReferences(list);

			if (this._camera != null)
				list.Add(this.Camera);
		}

		#endregion

	}
}