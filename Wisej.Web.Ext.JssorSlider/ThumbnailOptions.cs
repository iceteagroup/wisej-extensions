using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.JssorSlider
{
	/// <summary>
	/// Represents the options of the thumbnail navigator (the panel with the preview images of the slides) of a <see cref="JssorSlider"/>.
	/// </summary>
	/// <example>
	/// Showing a strip of 5 thumbnails that wraps around:
	/// <code><![CDATA[
	/// var thumbnails = new ThumbnailOptions(this.jssorSlider1)
	/// {
	///     Visible = true,
	///     Wrap = true,
	///     Columns = 5,
	///     Rows = 1,
	///     SpacingX = 8,
	///     AutoCenterHorizontally = true
	/// };
	/// ]]></code>
	/// </example>
	public class ThumbnailOptions : OptionsBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ThumbnailOptions"/> class for the specified slider.
		/// </summary>
		/// <param name="slider">The <see cref="JssorSlider"/> that owns these options.</param>
		public ThumbnailOptions(JssorSlider slider) : base(slider)
		{
		}

		internal override string ClassName
		{
			get
			{
				return "$JssorThumbnailNavigator$";
			}
		}

		/// <summary>
		/// Returns or sets whether the thumbnail navigator wraps around (loops) when it reaches the last thumbnail.
		/// </summary>
		public bool Wrap
		{
			get { return base.GetState(STATE_WRAP); }
			set
			{
				if (this.Wrap != value)
				{
					base.SetState(STATE_WRAP, value);
					Update();
				}
			}
		}

		/// <summary>
		/// Returns or sets the number of thumbnails displayed in the thumbnail navigator at the same time.
		/// </summary>
		/// <remarks>
		/// The default is 1.
		/// </remarks>
		public int Columns
		{
			get { return this._columns; }
			set
			{
				if (this._columns != value)
				{
					this._columns = value;
					Update();
				}
			}
		}
		private int _columns = 1;

		/// <summary>
		/// Returns or sets the number of rows (lanes) used to arrange the thumbnails.
		/// </summary>
		/// <remarks>
		/// The default is 1.
		/// </remarks>
		public int Rows
		{
			get { return this._rows; }
			set
			{
				if (this._rows != value)
				{
					this._rows = value;
					Update();
				}
			}
		}
		private int _rows = 1;

		/// <summary>
		/// Returns or sets the horizontal space in pixels between the thumbnails.
		/// </summary>
		public int SpacingX
		{
			get { return this._spacingX; }
			set
			{
				if (this._spacingX != value)
				{
					this._spacingX = value;
					Update();
				}
			}
		}
		private int _spacingX = 0;

		/// <summary>
		/// Returns or sets the vertical space in pixels between the thumbnails.
		/// </summary>
		public int SpacingY
		{
			get { return this._spacingY; }
			set
			{
				if (this._spacingY != value)
				{
					this._spacingY = value;
					Update();
				}
			}
		}
		private int _spacingY = 0;

		/// <summary>
		/// Returns or sets the orientation used to arrange the thumbnails.
		/// </summary>
		/// <remarks>
		/// The default is <see cref="Orientation.Horizontal"/>.
		/// </remarks>
		public Orientation Orientation
		{
			get { return this._orientation; }
			set
			{
				if (this._orientation != value)
				{
					this._orientation = value;
					Update();
				}
			}
		}
		private Orientation _orientation = Orientation.Horizontal;

		/// <summary>
		/// Returns or sets whether the thumbnail navigator is displayed.
		/// </summary>
		/// <remarks>
		/// When <see cref="ShowOnMouseOver"/> is also <c>true</c>, the thumbnail navigator is displayed only while the pointer is over the slider.
		/// </remarks>
		public bool Visible
		{
			get { return base.GetState(STATE_VISIBLE); }
			set
			{
				if (this.Visible != value)
				{
					base.SetState(STATE_VISIBLE, value);
					Update();
				}
			}
		}

		/// <summary>
		/// Returns or sets whether the thumbnail navigator is displayed only when the pointer is over the slider.
		/// </summary>
		/// <remarks>
		/// Applies only when <see cref="Visible"/> is <c>true</c>.
		/// </remarks>
		public bool ShowOnMouseOver
		{
			get { return base.GetState(STATE_SHOWONMOUSEOVER); }
			set
			{
				if (this.ShowOnMouseOver != value)
				{
					base.SetState(STATE_SHOWONMOUSEOVER, value);
					Update();
				}
			}
		}

		/// <summary>
		/// Returns or sets whether the thumbnail navigator is automatically centered vertically in the slider.
		/// </summary>
		public bool AutoCenterVertically
		{
			get { return base.GetState(STATE_AUTOCENTERV); }
			set
			{
				if (this.AutoCenterVertically != value)
				{
					base.SetState(STATE_AUTOCENTERV, value);
					Update();
				}
			}
		}

		/// <summary>
		/// Returns or sets whether the thumbnail navigator is automatically centered horizontally in the slider.
		/// </summary>
		public bool AutoCenterHorizontally
		{
			get { return base.GetState(STATE_AUTOCENTERH); }
			set
			{
				if (this.AutoCenterHorizontally != value)
				{
					base.SetState(STATE_AUTOCENTERH, value);
					Update();
				}
			}
		}

	}
}
