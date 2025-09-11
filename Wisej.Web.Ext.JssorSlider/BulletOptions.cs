using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.JssorSlider
{
	public class BulletOptions : OptionsBase
	{
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
