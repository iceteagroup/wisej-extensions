using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.JssorSlider
{
	/// <summary>
	/// Represents the options of the bullet navigator (the row of dots indicating the current slide) of a <see cref="JssorSlider"/>.
	/// </summary>
	/// <example>
	/// Showing the bullets at the bottom center of the slider with 10 pixels between them:
	/// <code><![CDATA[
	/// var bullets = new BulletOptions(this.jssorSlider1)
	/// {
	///     Visible = true,
	///     AutoCenterHorizontally = true,
	///     Orientation = Orientation.Horizontal,
	///     SpacingX = 10
	/// };
	/// ]]></code>
	/// </example>
	public class BulletOptions : OptionsBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BulletOptions"/> class for the specified slider.
		/// </summary>
		/// <param name="slider">The <see cref="JssorSlider"/> that owns these options.</param>
		public BulletOptions(JssorSlider slider) : base(slider)
		{
		}

		internal override string ClassName
		{
			get
			{
				return "$JssorBulletNavigator$";
			}
		}

		/// <summary>
		/// Returns or sets whether the bullet navigator is displayed.
		/// </summary>
		/// <remarks>
		/// When <see cref="ShowOnMouseOver"/> is also <c>true</c>, the bullet navigator is displayed only while the pointer is over the slider.
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
		/// Returns or sets whether the bullet navigator is displayed only when the pointer is over the slider.
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
		/// Returns or sets whether the bullet navigator is automatically centered vertically in the slider.
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
		/// Returns or sets whether the bullet navigator is automatically centered horizontally in the slider.
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

		/// <summary>
		/// Returns or sets the orientation used to arrange the bullets.
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
		/// Returns or sets the horizontal space in pixels between the bullets.
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
		/// Returns or sets the vertical space in pixels between the bullets.
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


	}
}
