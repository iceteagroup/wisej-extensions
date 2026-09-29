///////////////////////////////////////////////////////////////////////////////
//
// (C) 2020 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.Barcode
{
	/// <summary>
	/// A component for extending the functionality of the Wisej Camera to include barcode detection.
	/// </summary>
	/// <remarks>
	/// Set the <see cref="Camera"/> property to a Wisej.Web.Ext.Camera.Camera control and handle the
	/// <see cref="ScanSuccess"/> event. The barcodes are detected in the browser, so the video frames are not sent
	/// to the server. Use <see cref="ScanMode"/> to choose whether the reader scans continuously, stops after the
	/// first barcode, or scans only when <see cref="ScanImage"/> is called.
	/// </remarks>
	/// <example>
	/// The following example scans barcodes continuously with a camera and adds them to a list:
	/// <code><![CDATA[
	/// private BarcodeReader barcodeReader;
	///
	/// private void Page1_Load(object sender, EventArgs e)
	/// {
	///     this.barcodeReader = new BarcodeReader(this.components)
	///     {
	///         Camera = this.camera1,
	///         ScanMode = ScanMode.Automatic
	///     };
	///
	///     this.barcodeReader.ScanSuccess += barcodeReader_ScanSuccess;
	/// }
	///
	/// private void barcodeReader_ScanSuccess(object sender, ScanEventArgs e)
	/// {
	///     this.listBoxCodes.Items.Add(e.Data);
	/// }
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Barcode))]
	[ApiCategory("Barcode")]
	public partial class BarcodeReader : Component, IExtenderProvider
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.Barcode.BarcodeReader" /> without a specified container.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var reader = new BarcodeReader();
		/// reader.Camera = this.camera1;
		/// reader.ScanSuccess += (s, e) => AlertBox.Show(e.Data);
		/// ]]></code>
		/// </example>
		public BarcodeReader()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Wisej.Web.Ext.Barcode.BarcodeReader"/> class with a specified container.
		/// </summary>
		/// <param name="container">An <see cref="System.ComponentModel.IContainer"/>container.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="container"/> is null.</exception>
		/// <remarks>
		/// Adding the component to a container ensures that it's disposed together with the container,
		/// for example when the owning page or form is disposed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // "components" is the container created by the designer for the page or form.
		/// var reader = new BarcodeReader(this.components);
		/// reader.Camera = this.camera1;
		/// reader.ScanMode = ScanMode.AutomaticOnce;
		/// ]]></code>
		/// </example>
		public BarcodeReader(IContainer container) : this()
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the user scans a barcode.
		/// </summary>
		public event ScanEventHandler ScanSuccess
		{
			add { base.AddHandler(nameof(ScanSuccess), value); }
			remove { base.RemoveHandler(nameof(ScanSuccess), value); }
		}

		/// <summary>
		/// Fires the Scan event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnScanSuccess(ScanEventArgs e)
		{
			((ScanEventHandler)base.Events[nameof(ScanSuccess)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when no barcode is detected in <see cref="ScanMode.Manual"/> mode.
		/// </summary>
		public event ScanEventHandler ScanError
		{
			add { base.AddHandler(nameof(ScanError), value); }
			remove { base.RemoveHandler(nameof(ScanError), value); }
		}

		/// <summary>
		/// Fires the ScanError event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnScanError(ScanEventArgs e)
		{
			((ScanEventHandler)base.Events[nameof(ScanError)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the mode in which the scanner should detect barcodes.
		/// </summary>
		[DefaultValue(ScanMode.Automatic)]
		public ScanMode ScanMode
		{
			get
			{
				return this._scanMode;
			}
			set
			{
				if (this._scanMode != value)
				{
					this._scanMode = value;

					Update();
				}
			}
		}
		private ScanMode _scanMode = ScanMode.Automatic;

		/// <summary>
		/// The Wisej Camera instance to attach to.
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

        #endregion

        #region Methods

        /// <summary>
        /// Decodes a barcode from the given image.
        /// </summary>
        /// <param name="image">The image of the barcode.</param>
        /// <returns>The data encoded in the barcode, or an empty string if no barcode is found in the image.</returns>
        /// 
        /// <remarks>Uses the ZXing.NET library to parse the image. The image is decoded on the server,
        /// so this method doesn't need the <see cref="Camera"/>.</remarks>
        /// <example>
        /// The following example decodes a barcode from an image uploaded by the user:
        /// <code><![CDATA[
        /// private void upload1_Uploaded(object sender, UploadedEventArgs e)
        /// {
        ///     using (var image = Image.FromStream(e.Files[0].InputStream))
        ///     {
        ///         var data = this.barcodeReader1.DecodeBarcode(image);
        ///
        ///         if (data == "")
        ///				this.textBoxCode.Text = "";
        ///             AlertBox.Show("No barcode found.");
        ///         else
        ///             this.textBoxCode.Text = data;
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        public string DecodeBarcode(Image image)
		{
			var reader = new ZXing.BarcodeReader();			

			var bmp = new Bitmap(image);
			var data = reader.Decode(bmp);			

			if (data != null)
				return data.Text;
			else
				return "";
		}

		/// <summary>
		/// Scans the last frame from the attached Camera for a barcode.
		/// </summary>
		/// <remarks>
		/// Fires the <see cref="ScanSuccess"/> event when a barcode is detected, or the <see cref="ScanError"/>
		/// event when no barcode is found or the camera image isn't available. Use this method when
		/// <see cref="ScanMode"/> is set to <see cref="ScanMode.Manual"/>. The events are sent to the server
		/// only when a handler is attached to <see cref="ScanSuccess"/>.
		/// </remarks>
		/// <example>
		/// The following example scans the camera image when the user clicks a button:
		/// <code><![CDATA[
		/// private void Page1_Load(object sender, EventArgs e)
		/// {
		///     this.barcodeReader1.ScanMode = ScanMode.Manual;
		///     this.barcodeReader1.ScanSuccess += (s, args) => this.textBoxCode.Text = args.Data;
		///     this.barcodeReader1.ScanError += (s, args) => AlertBox.Show("No barcode found. Try again.");
		/// }
		///
		/// private void buttonScan_Click(object sender, EventArgs e)
		/// {
		///     this.barcodeReader1.ScanImage();
		/// }
		/// ]]></code>
		/// </example>
		public void ScanImage()
		{
			Call("scanImage");
		}

		/// <summary>
		/// Resets the scanner in <see cref="ScanMode.AutomaticOnce"/> mode.
		/// </summary>
		/// <remarks>
		/// In <see cref="ScanMode.AutomaticOnce"/> mode the reader stops after the first barcode is detected.
		/// Call this method to start scanning for the next barcode. It has no effect in the other modes.
		/// </remarks>
		/// <example>
		/// The following example scans one barcode at a time and restarts the scanner after the user confirms it:
		/// <code><![CDATA[
		/// private async void barcodeReader1_ScanSuccess(object sender, ScanEventArgs e)
		/// {
		///     var result = await MessageBox.ShowAsync($"Add item {e.Data}?", buttons: MessageBoxButtons.YesNo);
		///     if (result == DialogResult.Yes)
		///         this.listBoxItems.Items.Add(e.Data);
		///
		///     this.barcodeReader1.ResetScanner();
		/// }
		/// ]]></code>
		/// </example>
		public void ResetScanner()
		{
			Call("resetScanner");
		}

		#endregion

		#region IExtenderProvider

		/// <summary>
		/// Returns whether this component can provide its extender properties to the specified object.
		/// </summary>
		/// <param name="extendee">The object to check.</param>
		/// <returns>true if <paramref name="extendee"/> is a <see cref="Control"/>; otherwise false.</returns>
		/// <remarks>
		/// This method is part of the <see cref="IExtenderProvider"/> implementation and is used by the designer.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var reader = new BarcodeReader();
		///
		/// Console.WriteLine(reader.CanExtend(this.camera1)); // True
		/// Console.WriteLine(reader.CanExtend("text"));       // False
		/// ]]></code>
		/// </example>
		public bool CanExtend(object extendee)
		{
			return (extendee is Control); // TODO: Camera.
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

		/// <summary>
		/// Fires events from the client.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "scanSuccess":
					OnScanSuccess(new ScanEventArgs(e.Parameters.Data, true));
					break;

				case "scanError":
					OnScanError(new ScanEventArgs(e.Parameters.Data, false));
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

			config.className = "wisej.web.ext.BarcodeReader";

			config.camera = this.Camera;
			config.scanMode = this.ScanMode;

			if (base.Events[nameof(ScanSuccess)] != null)
			{
				config.wiredEvents = new WiredEvents();
				config.wiredEvents.Add("scanError(Data)");
				config.wiredEvents.Add("scanSuccess(Data)");
			}
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
