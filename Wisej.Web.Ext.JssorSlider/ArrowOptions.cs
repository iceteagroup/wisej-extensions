using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.JssorSlider
{
	/// <summary>
	/// Represents the options of the arrow navigator (the left and right buttons) of a <see cref="JssorSlider"/>.
	/// </summary>
	/// <example>
	/// Showing the navigation arrows centered vertically, only when the pointer is over the slider:
	/// <code><![CDATA[
	/// var arrows = new ArrowOptions(this.jssorSlider1)
	/// {
	///     Visible = true,
	///     ShowOnMouseOver = true,
	///     AutoCenter = true
	/// };
	/// ]]></code>
	/// </example>
	public class ArrowOptions : OptionsBase
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="ArrowOptions"/> class for the specified slider.
		/// </summary>
		/// <param name="slider">The <see cref="JssorSlider"/> that owns these options.</param>
		public ArrowOptions(JssorSlider slider) : base(slider)
		{
		}

		internal override string ClassName
		{
			get
			{
				return "$JssorArrowNavigator$";
			}
		}

		/// <summary>
		/// Returns or sets whether the arrow navigator is displayed.
		/// </summary>
		/// <remarks>
		/// When <see cref="ShowOnMouseOver"/> is also <c>true</c>, the arrow navigator is displayed only while the pointer is over the slider.
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
		/// Returns or sets whether the arrow navigator is displayed only when the pointer is over the slider.
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
		/// Returns or sets whether the arrow buttons are automatically centered in the slider.
		/// </summary>
		public bool AutoCenter
		{
			get { return base.GetState(STATE_AUTOCENTER); }
			set
			{
				if (this.AutoCenter != value)
				{
					base.SetState(STATE_AUTOCENTER, value);
					Update();
				}
			}
		}

	}
}
