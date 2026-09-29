using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.Signature
{
	/// <summary>
	/// Signature is a simple control for drawing and exporting user signatures.
	/// </summary>
	/// <remarks>
	/// The user draws on an HTML canvas using the mouse, pen or touch. The drawing, and the undo/redo history,
	/// exist only on the client: use <see cref="GetImageAsync"/> to retrieve the signature as an image and
	/// <see cref="Load"/> to display a saved signature. The <see cref="SignatureChange"/> event is fired
	/// every time the user draws a stroke or a dot, and when the control is cleared or an action is undone or redone.
	/// </remarks>
	/// <example>
	/// Saving the signature entered by the user:
	/// <code><![CDATA[
	/// private async void buttonSave_Click(object sender, EventArgs e)
	/// {
	///     if (await this.signature1.IsEmptyAsync())
	///     {
	///         AlertBox.Show("Please sign before continuing.");
	///         return;
	///     }
	/// 
	///     var image = await this.signature1.GetImageAsync();
	///     image.Save(Application.MapPath("~/Signatures/order-1234.png"), System.Drawing.Imaging.ImageFormat.Png);
	/// }
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[Description("Signature is a simple control for drawing and exporting user signatures.")]
	public class Signature : Control
	{

		#region Events

		/// <summary>
		/// Fires when the signature changes.
		/// </summary>
		public event EventHandler SignatureChange;

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the border style of the signature.
		/// </summary>
		/// <returns>One of the <see cref="T:Wisej.Web.BorderStyle" /> values. The default is <see cref="F:Wisej.Web.BorderStyle.Solid" />.</returns>
		[Browsable(true)]
		[DefaultValue(BorderStyle.None)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Description("Gets or sets the border style of the signature.")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this._borderStyle;
			}
			set
			{
				if (this._borderStyle != value)
				{
					this._borderStyle = value;

					Update();
				}
			}
		}
		private BorderStyle _borderStyle = BorderStyle.Solid;

		/// <summary>
		/// Returns or sets the signature line color.
		/// </summary>
		/// <remarks>
		/// The default is <see cref="Color.Black"/>. The color applies to the strokes drawn after it has been changed;
		/// existing strokes keep their color.
		/// </remarks>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Description("Gets or sets the signature line color.")]
		public Color LineColor
		{
			get
			{
				return this._lineColor;
			}
			set
			{
				if (this._lineColor != value) 
				{
					this._lineColor = value;

					Update();
				}
			}
		}
		private Color _lineColor = Color.Black;

		/// <summary>
		/// Returns or sets the signature line width, in pixels.
		/// </summary>
		/// <remarks>
		/// The default is 1. The width applies to the strokes drawn after it has been changed.
		/// A single tap draws a dot with a radius equal to the line width.
		/// </remarks>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Description("Gets or sets the signature line width.")]
		public int LineWidth
		{
			get
			{
				return this._lineWidth;
			}
			set
			{
				if (this._lineWidth != value)
				{
					this._lineWidth = value;

					Update();
				}
			}
		}
		private int _lineWidth = 1;

		/// <summary>
		/// Returns or sets whether the control is read only.
		/// </summary>
		/// <remarks>
		/// When true, the user cannot draw on the control. The signature can still be changed in code
		/// using <see cref="Clear"/>, <see cref="Load"/>, <see cref="Undo"/> and <see cref="Redo"/>.
		/// </remarks>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Description("Gets or sets whether the control is read only.")]
		public bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				if (this._readOnly != value)
				{
					this._readOnly = value;

					this.Update();
				}
			}
		}
		private bool _readOnly = false;

		#endregion

		#region Methods

		/// <summary>
		/// Asynchronously checks whether there is an undone action that can be redone.
		/// </summary>
		/// <returns>A task that completes with true if <see cref="Redo"/> can restore an undone action.</returns>
		/// <remarks>
		/// The history is kept on the client. It's reset when the user draws a new stroke after undoing an action.
		/// </remarks>
		/// <example>
		/// Enabling the redo button after the signature changes:
		/// <code><![CDATA[
		/// private async void signature1_SignatureChange(object sender, EventArgs e)
		/// {
		///     this.buttonRedo.Enabled = await this.signature1.CanRedoAsync();
		/// }
		/// ]]></code>
		/// </example>
		public async Task<bool> CanRedoAsync()
		{
			return await CallAsync("canRedo");
		}

		/// <summary>
		/// Asynchronously checks whether there is a user action that can be undone.
		/// </summary>
		/// <returns>A task that completes with true if <see cref="Undo"/> can remove the last action.</returns>
		/// <remarks>
		/// Only the strokes drawn by the user are recorded in the history; <see cref="Clear"/> and <see cref="Load"/> are not.
		/// </remarks>
		/// <example>
		/// Enabling the undo button after the signature changes:
		/// <code><![CDATA[
		/// private async void signature1_SignatureChange(object sender, EventArgs e)
		/// {
		///     this.buttonUndo.Enabled = await this.signature1.CanUndoAsync();
		/// }
		/// ]]></code>
		/// </example>
		public async Task<bool> CanUndoAsync()
		{
			return await CallAsync("canUndo");
		}

		/// <summary>
		/// Clears the <see cref="Signature"/> control.
		/// </summary>
		/// <remarks>
		/// Fires the <see cref="SignatureChange"/> event. Clearing is not recorded in the undo history and it doesn't reset it.
		/// </remarks>
		/// <example>
		/// Clearing the signature from a button:
		/// <code><![CDATA[
		/// private void buttonClear_Click(object sender, EventArgs e)
		/// {
		///     this.signature1.Clear();
		/// }
		/// ]]></code>
		/// </example>
		public void Clear()
		{
			this.Call("clear");
		}

		/// <summary>
		/// Asynchronously retrieves an image of the signature.
		/// </summary>
		/// <returns>A task that completes with a <see cref="Bitmap"/> of the signature.</returns>
		/// <remarks>
		/// The image is exported from the client canvas as a PNG with a transparent background and it has the size of the canvas in pixels.
		/// </remarks>
		/// <example>
		/// Showing the signature in a <see cref="PictureBox"/>:
		/// <code><![CDATA[
		/// private async void buttonPreview_Click(object sender, EventArgs e)
		/// {
		///     this.pictureBox1.Image = await this.signature1.GetImageAsync();
		/// }
		/// ]]></code>
		/// </example>
		public async Task<Image> GetImageAsync()
		{
			var base64 = await this.CallAsync("getImage");
			var bytes = Convert.FromBase64String(base64);

			using (var ms = new MemoryStream(bytes))
				return new Bitmap(ms);
		}

		/// <summary>
		/// Asynchronously checks whether the signature is empty.
		/// </summary>
		/// <returns>A task that completes with true if the signature is empty.</returns>
		/// <remarks>
		/// The signature is considered empty when all the pixels of the canvas are fully transparent.
		/// After an image has been loaded with <see cref="Load"/>, the signature is not empty.
		/// </remarks>
		/// <example>
		/// Validating the signature before submitting a form:
		/// <code><![CDATA[
		/// private async void buttonSubmit_Click(object sender, EventArgs e)
		/// {
		///     if (await this.signature1.IsEmptyAsync())
		///         MessageBox.Show("The signature is required.");
		///     else
		///         SubmitOrder();
		/// }
		/// ]]></code>
		/// </example>
		public async Task<bool> IsEmptyAsync()
		{
			return await CallAsync("isEmpty");
		}

		/// <summary>
		/// Loads the given image into the signature control.
		/// </summary>
		/// <param name="image">The image to load.</param>
		/// <remarks>
		/// The image is sent to the client as a PNG data URL and drawn asynchronously at its original size in the top-left
		/// corner of the canvas, on top of the current content. Call <see cref="Clear"/> first to replace the current signature.
		/// Loading an image doesn't fire the <see cref="SignatureChange"/> event and it's not recorded in the undo history.
		/// </remarks>
		/// <example>
		/// Displaying a previously saved signature in read-only mode:
		/// <code><![CDATA[
		/// this.signature1.ReadOnly = true;
		/// this.signature1.Clear();
		/// this.signature1.Load(Image.FromFile(Application.MapPath("~/Signatures/order-1234.png")));
		/// ]]></code>
		/// </example>
		public void Load(Image image)
		{
			using (var ms = new MemoryStream())
			{
				image.Save(ms, ImageFormat.Png);

				ms.Position = 0;

				var base64 = Convert.ToBase64String(ms.ToArray());
				var url = $"data:image/png;base64,{base64}";

				this.Call("loadImage", url);
			}
		}

		/// <summary>
		/// Redoes the last action undone by <see cref="Undo"/>.
		/// </summary>
		/// <remarks>
		/// Nothing happens if there is no action to redo. Fires the <see cref="SignatureChange"/> event.
		/// </remarks>
		/// <example>
		/// Redoing the last undone stroke:
		/// <code><![CDATA[
		/// private void buttonRedo_Click(object sender, EventArgs e)
		/// {
		///     this.signature1.Redo();
		/// }
		/// ]]></code>
		/// </example>
		public void Redo()
		{
			this.Call("redo");
		}

		/// <summary>
		/// Undoes the last stroke or dot drawn by the user.
		/// </summary>
		/// <remarks>
		/// Nothing happens if there is no action to undo. Fires the <see cref="SignatureChange"/> event.
		/// </remarks>
		/// <example>
		/// Removing the last stroke:
		/// <code><![CDATA[
		/// private void buttonUndo_Click(object sender, EventArgs e)
		/// {
		///     this.signature1.Undo();
		/// }
		/// ]]></code>
		/// </example>
		public void Undo()
		{
			this.Call("undo");
		}

		#endregion

		#region Wisej Implementation

		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			switch (e.Type)
			{
				case "signatureChange":
					SignatureChange?.Invoke(this, EventArgs.Empty);
					break;

				default:
					base.OnWidgetEvent(e);
					break;
			}
		}

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.Signature";

			config.readOnly = this.ReadOnly;
			config.lineColor = this.LineColor;
			config.lineWidth = this.LineWidth;
			config.borderStyle = this.BorderStyle;
		}

		#endregion

	}
}