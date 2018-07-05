using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.JssorSlider
{
	public class ArrowOptions : OptionsBase
	{

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
