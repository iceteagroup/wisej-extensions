using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Wisej.Core;

namespace Wisej.Web.Ext.Signature
{
	[ToolboxItem(true)]
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
		/// Gets or sets the border style of the signature.
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
		/// Gets or sets the signature line color.
		/// </summary>
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
		/// Gets or sets the signature line width.
		/// </summary>
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
		/// Gets or sets whether the control is read only.
		/// </summary>
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

		public async Task<bool> CanRedoAsync()
		{
			return await CallAsync("canRedo");
		}

		public async Task<bool> CanUndoAsync()
		{
			return await CallAsync("canUndo");
		}

		/// <summary>
		/// Clears the <see cref="Signature"/> control.
		/// </summary>
		public void Clear()
		{
			this.Call("clear");
		}

		/// <summary>
		/// Gets an image of the signature control.
		/// </summary>
		/// <returns>The signature image.</returns>
		public async Task<Image> GetImageAsync()
		{
			var base64 = await this.CallAsync("getImage");
			var bytes = Convert.FromBase64String(base64);

			using (var ms = new MemoryStream(bytes))
				return new Bitmap(ms);
		}

		/// <summary>
		/// Checks whether the signature is empty or not.
		/// </summary>
		/// <returns>True if the signature is empty.</returns>
		public async Task<bool> IsEmptyAsync()
		{
			return await CallAsync("isEmpty");
		}

		/// <summary>
		/// Loads the given image into the signature control.
		/// </summary>
		/// <param name="image">The image to load.</param>
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
		/// Redo the last user action.
		/// </summary>
		public void Redo()
		{
			this.Call("redo");
		}

		/// <summary>
		/// Undo the last user action.
		/// </summary>
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